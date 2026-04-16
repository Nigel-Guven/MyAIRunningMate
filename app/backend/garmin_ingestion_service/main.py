
from pathlib import Path
import time

from garminconnect import Garmin
from application.garmin_persist import sync_activity
from application.garmin_fit_cleaner import format_file
import config
from application import garmin_connect_ingest


def main():
    
    if not config.EMAIL or not config.PASSWORD:
        raise ValueError("Missing GARMIN_EMAIL or GARMIN_PASSWORD")
    
    #client = Garmin(config.EMAIL, config.PASSWORD)
    #client.login()
    
    #config.DOWNLOAD_DIR.mkdir(exist_ok=True)
    
    #garmin_connect_ingest.download_all_activities(client, config.DOWNLOAD_DIR)
    BASE_DIR = Path(__file__).resolve().parents[3]
    DOWNLOAD_DIR = BASE_DIR / "data" / "garmin_activities"
    
    for file in Path(DOWNLOAD_DIR).glob("*.fit"):
        activity = format_file(file)
        print(activity)

if __name__ == "__main__":
    while True:
        main()
