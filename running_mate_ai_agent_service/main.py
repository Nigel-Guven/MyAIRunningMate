from fastapi import FastAPI
from routes import router as ai_router

app = FastAPI(title="AI Agent Service")
app.include_router(ai_router, prefix="/ai")
