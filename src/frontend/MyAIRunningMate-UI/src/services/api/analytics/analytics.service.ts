import type { AnalyticsViewResponse } from '../../../types/analytics/analyticsViewResponse';
import { analyticsApi } from './analytics.api';

export const analyticsService = {
  async getDashboardData(year: number): Promise<AnalyticsViewResponse> {
    try {
      const response = await analyticsApi.getAnalytics(year);
      
      return response;
    } catch (error) {
      console.error(`Failed to fetch analytics for year ${year}:`, error);
      throw error;
    }
  },
};