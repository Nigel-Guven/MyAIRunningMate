from app.domain.models.ingestion.sport import Sport

KEY = "sport"

def process_sport_messages(fitfile):
    
    sport_data = Sport()
    
    for entity in fitfile.get_messages(KEY):
        sport_data.sport_type = entity.get_value("sport")
        sport_data.sport_sub_type = entity.get_value("sub_sport")
        sport_data.sport_name = entity.get_value("name")

    return sport_data