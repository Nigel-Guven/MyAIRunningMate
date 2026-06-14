from pydantic import BaseModel, Field
from typing import List, Optional, Dict, Any
from datetime import datetime
from app.domain.models.lap import Lap
from app.domain.models.time_series_record import TimeSeriesRecord

class Activity(BaseModel):
    garmin_id: Optional[str] = None
    start_time: Optional[datetime] = None 
    type: Optional[str] = None
    duration_seconds: Optional[float] = None
    moving_time_seconds: Optional[float] = None
    distance_metres: Optional[float] = None
    calories: Optional[int] = None
    
    # Performance Summaries
    average_heart_rate: Optional[int] = None
    max_heart_rate: Optional[int] = None
    total_elevation_gain: Optional[float] = None
    training_effect: Optional[float] = None
    raw_pace_seconds_per_meter: Optional[float] = None
    
    # Sport Specific Configs
    detected_pool_length: Optional[int] = None
    
    # Nested Collections
    time_series: List[TimeSeriesRecord] = Field(default_factory=list)
    laps: List[Lap] = Field(default_factory=list)