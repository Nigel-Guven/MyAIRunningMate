from typing import List, Optional

from pydantic import BaseModel, ConfigDict, Field

from app.schemas.ingestion.activity_metrics_schema import ActivityMetricsSchema
from app.schemas.ingestion.best_effort_schema import BestEffortSchema
from app.schemas.ingestion.lap_schema import LapSchema
from app.schemas.ingestion.session_schema import SessionSchema
from app.schemas.ingestion.sport_schema import SportSchema
from app.schemas.ingestion.time_series_schema import TimeSeriesSchema
from app.schemas.ingestion.user_metrics_schema import UserMetricsSchema

class IngestionAggregateSchema(BaseModel):
    garmin_id: Optional[str] = None
    sport: Optional[SportSchema] = None
    activityMetrics: Optional[ActivityMetricsSchema] = None
    userMetrics: Optional[UserMetricsSchema] = None
    session: Optional[SessionSchema] = None
    bestEfforts: List[BestEffortSchema] = Field(default_factory=list)
    laps: List[LapSchema] = Field(default_factory=list)
    time_series: List[TimeSeriesSchema] = Field(default_factory=list)
    
    model_config = ConfigDict(from_attributes=True)