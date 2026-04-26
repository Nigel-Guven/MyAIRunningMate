from fastapi import APIRouter, UploadFile, File, HTTPException
import tempfile
import shutil

from services.activity_cleaner_service import process_fit_file
from schemas.activity_schema import ActivitySchema

router = APIRouter()


@router.post("/fit_file/upload", response_model=ActivitySchema)
async def upload_fit(file: UploadFile = File(...)):
    if not file.filename.endswith(".fit"):
        raise HTTPException(status_code=400, detail="Invalid file type")

    activity = process_fit_file(file)

    return ActivitySchema.model_validate(activity, from_attributes=True)