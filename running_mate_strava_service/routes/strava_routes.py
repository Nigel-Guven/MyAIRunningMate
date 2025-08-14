from fastapi import APIRouter, Request
from fastapi.responses import RedirectResponse
from services.strava_client import (
    get_auth_url, exchange_token_service, get_activities
)

router = APIRouter()

@router.get("/")
def root():
    return {"message": "Strava Service running"}

@router.get("/authorize")
def authorize():
    return RedirectResponse(get_auth_url())

@router.get("/exchange_token")
def exchange_token(request: Request, code: str):
    tokens = exchange_token_service(code)
    return {
        "message": "Tokens saved successfully!",
        **tokens
    }

@router.get("/activities")
def fetch_activities(limit: int = 5):
    activities = get_activities(limit)
    return {"activities": activities}
