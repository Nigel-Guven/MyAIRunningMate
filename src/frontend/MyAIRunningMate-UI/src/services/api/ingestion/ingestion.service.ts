import { ingestionApi } from './ingestion.api';
import type { IngestionViewDto } from '../../../types/views/ingestionView';

export const ingestionService = {
  uploadFitFile: (
    file: File,
    onProgress?: (p: number) => void
  ): Promise<IngestionViewDto> => {
    return ingestionApi.uploadFitFile(file, onProgress);
  },
};

export type UploadProgressCallback = (percent: number) => void;