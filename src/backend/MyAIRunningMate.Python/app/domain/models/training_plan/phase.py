from dataclasses import dataclass
from .phase_constraints import PhaseConstraints
from .enums.phase_enums import PhaseName
from .enums.phase_enums import PhaseFocus


@dataclass
class Phase:
    name: PhaseName
    start_week: int
    end_week: int
    focus: PhaseFocus
    constraints: PhaseConstraints


def get_phase(structure, week: int):
    for phase in structure.phases:
        if phase.start_week <= week <= phase.end_week:
            return phase
    raise ValueError("Invalid week")


