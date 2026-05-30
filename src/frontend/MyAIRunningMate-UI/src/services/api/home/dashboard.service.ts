import { dashboardApi }from './dashboard.api';

import type {DashboardData,} from '../../../types/dashboard.types';
import type { BestEffortRequest } from '../../../types/bestefforts.types';

export const dashboardService = {
  loadDashboard:
    async (): Promise<DashboardData> => {

      const [
        primaryEvent,
        upcomingEvents,
        bestEfforts,
        latestWeight,
        volume,
      ] = await Promise.all([
        dashboardApi.getPrimaryEvent(),
        dashboardApi.getUpcomingEvents(),
        dashboardApi.getBestEfforts(),
        dashboardApi.getLatestWeight(),
        dashboardApi.getWeeklyVolume(),
      ]);

      return {
        primaryEvent,
        upcomingEvents,
        bestEfforts,
        latestWeight,
        volume,
      };  
    },
    
    updateEffort: async ( payload: BestEffortRequest ): Promise<BestEffortRequest> => {
        return await dashboardApi.updateBestEffort({distance_label: payload.distance_label,time_seconds: payload.time_seconds,achieved_at: payload.achieved_at});
    },
};