from fastapi import APIRouter, Query
from services.ai_agent import analyze_single_activity, analyze_recent_activities

router = APIRouter()

@router.get("/analyze/single")
def analyze_single(activity_id: int = Query(...)):
    return analyze_single_activity(activity_id)

@router.get("/analyze/recent")
def analyze_recent(limit: int = Query(7)):
    return analyze_recent_activities(limit)
