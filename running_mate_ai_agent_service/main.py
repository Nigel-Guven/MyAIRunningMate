from fastapi import FastAPI
from routes import router as ai_router

app = FastAPI(title="AI Agent Service")

@app.get("/")
def root():
    return {"message": "AI Agent Service running"}

app.include_router(ai_router, prefix="/ai")
