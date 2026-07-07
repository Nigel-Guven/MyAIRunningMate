from datetime import datetime
from typing import Optional
from pydantic import BaseModel

class BestEffort(BaseModel):
    best_effort_distance_metres: Optional[int] = None
    best_effort_time_seconds: Optional[float] = None
    best_effort_is_personal_record: Optional[bool] = False