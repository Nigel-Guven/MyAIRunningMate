#!/bin/bash

ROOT="$(pwd)"

cleanup() {
    echo ""
    echo "Stopping services..."
    kill $FRONTEND_PID $PYTHON_PID $DOTNET_PID 2>/dev/null
}

trap cleanup EXIT INT TERM

echo "Starting Frontend..."
(
    cd "$ROOT/src/frontend/MyAIRunningMate-UI" || exit 1
    npm run dev
) &
FRONTEND_PID=$!

echo "Starting Python API..."
(
    cd "$ROOT/src/backend/MyAIRunningMate.Python" || exit 1
    source ./venv/Scripts/activate
    uvicorn app.main:app --reload
) &
PYTHON_PID=$!

echo "Starting C# API..."
(
    cd "$ROOT/src/backend/MyAIRunningMate" || exit 1
    dotnet run --project MyAIRunningMate.Service
) &
DOTNET_PID=$!

echo ""
echo "Services started:"
echo "Frontend PID: $FRONTEND_PID"
echo "Python PID:   $PYTHON_PID"
echo "C# API PID:   $DOTNET_PID"
echo ""
echo "Press Ctrl+C to stop all services."

wait