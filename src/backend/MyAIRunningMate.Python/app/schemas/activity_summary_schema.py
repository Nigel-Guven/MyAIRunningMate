import datetime
from pydantic import BaseModel


class ActivitySummarySchema(BaseModel):
    exercise_type: str
    start_date: datetime.datetime
    duration_seconds: float
    distance_metres: float
    average_heart_rate: int
    max_heart_rate: int
    total_elevation_gain: float
    average_seconds_per_kilometre: float
    training_effect: float
    
    