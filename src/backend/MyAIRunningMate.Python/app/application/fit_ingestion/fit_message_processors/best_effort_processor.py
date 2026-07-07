from app.domain.models.ingestion.best_effort import BestEffort

KEY = 113

def process_best_effort_messages(fitfile):
    
    best_effort_list = []
    
    for entity in fitfile.get_messages(KEY):
        
        raw_dist = entity.get_value(2)
        raw_time = entity.get_value(3)
        raw_pr   = entity.get_value(5)
        
        best_effort_data = BestEffort(
            best_effort_distance_metres = (raw_dist / 100.0) if raw_dist is not None else None,
            best_effort_time_seconds = (raw_time / 1000.0) if raw_time is not None else None,
            best_effort_is_personal_record = (raw_pr == 1)
        )
        
        if best_effort_data.best_effort_distance_metres is None:
            continue
        
        best_effort_list.append(best_effort_data)
        
    return best_effort_list