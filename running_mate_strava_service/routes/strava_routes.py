from fastapi import APIRouter, Query
from fastapi.responses import RedirectResponse
from services.strava_client import get_auth_url, exchange_token_service, get_activities
from kafka import KafkaProducer
from kafka.errors import NoBrokersAvailable
import time
import json
import os

router = APIRouter()

# The Kafka broker and topic are defined here
KAFKA_BROKER = os.getenv("KAFKA_BROKER", "kafka:9092")
ACTIVITY_TOPIC = os.getenv("ACTIVITY_TOPIC", "activities_topic")

producer = None

def get_producer(retries=10, delay=5):
    """
    Initializes a Kafka producer with retry logic for lazy connection.
    """
    global producer
    if producer is None:
        while retries > 0:
            try:
                producer = KafkaProducer(
                    bootstrap_servers=KAFKA_BROKER,
                    value_serializer=lambda v: json.dumps(v).encode("utf-8")
                )
                print("Kafka producer connected")
                break
            except NoBrokersAvailable:
                print(f"Kafka not available, retrying in {delay}s...")
                time.sleep(delay)
                retries -= 1
        if producer is None:
            raise RuntimeError("Kafka not available after retries")
    return producer

@router.get("/")
def root():
    """
    Basic endpoint to confirm the service is running.
    """
    return {"message": "Strava Service running"}

@router.get("/authorize")
def authorize():
    """
    Redirects to the Strava OAuth authorization page.
    """
    return RedirectResponse(get_auth_url())

@router.get("/exchange_token")
def exchange_token(code: str):
    """
    Exchanges the authorization code for tokens.
    """
    tokens = exchange_token_service(code)
    return {"message": "Tokens saved successfully!", **tokens}

@router.get("/activities")
def fetch_activities(limit: int = Query(5)):
    """
    Fetches the latest activities from Strava and publishes them to the Kafka topic.
    This is a synchronous endpoint, so the function will wait for the producer to connect.
    """
    activities = get_activities(limit)
    p = get_producer()  # Lazy init of the producer
    for a in activities:
        p.send(ACTIVITY_TOPIC, value=a)
    return {"message": f"{len(activities)} activities fetched and sent to Kafka."}