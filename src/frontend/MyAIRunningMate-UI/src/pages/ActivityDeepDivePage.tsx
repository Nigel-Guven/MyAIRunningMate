import React, { useState, useEffect } from 'react';
import type { AggregateArtifactResponse } from '../types/aggregates/aggregateArtifactResponse';
import { activityService } from '../services/api/activity/activity.service';
import { ActivityHeader } from '../components/activity/ActivityHeader';
import { ActivityMap } from '../components/activity/ActivityMap';
import { UserSpecificDetails } from '../components/activity/UserSpecificDetails';
import { useParams } from 'react-router';

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
    <div className="min-h-screen bg-slate-950 text-slate-100 p-4 md:p-8">
      <div className="max-w-6xl mx-auto space-y-6">
        
        {/* Component 1: Header */}
        <ActivityHeader 
          exerciseName={activity_details.exercise_name}
          startTime={activity_details.start_time}
          location={activity_details.location || "Unknown Location"} 
          distanceMetres={activity_details.distance_metres}
          totalTime={activity_details.total_time}
          numberOfLaps={activity_details.number_of_laps}
        />

        {/* Dynamic Responsive Grid Layout */}
        <div className={`grid grid-cols-1 gap-6 items-start ${
          activity_details.map_polyline ? 'lg:grid-cols-3' : 'max-w-3xl mx-auto'
        }`}>
          
          {/* Component 2: Map (renders conditionally only if polyline exists) */}
          {activity_details.map_polyline && (
            <div className="lg:col-span-2">
              <ActivityMap 
                mapPolyline={activity_details.map_polyline}
                locationName={activity_details.location || "Unknown Location"}
              />
            </div>
          )}

          {/* Component 3: User Biometric Thresholds */}
          <div className={activity_details.map_polyline ? 'lg:col-span-1' : 'w-full'}>
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

        </div>

      </div>
    </div>
  );
};