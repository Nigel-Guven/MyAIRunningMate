from datetime import datetime

from pydantic import BaseModel
from typing import Optional

class Lap(BaseModel):
    lap_number: int
    lap_start_time: Optional[datetime] = None
    lap_duration_seconds: Optional[float] = None
    lap_distance_metres: Optional[float] = None
    lap_average_speed: Optional[float] = None
    lap_average_heart_rate: Optional[int] = None
    lap_max_heart_rate: Optional[int] = None

    # Running/Cycling Sport Extensions
    lap_average_cadence: Optional[int] = None
    
    # Swim Specific Extensions
    lap_swim_stroke: Optional[str] = None
    lap_num_lengths: Optional[int] = None