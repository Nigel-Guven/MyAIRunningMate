from pydantic import BaseModel, ConfigDict, Field
from typing import List, Optional
import datetime
from app.schemas.lap_schema import LapSchema
from app.schemas.time_series_schema import TimeSeriesSchema

class ActivitySchema(BaseModel):
    garmin_id: str
    start_time: Optional[datetime.datetime] = None
    type: Optional[str] = None
    duration_seconds: Optional[float] = None
    moving_time_seconds: Optional[float] = None
    distance_metres: Optional[float] = None
    calories: Optional[int] = None

    average_heart_rate: Optional[int] = None
    max_heart_rate: Optional[int] = None
    total_elevation_gain: Optional[float] = None
    training_effect: Optional[float] = None
    raw_pace_seconds_per_meter: Optional[float] = None
    
    # Pool Config Context
    detected_pool_length: Optional[int] = None

    time_series: List[TimeSeriesSchema] = Field(default_factory=list)
    laps: List[LapSchema] = Field(default_factory=list)

    model_config = ConfigDict(from_attributes=True)