
from dataclasses import dataclass, field
from typing import List, Optional

from domain.models.lap import Lap


@dataclass
class Activity:
    garmin_id: Optional[str] = None
    start_time: Optional[str] = None
    type: Optional[str] = None
    duration_seconds: Optional[float] = None
    distance_metres: Optional[float] = None
    average_heart_rate: Optional[float] = None
    max_heart_rate: Optional[float] = None
    total_elevation_gain: Optional[float] = None
    average_pace_seconds_per_kilometre: Optional[float] = None
    training_effect: Optional[float] = None
    laps: List[Lap] = field(default_factory=list)
    