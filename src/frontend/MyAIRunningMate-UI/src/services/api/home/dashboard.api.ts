import { http } from '../config/http';

import { API_ENDPOINTS } from '../config/endpoints';

import type { EventViewDto,} from '../../../types/views/eventView';

import type { BestEffortViewDto, } from '../../../types/views/bestEffortView';

import type { WeightResponse, } from '../../../types/weight/weight.types';

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

  getLatestWeight: () =>
    http.get<WeightResponse>(
      API_ENDPOINTS.weight.latest
    ),
};