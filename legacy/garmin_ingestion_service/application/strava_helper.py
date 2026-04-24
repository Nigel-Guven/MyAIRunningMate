


import database_client


def insert_activity(activity, map_id=None):
    data = {
        "strava_id": activity["id"],
        "resource_name": activity.get("name"),
        "elapsed_time": activity.get("elapsed_time"),
        "distance_metres": activity.get("distance"),
        "total_elevation_gain": activity.get("total_elevation_gain"),
        "average_cadence": activity.get("average_cadence"),
        "type": activity.get("type"),
        "start_date": activity.get("start_date"),
        "achievement_count": activity.get("achievement_count"),
        "kudos_count": activity.get("kudos_count"),
        "athlete_count": activity.get("athlete_count"),
        "pr_count": activity.get("pr_count"),
        "elevation_low": activity.get("elev_low"),
        "elevation_high": activity.get("elev_high"),
        "map_id": map_id   
    }

    response = database_client.supabase.table("strava_resource").insert(data).execute()

    if not response.data:
        raise Exception("Strava Activity Insert failed")

    return response.data[0]["id"]

def insert_map(polyline):
    data = {
        "summary_polyline": polyline,
    }

    response = database_client.supabase.table("strava_resource_map").insert(data).execute()

    if not response.data:
        raise Exception("Map Insert failed")

    return response.data[0]["id"]