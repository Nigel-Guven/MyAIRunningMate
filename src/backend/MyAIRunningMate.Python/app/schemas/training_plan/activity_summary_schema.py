import datetime

from pydantic import BaseModel


class ActivitySummarySchema(BaseModel):
    exercise_type: str
    start_date: datetime.datetime
    duration_seconds: float
    distance_metres: float = 0
    average_heart_rate: int = 0
    max_heart_rate: int = 0
    total_elevation_gain: float = 0
    average_seconds_per_kilometre: float = 0
    training_effect: float = 0
    
    