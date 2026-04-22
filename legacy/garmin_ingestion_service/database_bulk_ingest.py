

import database_client


def insert_activity_to_supabase(activity):
    data = {
        "garmin_activity_id": activity.id,
        "start_time": activity.date.isoformat(),
        "type": activity.type,
        "duration_seconds": activity.duration_seconds,
        "distance_metres": activity.distance_metres,
        "avg_heart_rate": activity.average_heart_rate,
        "max_heart_rate": activity.max_heart_rate,
        "total_elevation_gain": activity.total_elevation_gain,
        "avg_seconds_per_km": activity.average_pace_seconds_per_kilometre,
        "training_effect": activity.training_effect
    }

    response = database_client.supabase.table("activity").insert(data).execute()

    if not response.data:
        raise Exception("Insert failed")

    return response.data[0]["id"]

def insert_laps(activity_uuid, activity):
    lap_rows = []

    for i, lap in enumerate(activity.laps):
        lap_rows.append({
            "activity_id": activity_uuid,
            "lap_number": i + 1,
            "distance_metres": lap.distance_metres,
            "duration_seconds": lap.duration_seconds,
            "avg_heart_rate": lap.average_heart_rate
        })

    database_client.supabase.table("lap").insert(lap_rows).execute()