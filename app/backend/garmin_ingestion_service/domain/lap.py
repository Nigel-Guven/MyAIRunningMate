from dataclasses import dataclass
from typing import Optional


@dataclass
class Lap:
    lap: Optional[int] = None
    distance_metres: Optional[float] = None
    duration_seconds: Optional[float] = None
    average_heart_rate: Optional[int] = None