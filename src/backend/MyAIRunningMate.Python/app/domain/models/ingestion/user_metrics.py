from datetime import datetime
from typing import Optional
from pydantic import BaseModel

class UserMetrics(BaseModel):
    user_volumetric_oxygen_max: Optional[float] = None
    user_max_heart_rate: Optional[int] = None
    user_lactate_threshold_heart_rate: Optional[float] = None
    user_lactate_threshold_power: Optional[float] = None
    user_lactate_threshold_speed: Optional[float] = None
    user_beginning_body_battery: Optional[int] = None
    user_beginning_body_potential: Optional[int] = None