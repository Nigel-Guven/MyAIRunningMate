from datetime import datetime
import inspect
import io
from pathlib import Path
import time
import zipfile
from tqdm import tqdm
from garminconnect import Garmin
import config

def download_latest_activity(client, download_dir: Path):
    
    download_dir.mkdir(parents=True, exist_ok=True)

    print("Fetching latest activity...")

    activities = client.get_activities(0, 1)

    if not activities:
        print("No activities found.")
        return

    latest = activities[0]
    activity_id = latest["activityId"]

    filename = download_dir / f"{activity_id}.fit"

    if filename.exists():
        print(f"Activity {activity_id} already exists. Skipping.")
        return

    try:
        print(inspect.signature(client.download_activity))
        
        fit_data_zip = client.download_activity(activity_id, dl_fmt=Garmin.ActivityDownloadFormat.ORIGINAL)
        fit_bytes = unzip_bytes(fit_data_zip)
            
        with open(filename, "wb") as f:
            f.write(fit_bytes)

        print(f"Downloaded latest activity: {activity_id}")

    except Exception as e:
        print(f"Failed to download latest activity: {e}")
        
def download_all_activities(client, download_dir: Path):
    
    download_dir.mkdir(parents=True, exist_ok=True)
    
    print("Fetching all activities")

    all_activities = []
    start = 0

    while True:
        activities = client.get_activities(start, config.PAGE_SIZE)
        if not activities:
            break

        all_activities.extend(activities)
        start += config.PAGE_SIZE
        print(f"Fetched {len(all_activities)} activities so far...")

    print(f"Total activities found: {len(all_activities)}")

    for activity in tqdm(all_activities, desc="Downloading activities"):
        activity_id = activity["activityId"]

        filename = download_dir / f"{activity_id}_ACTIVITY.fit"

        if filename.exists():
            continue

        try:
            fit_data_zip = client.download_activity(activity_id, dl_fmt=Garmin.ActivityDownloadFormat.ORIGINAL)
            fit_bytes = unzip_bytes(fit_data_zip)
            
            with open(filename, "wb") as f:
                f.write(fit_bytes)

            time.sleep(config.REQUEST_DELAY_SECONDS)

        except Exception as e:
            print(f"Failed to download {activity_id}: {e}")

    print("Done!")
    
      
def unzip_bytes(zip_bytes: bytes) -> bytes:
    with io.BytesIO(zip_bytes) as bio:
        with zipfile.ZipFile(bio) as z:
            # Find the first .fit file
            fit_name = [n for n in z.namelist() if n.lower().endswith(".fit")][0]
            with z.open(fit_name) as f:
                return f.read()  # raw bytes of the FIT file