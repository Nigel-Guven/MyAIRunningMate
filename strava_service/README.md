# Strava Data Collection Service

A **FastAPI-based service** that connects to Strava, retrieves athlete activities, and manages OAuth tokens. The service handles token refresh automatically and exposes endpoints to fetch the latest activities.

---

## Features

* **OAuth2 authorization** with Strava
* **Automatic access token validation and refresh**
* **Fetches latest Strava activities**
* Organized in modular FastAPI routes and services
* Ready for Dockerization

---

## Requirements

* Python 3.10+
* [pip](https://pip.pypa.io/en/stable/installation/)
* [Docker](https://www.docker.com/products/docker-desktop) (optional, for containerized deployment)

---

## Installation

1.  **Clone the repository**

    ```bash
    git clone <your-repo-url>
    cd strava_service
    ```

2.  **Set up a virtual environment**

    ```bash
    python -m venv venv
    # Linux/macOS
    source venv/bin/activate
    # Windows
    venv\Scripts\activate
    ```

3.  **Install dependencies**

    ```bash
    pip install -r requirements.txt
    ```

4.  **Create a `.env` file** with your Strava credentials:

    ```env
    STRAVA_CLIENT_ID=<your_client_id>
    STRAVA_CLIENT_SECRET=<your_client_secret>
    STRAVA_ACCESS_TOKEN=<optional_initial_access_token>
    STRAVA_REFRESH_TOKEN=<optional_initial_refresh_token>
    ```

---

## Running the Service

Start the FastAPI server:

```bash
uvicorn main:app --reload

The service will be available at http://localhost:8000.

Endpoints
GET /strava/ → Health check

GET /strava/authorize → Redirects to Strava OAuth page

GET /strava/exchange_token?code=<code> → Exchanges OAuth code for tokens

GET /strava/activities?limit=5 → Fetches latest activities (default 5)

Dockerization
Build the Docker image

Bash

docker build -f .docker/Dockerfile.Strava -t strava_service .
Run the container

Bash

docker run -p 8000:8000 --env-file .env strava_service
The service will be available at http://localhost:8000.

Notes
Tokens are automatically stored in the .env file after authorization.

The service validates and refreshes the access token on startup.

Ensure your Strava app has the read,activity:read_all scope enabled.