# app/domain/models/time_series_record.py
from pydantic import BaseModel
from typing import Optional
from datetime import datetime

class TimeSeriesRecord(BaseModel):
    timestamp: datetime
    distance_meters: Optional[float] = None
    heart_rate: Optional[int] = None
    cadence: Optional[int] = None
    latitude: Optional[float] = None
    longitude: Optional[float] = None