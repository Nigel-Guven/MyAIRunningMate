import datetime
from app.domain.models.training_plan.phase import PhaseName
from app.domain.models.training_plan.enums.workout_types import WorkoutType

def post_process_week(week_result, workout_specs, phase, request, week_num):
    validated_events = []

    for event in week_result["events"]:
        subtype_str = event.get("exercise_subtype")
        workout_type = None

        if subtype_str:
            try:
                workout_type = WorkoutType(subtype_str.lower())
            except ValueError:
                try:
                    workout_type = WorkoutType(subtype_str.upper())
                except ValueError:
                    workout_type = None

        if not workout_type or workout_type not in workout_specs:
            validated_events.append({
                "event_date": event.get("event_date"),
                "exercise_type": "rest",
                "exercise_subtype": "rest",
                "description": event.get("description", "Auto rest (fallback)"),
                "distance_metres": 0
            })
            continue

        spec = workout_specs[workout_type]

        if hasattr(spec, 'base_distance_range_meters') and spec.base_distance_range_meters:
            min_meters, max_meters = spec.base_distance_range_meters

            raw_distance = event.get("distance_metres")

            if raw_distance is None or raw_distance == 0:
                meters = int((min_meters + max_meters) / 2)
            else:
                try:
                    meters = int(raw_distance)
                except (ValueError, TypeError):
                    meters = min_meters

            meters = max(min_meters, min(meters, max_meters))
            event["distance_metres"] = meters
        else:
            event["distance_metres"] = 0

        validated_events.append(event)

    if phase.name == PhaseName.TAPER and week_num == phase.end_week and validated_events:
        # Instead of appending an 8th day to a 7-day week, replace the final Sunday event
        race_distance_meters = 5000  # Default fallback fallback
        
        # Safely extract numeric distance out of the primary goal string if present
        if hasattr(request, 'primary_goal') and "10k" in str(request.primary_goal).lower():
            race_distance_meters = 10000
        elif hasattr(request, 'primary_goal') and "8k" in str(request.primary_goal).lower():
            race_distance_meters = 8000

        validated_events[-1] = {
            "event_date": validated_events[-1]["event_date"],
            "exercise_type": "event",
            "exercise_subtype": "race_event",
            "description": f"RACE DAY: {request.primary_goal}!",
            "distance_metres": race_distance_meters
        }

    return validated_events

def build_response(all_events, request):
    start_date = datetime.date.today() + datetime.timedelta(days=1)

    total_days = len(all_events)
    end_date = start_date + datetime.timedelta(days=max(0, total_days - 1))

    return {
        "title": f"{request.primary_goal} training plan",
        "start_date": start_date.isoformat(),
        "end_date": end_date.isoformat(),
        "description": "AI generated structured training plan",
        "training_plan_events": all_events
    }