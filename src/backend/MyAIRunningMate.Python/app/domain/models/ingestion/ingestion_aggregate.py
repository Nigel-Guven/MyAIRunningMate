from pydantic import BaseModel, Field
from typing import List, Optional
from app.domain.models.ingestion.activity_metrics import ActivityMetrics
from app.domain.models.ingestion.best_effort import BestEffort
from app.domain.models.ingestion.lap import Lap
from app.domain.models.ingestion.session import Session
from app.domain.models.ingestion.sport import Sport
from app.domain.models.ingestion.time_series_record import TimeSeriesRecord
from app.domain.models.ingestion.user_metrics import UserMetrics

class IngestionAggregate(BaseModel):
    garmin_id: Optional[str] = None
    sport: Optional[Sport] = None
    activityMetrics: Optional[ActivityMetrics] = None
    userMetrics: Optional[UserMetrics] = None
    sessions: List[Session] = Field(default_factory=list)
    bestEfforts: List[BestEffort] = Field(default_factory=list)
    laps: List[Lap] = Field(default_factory=list)
    time_series: List[TimeSeriesRecord] = Field(default_factory=list)
    
    