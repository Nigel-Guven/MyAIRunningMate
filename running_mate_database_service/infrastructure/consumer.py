import os
import json
from kafka import KafkaConsumer
from sqlalchemy.orm import Session
from .database import SessionLocal
from .models import Activity

KAFKA_BROKER = os.getenv("KAFKA_BROKER", "kafka:9092")
ACTIVITY_TOPIC = os.getenv("ACTIVITY_TOPIC", "activities_topic")

def save_activity_to_db(db: Session, activity_data: dict):
    # Avoid duplicates
    if db.query(Activity).filter(Activity.id == activity_data["id"]).first():
        return

    new_activity = Activity(
        id=activity_data["id"],
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


def consume_activities():
    consumer = KafkaConsumer(
        ACTIVITY_TOPIC,
        bootstrap_servers=[KAFKA_BROKER],
        value_deserializer=lambda m: json.loads(m.decode("utf-8")),
        auto_offset_reset="earliest",
        enable_auto_commit=True,
        group_id="database_service_group"
    )

    db = SessionLocal()
    print(f"Database Service consuming from topic '{ACTIVITY_TOPIC}'...")
    try:
        for message in consumer:
            activity_data = message.value
            save_activity_to_db(db, activity_data)
            print(f"Saved activity {activity_data['id']} to DB")
    except KeyboardInterrupt:
        print("Consumer stopped manually.")
    finally:
        db.close()
