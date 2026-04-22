from dataclasses import asdict
from logging import config

import requests


def sync_activity(activity):
    try:
        payload = asdict(activity)
        if payload['date']:
            payload['date'] = payload['date'].isoformat()
            
        response = requests.post(f"{config.DB_SERVICE_URL}/activities/", json=payload)
        response.raise_for_status()
        print(f"Successfully synced activity {activity.id}")
    except Exception as e:
        print(f"Failed to sync {activity.id}: {e}")
        