const TOKEN_KEY = 'token';

export const tokenStorage = {
  get: () => localStorage.getItem(TOKEN_KEY),

  set: (token: string) => {
    localStorage.setItem(TOKEN_KEY, token);
  },

  clear: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('is_strava_connected');
  },
};