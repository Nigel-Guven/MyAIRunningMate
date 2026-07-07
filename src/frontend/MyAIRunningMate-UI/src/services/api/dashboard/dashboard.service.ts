import type { DashboardTypes } from "../../../types/dashboard/dashboard.types";
import { dashboardApi } from "./dashboard.api";


export const dashboardService = {
  loadDashboard: async (offset: number): Promise<DashboardTypes> => {
    const results = await Promise.allSettled([
      dashboardApi.getPrimaryEvent(),
      dashboardApi.getUpcomingEvents(),
      dashboardApi.getBestEfforts(),
      dashboardApi.getLatestWeight(),
      dashboardApi.getWeeklyInsights(offset),
    ]);

    return {
      primaryEvent: results[0].status === 'fulfilled' ? results[0].value : null,
      upcomingEvents: results[1].status === 'fulfilled' ? results[1].value : [],
      bestEfforts: results[2].status === 'fulfilled' ? results[2].value : [],
      latestWeight: results[3].status === 'fulfilled' ? results[3].value : null,
      weeklyInsights: results[4].status === 'fulfilled' ? results[4].value : null, 
    };
  },

  getWeekLabel(offset: number) {
    if (offset === 0) return "Current Week";
    if (offset === -1) return "Last Week";

    return `${Math.abs(offset)} Weeks Ago`;
  },
};