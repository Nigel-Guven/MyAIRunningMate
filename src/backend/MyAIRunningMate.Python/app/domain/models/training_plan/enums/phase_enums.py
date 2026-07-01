
from enum import Enum


class PhaseName(Enum):
    BASE = "base"
    BUILD = "build"
    PEAK = "peak"
    TAPER = "taper"
    RECOVERY = "recovery"
    
class PhaseFocus(Enum):
    FOUNDATION = "aerobic foundation and injury prevention"
    THRESHOLD = "threshold and endurance"
    RACE_SPECIFIC = "race specific sharpening"
    TAPERING = "reduce fatigue for race day"
    RECOVERY = "full recovery and reset"