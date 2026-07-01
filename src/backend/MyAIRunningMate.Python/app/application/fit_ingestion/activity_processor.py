from app.application.fit_ingestion.garmin_fit_parser import format_file
from app.domain.models.ingestion.ingestion_aggregate import IngestionAggregate

def process_fit_file(file) -> IngestionAggregate:
    return format_file(file)