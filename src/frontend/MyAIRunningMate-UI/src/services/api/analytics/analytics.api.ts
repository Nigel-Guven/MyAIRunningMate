import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';
import type { AnalyticsDashboardView } from '../../../types/views/analyticsView';

export const analyticsApi = {
  getAnalytics: (year: number) =>
    http.get<AnalyticsDashboardView>(API_ENDPOINTS.analytics.statistics, {
      params: { year },
    }),
};