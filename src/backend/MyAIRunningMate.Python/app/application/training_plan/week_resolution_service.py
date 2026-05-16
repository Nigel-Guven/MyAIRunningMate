from app.application.training_plan.gemini_service import _call_gemini_structured

def generate_week_plan(prompt_payload: dict) -> dict:
    prompt = f"""
You are an elite endurance and hybrid athletic coach. 
Generate exactly 7 sequential days of training for a microcycle training block.

CONSTRAINTS & RULES:
- Week Timeline: Week {prompt_payload['week']}
- Macrocycle Phase: {prompt_payload['phase']} (Focus: {prompt_payload['focus']})
- Start Date: {prompt_payload['start_date']}
- Intensity Strategy: {prompt_payload['constraints']['intensity']}
- Volumetric Multiplier: {prompt_payload['constraints']['volume_multiplier']}
- Allowed Workouts Pool: {prompt_payload['constraints']['allowed_workouts']}

EXECUTION RULES:
1. Generate exactly 1 event per day for 7 consecutive days starting on the Start Date.
2. Only use workout subtypes present in the Allowed Workouts Pool.
3. Scale the distance_metres logically based on the phase intensity and volume multiplier.
4. Set distance_metres strictly to 0 for rest days and strength sessions.
5. Keep descriptions brief, actionable, and under 50 characters.
"""

    return _call_gemini_structured(prompt)