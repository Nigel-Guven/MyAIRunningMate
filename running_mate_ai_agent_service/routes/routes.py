from fastapi import APIRouter, Query
from services.ai_agent import get_activity_insights, analyze_recent_activities, fetch_strava_activities

router = APIRouter()

# Health check
@router.get("/")
def root():
    return {"message": "AI Agent Service running"}

# Analyze a single activity (most recent if no ID provided)
@router.get("/analyze/single")
def analyze_single(activity_id: int = Query(None, description="ID of the activity to analyze")):
    activities = fetch_strava_activities(limit=10)  # get recent activities
    if not activities:
        return {"error": "No activities found"}

    if activity_id:
        activity = next((a for a in activities if a["id"] == activity_id), None)
        if not activity:
            return {"error": "Activity not found"}
    else:
        activity = activities[0]  # most recent if no ID given

    return get_activity_insights(activity)

# Forecast using recent activities
@router.get("/analyze/recent")
def analyze_recent(limit: int = Query(7, description="Number of recent activities to analyze")):
    return analyze_recent_activities(limit)
