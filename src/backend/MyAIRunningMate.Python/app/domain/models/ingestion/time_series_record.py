from pydantic import BaseModel
from typing import Optional
from datetime import datetime

class TimeSeriesRecord(BaseModel):
    tsr_timestamp: datetime
    tsr_latitude: Optional[float] = None
    tsr_longitude: Optional[float] = None
    tsr_heart_rate: Optional[int] = None
    tsr_cadence: Optional[int] = None
    tsr_distance_metres: Optional[float] = None
    tsr_power: Optional[int] = None
    
    
    