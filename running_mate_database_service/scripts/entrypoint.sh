#!/bin/sh

# Export Postgres variables
export PGPASSWORD=$POSTGRES_PASSWORD

echo "Waiting for Postgres..."
retries=10
until psql -h "postgres_service" -U "$POSTGRES_USER" -d "$POSTGRES_DB" -c '\q'; do
  retries=$((retries-1))
  if [ $retries -le 0 ]; then
    echo "Postgres not ready after retries. Exiting."
    exit 1
  fi
  echo "Postgres not ready, retrying in 5 seconds..."
  sleep 5
done
echo "Postgres is ready!"

# Run Alembic migrations
echo "Running Alembic migrations..."
alembic upgrade head

# Start Kafka consumer
echo "Starting Kafka consumer..."
python main.py
