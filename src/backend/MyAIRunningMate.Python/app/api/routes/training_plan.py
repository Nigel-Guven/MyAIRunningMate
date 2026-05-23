from datetime import datetime
import os

from dotenv import load_dotenv
from fastapi import APIRouter, HTTPException
from google import genai
from app.schemas.training_plan_schema import TrainingPlanResponse
from app.schemas.training_plan_request_schema import TrainingPlanRequest
from app.application.training_plan import plan_orchestrator

router = APIRouter(prefix="/training_plan", tags=["training_plan"])

@router.post("/draft", response_model=TrainingPlanResponse)
async def process_training_plan(request_data: TrainingPlanRequest):
    try:
        raw_plan = plan_orchestrator.generate_full_plan(request_data)

        return TrainingPlanResponse(**raw_plan)
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))