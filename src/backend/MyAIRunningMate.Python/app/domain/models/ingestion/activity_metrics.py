from datetime import datetime
from typing import Optional
from pydantic import BaseModel

class ActivityMetrics(BaseModel):
    activity_metrics_start_time: Optional[datetime] = None
    activity_metrics_ending_body_battery: Optional[int] = None
    activity_metrics_ending_potential: Optional[int] = None
    activity_metrics_total_ascent: Optional[int] = None
    activity_metrics_total_descent: Optional[int] = None
    activity_metrics_recovery_time: Optional[float] = None  
    activity_metrics_num_laps: Optional[int] = None