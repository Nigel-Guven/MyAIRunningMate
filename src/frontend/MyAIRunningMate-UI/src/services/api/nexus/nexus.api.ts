// src/api/nexus.api.ts



const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api';

export const nexusApi = {
  generatePlan: async (data: TrainingPlanRequest): Promise<TrainingPlanResponse> => {
    const response = await fetch(`${API_BASE_URL}/nexus/plan`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`API Error: ${response.status} ${response.statusText}`);
    }

    return response.json();
  },
};