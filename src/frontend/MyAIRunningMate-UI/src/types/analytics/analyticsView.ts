import type { WeeklyInsightsResponse } from "../dashboard/weeklyInsightsResponse";
import type { YearlyStatisticsDto } from "./yearlyStatistics.types";


export interface AnalyticsDashboardView {
  summary: YearlyStatisticsDto;
  year_weekly_volume: WeeklyInsightsResponse[];
}