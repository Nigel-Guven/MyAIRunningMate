import { apiClient } from './client';

export const http = {
  get: async <T>(url: string, config?: any): Promise<T> => 
    (await apiClient.get<T>(url, config)).data,

  post: async <T>(url: string, body?: unknown, config?: any): Promise<T> => 
    (await apiClient.post<T>(url, body, config)).data,

  put: async <T>(url: string, body?: unknown, config?: any): Promise<T> => 
    (await apiClient.put<T>(url, body, config)).data,

  delete: async <T>(url: string, config?: any): Promise<T> => 
    (await apiClient.delete<T>(url, config)).data,
};