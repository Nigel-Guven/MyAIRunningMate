from pydantic import BaseModel
from typing import List, Optional
from datetime import datetime
from .lap_schema import LapSchema

class ActivitySchema(BaseModel):
    id: str
    date: Optional[datetime]
    type: Optional[str]
    duration_seconds: Optional[float]
    distance_metres: Optional[float]
    average_heart_rate: Optional[int]
    max_heart_rate: Optional[int]
    total_elevation_gain: Optional[float]
    training_effect: Optional[float]
    v02_max: Optional[float]
    average_pace_seconds_per_kilometre: Optional[float]
    laps: List[LapSchema]