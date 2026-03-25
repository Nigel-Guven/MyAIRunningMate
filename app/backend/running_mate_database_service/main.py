from fastapi import FastAPI
from database.base import Base
from database.session import engine
from api import activities

app = FastAPI(title="Running Mate Database Service")

app.include_router(activities.router)