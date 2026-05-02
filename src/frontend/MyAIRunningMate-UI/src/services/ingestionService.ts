import { apiClient } from './apiClient';
import type { IngestionViewResult } from '../types/ingestionView';

export const uploadFitFile = async (
  file: File,
  onProgress?: (percent: number) => void
): Promise<IngestionViewResult> => {
  const formData = new FormData();
  formData.append('file', file);

  const response = await apiClient.post<IngestionViewResult>('/fitfile/upload', formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
    onUploadProgress: (progressEvent) => {
      if (onProgress) {
        const percentCompleted = Math.round(
          (progressEvent.loaded * 100) / (progressEvent.total || 1)
        );
        onProgress(percentCompleted);
      }
    },
  });

  return response.data;
};