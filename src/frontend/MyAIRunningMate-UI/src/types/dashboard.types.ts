import type {
  EventViewDto,
} from '../types/views/eventView';

import type {
  BestEffortViewDto,
} from '../types/views/bestEffortView';

import type {
  WeightResponse,
} from '../types/weight/weight.types';

export interface DashboardData {
  primaryEvent: EventViewDto | null;

  upcomingEvents: EventViewDto[];

  bestEfforts: BestEffortViewDto[];

  latestWeight: WeightResponse | null;
}