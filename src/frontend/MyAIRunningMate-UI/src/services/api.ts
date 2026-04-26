// src/services/api.ts
import axios from 'axios';
import type { ActivityResult } from '../types/activity';

const API_BASE_URL = "http://localhost:7001/api"; // Adjust to your actual C# port

export const uploadFitFile = async (
  file: File, 
  onProgress: (percent: number) => void
): Promise<ActivityResult> => {
  const formData = new FormData();
  formData.append('file', file);

  const response = await axios.post<ActivityResult>(`${API_BASE_URL}/fitfile/upload`, formData, {
    onUploadProgress: (progressEvent) => {
      const percentCompleted = Math.round((progressEvent.loaded * 100) / (progressEvent.total || 1));
      onProgress(percentCompleted);
    },
  });

  return response.data;
};

export const getMonthlyActivities = async (month: number, year: number): Promise<AggregateArtifactDto[]> => {
  const response = await axios.get<AggregateArtifactDto[]>(`${API_BASE_URL}/activity/monthly`, {
    params: { month, year }
  });
  return response.data;
};