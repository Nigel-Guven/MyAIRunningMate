const TOKEN_KEY = 'token';
const USER_ID_KEY = 'userId';

export const authStorage = {
  get: () => localStorage.getItem(TOKEN_KEY),

  set: (token: string, userId: string) => {
    localStorage.setItem(TOKEN_KEY, token);
    localStorage.setItem(USER_ID_KEY, userId);

    window.dispatchEvent(new Event('auth-change'));
  },

  clear: () => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_ID_KEY);

    window.dispatchEvent(new Event('auth-change'));
  },
};