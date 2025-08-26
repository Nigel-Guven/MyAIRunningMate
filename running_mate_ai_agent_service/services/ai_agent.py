import requests
from datetime import datetime
from .cleaner import clean_activity
from .insights import get_gemini_insights

DB_SERVICE_URL = "http://db_service:8000"

def fetch_recent_activities(limit=7):
    r = requests.get(f"{DB_SERVICE_URL}/activities/", params={"limit": limit})
    return r.json()

def analyze_single_activity(activity_id: int):
    r = requests.get(f"{DB_SERVICE_URL}/activities/{activity_id}")
    activity = r.json()

    if not activity or "error" in activity:
        return {"error": "Activity not found"}

    clean_data = clean_activity(activity)
    insights_text = get_gemini_insights(clean_data)

    save = requests.post(f"{DB_SERVICE_URL}/insights/", params={"activity_id": activity["id"], "content": insights_text})
    return {"activity_id": activity["id"], "insight": insights_text}

def analyze_recent_activities(limit=7):
    activities = fetch_recent_activities(limit)
    if not activities or "error" in activities:
        return {"error": "No activities found"}

    clean_activities = [clean_activity(a) for a in activities]
    insights_text = get_gemini_insights(str(clean_activities))

    save = requests.post(f"{DB_SERVICE_URL}/insights/", params={"activity_id": 0, "content": insights_text})
    return {"forecast": insights_text}
