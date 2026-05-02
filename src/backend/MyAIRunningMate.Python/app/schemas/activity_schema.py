from pydantic import BaseModel, ConfigDict
from typing import List, Optional
from datetime import datetime
from schemas.lap_schema import LapSchema

class ActivitySchema(BaseModel):
    garmin_id: str
    start_time: Optional[datetime] = None
    type: Optional[str] = None
    duration_seconds: Optional[float] = None
    distance_metres: Optional[float] = None
    average_heart_rate: Optional[int] = None
    max_heart_rate: Optional[int] = None
    total_elevation_gain: Optional[float] = None
    training_effect: Optional[float] = None
    average_pace_seconds_per_kilometre: Optional[float] = None
    laps: List[LapSchema] = []

    model_config = ConfigDict(from_attributes=True)