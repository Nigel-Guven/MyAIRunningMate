from pydantic import BaseModel, ConfigDict
from typing import Optional

class SessionSchema(BaseModel):
    session_elapsed_time: Optional[float] = None
    session_moving_time: Optional[float] = None
    session_distance_metres: Optional[float] = None 
    session_total_cycles: Optional[float] = None
    session_total_calories: Optional[float] = None
    session_estimated_sweat_loss: Optional[float] = None
    session_average_temperature: Optional[float] = None
    session_max_temperature: Optional[float] = None
    session_average_heart_rate: Optional[int] = None
    session_max_heart_rate: Optional[int] = None
    session_average_power: Optional[float] = None
    session_max_power: Optional[float] = None
    session_average_cadence: Optional[float] = None
    session_max_cadence: Optional[float] = None
    session_average_vertical_oscillation: Optional[float] = None
    session_step_length: Optional[float] = None
    session_average_vertical_ratio: Optional[float] = None
    session_average_stance_time: Optional[float] = None
    session_aerobic_training_effect: Optional[float] = None
    session_anaerobic_training_effect: Optional[float] = None
    session_average_swolf: Optional[float] = None
    session_pool_length: Optional[int] = None

    model_config = ConfigDict(from_attributes=True)