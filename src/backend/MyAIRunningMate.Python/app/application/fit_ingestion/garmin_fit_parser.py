from pathlib import Path
from fitparse import FitFile # type: ignore[import-untyped]
from app.domain.models.ingestion.session import Session
from app.domain.models.ingestion.user_metrics import UserMetrics
from app.domain.models.ingestion.ingestion_aggregate import IngestionAggregate
import app.application.fit_ingestion.fit_message_processors.sport_processor as sport_proc
import app.application.fit_ingestion.fit_message_processors.activity_metrics_processor as activity_proc
import app.application.fit_ingestion.fit_message_processors.best_effort_processor as best_eff_proc
import app.application.fit_ingestion.fit_message_processors.lap_processor as lap_proc
import app.application.fit_ingestion.fit_message_processors.record_processor as record_proc
import app.application.fit_ingestion.fit_message_processors.session_processor as session_proc
import app.application.fit_ingestion.fit_message_processors.user_metrics_processor as user_proc

def format_file(file_obj) -> IngestionAggregate:
    fitfile = FitFile(file_obj.file)
    return clean_fit_file(fitfile, file_obj.filename)

def extract_activity_id(filename: str) -> str:
    name = Path(filename).stem
    if name.endswith("_ACTIVITY"):
        return name.replace("_ACTIVITY", "")
    return name

def clean_fit_file(fitfile, filename: str) -> IngestionAggregate:

    aggregateObject = IngestionAggregate()
    aggregateObject.garmin_id = extract_activity_id(filename)
    
    aggregateObject.sport = sport_proc.process_sport_messages(fitfile)
    aggregateObject.activity_metrics = activity_proc.process_activity_metrics_messages(fitfile)
    aggregateObject.user_metrics = user_proc.process_user_metrics_messages(fitfile)
    aggregateObject.session = session_proc.process_session_messages(fitfile)
    aggregateObject.best_efforts = best_eff_proc.process_best_effort_messages(fitfile)
    aggregateObject.laps = lap_proc.process_lap_messages(fitfile)
    aggregateObject.time_series = record_proc.process_record_messages(fitfile)
    
    aggregateObject.activity_metrics.activity_metrics_num_laps = len(aggregateObject.laps)
    

    return aggregateObject