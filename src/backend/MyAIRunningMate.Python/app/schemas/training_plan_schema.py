import datetime
from typing import List

from pydantic import BaseModel


class TrainingPlanEventResponse(BaseModel):
    event_date: datetime.datetime
    exercise_type: str
    exercise_subtype: str
    description: str
    distance_metres: str

class TrainingPlanResponse(BaseModel):
    title: str
    start_date: datetime.datetime
    end_date: datetime.datetime
    description: str
    training_plan_events: List[TrainingPlanEventResponse]