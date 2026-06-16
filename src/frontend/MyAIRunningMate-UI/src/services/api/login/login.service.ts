import { loginApi } from '../login/login.api';
import { authStorage } from '../config/authStorage';
import type { LoginRequest } from '../../../types/login/loginRequest';
import type { LoginResponse } from '../../../types/login/loginResponse'; // Import your response type

export const loginService = {
  login: async (request: LoginRequest): Promise<LoginResponse> => {
    // TypeScript now knows 'response' IS a LoginResponse
    const response = await loginApi.login(request);
    
    // Now these properties are type-safe and won't be 'unknown'
    authStorage.set(response.token, response.user_id);

    return response;
  },

  logout: () => {
    authStorage.clear();
    window.location.href = '/login';
  },

  isAuthenticated: () => !!authStorage.get(),
};