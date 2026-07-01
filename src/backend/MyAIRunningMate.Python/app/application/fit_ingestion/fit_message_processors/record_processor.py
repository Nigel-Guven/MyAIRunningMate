from app.domain.models.ingestion.time_series_record import TimeSeriesRecord

KEY = "record"

def semicircles_to_degrees(semicircles) -> float:
    if semicircles is None:
        return None
    return float(semicircles) * (180.0 / 2**31)

def process_record_messages(fitfile):
    
    record_list = []
    
    for entity in fitfile.get_messages(KEY):
        
        timestamp = entity.get_value("timestamp")
        if not timestamp:
            continue
        
        raw_lat = entity.get_value("position_lat")
        raw_long = entity.get_value("position_long")
        
        if not raw_lat:
            continue
        
        if not raw_long:
            continue
        
        lat_deg = semicircles_to_degrees(raw_lat)
        long_deg = semicircles_to_degrees(raw_long)
        
        record_data = TimeSeriesRecord(
            tsr_timestamp = timestamp,
            tsr_latitude = lat_deg,
            tsr_longitude = long_deg,
            tsr_heart_rate = entity.get_value("heart_rate"),
            tsr_cadence = entity.get_value("cadence"),
            tsr_distance_metres = entity.get_value("distance"),
            tsr_power = entity.get_value("power")
        )
        
        record_list.append(record_data)
        
    return record_list