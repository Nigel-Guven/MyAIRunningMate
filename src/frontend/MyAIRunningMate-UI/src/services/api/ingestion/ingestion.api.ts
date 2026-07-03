import type { IngestionViewResponse } from '../../../types/ingestion/ingestionViewResponse';
import { API_ENDPOINTS } from '../config/endpoints';
import { http } from '../config/http';

export const ingestionApi = {
  uploadFitFile: (
    file: File,
    onProgress?: (percent: number) => void
  ): Promise<IngestionViewResponse> => {

    const formData = new FormData();
    formData.append('file', file);

    return http.post<IngestionViewResponse>(
      API_ENDPOINTS.ingestion.upload,
      formData,
      {
        timeout: 300000,
        headers: {
          'Content-Type': 'multipart/form-data',
        },
        onUploadProgress: (event: any) => {
          if (!onProgress) return;

          const percent = Math.round(
            (event.loaded * 100) /
            (event.total || 1)
          );

          onProgress(percent);
        },
      }
    );
  },
};