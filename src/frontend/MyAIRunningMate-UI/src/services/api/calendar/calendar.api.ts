import { http } from '../config/http';
import { API_ENDPOINTS } from '../config/endpoints';

import type {
  CalendarViewDto,
} from '../../../types/views/calendarView';
import type { TrainingPlanView } from '../../../types/views/trainingPlanView';

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

  getTrainingPlan: (month: number, year: number) =>
    http.get<TrainingPlanView>(
      API_ENDPOINTS.calendar.trainingPlan, 
      {
        params: { month, year },
      }
    ),
};