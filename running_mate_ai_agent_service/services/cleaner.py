def clean_activity(activity):
    """
    Cleans and formats a raw Strava activity dictionary for analysis.
    """
    return {
        "name": activity.get("name"),
        "distance_km": round(activity.get("distance", 0) / 1000, 2),
        "moving_time_min": round(activity.get("moving_time", 0) / 60, 1),
        "elapsed_time_min": round(activity.get("elapsed_time", 0) / 60, 1),
        "elevation_gain_m": activity.get("total_elevation_gain", 0),
        "elev_high": activity.get("elev_high", 0),
        "elev_low": activity.get("elev_low", 0),
        "type": activity.get("type"),
        "average_speed_m_s": activity.get("average_speed", 0),
        "max_speed_m_s": activity.get("max_speed", 0),
        "average_cadence": activity.get("average_cadence"),
        "average_watts": activity.get("average_watts"),
        "max_watts": activity.get("max_watts"),
        "weighted_average_watts": activity.get("weighted_average_watts"),
        "kilojoules": activity.get("kilojoules"),
        "start_date": activity.get("start_date_local")
    }