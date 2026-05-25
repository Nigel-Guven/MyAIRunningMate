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
      // If the backend returns a 404 (No active plan found for this window), return null safely
      if (error?.status === 404 || error?.response?.status === 404) {
        return null;
      }
      throw error;
    }
  },
};