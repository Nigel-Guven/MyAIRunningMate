from fastapi import FastAPI
from services.ai_agent import analyze_activities

app = FastAPI(title="AI Agent Service")

@app.get("/analyze")
def analyze(limit: int = 5):
    return analyze_activities(limit)