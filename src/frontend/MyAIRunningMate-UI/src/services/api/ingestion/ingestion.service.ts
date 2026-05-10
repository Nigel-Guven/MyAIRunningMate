import type { IngestionViewResult } from "../types/ingestionView";
import { apiClient } from "./apiClient";

export const uploadFitFile = async (
  file: File,
  onProgress?: (percent: number) => void
): Promise<IngestionViewResult> => {
  const formData = new FormData();
  formData.append('file', file);

  const response = await apiClient.post<IngestionViewResult>('/fitfile/upload', formData, {
    // 5 minute timeout to allow for C# / Python debugging
    timeout: 300000, 
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