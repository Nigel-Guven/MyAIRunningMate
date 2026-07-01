from app.domain.models.ingestion.lap import Lap

KEY = "lap"

def get_average_swolf(entity):
    swolf_val = entity.get_value("unknown_73")
    return swolf_val

def process_lap_messages(fitfile):
    lap_list = []
    
    lap_counter = 1
    
    for entity in fitfile.get_messages(KEY):
        
        lap_data = Lap(
            lap_number=lap_counter,
            lap_start_time=entity.get_value("start_time"),
            lap_duration_seconds=entity.get_value("total_timer_time") or entity.get_value("total_elapsed_time") or 0.0,
            lap_distance_metres=entity.get_value("total_distance"),
            lap_average_speed=entity.get_value("enhanced_avg_speed"),
            lap_average_heart_rate=entity.get_value("avg_heart_rate"),
            lap_max_heart_rate=entity.get_value("max_heart_rate"),
            lap_average_cadence=entity.get_value("avg_cadence"),
            lap_swim_stroke=entity.get_value("swim_stroke"),
            lap_num_lengths=entity.get_value("num_lengths")
        )

        lap_list.append(lap_data)
        lap_counter += 1
        
    return lap_list