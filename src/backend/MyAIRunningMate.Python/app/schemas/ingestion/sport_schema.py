from pydantic import BaseModel, ConfigDict
from typing import Optional

class SportSchema(BaseModel):
    sport_type: Optional[str] = None
    sport_sub_type: Optional[str] = None 
    sport_name: Optional[str] = None

    model_config = ConfigDict(from_attributes=True)