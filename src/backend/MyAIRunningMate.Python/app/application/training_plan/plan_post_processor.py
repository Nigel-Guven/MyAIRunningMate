import datetime

from app.domain.models.phase import PhaseName
from app.domain.models.enums.workout_types import WorkoutType

def post_process_week(week_result, workout_specs, phase, request, week_num):

    validated_events = []

    for event in week_result["events"]:

        subtype = event.get("exercise_subtype")

        workout_type = None

        # -----------------------------
        # SAFE ENUM MAPPING
        # -----------------------------
        try:
            workout_type = WorkoutType(subtype)
        except Exception:
            workout_type = None

        # -----------------------------
        # FALLBACK: INVALID → REST DAY
        # -----------------------------
        if not workout_type or workout_type not in workout_specs:

            validated_events.append({
                "event_date": event.get("event_date"),
                "exercise_type": "rest",
                "exercise_subtype": "rest",
                "description": "Auto rest (fallback)",
                "distance_metres": 0
            })

            continue

        spec = workout_specs[workout_type]

        # -----------------------------
        # enforce distance bounds
        # -----------------------------
        if spec.base_distance_range_km:

            min_km, max_km = spec.base_distance_range_km

            km = event.get("distance_metres", 0) / 1000

            km = max(min_km, min(km, max_km))

            event["distance_metres"] = int(km * 1000)

        validated_events.append(event)

    # -----------------------------
    # inject RACE EVENT
    # -----------------------------
    if phase.name == PhaseName.TAPER and week_num == phase.end_week and validated_events:

        validated_events.append({
            "event_date": validated_events[-1]["event_date"],
            "exercise_type": "event",
            "exercise_subtype": request.primary_goal,
            "description": "RACE DAY",
            "distance_metres": request.primary_goal
        })

    return validated_events

def build_response(all_events, request):
    start_date = datetime.date.today() + datetime.timedelta(days=1)

    return {
        "title": f"{request.primary_goal} training plan",
        "start_date": start_date,
        "end_date": start_date + datetime.timedelta(days=len(all_events)),
        "description": "AI generated structured training plan",
        "training_plan_events": all_events
    }