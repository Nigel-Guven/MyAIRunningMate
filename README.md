<p align="center">
  <img src="documents/MyAIRunningMateLogo.png" alt="MyAIRunningMate Logo" width="400"/>
</p>

# 🏃‍♂️🏊 Strava Data Analysis – Python/React Application

> **AI-powered running & swimming analysis**

---

## 📜 Overview
This project is a **Python/React-based service** designed to collect, process, and analyze exercise data from **Strava**, providing users with **personalized AI feedback** and **interactive visualizations**.  

---

## 🏗 High-Level Architecture
![Architecture Diagram](documents/HighLevelArchitectureDiagram.png) <!-- Optional: Add diagram image -->

**Key Components:**
- **Data Sources** - Strava, Google Sheets
- **Frontend UI Service** – React UI for data visualization & AI feedback
- **API Gateway** – FastAPI routes requests, rate limiting, handles authentication, API versioning
- **Strava Ingestion Service** – Fetches raw workout data from Strava API
- **Analytics Service** – Processes and stores workout metrics
- **AI Insight Service** – Aggregated Metrics, Trends, Conversational, Insights, Recommendations
- **Data Storage** – Postgres, monitoring tools
---

Extra:
Strava Async
Google Sheets Async
Analytics Recomputation
AI Insight Generation
Celery, RQ 
Cron
Frontend - 
  Dashboard - Mileage, Training load, Pace Trends
  Calendar - Planned vs Actual, Racing Countdown
  AI Coach - Chat UI, Predefined prompts, weekly insights feed
  Settings - Strava Connection, Sheet selection, Preferences
Authentication/Security - JWT Auth, OAuth for Strava, Secret in env vars, Token refresh handling
Key Architectural Principle -> System computes facts deterministically, Uses AI for interpretation and explanation - cheaper reliable impressive


## 🔍 Service Breakdown

### 1. **Frontend UI Service**
- **Tech**: PyQt5 / PySide6 (native) or Electron (web-based)
- **Purpose**: Display workout stats, receive AI feedback
- **Communication**: HTTP / gRPC

### 2. **API Gateway**
- **Tech**: FastAPI / NGINX
- **Purpose**: Routing, authentication, API versioning

### 3. **Strava Data Collection Service**
- **Tech**: Python + Strava API
- **Purpose**: Authenticate & fetch workout logs
- **Output**: Publishes events to **Kafka**

### 4. **Data Pipeline & Analytics Service**
- **Tech**: Pandas, NumPy, TimescaleDB/PostgreSQL, Scikit-learn (optional)
- **Purpose**: Process raw data, calculate metrics, predictive analytics

### 5. **AI Agent Service**
- **Tech**: Python + OpenAI API (or similar)
- **Purpose**: Provide personalized AI-powered workout feedback

### 6. **Data Visualization Service**
- **Tech**: Matplotlib, Plotly, Bokeh
- **Purpose**: Serve visual charts/dashboards via API

---

## 🛠 Infrastructure & Tooling
| Tool / Service      | Purpose |
|---------------------|---------|
| **Docker**          | Containerize microservices |
| **Kubernetes**      | Orchestrate and scale services |
| **Helm**            | Manage Kubernetes configurations |
| **Terraform**       | Provision cloud infrastructure |
| **Kafka**           | Event streaming backbone |
| **PostgreSQL + TimescaleDB** | Store time-series metrics |
| **Prometheus + Grafana** | Monitoring and dashboards |
| **MinIO**           | Store large files (GPX/FIT) |
| **Airflow** *(optional)* | ETL scheduling |
| **gRPC** *(optional)* | High-performance service-to-service communication |

---

## 🚀 Getting Started

### 1️⃣ Clone Repository
```bash
git clone https://github.com/your-username/strava-analytics.git
cd strava-analytics
