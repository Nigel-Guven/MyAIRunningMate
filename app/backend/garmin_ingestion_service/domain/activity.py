
from dataclasses import dataclass, field
from typing import List, Optional

from domain.lap import Lap


@dataclass
class Activity:
    id: Optional[str] = None
    date: Optional[str] = None
    type: Optional[str] = None
    duration_seconds: Optional[float] = None
    distance_metres: Optional[float] = None
    average_heart_rate: Optional[float] = None
    max_heart_rate: Optional[float] = None
    total_elevation_gain: Optional[float] = None
    average_pace_seconds_per_kilometre: Optional[float] = None
    training_effect: Optional[float] = None
    v02_max: Optional[float] = None
    laps: List[Lap] = field(default_factory=list)
    