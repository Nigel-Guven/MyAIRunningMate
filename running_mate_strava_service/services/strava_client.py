import os
import requests
# The httpx import has been removed as it is no longer used
from config import STRAVA_CLIENT_ID, STRAVA_CLIENT_SECRET, REDIRECT_URI

def get_auth_url():
    """
    Generates the Strava OAuth authorization URL.
    """
    scope = "read,activity:read_all"
    return (
        f"[https://www.strava.com/oauth/authorize](https://www.strava.com/oauth/authorize)"
        f"?client_id={STRAVA_CLIENT_ID}&response_type=code"
        f"&redirect_uri={REDIRECT_URI}&approval_prompt=force"
        f"&scope={scope}"
    )

def exchange_token_service(code: str):
    """
    Exchanges the authorization code for access and refresh tokens.
    """
    payload = {
        "client_id": STRAVA_CLIENT_ID,
        "client_secret": STRAVA_CLIENT_SECRET,
        "code": code,
        "grant_type": "authorization_code"
    }
    response = requests.post("[https://www.strava.com/oauth/token](https://www.strava.com/oauth/token)", data=payload)
    response.raise_for_status()
    tokens = response.json()
    return {
        "access_token": tokens["access_token"],
        "refresh_token": tokens["refresh_token"]
    }

def get_access_token():
    """
    Retrieves the access token from environment variables.
    """
    return os.getenv("STRAVA_ACCESS_TOKEN")

def refresh_access_token():
    """
    Refreshes the Strava access token using the refresh token.
    """
    refresh_token = os.getenv("STRAVA_REFRESH_TOKEN")
    payload = {
        "client_id": STRAVA_CLIENT_ID,
        "client_secret": STRAVA_CLIENT_SECRET,
        "grant_type": "refresh_token",
        "refresh_token": refresh_token
    }
    response = requests.post("[https://www.strava.com/oauth/token](https://www.strava.com/oauth/token)", data=payload)
    response.raise_for_status()
    tokens = response.json()
    return tokens["access_token"]

def validate_or_refresh_token():
    """
    Checks if the current token is valid and refreshes it if needed.
    """
    access_token = get_access_token()
    url = "[https://www.strava.com/api/v3/athlete](https://www.strava.com/api/v3/athlete)"
    headers = {"Authorization": f"Bearer {access_token}"}
    response = requests.get(url, headers=headers)
    if response.status_code == 401:
        new_token = refresh_access_token()
        print("Access token refreshed on startup.")
    else:
        print("Access token valid on startup.")

def get_activities(limit: int = 5):
    """
    Fetches activities from the Strava API.
    """
    access_token = get_access_token()
    url = f"[https://www.strava.com/api/v3/athlete/activities?per_page=](https://www.strava.com/api/v3/athlete/activities?per_page=){limit}&page=1"
    headers = {"Authorization": f"Bearer {access_token}"}
    response = requests.get(url, headers=headers)
    if response.status_code == 401:
        access_token = refresh_access_token()
        headers["Authorization"] = f"Bearer {access_token}"
        response = requests.get(url, headers=headers)
    response.raise_for_status()
    return response.json()