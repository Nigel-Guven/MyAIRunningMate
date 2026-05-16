import json
import os
import re

import google.generativeai as genai
from dotenv import load_dotenv


load_dotenv()

genai.configure(
    api_key=os.getenv("GEMINI_API_KEY"),
    transport="rest"
)

MODEL_ID = "gemini-2.5-flash"

model = genai.GenerativeModel(MODEL_ID)


def _clean_json_response(text: str) -> str:

    text = text.strip()

    if text.startswith("```"):
        text = re.sub(r"^```(?:json)?\n?", "", text)
        text = re.sub(r"\n?```$", "", text)

    # remove trailing commas
    text = re.sub(r",\s*([}\]])", r"\1", text)

    return text.strip()


def _call_gemini(prompt: str) -> dict:

    response = model.generate_content(
        prompt,
        generation_config={
            "temperature": 0.1,
            "max_output_tokens": 4500,
            "response_mime_type": "application/json"
        }
    )

    cleaned = _clean_json_response(response.text)

    try:
        return json.loads(cleaned)

    except json.JSONDecodeError as e:

        print("=== INVALID GEMINI JSON ===")
        print(cleaned)

        raise ValueError(
            f"Gemini returned invalid JSON: {str(e)}"
        )