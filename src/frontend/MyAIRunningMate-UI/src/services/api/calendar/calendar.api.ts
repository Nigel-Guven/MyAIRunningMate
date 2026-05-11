import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';

import type {
  CalendarViewDto,
} from '../../../types/views/calendarView';

export const calendarApi = {
  getMonthlyActivities: (
    month: number,
    year: number
  ) =>
    http.get<CalendarViewDto[]>(
      API_ENDPOINTS.calendar.display,
      {
        params: { month, year },
      }
    ),
};