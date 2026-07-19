import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';
import type { AnalyticsViewResponse } from '../../../types/analytics/analyticsViewResponse';

export const analyticsApi = {
  getAnalytics: (year: number) =>
    http.get<AnalyticsViewResponse>(API_ENDPOINTS.analytics.statistics, {
      params: { year },
    }),
};