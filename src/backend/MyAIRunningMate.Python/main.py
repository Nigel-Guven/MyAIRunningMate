from fastapi import FastAPI
from api.routes import activities

app = FastAPI()

app.include_router(activities.router, prefix="/api")