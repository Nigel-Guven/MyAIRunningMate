import React, { useState, useEffect } from 'react';
import type { AggregateArtifactResponse } from '../types/aggregates/aggregateArtifactResponse';
import { activityService } from '../services/api/activity/activity.service';
import { ActivityHeader } from '../components/activity/ActivityHeader';
import { ActivityMap } from '../components/activity/ActivityMap';
import { UserSpecificDetails } from '../components/activity/UserSpecificDetails';
import { useParams } from 'react-router';
import { ActivityMetrics } from '../components/activity/ActivityMetrics';
import { LapTable } from '../components/activity/LapTable';
import { BestEfforts } from '../components/activity/BestEfforts';
import { TimeSeriesGraph } from '../components/activity/TimeSeriesGraph';

export const ActivityDeepDivePage: React.FC = () => {
  const { activityId } = useParams() as { activityId: string };
  
  const [data, setData] = useState<AggregateArtifactResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isMounted = true;

    const fetchActivity = async () => {
      try {
        setLoading(true);
        setError(null);
        
        const response = await activityService.getDetail(activityId);
        
        console.log("Raw Response received from service:", response);

        if (isMounted) {
          let payload: any = response;
          if (response && typeof response === 'object' && 'data' in response) {
            payload = (response as any).data;
          }

          if (!payload || !payload.activity_details) {
            console.error("Payload validation failed. Keys present:", payload ? Object.keys(payload) : "none");
            throw new Error("API response structure is invalid or missing 'activity_details'.");
          }

          setData(payload as AggregateArtifactResponse);
        }
      } catch (err: any) {
        console.error("Fetch Exception:", err);
        if (isMounted) {
          setError(err.message || "An unexpected error occurred while communicating with the C# backend API.");
        }
      } finally {
        if (isMounted) {
          setLoading(false);
        }
      }
    };

    if (activityId) {
      fetchActivity();
    } else {
      setLoading(false);
      setError("No valid activity ID was provided to the page.");
    }

    return () => {
      isMounted = false;
    };
  }, [activityId]);

  if (loading) {
    return (
      <div className="min-h-screen bg-slate-950 text-white flex items-center justify-center">
        <div className="text-center space-y-4">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-emerald-500 mx-auto"></div>
          <p className="text-slate-400 text-sm">Processing activity metrics...</p>
        </div>
      </div>
    );
  }

  if (error || !data) {
    return (
      <div className="min-h-screen bg-slate-950 text-white flex items-center justify-center p-4">
        <div className="bg-slate-900 border border-red-500/20 p-6 rounded-2xl max-w-md text-center">
          <p className="text-red-400 font-medium mb-2">Error Loading Activity</p>
          <p className="text-slate-400 text-sm mb-4">{error || "No data returned."}</p>
        </div>
      </div>
    );
  }

  const { activity_details } = data;

  return (
    <div className="min-h-screen bg-slate-950 text-slate-100 md:px-4 py-4 md:py-8">
      <div className="w-full mx-auto space-y-6">
        
        {/* Component 1: Header */}
        <ActivityHeader 
          garminActivityId={activity_details.garmin_activity_id}
          exerciseName={activity_details.exercise_name}
          startTime={activity_details.start_time}
          location={activity_details.location || "Unknown Location"} 
          distanceMetres={activity_details.distance_metres}
          movingTime={activity_details.moving_time}
          totalTime={activity_details.total_time}
          totalAscent={activity_details.total_ascent || null}
          totalDescent={activity_details.total_descent || null}
          numberOfLaps={activity_details.number_of_laps}
        />

        {/* Responsive Grid Layout for Map & Biometrics */}
        <div className={`grid grid-cols-1 gap-4 items-start ${
          activity_details.map_polyline ? 'lg:grid-cols-[3fr_2fr]' : 'max-w-3xl mx-auto'
        }`}>
          {activity_details.map_polyline && (
            <ActivityMap 
              mapPolyline={activity_details.map_polyline}
              locationName={activity_details.location || "Unknown Location"}
            />
          )}
          <UserSpecificDetails 
            beginningBodyBattery={activity_details.beginning_body_battery}
            beginningBodyPotential={activity_details.beginning_body_potential}
            endingBodyBattery={activity_details.ending_body_battery}
            endingPotential={activity_details.ending_potential}
            userVolumetricOxygenMax={activity_details.user_volumetric_oxygen_max}
            userMaxHeartRate={activity_details.user_max_heart_rate}
            userLactateThresholdHeartRate={activity_details.user_lactate_threshold_heart_rate}
            userLactateThresholdPower={activity_details.user_lactate_threshold_power}
            userLactateThresholdSpeed={activity_details.user_lactate_threshold_speed}
            recoveryTimeMinutes={activity_details.recovery_time}
          />
        </div>

        {/* Core Breakdown Layout: Balanced 2-Column System */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-4 items-start">
          
          {/* Left Column (Sidebar): Stacked Metrics & Personal Bests */}
          <div className="space-y-4 lg:col-span-1">
            {data.best_efforts && (
              <BestEfforts efforts={data.best_efforts} />
            )}
            <ActivityMetrics metrics={data.activity_metrics} />
          </div>

          {/* Right Column (Main Content): Spans 2 columns */}
          <div className="lg:col-span-2">
            <LapTable laps={data.laps} />
          </div>

        </div>

        {/* Component 7: Time Series Graph remains anchored safely at the bottom */}
        {data.time_series_records.length != 0 && (
        <TimeSeriesGraph
          records={data.time_series_records}
          averageHeartRate={data.activity_metrics.average_heart_rate}
        />)}
      </div>
    </div>
  );
};