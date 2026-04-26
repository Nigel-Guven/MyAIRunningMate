export interface AggregateArtifactDto {
  activityId: string;
  resourceId: string;
  garminActivityId: string;
  stravaId: string | null;
  name: string;
  exerciseType: string;
  startDate: string; 
  startTime: string;
  elapsedTime: number;
  averageCadence: number;
  averageSecondPerKilometre: number;
  totalElevationGain: number;
  elevationLow: number;
  elevationHigh: number;
  durationSeconds: number;
  distanceMetres: number;
  averageHeartRate: number;
  maxHeartRate: number;
  trainingEffect: number;
  achievementCount: number;
  kudosCount: number;
  athleteCount: number;
  personalRecordCount: number;
  map: MapDto | null;
  laps: LapDto[];
}