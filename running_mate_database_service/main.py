import os
import asyncio
from aiokafka import AIOKafkaConsumer
from infrastructure.crud import save_activity
from infrastructure.database import SessionLocal, init_db
import json
import time
from sqlalchemy.exc import OperationalError

KAFKA_BROKER = os.getenv("KAFKA_BROKER", "kafka:9092")
ACTIVITY_TOPIC = os.getenv("ACTIVITY_TOPIC", "activities_topic")

def wait_for_postgres(retries=10, delay=5):
    for attempt in range(retries):
        try:
            init_db()
            print("Postgres is ready!")
            return
        except OperationalError:
            print(f"Postgres not ready, retrying in {delay} seconds... (attempt {attempt+1}/{retries})")
            time.sleep(delay)
    raise RuntimeError("Postgres failed to start after retries")

# Wait for Postgres before consuming
wait_for_postgres()

async def consume_activities():
    consumer = AIOKafkaConsumer(
        ACTIVITY_TOPIC,
        bootstrap_servers=KAFKA_BROKER,
        group_id="database_service_group",
        value_deserializer=lambda m: json.loads(m.decode("utf-8"))
    )
    await consumer.start()
    try:
        async for message in consumer:
            activity_data = message.value
            print(f"Received activity {activity_data.get('id')} from Kafka")
            db = SessionLocal()
            try:
                save_activity(db, activity_data)
            finally:
                db.close()
    finally:
        await consumer.stop()

if __name__ == "__main__":
    asyncio.run(consume_activities())
