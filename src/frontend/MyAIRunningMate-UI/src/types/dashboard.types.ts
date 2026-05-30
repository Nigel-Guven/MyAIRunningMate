import type {EventViewDto} from '../types/views/eventView';

import type {BestEffortViewDto} from './bestefforts.types';

import type {WeightResponse} from '../types/weight/weight.types';
import type { WeeklyVolumeDto } from './weeklyVolume.types';

export interface DashboardData {
  primaryEvent: EventViewDto | null;

  upcomingEvents: EventViewDto[];

  bestEfforts: BestEffortViewDto[];

  volume: WeeklyVolumeDto;

  latestWeight: WeightResponse | null;
}