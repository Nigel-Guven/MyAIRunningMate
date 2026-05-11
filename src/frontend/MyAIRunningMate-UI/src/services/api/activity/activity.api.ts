import { http } from '../config/http';

import { API_ENDPOINTS } from '../config/endpoints';

import type { AggregateArtifactViewDto, } from '../../../types/views/aggregateArtifactView';

export const activityApi = {
  getDetail: (
    activityId: string
  ) =>
    http.get<AggregateArtifactViewDto>(
      API_ENDPOINTS.activityView.activityAggregate,
      {
        params: {
          activityId,
        },
      }
    ),
};