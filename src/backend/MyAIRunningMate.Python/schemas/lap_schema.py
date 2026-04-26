from pydantic import BaseModel
from typing import Optional

class LapSchema(BaseModel):
    lap: int
    distance_metres: Optional[float]
    duration_seconds: Optional[float]
    average_heart_rate: Optional[int]