import requests
from config import STRAVA_SERVICE_URL

def analyze_activities(limit: int = 5):
    url = f"{STRAVA_SERVICE_URL}/strava/activities?limit={limit}"
    response = requests.get(url)
    if response.status_code != 200:
        return {"error": "Failed to fetch activities"}
    activities = response.json().get("activities", [])
    
    total_activities = len(activities)
    return {"total_activities": total_activities, "sample_insight": "Keep up the good work!"}