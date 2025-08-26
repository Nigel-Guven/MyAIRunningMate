from .models import Activity

def save_activity(db, activity_data):
    existing = db.query(Activity).filter(Activity.id == activity_data["id"]).first()
    if existing:
        return existing

    new_activity = Activity(
        id=activity_data.get("id"),
        name=activity_data.get("name"),
        distance=activity_data.get("distance"),
        moving_time=activity_data.get("moving_time"),
        elapsed_time=activity_data.get("elapsed_time"),
        elevation_gain=activity_data.get("total_elevation_gain"),
        type=activity_data.get("type"),
        start_date=activity_data.get("start_date"),
        start_date_local=activity_data.get("start_date_local"),
        average_speed=activity_data.get("average_speed"),
        max_speed=activity_data.get("max_speed"),
        average_cadence=activity_data.get("average_cadence"),
        average_watts=activity_data.get("average_watts"),
        kilojoules=activity_data.get("kilojoules"),
    )
    db.add(new_activity)
    db.commit()
    db.refresh(new_activity)
    return new_activity
