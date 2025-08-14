import os
import requests
from config import STRAVA_CLIENT_ID, STRAVA_CLIENT_SECRET, REDIRECT_URI

def get_auth_url():
    scope = "read,activity:read_all"
    return (
        f"https://www.strava.com/oauth/authorize"
        f"?client_id={STRAVA_CLIENT_ID}&response_type=code"
        f"&redirect_uri={REDIRECT_URI}&approval_prompt=force"
        f"&scope={scope}"
    )

def exchange_token_service(code: str):
    payload = {
        "client_id": STRAVA_CLIENT_ID,
        "client_secret": STRAVA_CLIENT_SECRET,
        "code": code,
        "grant_type": "authorization_code"
    }
    response = requests.post("https://www.strava.com/oauth/token", data=payload)
    response.raise_for_status()
    tokens = response.json()

    # In Docker, just return tokens; don't try to write to .env
    return {
        "access_token": tokens["access_token"],
        "refresh_token": tokens["refresh_token"]
    }

def get_access_token():
    return os.getenv("STRAVA_ACCESS_TOKEN")

def refresh_access_token():
    refresh_token = os.getenv("STRAVA_REFRESH_TOKEN")
    payload = {
        "client_id": STRAVA_CLIENT_ID,
        "client_secret": STRAVA_CLIENT_SECRET,
        "grant_type": "refresh_token",
        "refresh_token": refresh_token
    }
    response = requests.post("https://www.strava.com/oauth/token", data=payload)
    response.raise_for_status()
    tokens = response.json()
    
    # Just return the new access token
    return tokens["access_token"]

def validate_or_refresh_token():
    access_token = get_access_token()
    url = "https://www.strava.com/api/v3/athlete"
    headers = {"Authorization": f"Bearer {access_token}"}
    response = requests.get(url)
    if response.status_code == 401:
        new_token = refresh_access_token()
        print("Access token refreshed on startup.")
    else:
        print("Access token valid on startup.")

def get_activities(limit: int = 5):
    access_token = get_access_token()
    url = f"https://www.strava.com/api/v3/athlete/activities?per_page={limit}&page=1"
    headers = {"Authorization": f"Bearer {access_token}"}
    response = requests.get(url, headers=headers)
    if response.status_code == 401:
        access_token = refresh_access_token()
        headers["Authorization"] = f"Bearer {access_token}"
        response = requests.get(url, headers=headers)
    response.raise_for_status()
    return response.json()
