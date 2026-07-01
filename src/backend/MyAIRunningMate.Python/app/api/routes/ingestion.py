from fastapi import APIRouter, UploadFile, File, HTTPException
from app.application.fit_ingestion.activity_processor import process_fit_file
from app.schemas.ingestion.ingestion_aggregate_schema import IngestionAggregateSchema

router = APIRouter(prefix="/ingestion", tags=["ingestion"])


@router.post("/process", response_model=IngestionAggregateSchema)
async def upload_fit(file: UploadFile = File(..., alias="file")):
    if not file.filename or not file.filename.endswith(".fit"):
        raise HTTPException(status_code=400, detail="Invalid file type. Only .fit files are supported.")
    print(file.filename)
    activity = process_fit_file(file)

    return IngestionAggregateSchema.model_validate(activity)