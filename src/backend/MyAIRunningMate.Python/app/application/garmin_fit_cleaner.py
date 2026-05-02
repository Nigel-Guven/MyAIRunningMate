from pathlib import Path
from fitparse import FitFile
from domain.models.lap import Lap
from domain.models.activity import Activity

def format_file(file_obj) -> Activity:
    fitfile = FitFile(file_obj.file)
    return clean_fit_file(fitfile, file_obj.filename)

def extract_activity_id(filename: str) -> str:
    name = Path(filename).stem

    if name.endswith("_ACTIVITY"):
        return name.replace("_ACTIVITY", "")

    return name

def clean_fit_file(fitfile, filename: str) -> Activity:
    activity_data = Activity()
    activity_data.garmin_id = extract_activity_id(filename)

    for record in fitfile.get_messages("session"):
        activity_data.start_time = record.get_value("start_time")
        activity_data.type = record.get_value("sport")
        activity_data.duration_seconds = record.get_value("total_elapsed_time")
        activity_data.distance_metres = record.get_value("total_distance")
        activity_data.average_heart_rate = record.get_value("avg_heart_rate")
        activity_data.max_heart_rate = record.get_value("max_heart_rate")
        activity_data.total_elevation_gain = record.get_value("total_ascent")
        activity_data.training_effect = record.get_value("total_training_effect")

        if activity_data.distance_metres and activity_data.duration_seconds and activity_data.distance_metres > 0:
            distance_km = activity_data.distance_metres / 1000
            activity_data.average_pace_seconds_per_kilometre = (
                activity_data.duration_seconds / distance_km
            )

    lap_number = 1
    for lap in fitfile.get_messages("lap"):
        activity_data.laps.append(
            Lap(
                lap=lap_number,
                distance_metres=lap.get_value("total_distance"),
                duration_seconds=lap.get_value("total_elapsed_time"),
                average_heart_rate=lap.get_value("avg_heart_rate")
            )
        )
        lap_number += 1

    return activity_data