import datetime

from app.schemas.training_plan.training_plan_request_schema import TrainingPlanRequest
from app.application.training_plan.week_resolution_service import generate_week_plan
from app.application.training_plan.plan_post_processor import post_process_week, build_response
from app.domain.models.training_plan.rules.workout_spec import WORKOUT_SPECS
from app.domain.models.training_plan.rules.plan_structure import build_plan_structure
from app.domain.models.training_plan.phase import get_phase



def generate_full_plan(request: TrainingPlanRequest):

    structure = build_plan_structure(request.training_plan_length)

    start_date = datetime.date.today() + datetime.timedelta(days=1)

    all_events = []

    for week in range(1, structure.total_weeks + 1):

        phase = get_phase(structure, week)

        workout_specs =  WORKOUT_SPECS

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

    safe_events = []
    for event in all_events:

        raw_distance = event.get("distance_metres", 0)
        
        if isinstance(raw_distance, int):
            clean_distance = raw_distance
        elif isinstance(raw_distance, str) and raw_distance.isdigit():
            clean_distance = int(raw_distance)
        else:
            clean_distance = 0
            
        event["distance_metres"] = clean_distance
        safe_events.append(event)

    # Pass the sanitized events list into your response builder
    return build_response(safe_events, request)