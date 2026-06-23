import type { BestEffortRequest } from "../../../types/dashboard/bestEffortRequest";
import type { DashboardTypes } from "../../../types/dashboard/dashboard.types";
import { dashboardApi } from "./dashboard.api";


export const dashboardService = {
  loadDashboard: async (): Promise<DashboardTypes> => {
    const results = await Promise.allSettled([
      dashboardApi.getPrimaryEvent(),
      dashboardApi.getUpcomingEvents(),
      dashboardApi.getBestEfforts(),
      dashboardApi.getLatestWeight(),
      dashboardApi.getWeeklyInsights(),
    ]);

    return {
      primaryEvent: results[0].status === 'fulfilled' ? results[0].value : null,
      upcomingEvents: results[1].status === 'fulfilled' ? results[1].value : [],
      bestEfforts: results[2].status === 'fulfilled' ? results[2].value : [],
      latestWeight: results[3].status === 'fulfilled' ? results[3].value : null,
      weeklyInsights: results[4].status === 'fulfilled' ? results[4].value : null, 
    };
  },
    
    updateEffort: async ( payload: BestEffortRequest ): Promise<BestEffortRequest> => {
        return await dashboardApi.updateBestEffort(
          {
            distance_label: payload.distance_label,
            new_personal_record_time: payload.new_personal_record_time,
            new_personal_record_date: payload.new_personal_record_date
          });
    },
};