from datetime import datetime
from typing import Optional

from pydantic import BaseModel, ConfigDict


class BestEffortSchema(BaseModel):
    best_effort_distance_metres: Optional[float] = None
    best_effort_time_seconds: Optional[float] = None
    best_effort_is_personal_record: Optional[bool] = False
    
    model_config = ConfigDict(from_attributes=True)