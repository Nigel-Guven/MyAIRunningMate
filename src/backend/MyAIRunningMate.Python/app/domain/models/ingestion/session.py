from typing import Optional
from pydantic import BaseModel

class Session(BaseModel):
    session_elapsed_time: Optional[float] = None
    session_moving_time: Optional[float] = None
    session_distance_metres: Optional[float] = None 
    session_total_cycles: Optional[int] = None
    session_total_calories: Optional[int] = None
    session_estimated_sweat_loss: Optional[int] = None
    session_average_temperature: Optional[int] = None
    session_max_temperature: Optional[int] = None
    session_average_heart_rate: Optional[int] = None
    session_max_heart_rate: Optional[int] = None
    session_average_power: Optional[int] = None
    session_max_power: Optional[int] = None
    session_average_cadence: Optional[int] = None
    session_max_cadence: Optional[int] = None
    session_average_vertical_oscillation: Optional[float] = None
    session_step_length: Optional[float] = None
    session_average_vertical_ratio: Optional[float] = None
    session_average_stance_time: Optional[float] = None
    session_aerobic_training_effect: Optional[float] = None
    session_anaerobic_training_effect: Optional[float] = None
    session_average_swolf: Optional[int] = None
    session_pool_length: Optional[int] = None