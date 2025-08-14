import os
import requests

def get_gemini_insights(activity_data):
    api_key = os.getenv("GEMINI_API_KEY")
    if not api_key:
        raise ValueError("Gemini API key is not set")

    url = "https://gemini.googleapis.com/v1beta2/models/gemini-2.5-chat/completions"
    headers = {
        "Authorization": f"Bearer {api_key}",
        "Content-Type": "application/json",
    }
    data = {
        "messages": [{"role": "user", "content": activity_data}],
        "temperature": 0.7,
    }
    response = requests.post(url, headers=headers, json=data)
    response.raise_for_status()
    return response.json()["choices"][0]["message"]["content"]
