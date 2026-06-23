import { useState, useEffect } from 'react';
import logo from '../assets/applogo.png';
import { formatTime } from '../services/helpers/formatTime';
import { getDaysUntil } from '../services/helpers/getDaysUntil';
import type { DashboardTypes } from '../types/dashboard/dashboard.types';
import type { BestEffortRequest } from '../types/dashboard/bestEffortRequest';
import { dashboardService } from '../services/api/dashboard/dashboard.service';
import { authStorage } from '../services/api/config/authStorage';

const initialState: DashboardTypes = {
  primaryEvent: null,
  upcomingEvents: [],
  bestEfforts: [],
  latestWeight: null,
  weeklyInsights: null,
};

// Strong fallbacks for weekly insights to completely eliminate inline optional chaining
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
  mean_average_heart_rate: null,
  mean_max_heart_rate: null,
  mean_training_effect: null,
  total_training_effect: null,
  total_calories_burned: 0,
  running_time_break_seconds: 0,
  running_moving_efficiency: 0,
  caloric_intensity: 0,
  elevation_intensity: 0,
  rest_days: 0,
  locations: [],
};

export const DashboardPage = () => {
  const [dashboard, setDashboard] = useState<DashboardTypes>(initialState);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [editingLabel, setEditingLabel] = useState<string | null>(null);
  const [tempSeconds, setTempSeconds] = useState<string>("");

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
        if (isMounted) {
          setDashboard(data);
        }
      } catch (err) {
        if (isMounted) setError('Failed to load command center.');
      } finally {
        if (isMounted) setLoading(false);
      }
    };
    
    loadDashboard();
    return () => { isMounted = false; };
  }, []);

  if (loading) return <div className="p-12 text-slate-500 font-mono animate-pulse uppercase">Synchronizing Command Center...</div>;
  if (error) return <div className="p-12 text-red-400">{error}</div>;

  // Extract metrics and explicitly default null structures to clean up UI rendering logic
  const { primaryEvent, upcomingEvents, bestEfforts, latestWeight } = dashboard;
  const insights = dashboard.weeklyInsights || defaultWeeklyInsights;

  // Pre-calculated variables for the activity distribution bar
  const segments = [
    { label: "Morn", count: insights.morning_activities, color: "bg-red-400" },
    { label: "Aft", count: insights.afternoon_activities, color: "bg-amber-400" },
    { label: "Eve", count: insights.evening_activities, color: "bg-blue-400" },
    { label: "Night", count: insights.night_activities, color: "bg-purple-400" },
  ];

  return (
    <div className="space-y-8 animate-in fade-in duration-700 pb-12">
      
      {/* 1. Header Section */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <img src={logo} alt="Logo" className="h-14 w-14 rounded-xl shadow-lg shadow-blue-900/20" />
          <div>
            <h2 className="text-3xl font-black tracking-tighter uppercase italic">Command Center</h2>
            <p className="text-slate-400 font-medium">Welcome back, Nigel.</p>
          </div>
        </div>
        
        <div className="bg-slate-900/50 p-3 px-6 rounded-2xl border border-slate-800">
          <p className="text-[10px] text-slate-500 uppercase font-black tracking-widest text-right">Weight</p>
          <p className="text-2xl font-black text-blue-400 italic">
            {latestWeight?.weight_in_pounds ? (latestWeight.weight_in_pounds * 0.453592).toFixed(1) : '0.0'} 
            <span className="text-xs text-slate-600 not-italic ml-1">KG</span>
          </p>
        </div>
      </div>

      {/* 2. Primary Event Hero (Full Width) */}
      <div className="relative overflow-hidden rounded-3xl bg-gradient-to-br from-blue-600 via-blue-700 to-indigo-900 p-8 text-white shadow-2xl shadow-blue-900/40 border border-white/10">
        <div className="relative z-10 grid grid-cols-1 lg:grid-cols-2 gap-8 items-center">
          <div>
            <div className="flex items-center gap-3 mb-4">
              <span className="px-3 py-1 rounded-full bg-black/20 border border-white/20 text-[10px] font-black uppercase tracking-widest">
                {primaryEvent?.event_type || 'Race'} • {(primaryEvent?.distance_metres || 0) / 1000}KM
              </span>
            </div>
            <h3 className="text-5xl font-black italic tracking-tighter uppercase leading-none mb-4">
              {primaryEvent?.event_name || "No Objective Set"}
            </h3>
            <div className="flex flex-wrap gap-6 text-blue-100/80 mb-6">
              <div className="flex flex-col">
                <span className="text-[10px] uppercase font-bold opacity-60">Location</span>
                <span className="text-sm font-bold uppercase">{primaryEvent?.event_location || 'Unknown'}</span>
              </div>
              <div className="flex flex-col border-l border-white/20 pl-6">
                <span className="text-[10px] uppercase font-bold opacity-60">Date</span>
                <span className="text-sm font-bold uppercase">{primaryEvent ? new Date(primaryEvent.event_date).toLocaleDateString() : '--'}</span>
              </div>
            </div>
            {primaryEvent?.event_info && primaryEvent.event_info !== "null" && (
              <p className="text-blue-100/70 text-sm italic max-w-md leading-relaxed mb-6">"{primaryEvent.event_info}"</p>
            )}
            {primaryEvent?.event_url && primaryEvent.event_url !== "null" && (
              <a href={primaryEvent.event_url} target="_blank" rel="noreferrer" className="inline-flex items-center gap-2 text-[10px] font-black uppercase tracking-[0.2em] bg-white/10 hover:bg-white/20 px-4 py-2 rounded-lg transition-all">
                Event Website →
              </a>
            )}
          </div>

          <div className="flex justify-start lg:justify-end gap-12">
            <div className="text-center">
              <p className="text-7xl font-black italic leading-none">{primaryEvent ? getDaysUntil(primaryEvent.event_date) : '--'}</p>
              <p className="text-blue-200 text-[10px] uppercase font-bold tracking-widest mt-2">Days Remaining</p>
            </div>
            <div className="text-center border-l border-white/20 pl-12">
              <p className="text-7xl font-black italic leading-none text-blue-300">3:30</p>
              <p className="text-blue-200 text-[10px] uppercase font-bold tracking-widest mt-2">Target Time</p>
            </div>
          </div>
        </div>
        <div className="absolute -right-8 -bottom-8 text-[14rem] font-black opacity-10 italic select-none leading-none">GO</div>
      </div>

      {/* 3. Middle Metrics Row */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        
        {/* Weekly Volume Block */}
        <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6 backdrop-blur-sm flex flex-col justify-between">
          <div>
            <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-5">
              Weekly Volume
            </h4>

            {/* RUN + SWIM */}
            <div className="grid grid-cols-2 gap-4 mb-6">
              <div>
                <p className="text-[9px] font-bold text-slate-500 uppercase">Running</p>
                <div className="flex items-end gap-1 mt-1">
                  <span className="text-4xl font-black italic text-blue-400">
                    {(insights.running_distance_metres / 1000).toFixed(1)}
                  </span>
                  <span className="text-xs font-bold text-slate-500 mb-1">KM</span>
                </div>

                <p className="text-[10px] text-slate-600 mt-1 font-mono">
                  Total Time: {formatTime(insights.running_time_seconds)}
                </p>
                <p className="text-[10px] text-slate-600 mt-1 font-mono">
                  Moving Time: {formatTime(insights.running_moving_time_seconds)}
                </p>
              </div>

              <div className="border-l border-slate-800/80 pl-4">
                <p className="text-[9px] font-bold text-slate-500 uppercase">Swimming</p>
                <div className="flex items-end gap-1 mt-1">
                  <span className="text-4xl font-black italic text-purple-400">
                    {insights.swimming_distance_metres}
                  </span>
                  <span className="text-xs font-bold text-slate-500 mb-1">M</span>
                </div>

                <p className="text-[10px] text-slate-600 mt-1 font-mono">
                  Swimming Time: {formatTime(insights.swimming_time_seconds)}
                </p>
              </div>
            </div>

            <hr className="border-slate-800/60 my-5" />

            {/* ================= ACTIVITY DISTRIBUTION BAR ================= */}
            <div className="mb-6">
              <p className="text-[9px] font-bold text-slate-500 uppercase tracking-wider mb-2">
                Activity Time Distribution
              </p>

              {/* BAR */}
              <div className="flex w-full h-3 rounded-full overflow-hidden bg-slate-800">
                {segments.map((s, idx) => (
                  <div
                    key={idx}
                    className={`flex-1 transition-all duration-300 ${
                      s.count > 0 ? s.color : "bg-slate-700"
                    }`}
                  />
                ))}
              </div>

              {/* LABELS */}
              <div className="flex justify-between mt-2 text-[9px] text-slate-500 font-mono">
                {segments.map((s, idx) => (
                  <span key={idx}>
                    {s.label} {s.count}
                  </span>
                ))}
              </div>
            </div>

            {/* ================= SECONDARY STATS ================= */}
            <div className="grid grid-cols-2 sm:grid-cols-3 gap-y-4 gap-x-3 text-left">
              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Elevation Gain</p>
                <p className="text-sm font-black text-white font-mono">
                  {insights.total_running_elevation_gain.toFixed(0)}m
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Average Heart Rate</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  {insights.mean_average_heart_rate ? `${insights.mean_average_heart_rate} bpm` : "--"}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Max Heart Rate</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  {insights.mean_max_heart_rate ? `${insights.mean_max_heart_rate} bpm` : "--"}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Training Score</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  {insights.mean_training_effect ? insights.mean_training_effect.toFixed(1) : "--"}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Total Score</p>
                <p className="text-sm font-black text-white font-mono">
                  {insights.total_training_effect ? insights.total_training_effect.toFixed(1) : "--"}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Calories</p>
                <p className="text-sm font-black text-orange-400 font-mono">
                  {insights.total_calories_burned}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Time spent resting</p>
                <p className="text-sm font-black text-slate-500 font-mono">
                  {(insights.running_time_break_seconds / 60).toFixed(0)}m
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Moving Efficiency</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  {insights.running_moving_efficiency.toFixed(0)}%
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Caloric Intensity</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  {insights.caloric_intensity.toFixed(0)}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Elevation Intensity</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  {insights.elevation_intensity.toFixed(1)}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Rest Days</p>
                <p className="text-sm font-black text-emerald-400 font-mono">
                  {insights.rest_days}
                </p>
              </div>

              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Consistency</p>
                <p className="text-sm font-black text-orange-400 font-mono">
                  {insights.training_consistency_score}
                </p>
              </div>
            </div>
          </div>

          {/* ================= LOCATIONS ================= */}
          <div className="mt-6 pt-4 border-t border-slate-800 text-[10px] text-slate-600 font-mono uppercase tracking-tight">
            {insights.locations.length > 0 ? (
              <div className="flex flex-col gap-1">
                {insights.locations.map((loc, idx) => (
                  <span key={idx} className="truncate">{loc}</span>
                ))}
              </div>
            ) : (
              "No locations recorded"
            )}
          </div>
        </div>

        {/* AI Mate */}
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

        {/* Schedule Summary */}
        <div className="rounded-3xl border border-slate-800 bg-slate-900 p-6">
          <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-4">Next Up</h4>
          {upcomingEvents.slice(0, 3).map((event, i) => (
            <div key={i} className="flex items-center gap-4 mb-3 last:mb-0 p-2 rounded-xl hover:bg-white/5 transition-colors">
              <div className="text-center bg-blue-500/10 p-2 rounded-lg min-w-[45px]">
                <p className="text-[10px] font-black text-blue-400 uppercase leading-none mb-1">
                  {new Date(event.event_date).toLocaleString('default', { month: 'short' })}
                </p>
                <p className="text-lg font-black text-white leading-none">{new Date(event.event_date).getDate()}</p>
              </div>
              <div className="group p-4 rounded-2xl transition-all w-full min-w-0">
                <p className="text-xs font-bold text-white leading-tight uppercase truncate">
                  <a 
                    href={event.event_url} 
                    target="_blank" 
                    rel="noopener noreferrer" 
                    className="transition-colors duration-200 hover:text-blue-400"
                  >
                    {event.event_name}
                  </a>
                </p>
                <p className="pt-2 text-[10px] text-slate-600 font-mono uppercase truncate">{event.event_location}</p>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* 4. Personal Records */}
      <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6">
        <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-6">
          Personal Records
        </h4>
        <div className="grid grid-cols-2 md:grid-cols-5 gap-4">
          {bestEfforts.map((effort) => {
            const isEditing = editingLabel === effort.distance_label;

            return (
              <div 
                key={effort.distance_label} 
                className={`group p-4 rounded-2xl border transition-all ${
                  isEditing 
                    ? "bg-slate-950 border-blue-500 shadow-lg shadow-blue-500/10" 
                    : "bg-black/20 border-slate-800/50 hover:border-blue-500/50"
                }`}
              >
                <p className="text-[10px] font-bold text-slate-500 uppercase mb-1">
                  {effort.distance_label}
                </p>

                <div className="flex justify-between items-end h-7">
                  {isEditing ? (
                    <div className="flex items-center gap-2 w-full">
                      <input
                        type="number"
                        value={tempSeconds}
                        onChange={(e) => setTempSeconds(e.target.value)}
                        placeholder="Secs"
                        className="w-full bg-slate-900 text-white font-mono font-bold text-sm px-2 py-1 rounded border border-slate-700 focus:outline-none focus:border-blue-500 [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
                        autoFocus
                        onKeyDown={(e) => {
                          if (e.key === 'Enter') {
                            const parsed = parseInt(tempSeconds);
                            if (!isNaN(parsed)) {
                              handleUpdateEffort(effort.distance_label, parsed);
                              setEditingLabel(null);
                            }
                          }
                        }}
                      />
                      <button
                        onClick={() => {
                          if (tempSeconds) {
                            handleUpdateEffort(effort.distance_label, parseInt(tempSeconds));
                          }
                          setEditingLabel(null);
                        }}
                        className="text-[10px] bg-emerald-600 hover:bg-emerald-500 text-white px-2 py-1 rounded font-black uppercase tracking-tight"
                      >
                        Save
                      </button>
                      <button
                        onClick={() => setEditingLabel(null)}
                        className="text-[10px] bg-slate-800 hover:bg-slate-700 text-slate-400 px-2 py-1 rounded font-black uppercase tracking-tight"
                      >
                        ×
                      </button>
                    </div>
                  ) : (
                    <>
                      <span className="font-mono font-black text-blue-400 text-xl leading-none">
                        {effort.time_achievement !== null ? formatTime(effort.time_achievement) : "--:--"}
                      </span>
                      <button 
                        onClick={() => {
                          setTempSeconds(effort.time_achievement?.toString() || "");
                          setEditingLabel(effort.distance_label);
                        }}
                        className="opacity-0 group-hover:opacity-100 text-[9px] bg-blue-600 hover:bg-blue-500 text-white px-2 py-1 rounded font-black uppercase tracking-tighter transition-all cursor-pointer"
                      >
                        Set
                      </button>
                    </>
                  )}
                </div>
              </div>
            );
          })}
        </div>
      </div>

    </div>
  );
};