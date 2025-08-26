import os
import asyncio
from aiokafka import AIOKafkaConsumer
from infrastructure.crud import save_activity
from infrastructure.database import get_db
import json

KAFKA_BROKER = os.getenv("KAFKA_BROKER", "kafka:9092")
ACTIVITY_TOPIC = os.getenv("ACTIVITY_TOPIC", "activities_topic")
INSIGHTS_TOPIC = os.getenv("INSIGHTS_TOPIC", "insights_topic")

async def consume_activities():
    """
    Consumes activity messages from a Kafka topic and saves them to Firestore.
    """
    firestore_db = get_db()
    
    # Initialize Kafka consumer for the activity topic
    consumer = AIOKafkaConsumer(
        ACTIVITY_TOPIC,
        bootstrap_servers=KAFKA_BROKER,
        group_id="database_service_group",
        value_deserializer=lambda m: json.loads(m.decode("utf-8"))
    )
    
    print(f"Starting to consume from topic '{ACTIVITY_TOPIC}'...")
    
    await consumer.start()
    try:
        async for message in consumer:
            activity_data = message.value
            print(f"Received activity {activity_data.get('id')} from Kafka")
            
            # Save the activity data to Firestore
            await save_activity(firestore_db, activity_data)
            
            print(f"Saved activity {activity_data.get('id')} to Firestore")
    except Exception as e:
        print(f"An error occurred during consumption: {e}")
    finally:
        await consumer.stop()

if __name__ == "__main__":
    asyncio.run(consume_activities())