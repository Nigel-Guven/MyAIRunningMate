
import type { WeightResponse } from '../weight/weightResponse';
import type { BestEffortResponse } from './bestEffortResponse';
import type { EventViewResponse } from './eventViewResponse';

export interface DashboardTypes {
  primaryEvent: EventViewResponse | null;

  upcomingEvents: EventViewResponse[];

  bestEfforts: BestEffortResponse[];

  latestWeight: WeightResponse | null;
}