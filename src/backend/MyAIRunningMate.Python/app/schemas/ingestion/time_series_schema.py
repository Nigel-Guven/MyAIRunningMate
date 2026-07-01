from datetime import datetime

from pydantic import BaseModel, ConfigDict
from typing import Optional

class TimeSeriesSchema(BaseModel):
    tsr_timestamp: datetime
    tsr_latitude: Optional[float] = None
    tsr_longitude: Optional[float] = None
    tsr_heart_rate: Optional[int] = None
    tsr_cadence: Optional[int] = None
    tsr_distance_metres: Optional[float] = None
    tsr_power: Optional[float] = None

    model_config = ConfigDict(from_attributes=True)