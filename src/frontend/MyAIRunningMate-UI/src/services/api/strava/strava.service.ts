import { stravaApi } from './strava.api';

const STRAVA_KEY = 'is_strava_connected';

export const stravaService = {
  isConnectedLocally: () => {
    return localStorage.getItem(
      STRAVA_KEY
    ) === 'true';
  },

  setConnected: (value: boolean) => {
    localStorage.setItem(
      STRAVA_KEY,
      String(value)
    );
  },

  verifyConnection: async () => {
    const response =
      await stravaApi.getStatus();

    stravaService.setConnected(
      response.isStravaConnected
    );

    return response.isStravaConnected;
  },

  connect: async () => {
    const response =
      await stravaApi.getConnectUrl();

    window.location.href = response.url;
  },
};