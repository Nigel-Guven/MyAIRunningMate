import datetime

from app.schemas.training_plan_request_schema import TrainingPlanRequest
from app.application.training_plan.week_resolution_service import generate_week_plan
from app.application.training_plan.plan_post_processor import post_process_week, build_response
from app.application.training_plan.resolve_allowed_workouts import resolve_allowed_workouts
from app.domain.models.rules.workout_spec import WORKOUT_SPECS
from app.domain.models.rules.plan_structure import build_plan_structure
from app.domain.models.phase import get_phase



def generate_full_plan(request: TrainingPlanRequest):

    structure = build_plan_structure(request.training_plan_length)

    start_date = datetime.date.today() + datetime.timedelta(days=1)

    all_events = []

    for week in range(1, structure.total_weeks + 1):

        phase = get_phase(structure, week)

        allowed_workouts = resolve_allowed_workouts(phase, request)

        workout_specs = {
            wt.value: WORKOUT_SPECS[wt]
            for wt in allowed_workouts
        }

        prompt_payload = {
            "week": week,
            "phase": phase.name.value,
            "focus": phase.focus.value,
            "constraints": {
                "allowed_workouts": list(workout_specs.keys()),
                "intensity": phase.constraints.intensity,
                "volume_multiplier": phase.constraints.volume_multiplier,
            },
            "start_date": str(start_date + datetime.timedelta(days=(week - 1) * 7)),
        }

        week_result = generate_week_plan(prompt_payload)

        validated = post_process_week(
            week_result,
            workout_specs,
            phase,
            request,
            week
        )

        all_events.extend(validated)

    return build_response(all_events, request)