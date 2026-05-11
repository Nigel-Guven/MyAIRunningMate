import { dashboardApi }from './dashboard.api';

import type {DashboardData,} from '../../../types/dashboard.types';

export const dashboardService = {
  loadDashboard:
    async (): Promise<DashboardData> => {

      const [
        primaryEvent,
        upcomingEvents,
        bestEfforts,
        latestWeight,
      ] = await Promise.all([
        dashboardApi.getPrimaryEvent(),
        dashboardApi.getUpcomingEvents(),
        dashboardApi.getBestEfforts(),
        dashboardApi.getLatestWeight(),
      ]);

      return {
        primaryEvent,
        upcomingEvents,
        bestEfforts,
        latestWeight,
      };
    },
};