import { useState, useEffect } from 'react';
import type { DashboardTypes } from '../types/dashboard/dashboard.types';
import type { BestEffortRequest } from '../types/dashboard/bestEffortRequest';
import { dashboardService } from '../services/api/dashboard/dashboard.service';
import { authStorage } from '../services/api/config/authStorage';
import { DashboardHeader } from '../components/dashboard/DashboardHeader';
import { PrimaryEventHero } from '../components/dashboard/PrimaryEventHero';
import { WeeklyVolumeCard } from '../components/dashboard/WeeklyVolumeCard';
import { PersonalRecordsCard } from '../components/dashboard/PersonalRecordsCard';

const initialState: DashboardTypes = {
  primaryEvent: null,
  upcomingEvents: [],
  bestEfforts: [],
  latestWeight: null,
  weeklyInsights: null,
};

const defaultWeeklyInsights = {
  running_distance_metres: 0,
  running_time_seconds: 0,
  running_moving_time_seconds: 0,
  swimming_distance_metres: 0,
  swimming_time_seconds: 0,
  morning_activities: 0,
  afternoon_activities: 0,
  evening_activities: 0,
  night_activities: 0,
  total_running_elevation_gain: 0,
  mean_average_heart_rate: 0,
  mean_max_heart_rate: 0,
  mean_training_effect: 0,
  total_training_effect: 0,
  total_calories_burned: 0,
  running_time_break_seconds: 0,
  running_moving_efficiency: 0,
  caloric_intensity: 0,
  elevation_intensity: 0,
  rest_days: 0,
  locations: [],
  training_consistency_score: 0,
};

export const DashboardPage = () => {
  const [dashboard, setDashboard] = useState<DashboardTypes>(initialState);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const handleUpdateEffort = async (label: string, seconds: number) => {
    try {
      const payload: BestEffortRequest = {
        distance_label: label,
        new_personal_record_time: seconds,
        new_personal_record_date: new Date().toISOString()
      };
      await dashboardService.updateEffort(payload);
      const data = await dashboardService.loadDashboard();
      setDashboard(data);
    } catch (err) {
      console.error("Update failed", err);
      alert("Failed to update Personal Record");
    }
  };

  useEffect(() => {
    let isMounted = true;

    const loadDashboard = async () => {
      const localToken = authStorage.get();
      if (!localToken) {
        setError('Session initializing. Please wait...');
        return;
      }

      try {
        setLoading(true);
        const data = await dashboardService.loadDashboard();
        if (isMounted) setDashboard(data);
      } catch (err) {
        if (isMounted) setError('Failed to load command center.');
      } {
        if (isMounted) setLoading(false);
      }
    };
    
    loadDashboard();
    return () => { isMounted = false; };
  }, []);

  if (loading) return <div className="p-12 text-slate-500 font-mono animate-pulse uppercase">Synchronizing Command Center...</div>;
  if (error) return <div className="p-12 text-red-400">{error}</div>;

  const { primaryEvent, upcomingEvents, bestEfforts, latestWeight } = dashboard;
  const insights = dashboard.weeklyInsights || defaultWeeklyInsights;

  return (
    <div className="space-y-8 animate-in fade-in duration-700 pb-12">
      <DashboardHeader latestWeight={latestWeight} />
      
      <PrimaryEventHero primaryEvent={primaryEvent} />

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <WeeklyVolumeCard insights={insights} />

        {/* AI Mate (Static UI Section kept inline for brevity, or extract if preferred) */}
        <div className="rounded-3xl border border-slate-800 bg-slate-900 p-6 flex flex-col justify-between">
          <div className="flex items-center gap-2 mb-4">
            <span className="h-2 w-2 rounded-full bg-purple-500 animate-pulse" />
            <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em]">Nexus AI Mate</h4>
          </div>
          <p className="text-slate-300 italic text-sm leading-relaxed">
            "Your training load is up 12%. Focus on pool recovery tomorrow to keep shins fresh for Sunday's long run."
          </p>
          <div className="mt-4 pt-4 border-t border-slate-800 text-[10px] text-slate-600 font-mono uppercase">Status: Analyzing Data</div>
        </div>

        {/* Upcoming Schedule Card */}
        <div className="rounded-3xl border border-slate-800 bg-slate-900 p-6">
          <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-4">Next Up</h4>
          {upcomingEvents.map((event, i) => (
            <div key={i} className="flex items-center gap-4 mb-3 last:mb-0 p-2 rounded-xl hover:bg-white/5 transition-colors">
              <div className="text-center bg-blue-500/10 p-2 rounded-lg min-w-[45px]">
                <p className="text-[10px] font-black text-blue-400 uppercase leading-none mb-1">
                  {new Date(event.event_date).toLocaleString('default', { month: 'short' })}
                </p>
                <p className="text-lg font-black text-white leading-none">{new Date(event.event_date).getDate()}</p>
              </div>
              <div className="group p-4 rounded-2xl transition-all w-full min-w-0">
                <p className="text-xs font-bold text-white leading-tight uppercase truncate">
                  <a href={event.event_url} target="_blank" rel="noopener noreferrer" className="transition-colors duration-200 hover:text-blue-400">
                    {event.event_name}
                  </a>
                </p>
                <p className="pt-2 text-[10px] text-slate-600 font-mono uppercase truncate">{event.event_location}</p>
              </div>
            </div>
          ))}
        </div>
      </div>

      <PersonalRecordsCard bestEfforts={bestEfforts} onUpdateEffort={handleUpdateEffort} />
    </div>
  );
};