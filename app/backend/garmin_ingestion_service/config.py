import os
from pathlib import Path

from dotenv import load_dotenv

load_dotenv()

# Client Login
EMAIL = os.getenv("GARMIN_EMAIL")
PASSWORD = os.getenv("GARMIN_PASSWORD")

# Calling Garmin
DOWNLOAD_DIR = Path("../../../data/garmin_activities")
PAGE_SIZE = 50
REQUEST_DELAY_SECONDS = 1