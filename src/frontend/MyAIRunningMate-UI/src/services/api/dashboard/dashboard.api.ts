import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';
import type { EventViewResponse } from '../../../types/dashboard/eventViewResponse';
import type { BestEffortResponse } from '../../../types/dashboard/bestEffortResponse';
import type { BestEffortRequest } from '../../../types/dashboard/bestEffortRequest';
import type { WeightResponse } from '../../../types/weight/weightResponse';

export const dashboardApi = {
  getPrimaryEvent: () =>
    http.get<EventViewResponse>(
      API_ENDPOINTS.events.primaryEvent
    ),

  getUpcomingEvents: () =>
    http.get<EventViewResponse[]>(
      API_ENDPOINTS.events.upcomingEvents
    ),

  getBestEfforts: () =>
    http.get<BestEffortResponse[]>(
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
    )
};