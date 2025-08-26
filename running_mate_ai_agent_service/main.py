import os
import asyncio
from fastapi import FastAPI
from services.ai_agent import consume_and_analyze

app = FastAPI(title="AI Agent Service")

@app.on_event("startup")
async def startup_event():
    """
    On startup, create a background task to consume activities from Kafka and analyze them.
    This replaces the need for a separate API endpoint to trigger analysis.
    """
    asyncio.create_task(consume_and_analyze())
    print("AI Agent Service started, listening for activities on Kafka.")

@app.get("/")
def root():
    """
    This remains as a basic health check endpoint for the service.
    """
    return {"message": "AI Agent Service running"}