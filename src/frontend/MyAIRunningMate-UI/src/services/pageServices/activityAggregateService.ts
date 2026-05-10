import type { AggregateArtifactViewDto } from "../types/aggregateArtifactView";
import { apiClient } from "./apiClient";

// services/activityService.ts
export const getActivityDetail = async (activityId: string): Promise<AggregateArtifactViewDto> => {
    const response = await apiClient.get<AggregateArtifactViewDto>(`/activity/aggregate?activityId=${activityId}`);
    return response.data;
};