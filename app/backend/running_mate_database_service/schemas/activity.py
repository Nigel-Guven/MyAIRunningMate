from pydantic import BaseModel, ConfigDict
from datetime import datetime
from typing import List, Optional

class LapBase(BaseModel):
    lap_number: int
    distance_metres: float
    duration_seconds: float
    average_heart_rate: Optional[int] = None

class LapCreate(LapBase):
    pass

class Lap(LapBase):
    id: int
    activity_id: str
    
    model_config = ConfigDict(from_attributes=True)

class ActivityBase(BaseModel):
    id: str
    date: datetime
    type: str
    duration_seconds: float
    distance_metres: float
    average_heart_rate: Optional[int] = None
    max_heart_rate: Optional[int] = None
    total_elevation_gain: Optional[float] = None
    average_pace_seconds_per_kilometre: Optional[float] = None
    training_effect: Optional[float] = None
    vo2_max: Optional[float] = None

class ActivityCreate(ActivityBase):
    laps: List[LapCreate] = []

class Activity(ActivityBase):
    laps: List[Lap] = []

    model_config = ConfigDict(from_attributes=True)