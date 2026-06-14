from pathlib import Path
from fitparse import FitFile
from app.domain.models.lap import Lap
from app.domain.models.activity import Activity

STROKE_MAPPING = {
    0: "Freestyle", 1: "Backstroke", 2: "Breaststroke", 
    3: "Butterfly", 4: "Drill", 5: "Mixed",
    "freestyle": "Freestyle", "backstroke": "Backstroke", 
    "breaststroke": "Breaststroke", "butterfly": "Butterfly", 
    "drill": "Drill", "mixed": "Mixed"
}

def format_file(file_obj) -> Activity:
    fitfile = FitFile(file_obj.file)
    return clean_fit_file(fitfile, file_obj.filename)

def extract_activity_id(filename: str) -> str:
    name = Path(filename).stem
    if name.endswith("_ACTIVITY"):
        return name.replace("_ACTIVITY", "")
    return name

def extract_stroke_safely(message) -> str:
    for field in message.fields:
        if field.name in ['swim_stroke', 'avg_swim_stroke', 'stroke_type'] and field.value is not None:
            return STROKE_MAPPING.get(field.value, "Unknown")
    return "Unknown"

def infer_missing_stroke(length_idx: int) -> str:
    if 1 <= length_idx <= 4: return "Freestyle"
    elif 5 <= length_idx <= 9: return "Breaststroke"
    elif 10 <= length_idx <= 13: return "Backstroke"
    return "Freestyle"

def clean_fit_file(fitfile, filename: str) -> Activity:
    activity_data = Activity()
    activity_data.garmin_id = extract_activity_id(filename)
    activity_data.laps = []
    activity_data.time_series = []

    # 1. PROFILE & CONFIGURATION PRE-SCAN
    is_swim = False
    detected_pool_length = None

    for session in fitfile.get_messages("session"):
        sport_val = session.get_value("sport")
        if sport_val:
            activity_data.type = str(sport_val).capitalize()
            is_swim = "swim" in activity_data.type.lower()
        
        pool_len_val = session.get_value("pool_length")
        if pool_len_val:
            detected_pool_length = round(pool_len_val)

    if is_swim:
        activity_data.detected_pool_length = detected_pool_length or 25

    for session in fitfile.get_messages("session"):
        activity_data.start_time = session.get_value("start_time")
        activity_data.duration_seconds = session.get_value("total_elapsed_time")
        activity_data.moving_time_seconds = session.get_value("total_timer_time") or activity_data.duration_seconds
        activity_data.distance_metres = session.get_value("total_distance") or 0.0
        activity_data.average_heart_rate = session.get_value("avg_heart_rate")
        activity_data.max_heart_rate = session.get_value("max_heart_rate")
        activity_data.total_elevation_gain = session.get_value("total_ascent")
        activity_data.training_effect = session.get_value("total_training_effect")
        activity_data.calories = session.get_value("total_calories")

        # Set raw base pace (seconds per meter) to allow flexible downstream formatting
        if activity_data.distance_metres > 0 and activity_data.moving_time_seconds:
            activity_data.raw_pace_seconds_per_meter = activity_data.moving_time_seconds / activity_data.distance_metres
            
    processed_lengths = []
    if is_swim:
        length_idx = 0
        for length in fitfile.get_messages("length"):
            l_time = length.get_value("total_timer_time") or length.get_value("total_elapsed_time")
            if not l_time or l_time == 0:
                continue

            length_idx += 1
            l_dist = length.get_value("total_distance") or activity_data.detected_pool_length
            
            stroke_str = extract_stroke_safely(length)
            if stroke_str == "Unknown":
                stroke_str = infer_missing_stroke(length_idx)

            swolf_val = length.get_value("swolf") or length.get_value("avg_swolf")
            if not swolf_val:
                strokes = length.get_value("total_strokes") or length.get_value("num_strokes")
                swolf_val = int(round(l_time + strokes)) if strokes else None

            processed_lengths.append({
                "stroke": stroke_str,
                "swolf": swolf_val
            })

    lap_number = 1
    lengths_distributed = 0

    for lap in fitfile.get_messages("lap"):
        lap_dist = lap.get_value("total_distance") or 0.0
        lap_time = lap.get_value("total_timer_time") or lap.get_value("total_elapsed_time") or 0.0
        avg_speed = lap.get_value("avg_speed") or (lap_dist / lap_time if lap_time > 0 else 0.0)

        lap_domain = Lap(
            lap=lap_number,
            distance_metres=lap_dist,
            duration_seconds=lap_time,
            average_heart_rate=lap.get_value("avg_heart_rate"),
            average_speed=avg_speed,
            average_cadence=lap.get_value("avg_cadence")
        )

        if is_swim and activity_data.detected_pool_length > 0:
            num_lengths_in_lap = int(lap_dist / activity_data.detected_pool_length)
            lap_lengths = processed_lengths[lengths_distributed : lengths_distributed + num_lengths_in_lap]
            lengths_distributed += num_lengths_in_lap

            if lap_lengths:
                valid_strokes = [l["stroke"] for l in lap_lengths if l["stroke"] != "Unknown"]
                lap_domain.primary_stroke = max(set(valid_strokes), key=valid_strokes.count) if valid_strokes else "Mixed"
                
                valid_swolfs = [l["swolf"] for l in lap_lengths if isinstance(l["swolf"], (int, float))]
                lap_domain.average_swolf = int(round(sum(valid_swolfs) / len(valid_swolfs))) if valid_swolfs else None
            else:
                lap_domain.primary_stroke = extract_stroke_safely(lap)
                lap_domain.average_swolf = lap.get_value("avg_swolf")
        else:
            lap_domain.primary_stroke = None
            lap_domain.average_swolf = None

        activity_data.laps.append(lap_domain)
        lap_number += 1

    for rec in fitfile.get_messages("record"):
        timestamp = rec.get_value("timestamp")
        if not timestamp:
            continue

        activity_data.time_series.append({
            "timestamp": timestamp.isoformat(),
            "distance_meters": rec.get_value("distance"),
            "heart_rate": rec.get_value("heart_rate"),
            "cadence": rec.get_value("cadence") if not is_swim else None
        })

    return activity_data