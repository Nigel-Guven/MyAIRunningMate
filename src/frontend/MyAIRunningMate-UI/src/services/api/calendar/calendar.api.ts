import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';
import type { CalendarViewResponse } from '../../../types/calendar/calendarViewResponse';
import type { TrainingPlanViewResponse } from '../../../types/nexus/trainingPlanViewResponse';

export const calendarApi = {
  getMonthlyActivities: (
    month: number,
    year: number
  ) =>
    http.get<CalendarViewResponse[]>(
      API_ENDPOINTS.calendar.display,
      {
        params: { month, year },
      }
    ),

  getTrainingPlan: (month: number, year: number) =>
    http.get<TrainingPlanViewResponse>(
      API_ENDPOINTS.calendar.trainingPlan, 
      {
        params: { month, year },
      }
    ),
};