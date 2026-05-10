import type { LoginRequest, LoginResponse} from '../../types/login/auth.types';
import { http } from './http';
import { API_ENDPOINTS } from './endpoints';

export const authApi = {
    login: (credentials: LoginRequest) => http.post<LoginResponse>( API_ENDPOINTS.session.login, credentials ),
};