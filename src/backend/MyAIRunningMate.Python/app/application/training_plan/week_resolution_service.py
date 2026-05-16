from app.application.training_plan.gemini_service import _call_gemini

def generate_week_plan(prompt_payload: dict) -> dict:

    prompt = f"""
You are an elite running coach.

Generate EXACTLY 7 days of training.

RULES:
- Allowed workouts: {prompt_payload['constraints']['allowed_workouts']}
- Intensity: {prompt_payload['constraints']['intensity']}
- Volume multiplier: {prompt_payload['constraints']['volume_multiplier']}
- Week: {prompt_payload['week']}
- Phase: {prompt_payload['phase']}
- Start date: {prompt_payload['start_date']}

CRITICAL RULES:
- Must include exactly 7 events (1 per day)
- No missing days
- No extra events
- Only use allowed_workouts
- Do NOT include race/event (system injects it)

STRICT JSON:
{{
  "events": [
    {{
      "event_date": "YYYY-MM-DD",
      "exercise_type": "running|swimming|strength|rest",
      "exercise_subtype": "string",
      "description": "max 50 chars",
      "distance_metres": 0
    }}
  ]
}}
"""

    return _call_gemini(prompt)