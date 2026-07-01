from app.domain.models.ingestion.user_metrics import UserMetrics

KEY = 79

def calculate_vo2_max(raw_value: int | None) -> float | None:
    if raw_value is None or raw_value == 0:
        return None
        
    scaled_vo2 = (raw_value / 65536.0) * 3.5

    return round(scaled_vo2, 2)

def process_user_metrics_messages(fitfile):
    user_metrics = UserMetrics()
    
    for entity in fitfile.get_messages(KEY):
        raw_vo2 = entity.get_value(17)
        raw_speed = entity.get_value(13)
        
        user_metrics.user_volumetric_oxygen_max = calculate_vo2_max(raw_vo2)
        user_metrics.user_max_heart_rate = entity.get_value(6)
        user_metrics.user_lactate_threshold_heart_rate = entity.get_value(11)
        user_metrics.user_lactate_threshold_power = entity.get_value(12)
        user_metrics.user_lactate_threshold_speed = raw_speed / 10.0 if raw_speed else None 
        user_metrics.user_beginning_body_battery = entity.get_value(15)
        user_metrics.user_beginning_body_potential = entity.get_value(32)
        
    return user_metrics