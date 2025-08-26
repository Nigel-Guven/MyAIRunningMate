from fastapi import APIRouter, Request, Query
from fastapi.responses import RedirectResponse
from services.strava_client import get_auth_url, exchange_token_service, get_activities
from kafka import KafkaProducer
from kafka.errors import NoBrokersAvailable
import time
import json
import os

router = APIRouter()

KAFKA_BROKER = os.getenv("KAFKA_BROKER", "kafka:9092")
ACTIVITY_TOPIC = os.getenv("ACTIVITY_TOPIC", "activities_topic")

producer = None

def get_producer(retries=10, delay=5):
    """Initialize Kafka producer with retry logic."""
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
    return {"message": "Strava Service running"}

@router.get("/authorize")
def authorize():
    return RedirectResponse(get_auth_url())

@router.get("/exchange_token")
def exchange_token(request: Request, code: str):
    tokens = exchange_token_service(code)
    return {"message": "Tokens saved successfully!", **tokens}

@router.get("/activities")
def fetch_activities(limit: int = Query(5)):
    activities = get_activities(limit)
    p = get_producer()  # Lazy init
    for a in activities:
        p.send(ACTIVITY_TOPIC, value=a)
    return {"activities": activities}
