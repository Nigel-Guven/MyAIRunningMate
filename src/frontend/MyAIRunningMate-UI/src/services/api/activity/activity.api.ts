import { http } from '../config/http';

import { API_ENDPOINTS } from '../config/endpoints';
import type { AggregateArtifactResponse } from '../../../types/aggregates/aggregateArtifactResponse';

export const activityApi = {
  getDetail: (
    activityId: string
  ) =>
    http.get<AggregateArtifactResponse>(
      API_ENDPOINTS.activityView.activityAggregate,
      {
        params: {
          activityId,
        },
      }
    ),
};