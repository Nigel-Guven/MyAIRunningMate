const TOKEN_KEY = 'token';
const USER_ID_KEY = 'userId';
const STRAVA_SYNCHRONIZED = 'is_strava_connected';

export const authStorage = {
  get: () => localStorage.getItem(TOKEN_KEY),

  set: (token: string, userId: string, isStravaConnected: boolean) => {
    localStorage.setItem(TOKEN_KEY, token);
    localStorage.setItem(USER_ID_KEY, userId);
    localStorage.setItem(STRAVA_SYNCHRONIZED, String(isStravaConnected))
  },

  clear: () => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_ID_KEY);
    localStorage.removeItem(STRAVA_SYNCHRONIZED);
  },
};