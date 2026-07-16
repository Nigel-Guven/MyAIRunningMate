import type { BestEffortResponse } from "../dashboard/bestEffortResponse";
import type { ActivityDetailsResponse } from "./activityDetailsResponse";
import type { ActivityMetricsResponse } from "./activityMetricsResponse";
import type { LapResponse } from "./lapResponse";
import type { TimeSeriesRecordResponse } from "./timeSeriesRecordResponse";


export interface AggregateArtifactResponse {
  activity_details: ActivityDetailsResponse;
  activity_metrics: ActivityMetricsResponse;
  time_series_records: TimeSeriesRecordResponse[];
  laps: LapResponse[];
  best_efforts: BestEffortResponse[] | null;
}