from enum import Enum


class WorkoutType(Enum):
    EASY_RUN = "easy_run"
    RECOVERY_RUN = "recovery_run"
    LONG_RUN = "long_run"
    TEMPO_RUN = "tempo_run"
    INTERVAL_RUN = "interval_run"
    FARTLEK = "fartlek"
    HILL_REPEATS = "hill_repeats"
    PROGRESSION_RUN = "progression_run"

    EASY_SWIM = "easy_swim"
    INTERVAL_SWIM = "interval_swim"
    LONG_SWIM = "long_swim"

    EVENT = "race_event"