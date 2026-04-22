
from pathlib import Path
import time
from database_client import supabase
from database_bulk_ingest import insert_activity_to_supabase, insert_laps
from application.garmin_fit_cleaner import format_file


def main():
    
    BASE_DIR = Path(__file__).resolve().parents[2]
    DOWNLOAD_DIR = BASE_DIR / "data" / "garmin_activities"
    
    for file in sorted(Path(DOWNLOAD_DIR).glob("*.fit")):
        
        if not file.exists():
            continue
        
        activity = format_file(file)
        
        print(f"Ingesting {activity.id}")

        try:
            existing = supabase.table("activity") \
                .select("id") \
                .eq("garmin_activity_id", activity.id) \
                .execute()

            if existing.data:
                continue
            
            activity_uuid = insert_activity_to_supabase(activity)
            insert_laps(activity_uuid, activity)

            print(f"[INGEST] {activity.type} | {activity.distance_metres}m")
            print(f"✔ Done {activity.id}")

        except Exception as e:
            print(f"❌ Failed {activity.id}: {e}")

if __name__ == "__main__":
        main()
        exit()
