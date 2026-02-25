from fitparse import FitFile
from datetime import datetime

from domain.lap import Lap
from domain.activity import Activity

def format_file(file):
    fitfile = FitFile(str(file))
    return clean_fit_file(fitfile, file.name)

def clean_fit_file(fitfile, filename) -> dict:
    
    activity_data = Activity()
    activity_data.id = filename.split(".")[0]
    # ---- Extract Session Summary ----
    for record in fitfile.get_messages("session"):
        activity_data.date = record.get_value("start_time")
        activity_data.type = record.get_value("sport")
        activity_data.duration_seconds = record.get_value("total_elapsed_time")
        activity_data.distance_metres = record.get_value("total_distance")
        activity_data.average_heart_rate = record.get_value("avg_heart_rate")
        activity_data.max_heart_rate = record.get_value("max_heart_rate")
        activity_data.total_elevation_gain = record.get_value("total_ascent")
        activity_data.training_effect = record.get_value("total_training_effect")
        activity_data.v02_max = record.get_value("enhanced_avg_respiration_rate")

        # Calculate pace if distance exists
        if activity_data.distance_metres and activity_data.duration_seconds:
            distance_km = activity_data.distance_metres / 1000
            activity_data.average_pace_seconds_per_kilometre = (
                activity_data.duration_seconds / distance_km
            )

    # ---- Extract Laps ----
    lap_number = 1
    for lap in fitfile.get_messages("lap"):
        activity_data.laps.append(Lap(
            lap=lap_number,
            distance_metres=lap.get_value("total_distance"),
            duration_seconds=lap.get_value("total_elapsed_time"),
            average_heart_rate=lap.get_value("avg_heart_rate")
        ))
        lap_number += 1

    return activity_data