from dataclasses import dataclass
from typing import Dict
from app.domain.models.enums.workout_types import WorkoutType
from app.domain.models.enums.load_category import LoadCategory


@dataclass
class WorkoutSpec:
    base_distance_range_km: tuple | None
    intensity: str
    loadCategory: LoadCategory
    requires_hard_effort: bool


WORKOUT_SPECS: Dict[WorkoutType, WorkoutSpec] = {
    WorkoutType.EASY_RUN: WorkoutSpec((3, 10), "low", LoadCategory.AEROBIC, False),
    WorkoutType.RECOVERY_RUN: WorkoutSpec((3, 8), "low", LoadCategory.RECOVERY, False),
    WorkoutType.LONG_RUN: WorkoutSpec((8, 35), "low", LoadCategory.AEROBIC, False),
    WorkoutType.TEMPO_RUN: WorkoutSpec((5, 12), "moderate", LoadCategory.QUALITY, True),
    WorkoutType.FARTLEK: WorkoutSpec((5, 10), "moderate", LoadCategory.QUALITY, True),
    WorkoutType.PROGRESSION_RUN: WorkoutSpec((5, 15), "moderate", LoadCategory.QUALITY, True),
    WorkoutType.INTERVAL_RUN: WorkoutSpec((4, 10), "high", LoadCategory.QUALITY, True),
    WorkoutType.HILL_REPEATS: WorkoutSpec((4, 8), "high", LoadCategory.QUALITY, True),

    WorkoutType.EASY_SWIM: WorkoutSpec((500, 1000), "low", LoadCategory.RECOVERY, False),
    WorkoutType.LONG_SWIM: WorkoutSpec((1000, 2500), "low", LoadCategory.AEROBIC, False),
    WorkoutType.INTERVAL_SWIM: WorkoutSpec((500, 1500), "high", LoadCategory.QUALITY, True),
    
    WorkoutType.EVENT: WorkoutSpec(None, "high", LoadCategory.QUALITY, True)
}