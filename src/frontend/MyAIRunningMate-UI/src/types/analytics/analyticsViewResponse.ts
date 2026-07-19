import type { YearlyAnalyticsDto } from "./yearlyAnalyticsResponse";
import type { YearlyStatisticsDto } from "./yearlyStatisticsResponse";

export interface AnalyticsViewResponse {
  yearly_analytics: YearlyAnalyticsDto;
  yearly_statistics: YearlyStatisticsDto;
}