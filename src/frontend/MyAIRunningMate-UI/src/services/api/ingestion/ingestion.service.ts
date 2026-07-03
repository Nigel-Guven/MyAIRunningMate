import type { IngestionViewResponse } from '../../../types/ingestion/ingestionViewResponse';
import { ingestionApi } from './ingestion.api';

export const ingestionService = {
  uploadFitFile: (
    file: File,
    onProgress?: (p: number) => void
  ): Promise<IngestionViewResponse> => {
    return ingestionApi.uploadFitFile(file, onProgress);
  },
};

export type UploadProgressCallback = (percent: number) => void;