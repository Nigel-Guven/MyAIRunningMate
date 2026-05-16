from app.application.fit_ingestion.garmin_fit_parser import format_file
from app.domain.models.activity import Activity

def process_fit_file(file) -> Activity:
    return format_file(file)