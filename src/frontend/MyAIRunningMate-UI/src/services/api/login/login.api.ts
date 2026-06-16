import type { LoginRequest } from "../../../types/login/loginRequest";
import type { LoginResponse } from "../../../types/login/loginResponse";
import { API_ENDPOINTS } from "../config/endpoints";
import { http } from "../config/http";

export const loginApi = {
  login: (request: LoginRequest) => http.post<LoginResponse>(API_ENDPOINTS.session.login, request)
};