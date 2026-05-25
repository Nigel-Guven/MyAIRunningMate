import os

from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from app.api.routes import ingestion
from app.api.routes import training_plan

app = FastAPI(
    title="My AI Running Mate Python API",
    version="1.0.0"
)

default_origins = "http://localhost:5173,http://localhost:7002,http://127.0.0.1:5173"
origins = [
    origin.strip()
    for origin in os.getenv("CORS_ORIGINS", default_origins).split(",")
    if origin.strip()
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

app.include_router(ingestion.router, prefix="/api")
app.include_router(training_plan.router, prefix="/api")
