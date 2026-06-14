from pydantic import BaseModel, ConfigDict
from typing import Optional

class LapSchema(BaseModel):
    lap: int
    distance_metres: Optional[float] = None
    duration_seconds: Optional[float] = None
    average_heart_rate: Optional[int] = None
    average_speed: Optional[float] = None
    
    # Running/Cycling Sport Extensions
    average_cadence: Optional[int] = None
    
    # Swimming Specific Extensions
    primary_stroke: Optional[str] = None
    average_swolf: Optional[int] = None

    model_config = ConfigDict(from_attributes=True)