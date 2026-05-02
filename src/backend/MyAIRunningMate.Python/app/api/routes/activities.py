from fastapi import APIRouter, UploadFile, File, HTTPException
from services.activity_cleaner_service import process_fit_file
from schemas.activity_schema import ActivitySchema

router = APIRouter(prefix="", tags=["activities"])


@router.post("/fit_file/upload", response_model=ActivitySchema)
async def upload_fit(file: UploadFile = File(...)):
    if not file.filename or not file.filename.endswith(".fit"):
        raise HTTPException(status_code=400, detail="Invalid file type. Only .fit files are supported.")

    activity = process_fit_file(file)

    return ActivitySchema.model_validate(activity)