import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';
import type { EventViewResponse } from '../../../types/dashboard/eventViewResponse';
import type { BestEffortResponse } from '../../../types/dashboard/bestEffortResponse';
import type { WeightResponse } from '../../../types/weight/weightResponse';
import type { WeeklyInsightsResponse } from '../../../types/dashboard/weeklyInsightsResponse';
import type { UserMetricsResponse } from '../../../types/dashboard/userMetricsResponse';

export const dashboardApi = {

  getUserMetrics: () =>
    http.get<UserMetricsResponse>(
      API_ENDPOINTS.dashboard.fitnessProfile
    ),

  getPrimaryEvent: () =>
    http.get<EventViewResponse>(
      API_ENDPOINTS.events.primaryEvent
    ),

  getUpcomingEvents: () =>
    http.get<EventViewResponse[]>(
      API_ENDPOINTS.events.upcomingEvents
    ),

  getBestEfforts: () =>
    http.get<BestEffortResponse[]>(
      API_ENDPOINTS.bestEfforts.allEfforts
    ),

  getLatestWeight: () =>
    http.get<WeightResponse>(
      API_ENDPOINTS.weight.latest
    ),

  getWeeklyInsights: (offset = 0) =>
    http.get<WeeklyInsightsResponse>(
      API_ENDPOINTS.dashboard.insights,
      {
        params: {
          weekOffset: offset,
        },
      }
    ),
};