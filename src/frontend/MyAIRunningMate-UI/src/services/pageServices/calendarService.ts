import { apiClient } from './apiClient';
import type { CalendarViewResult } from '../types/calendarView';

export const getMonthlyActivities = async (
  month: number,
  year: number
): Promise<CalendarViewResult[]> => {
  const response = await apiClient.get<CalendarViewResult[]>('/calendar/display', {
    params: { month, year },
  });
  return response.data;
};