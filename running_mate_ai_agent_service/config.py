import os
from dotenv import load_dotenv

load_dotenv()

# Environment variables for Kafka broker and topics
KAFKA_BROKER = os.getenv("KAFKA_BROKER", "kafka:9092")
ACTIVITY_TOPIC = os.getenv("ACTIVITY_TOPIC", "activities_topic")
INSIGHTS_TOPIC = os.getenv("INSIGHTS_TOPIC", "insights_topic")

# API key for the Gemini API
GEMINI_API_KEY = os.getenv("GEMINI_API_KEY")