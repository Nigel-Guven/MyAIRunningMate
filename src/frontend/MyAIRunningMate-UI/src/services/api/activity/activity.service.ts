import { activityApi } from './activity.api';

export const activityService = {
getDetail: async (
    activityId: string
  ) => {

    return activityApi.getDetail(
      activityId
    );
  },
};