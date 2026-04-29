import axios from 'axios';
import type { IngestionViewResult } from '../types/ingestionView';
import type { CalendarViewResult } from '../types/calendarView';

const API_BASE_URL = "http://localhost:7001/api"; // Adjust to your actual C# port

export const uploadFitFile = async (
  file: File, 
  onProgress: (percent: number) => void
): Promise<IngestionViewResult> => {
  const formData = new FormData();
  formData.append('file', file);

  const response = await axios.post<IngestionViewResult>(`${API_BASE_URL}/fitfile/upload`, formData, {
    onUploadProgress: (progressEvent) => {
      const percentCompleted = Math.round((progressEvent.loaded * 100) / (progressEvent.total || 1));
      onProgress(percentCompleted);
    },
  });

  return response.data;
};

export const getMonthlyActivities = async (month: number, year: number): Promise<CalendarViewResult[]> => {
  const response = await axios.get<CalendarViewResult[]>(`${API_BASE_URL}/calendar/monthly`, {
    params: { month, year }
  });
  return response.data;
};