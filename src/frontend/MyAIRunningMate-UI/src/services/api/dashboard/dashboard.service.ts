import type { DashboardTypes } from "../../../types/dashboard/dashboard.types";
import type { WeeklyInsightsResponse } from "../../../types/dashboard/weeklyInsightsResponse";
import { dashboardApi } from "./dashboard.api";

// Create a helper type for dashboard data excluding weekly insights
export type InitialDashboardData = Omit<DashboardTypes, 'weeklyInsights'>;

export const dashboardService = {
  // 1. Load only the static/initial dashboard data (Runs once on mount)
  loadInitialDashboard: async (): Promise<InitialDashboardData> => {
    const results = await Promise.allSettled([
      dashboardApi.getUserMetrics(),
      dashboardApi.getPrimaryEvent(),
      dashboardApi.getUpcomingEvents(),
      dashboardApi.getBestEfforts()
    ]);

    return {
      userMetrics: results[0].status === 'fulfilled' ? results[0].value : null,
      primaryEvent: results[1].status === 'fulfilled' ? results[1].value : null,
      upcomingEvents: results[2].status === 'fulfilled' ? results[2].value : [],
      bestEfforts: results[3].status === 'fulfilled' ? results[3].value : []
    };
  },

  // 2. Fetch weekly insights independently (Runs on mount & when offset changes)
  getWeeklyInsights: async (offset: number): Promise<WeeklyInsightsResponse | null> => {
    try {
      return await dashboardApi.getWeeklyInsights(offset);
    } catch (error) {
      console.error("Failed to fetch weekly insights:", error);
      return null;
    }
  },

  getWeekLabel(offset: number) {
    if (offset === 0) return "Current Week";
    if (offset === -1) return "Last Week";

    return `${Math.abs(offset)} Weeks Ago`;
  },
};