import os
from dotenv import load_dotenv

load_dotenv()

# Example: add any AI or DB keys here
AI_MODEL_PATH = os.getenv("AI_MODEL_PATH", "models/default_model.pkl")
STRAVA_SERVICE_URL = os.getenv("STRAVA_SERVICE_URL", "http://strava_service:8000")
