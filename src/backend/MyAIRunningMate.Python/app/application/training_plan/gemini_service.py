import os
from google import genai
from google.genai import types
from pydantic import BaseModel, Field
from typing import List
from dotenv import load_dotenv

load_dotenv()

client = genai.Client(api_key=os.getenv("GEMINI_API_KEY"))
MODEL_ID = "gemini-2.5-flash"

# Define the precise schema you expect Gemini to return for ONE event
class GeminiDailyEvent(BaseModel):
    event_date: str = Field(description="The ISO date string formatted as YYYY-MM-DD")
    exercise_type: str = Field(description="Must be exactly one of: running, swimming, strength, rest")
    exercise_subtype: str = Field(description="The specific workout type (e.g., Easy Run, Critical Swim laps, Lower Body Weight Lifting)")
    description: str = Field(description="Specific instructions for execution. Max 50 characters.")
    distance_metres: int = Field(description="Target distance for the day. Set to 0 if rest day or strength session.")

# The container model for the 7-day list
class GeminiWeeklyPlanResponse(BaseModel):
    events: List[GeminiDailyEvent]

def _call_gemini_structured(prompt: str) -> dict:
    try:
        response = client.models.generate_content(
            model=MODEL_ID,
            contents=prompt,
            config=types.GenerateContentConfig(
                temperature=0.1,
                response_mime_type="application/json",
                # This compiler-level flag forces Gemini to adhere exactly to your schema
                response_schema=GeminiWeeklyPlanResponse,
            ),
        )
        
        # Load and validate the string cleanly into an object
        validated_data = GeminiWeeklyPlanResponse.model_validate_json(response.text)
        return validated_data.model_dump()
        
    except Exception as e:
        raise ValueError(f"Gemini generation or schema validation failed: {str(e)}")