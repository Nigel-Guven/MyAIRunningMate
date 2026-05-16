from app.domain.models.enums.workout_types import WorkoutType

def resolve_allowed_workouts(phase, request):

    base = phase.constraints.allowed_workouts

    # goal-based filtering
    if request.primary_goal == "5k":
        return [w for w in base if w != WorkoutType.LONG_SWIM]

    if request.primary_goal == "marathon":
        return base + [WorkoutType.LONG_RUN]

    return base