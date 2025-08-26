import os
from fastapi import FastAPI
from routes.strava_routes import router as strava_router
from services.strava_client import validate_or_refresh_token
from kafka import KafkaProducer
from kafka.errors import NoBrokersAvailable
import time

app = FastAPI(title="Strava Data Collection Service")

@app.on_event("startup")
def startup_event():
    """
    Validates the Strava token and waits for the Kafka broker to be ready on application startup.
    """
    validate_or_refresh_token()
    print("Strava service started and token validated.")
    
    # Wait for Kafka to be ready
    kafka_broker = os.getenv("KAFKA_BROKER", "kafka:9092")
    retries = 10
    delay = 5
    while retries > 0:
        try:
            KafkaProducer(bootstrap_servers=kafka_broker)
            print("Kafka is ready!")
            break
        except NoBrokersAvailable:
            print(f"Kafka not available, retrying in {delay}s...")
            time.sleep(delay)
            retries -= 1
    else:
        raise RuntimeError("Kafka is not available after retries")

# Include Strava routes
app.include_router(strava_router, prefix="/strava")