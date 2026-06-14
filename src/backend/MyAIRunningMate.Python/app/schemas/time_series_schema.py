from datetime import datetime

from pydantic import BaseModel, ConfigDict
from typing import Optional

class TimeSeriesSchema(BaseModel):
    timestamp: datetime
    distance_meters: Optional[float] = None
    heart_rate: Optional[int] = None
    cadence: Optional[int] = None
    latitude: Optional[float] = None
    longitude: Optional[float] = None

    model_config = ConfigDict(from_attributes=True)