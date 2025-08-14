#!/bin/bash

# Exit on any error
set -e

echo "Starting Strava Service using docker-compose..."

# Navigate to the .docker directory (where this script is)
cd "$(dirname "$0")"

# Build and start container using docker-compose
docker compose up --build -d

echo "Strava Service is running at http://localhost:8000"
