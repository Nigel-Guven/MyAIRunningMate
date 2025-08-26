from firebase_admin import firestore

async def save_activity(db: firestore.Client, activity_data: dict):
    """
    Saves an activity to the 'activities' collection in Firestore.
    
    Args:
        db: The Firestore client instance.
        activity_data: A dictionary containing the activity data.
    """
    activity_id = str(activity_data.get("id"))
    
    # Get a reference to the 'activities' collection
    activities_ref = db.collection("activities")
    
    # Create a document reference with the activity ID
    doc_ref = activities_ref.document(activity_id)
    
    # Check if the document already exists to avoid duplicates
    doc = await doc_ref.get()
    if doc.exists:
        print(f"Activity {activity_id} already exists, skipping.")
        return
        
    # Prepare the data to be saved. Flatten nested data as Firestore doesn't
    # support nested relationships like SQLAlchemy does.
    data_to_save = {
        "id": activity_data.get("id"),
        "name": activity_data.get("name"),
        "distance": activity_data.get("distance"),
        "moving_time": activity_data.get("moving_time"),
        "elapsed_time": activity_data.get("elapsed_time"),
        "elevation_gain": activity_data.get("total_elevation_gain"),
        "type": activity_data.get("type"),
        "start_date": activity_data.get("start_date"),
        "start_date_local": activity_data.get("start_date_local"),
        "average_speed": activity_data.get("average_speed"),
        "max_speed": activity_data.get("max_speed"),
        "average_cadence": activity_data.get("average_cadence"),
        "average_watts": activity_data.get("average_watts"),
        "kilojoules": activity_data.get("kilojoules"),
    }
    
    # Save the data to the document. Firestore's set() method handles creation or update.
    await doc_ref.set(data_to_save)
    print(f"Document for activity {activity_id} created successfully.")