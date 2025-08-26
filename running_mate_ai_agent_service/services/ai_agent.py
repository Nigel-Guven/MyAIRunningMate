import os
import asyncio
import json
import requests
from aiokafka import AIOKafkaConsumer, AIOKafkaProducer
from services.cleaner import clean_activity
from config import KAFKA_BROKER, ACTIVITY_TOPIC, INSIGHTS_TOPIC, GEMINI_API_KEY

async def get_gemini_insights(activity_data: dict) -> str:
    """
    Generates running insights using the Gemini API based on activity data.
    The function uses a direct API call to the Gemini 2.5 Flash model.
    """
    if not GEMINI_API_KEY:
        raise ValueError("Gemini API key is not set")

    # Define the system prompt to guide the model's response
    system_prompt = (
        "You are a running coach AI. Analyze the provided running activity data and "
        "provide a concise, single-paragraph analysis with actionable advice. "
        "Focus on key metrics like speed, distance, and time. Your tone should be "
        "encouraging and motivational."
    )

    # Convert the cleaned activity data to a JSON string for the user prompt
    activity_json = json.dumps(activity_data)

    payload = {
        "contents": [{
            "parts": [{"text": activity_json}]
        }],
        "systemInstruction": {
            "parts": [{"text": system_prompt}]
        },
        "generationConfig": {
            "temperature": 0.7,
            "maxOutputTokens": 150
        }
    }

    headers = {
        "Content-Type": "application/json"
    }
    
    # Use the correct API endpoint for Gemini 2.5 Flash
    url = f"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-preview-05-20:generateContent?key={GEMINI_API_KEY}"

    try:
        response = requests.post(url, headers=headers, json=payload)
        response.raise_for_status()
        
        result = response.json()
        if 'candidates' in result and result['candidates']:
            return result['candidates'][0]['content']['parts'][0]['text']
        else:
            return "No insights generated for this activity."

    except requests.exceptions.RequestException as e:
        print(f"Error calling Gemini API: {e}")
        return f"Error: Failed to generate insights."

async def consume_and_analyze():
    """
    Consumes activities from the Kafka activities_topic, generates insights,
    and publishes the insights to the insights_topic.
    """
    # Initialize Kafka consumer
    consumer = AIOKafkaConsumer(
        ACTIVITY_TOPIC,
        bootstrap_servers=KAFKA_BROKER,
        group_id="ai_agent_group",
        value_deserializer=lambda m: json.loads(m.decode('utf-8'))
    )

    # Initialize Kafka producer
    producer = AIOKafkaProducer(
        bootstrap_servers=KAFKA_BROKER,
        value_serializer=lambda v: json.dumps(v).encode('utf-8')
    )
    
    await consumer.start()
    await producer.start()
    
    try:
        async for message in consumer:
            activity_data = message.value
            print(f"Received activity {activity_data.get('id')} from Kafka.")

            # Clean and analyze the activity data
            cleaned_data = clean_activity(activity_data)
            insights = await get_gemini_insights(cleaned_data)
            
            insight_message = {
                "activity_id": activity_data.get("id"),
                "insight_text": insights
            }
            
            # Publish the insights to the insights_topic
            await producer.send_and_wait(INSIGHTS_TOPIC, value=insight_message)
            print(f"Published insight for activity {activity_data.get('id')} to Kafka.")

    except Exception as e:
        print(f"An error occurred in the consumer loop: {e}")
    finally:
        await consumer.stop()
        await producer.stop()