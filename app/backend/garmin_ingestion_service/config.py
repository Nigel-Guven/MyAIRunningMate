import os
from pathlib import Path

from dotenv import load_dotenv

load_dotenv()

# Client Login
EMAIL = os.getenv("GARMIN_EMAIL")
PASSWORD = os.getenv("GARMIN_PASSWORD")

DB_SERVICE_URL = os.getenv("DB_SERVICE_URL", "http://api:8000")

# Calling Garmin
DOWNLOAD_DIR = Path("app/data/garmin_activities")
PAGE_SIZE = 50
REQUEST_DELAY_SECONDS = 1