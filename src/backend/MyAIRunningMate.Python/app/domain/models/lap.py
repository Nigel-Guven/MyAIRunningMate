from pydantic import BaseModel
from typing import Optional

class Lap(BaseModel):
    lap: int
    distance_metres: Optional[float] = None
    duration_seconds: Optional[float] = None
    average_heart_rate: Optional[int] = None
    average_speed: Optional[float] = None
    
    # Running/Cycling Sport Extensions
    average_cadence: Optional[int] = None
    
    # Swim Specific Extensions
    primary_stroke: Optional[str] = None
    average_swolf: Optional[int] = None