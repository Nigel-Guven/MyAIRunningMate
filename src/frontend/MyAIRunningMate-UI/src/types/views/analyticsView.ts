import type { WeeklyVolumeDto } from "../weeklyVolume.types";
import type { YearlyStatisticsDto } from "../yearlyStatistics.types";

export interface AnalyticsDashboardView {
  summary: YearlyStatisticsDto;
  year_weekly_volume: WeeklyVolumeDto[];
}