from application.garmin_fit_cleaner import format_file
from domain.models.activity import Activity

def process_fit_file(file) -> Activity:
    return format_file(file)