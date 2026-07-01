from app.domain.models.ingestion.session import Session

KEY = "session"

def process_session_messages(fitfile):
    sessions_list = []
    
    for entity in fitfile.get_messages(KEY):
        session_data = Session(
            session_elapsed_time=entity.get_value("total_elapsed_time"),
            session_moving_time=entity.get_value("total_timer_time"),
            session_distance_metres=entity.get_value("total_distance"),
            session_total_cycles=entity.get_value("total_cycles"),
            session_total_calories=entity.get_value("total_calories"),
            session_estimated_sweat_loss=entity.get_value(178),
            session_average_temperature=entity.get_value("avg_temperature"),
            session_max_temperature=entity.get_value("max_temperature"),
            session_average_heart_rate=entity.get_value("avg_heart_rate"),
            session_max_heart_rate=entity.get_value("max_heart_rate"),
            session_average_power=entity.get_value("avg_power"),
            session_max_power=entity.get_value("max_power"),
            session_average_cadence=entity.get_value("avg_cadence"),
            session_max_cadence=entity.get_value("max_cadence"),   
            session_average_vertical_oscillation=entity.get_value("avg_vertical_oscillation"),
            session_step_length=entity.get_value("avg_step_length"),
            session_average_vertical_ratio=entity.get_value("avg_vertical_ratio"),
            session_average_stance_time=entity.get_value("avg_stance_time"),
            session_aerobic_training_effect=entity.get_value("total_training_effect"),
            session_anaerobic_training_effect=entity.get_value("total_anaerobic_training_effect"),
            session_average_swolf=entity.get_value(80),
            session_pool_length=entity.get_value("pool_length")
        )
        
        sessions_list.append(session_data)
        
    return sessions_list