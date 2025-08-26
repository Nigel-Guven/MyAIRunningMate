import firebase_admin
from firebase_admin import firestore
from firebase_admin import credentials
import os

# Your Firebase Admin SDK credentials file path
CREDENTIALS_FILE = os.getenv("GOOGLE_APPLICATION_CREDENTIALS")

# Initialize the Firebase Admin SDK with the service account credentials
if not firebase_admin._apps:
    try:
        cred = credentials.Certificate(CREDENTIALS_FILE)
        firebase_admin.initialize_app(cred)
        print("Firebase Admin SDK initialized successfully.")
    except Exception as e:
        print(f"Error initializing Firebase Admin SDK: {e}")
        # Consider a more robust error handling mechanism in production

def get_db():
    """
    Returns a Firestore client instance.
    """
    return firestore.client()