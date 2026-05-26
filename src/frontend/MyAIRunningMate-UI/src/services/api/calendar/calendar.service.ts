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

  getTrainingPlan: async (month: number, year: number) => {
    try {
      return await calendarApi.getTrainingPlan(month, year);
    } catch (error: any) {
      if (error?.status === 404 || error?.response?.status === 404) {
        return null;
      }
      throw error;
    }
  },
};