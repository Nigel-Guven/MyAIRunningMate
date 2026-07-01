from app.domain.models.ingestion.activity_metrics import ActivityMetrics

ACTIVITY_KEY = "activity"
ACTIVITY_METRICS_KEY = 140

def process_activity_metrics_messages(fitfile):
    
    activity_metrics_data = ActivityMetrics()
    
    for entity in fitfile.get_messages(ACTIVITY_KEY):
        activity_metrics_data.activity_metrics_start_time = entity.get_value("timestamp")
        
    for entity in fitfile.get_messages(ACTIVITY_METRICS_KEY):
        activity_metrics_data.activity_metrics_ending_body_battery = entity.get_value(25)
        activity_metrics_data.activity_metrics_ending_potential = entity.get_value(50)
        activity_metrics_data.activity_metrics_recovery_time = entity.get_value(9)
        activity_metrics_data.activity_metrics_total_ascent = entity.get_value(60)
        activity_metrics_data.activity_metrics_total_descent = entity.get_value(61)
        
    return activity_metrics_data