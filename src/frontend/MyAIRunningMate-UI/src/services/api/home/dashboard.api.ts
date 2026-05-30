import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';
import type { EventViewDto,} from '../../../types/views/eventView';
import type { WeightResponse, } from '../../../types/weight/weight.types';
import type { BestEffortViewDto, BestEffortRequest } from '../../../types/bestefforts.types';
import type { WeeklyVolumeDto } from '../../../types/weeklyVolume.types';

export const dashboardApi = {
  getPrimaryEvent: () =>
    http.get<EventViewDto>(
      API_ENDPOINTS.events.primaryEvent
    ),

  getUpcomingEvents: () =>
    http.get<EventViewDto[]>(
      API_ENDPOINTS.events.upcomingEvents
    ),

  getBestEfforts: () =>
    http.get<BestEffortViewDto[]>(
      API_ENDPOINTS.bestEfforts.allEfforts
    ),

  updateBestEffort: ( payload: BestEffortRequest ) =>
    http.post<BestEffortRequest>(
      API_ENDPOINTS.bestEfforts.updateEffort, 
      payload
    ),

  getLatestWeight: () =>
    http.get<WeightResponse>(
      API_ENDPOINTS.weight.latest
    ),

  getWeeklyVolume: () =>
    http.get<WeeklyVolumeDto>(
      API_ENDPOINTS.dashboard.volume
    ),
};