from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from typing import List
from database.session import SessionLocal
from database import models
from schemas.activity import Activity, ActivityCreate

router = APIRouter(prefix="/activities", tags=["activities"])

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@router.get("/", response_model=List[Activity])
def list_activities(db: Session = Depends(get_db)):
    # joinedload can be used here to optimize the query and avoid N+1 issues
    return db.query(models.Activity).all()

@router.post("/", response_model=Activity)
def create_activity(activity: ActivityCreate, db: Session = Depends(get_db)):
    db_activity = models.Activity(**activity.model_dump(exclude={'laps'}))
    for lap_data in activity.laps:
        db_lap = models.Lap(**lap_data.model_dump(), activity=db_activity)
        db.add(db_lap)
    
    db.add(db_activity)
    db.commit()
    db.refresh(db_activity)
    return db_activity