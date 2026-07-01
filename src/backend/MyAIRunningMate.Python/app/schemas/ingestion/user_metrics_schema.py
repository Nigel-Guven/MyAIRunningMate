from datetime import datetime

from pydantic import BaseModel, ConfigDict
from typing import Optional

class UserMetricsSchema(BaseModel):
    user_volumetric_oxygen_max: Optional[float] = None
    user_max_heart_rate: Optional[float] = None
    user_lactate_threshold_heart_rate: Optional[float] = None
    user_lactate_threshold_power: Optional[float] = None
    user_lactate_threshold_speed: Optional[float] = None
    user_beginning_body_battery: Optional[int] = None
    user_beginning_body_potential: Optional[int] = None

    model_config = ConfigDict(from_attributes=True)