from app.domain.models.ingestion.time_series_record import TimeSeriesRecord

KEY = "record"
# Pre-calculate the constant to avoid doing math on every iteration
SEMICIRCLE_TO_DEG = 180.0 / (2**31)

def process_record_messages(fitfile):
    record_list = []
    
    get_messages = fitfile.get_messages
    
    for entity in get_messages(KEY):
        get_val = entity.get_value
        
        timestamp = get_val("timestamp")
        if not timestamp:
            continue
            
        raw_lat = get_val("position_lat")
        if not raw_lat:
            continue
            
        raw_long = get_val("position_long")
        if not raw_long:
            continue
        
        lat_deg = float(raw_lat) * SEMICIRCLE_TO_DEG
        long_deg = float(raw_long) * SEMICIRCLE_TO_DEG
        
        record_list.append(
            TimeSeriesRecord(
                tsr_timestamp=timestamp,
                tsr_latitude=lat_deg,
                tsr_longitude=long_deg,
                tsr_heart_rate=get_val("heart_rate"),
                tsr_cadence=get_val("cadence"),
                tsr_distance_metres=get_val("distance"),
                tsr_power=get_val("power")
            )
        )
        
    return record_list