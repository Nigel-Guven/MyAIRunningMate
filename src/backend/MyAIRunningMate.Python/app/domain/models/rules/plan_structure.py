from dataclasses import dataclass
from typing import List
from app.domain.models.phase import Phase
from app.domain.models.phase_constraints import PhaseConstraints
from app.domain.models.enums.phase_enums import PhaseName
from app.domain.models.enums.phase_enums import PhaseFocus
from app.domain.models.enums.workout_types import WorkoutType


DEFAULT_PLAN_WEEKS = 8
RECOVERY_WEEKS = 1


@dataclass
class PlanStructure:
    total_weeks: int
    phases: List[Phase]


def build_plan_structure(total_weeks: int = DEFAULT_PLAN_WEEKS) -> PlanStructure:

    recovery_week_start = total_weeks + 1

    if total_weeks == 4:
        phases = [
            Phase(
                PhaseName.BASE, 1, 1, PhaseFocus.FOUNDATION,
                PhaseConstraints( 1.0, "low",
                    [
                        WorkoutType.EASY_RUN,
                        WorkoutType.LONG_RUN,
                        WorkoutType.EASY_SWIM,
                        WorkoutType.LONG_SWIM
                    ],
                    max_sessions_per_week=4,
                    min_rest_days=2
                )
            ),
            Phase(
                PhaseName.BUILD, 2, 3, PhaseFocus.THRESHOLD,
                PhaseConstraints( 1.1, "moderate",
                    [
                        WorkoutType.LONG_RUN,
                        WorkoutType.TEMPO_RUN,
                        WorkoutType.FARTLEK,
                        WorkoutType.PROGRESSION_RUN,
                        WorkoutType.RECOVERY_RUN,
                        WorkoutType.INTERVAL_SWIM,
                        WorkoutType.LONG_SWIM
                    ],
                    max_sessions_per_week=5,
                    min_rest_days=1
                )
            ),
            Phase(
                PhaseName.TAPER, 4, 4, PhaseFocus.TAPERING,
                PhaseConstraints( 0.6, "low",
                    [
                        WorkoutType.EASY_RUN,
                        WorkoutType.EASY_SWIM,
                        WorkoutType.EVENT
                    ],
                    max_sessions_per_week=3,
                    min_rest_days=2
                )
            ),
        ]

    elif total_weeks == 8:
        phases = [
            Phase(
                PhaseName.BASE, 1, 2, PhaseFocus.FOUNDATION,
                PhaseConstraints( 1.0, "low",
                    [
                        WorkoutType.EASY_RUN,
                        WorkoutType.LONG_RUN,
                        WorkoutType.EASY_SWIM,
                        WorkoutType.LONG_SWIM
                    ],
                    max_sessions_per_week=5,
                    min_rest_days=2
                )
            ),
            Phase(
                PhaseName.BUILD, 3, 5, PhaseFocus.THRESHOLD,
                PhaseConstraints( 1.1, "moderate",
                    [
                        WorkoutType.LONG_RUN,
                        WorkoutType.TEMPO_RUN,
                        WorkoutType.FARTLEK,
                        WorkoutType.PROGRESSION_RUN,
                        WorkoutType.RECOVERY_RUN,
                        WorkoutType.INTERVAL_SWIM,
                        WorkoutType.LONG_SWIM
                    ],
                    max_sessions_per_week=6,
                    min_rest_days=1
                )
            ),
            Phase(
                PhaseName.PEAK, 6, 7, PhaseFocus.RACE_SPECIFIC,
                PhaseConstraints( 1.0, "high",
                    [
                        WorkoutType.LONG_RUN,
                        WorkoutType.TEMPO_RUN,
                        WorkoutType.FARTLEK,
                        WorkoutType.PROGRESSION_RUN,
                        WorkoutType.INTERVAL_RUN,
                        WorkoutType.HILL_REPEATS,
                        WorkoutType.RECOVERY_RUN,
                        WorkoutType.INTERVAL_SWIM,
                        WorkoutType.LONG_SWIM
                    ],
                    max_sessions_per_week=5,
                    min_rest_days=1
                )
            ),
            Phase(
                PhaseName.TAPER, 8, 8, PhaseFocus.TAPERING,
                PhaseConstraints( 0.6, "low",
                    [
                        WorkoutType.EASY_RUN,
                        WorkoutType.EASY_SWIM,
                        WorkoutType.EVENT
                    ],
                    max_sessions_per_week=3,
                    min_rest_days=2
                )
            ),
        ]

    elif total_weeks == 12:
        phases = [
            Phase(
                PhaseName.BASE, 1, 2, PhaseFocus.FOUNDATION,
                PhaseConstraints(1.0, "low", 
                [
                    WorkoutType.EASY_RUN,
                    WorkoutType.LONG_RUN,
                    WorkoutType.EASY_SWIM,
                    WorkoutType.LONG_SWIM
                ], 
                max_sessions_per_week=5, 
                min_rest_days=2)
            ),
            Phase(
                PhaseName.BUILD, 3, 7, PhaseFocus.THRESHOLD,
                PhaseConstraints(1.1, "moderate", 
                [
                    WorkoutType.LONG_RUN,
                    WorkoutType.TEMPO_RUN,
                    WorkoutType.FARTLEK,
                    WorkoutType.PROGRESSION_RUN,
                    WorkoutType.RECOVERY_RUN,
                    WorkoutType.INTERVAL_SWIM,
                    WorkoutType.LONG_SWIM,
                    WorkoutType.HILL_REPEATS,
                    WorkoutType.INTERVAL_RUN,
                ], 
                max_sessions_per_week=6, 
                min_rest_days=1)
            ),
            Phase(
                PhaseName.PEAK, 8, 10, PhaseFocus.RACE_SPECIFIC,
                PhaseConstraints(1.0, "high", 
                [
                    WorkoutType.EASY_RUN,
                    WorkoutType.LONG_RUN,
                    WorkoutType.TEMPO_RUN,
                    WorkoutType.FARTLEK,
                    WorkoutType.HILL_REPEATS,
                    WorkoutType.INTERVAL_RUN,
                    WorkoutType.PROGRESSION_RUN,
                    WorkoutType.RECOVERY_RUN,
                    WorkoutType.INTERVAL_SWIM,
                    WorkoutType.LONG_SWIM
                ], 
                max_sessions_per_week=5, 
                min_rest_days=1)
            ),
            Phase(
                PhaseName.TAPER, 11, 12, PhaseFocus.TAPERING,
                PhaseConstraints(0.6, "low", 
                [
                    WorkoutType.EASY_RUN,
                    WorkoutType.EASY_SWIM,
                    WorkoutType.EVENT
                ], 
                max_sessions_per_week=3, 
                min_rest_days=2)
            ),
        ]

    else:
        raise ValueError("Supported plan lengths: 4, 8, 12")

    phases.append(
        Phase(
            name=PhaseName.RECOVERY,
            start_week=recovery_week_start,
            end_week=recovery_week_start,
            focus=PhaseFocus.RECOVERY,
            constraints=PhaseConstraints(
                volume_multiplier=0.5,
                intensity="low",
                allowed_workouts=
                [
                    WorkoutType.RECOVERY_RUN,
                    WorkoutType.LONG_RUN,
                    WorkoutType.EASY_SWIM
                ],
                max_sessions_per_week=3,
                min_rest_days=3
            )
        )
    )

    return PlanStructure(total_weeks + 1, phases)