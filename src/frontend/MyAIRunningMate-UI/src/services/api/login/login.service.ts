import { loginApi } from '../login/login.api';
import { authStorage } from '../config/authStorage';

export const loginService = {
  login: async (
    email: string,
    password: string
  ) => {
    const response = await loginApi.login({
      email,
      password,
    });

    authStorage.set(
      response.token,
      response.user_id,
      response.is_strava_connected
    );

    return response;
  },

  logout: () => {
    authStorage.clear();

    window.location.href = '/login';
  },

  isAuthenticated: () => {
    return !!authStorage.get();
  },
};