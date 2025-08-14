import os
import requests
from .cleaner import clean_activity

STRAVA_SERVICE_URL = os.getenv("STRAVA_SERVICE_URL", "http://strava_service:8000")
GEMINI_API_KEY = os.getenv("GEMINI_API_KEY")
GEMINI_URL = "https://gemini.googleapis.com/v1beta2/models/gemini-2.5-chat/completions"

# ----------------------------
# Strava fetching functions
# ----------------------------
def fetch_strava_activities(limit=7, retries=5, delay=2):
    """Fetch recent activities from Strava service with retry."""
    for i in range(retries):
        try:
            resp = requests.get(f"{STRAVA_SERVICE_URL}/strava/activities?limit={limit}")
            resp.raise_for_status()
            return resp.json().get("activities", [])
        except requests.exceptions.RequestException:
            print(f"Strava service not ready, retrying {i+1}/{retries}...")
            import time; time.sleep(delay)
    return []

# ----------------------------
# Gemini AI integration
# ----------------------------
def get_activity_insights(activity):
    """Send a single activity to Gemini and get AI insights."""
    if not GEMINI_API_KEY:
        return {"error": "Gemini API key not set"}
    
    simple_activity = clean_activity(activity)
    
    payload = {
        "messages": [
            {"role": "user", "content": f"Analyze this running activity and provide insights: {simple_activity}"}
        ],
        "temperature": 0.7,
    }
    headers = {
        "Authorization": f"Bearer {GEMINI_API_KEY}",
        "Content-Type": "application/json",
    }
    
    try:
        response = requests.post(GEMINI_URL, headers=headers, json=payload)
        response.raise_for_status()
        return {"insights": response.json()["choices"][0]["message"]["content"]}
    except Exception as e:
        return {"error": str(e)}

def analyze_recent_activities(limit=7):
    """Fetch recent activities and provide a weekly forecast using Gemini."""
    activities = fetch_strava_activities(limit)
    if not activities:
        return {"error": "No activities fetched"}
    
    simple_activities = [clean_activity(a) for a in activities]
    
    prompt = (
        f"Analyze the following running activities and provide a training forecast for the next week "
        f"considering distance, pace, elevation, and recovery needs:\n{simple_activities}"
    )
    
    headers = {
        "Authorization": f"Bearer {GEMINI_API_KEY}",
        "Content-Type": "application/json",
    }
    payload = {
        "messages": [{"role": "user", "content": prompt}],
        "temperature": 0.7,
    }
    
    try:
        response = requests.post(GEMINI_URL, headers=headers, json=payload)
        response.raise_for_status()
        return {"forecast": response.json()["choices"][0]["message"]["content"]}
    except Exception as e:
        return {"error": str(e)}
