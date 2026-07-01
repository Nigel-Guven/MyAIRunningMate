from typing import Optional
from pydantic import BaseModel

class Sport(BaseModel):
    sport_type: Optional[str] = None
    sport_sub_type: Optional[str] = None 
    sport_name: Optional[str] = None