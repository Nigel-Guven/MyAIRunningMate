
from pathlib import Path

from garminconnect import Garmin
from application.garmin_fit_cleaner import clean_fit_file, format_file
import config
from application import garmin_connect_ingest


def main():
    
    if not config.EMAIL or not config.PASSWORD:
        raise ValueError("Missing GARMIN_EMAIL or GARMIN_PASSWORD in .env")
    
    client = Garmin(config.EMAIL, config.PASSWORD)
    client.login()
    
    config.DOWNLOAD_DIR.mkdir(exist_ok=True)
    
    #garmin_connect_ingest.download_latest_activity(client, config.DOWNLOAD_DIR)

    #for file in Path(config.DOWNLOAD_DIR).glob("*.fit"):
    #    item = format_file(file)
    #    print(item)

if __name__ == "__main__":
    main()