import type { AxiosRequestConfig } from 'axios';

import { apiClient } from './client';

export const http = {
  get: async <T>(
    url: string,
    config?: AxiosRequestConfig
  ): Promise<T> => {

    const response =
      await apiClient.get<T>(
        url,
        config
      );

    return response.data;
  },

  post: async <T>(
    url: string,
    body?: unknown,
    config?: AxiosRequestConfig
  ): Promise<T> => {

    const response =
      await apiClient.post<T>(
        url,
        body,
        config
      );

    return response.data;
  },

  put: async <T>(
    url: string,
    body?: unknown,
    config?: AxiosRequestConfig
  ): Promise<T> => {

    const response =
      await apiClient.put<T>(
        url,
        body,
        config
      );

    return response.data;
  },

  delete: async <T>(
    url: string,
    config?: AxiosRequestConfig
  ): Promise<T> => {

    const response =
      await apiClient.delete<T>(
        url,
        config
      );

    return response.data;
  },
};