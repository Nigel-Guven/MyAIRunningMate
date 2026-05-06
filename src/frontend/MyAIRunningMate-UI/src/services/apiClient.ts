import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || "https://localhost:7002/api";

export const apiClient = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');

    
    
    if (token) {
      config.headers = config.headers || {};
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response && error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      localStorage.removeItem('token');
      localStorage.removeItem('userId'); // Added for cleanup
      localStorage.removeItem('is_strava_connected'); // Added for cleanup
      
      window.location.href = '/login';
    }

    return Promise.reject(error);
  }
);