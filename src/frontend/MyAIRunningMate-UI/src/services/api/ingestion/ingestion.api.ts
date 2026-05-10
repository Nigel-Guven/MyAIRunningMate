import { apiClient } from '../config/client';

import type { IngestionViewDto } from '../../../types/views/ingestionView';

import { API_ENDPOINTS } from '../config/endpoints';
import { http } from '../config/http';

export const ingestionApi = {
  uploadFitFile: (
    file: File,
    onProgress?: (percent: number) => void
  ): Promise<IngestionViewDto> => {

    const formData = new FormData();
    formData.append('file', file);

    return http.postWithConfig<IngestionViewDto>(
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