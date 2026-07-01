import string
from typing import List

from pydantic import BaseModel
from app.schemas.training_plan.activity_summary_schema import ActivitySummarySchema


class TrainingPlanRequest(BaseModel):
    primary_goal: str                                       # 5k, 10k, half-marathon, marathon, ultra
    running_experience_years: int                           # 0-1, 1-3, 3-5, 5+ years 
    running_level: str                                      # beginner, intermediate, advanced
    training_plan_length: int                               # 4, 6, 8, 12 weeks
    pool_size: str                                          
    weight_pounds: float
    history: List[ActivitySummarySchema]