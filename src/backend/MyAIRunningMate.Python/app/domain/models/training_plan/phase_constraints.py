from dataclasses import dataclass
from typing import List
from .enums.workout_types import WorkoutType

@dataclass
class PhaseConstraints:
    volume_multiplier: float
    intensity: str
    allowed_workouts: List[WorkoutType]
    max_sessions_per_week: int
    min_rest_days: int