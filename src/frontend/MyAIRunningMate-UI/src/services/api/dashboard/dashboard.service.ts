import type { BestEffortRequest } from "../../../types/dashboard/bestEffortRequest";
import type { DashboardTypes } from "../../../types/dashboard/dashboard.types";
import { dashboardApi } from "./dashboard.api";


export const dashboardService = {
  loadDashboard:
    async (): Promise<DashboardTypes> => {

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
    
    updateEffort: async ( payload: BestEffortRequest ): Promise<BestEffortRequest> => {
        return await dashboardApi.updateBestEffort(
          {
            distance_label: payload.distance_label,
            new_personal_record_time: payload.new_personal_record_time,
            new_personal_record_date: payload.new_personal_record_date});
    },
};