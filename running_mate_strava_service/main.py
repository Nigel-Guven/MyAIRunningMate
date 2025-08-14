from fastapi import FastAPI
from routes.strava_routes import router as strava_router
from services.strava_client import validate_or_refresh_token

app = FastAPI(title="Strava Data Collection Service")

# Include Strava routes
app.include_router(strava_router, prefix="/strava")

@app.on_event("startup")
def startup_event():
    validate_or_refresh_token()
