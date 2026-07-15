
import type { BestEffortResponse } from './bestEffortResponse';
import type { EventViewResponse } from './eventViewResponse';
import type { UserMetricsResponse } from './userMetricsResponse';
import type { WeeklyInsightsResponse } from './weeklyInsightsResponse';

export interface DashboardTypes {
  userMetrics: UserMetricsResponse | null;

  primaryEvent: EventViewResponse | null;

  upcomingEvents: EventViewResponse[] | undefined;

  bestEfforts: BestEffortResponse[] | null;

  weeklyInsights: WeeklyInsightsResponse | null;
}