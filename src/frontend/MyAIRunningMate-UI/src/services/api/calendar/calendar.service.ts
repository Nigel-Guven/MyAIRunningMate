import { calendarApi } from './calendar.api';

export const calendarService = {
  getMonthlyActivities: async (
    month: number,
    year: number
  ) => {
    return calendarApi.getMonthlyActivities(
      month,
      year
    );
  },
};