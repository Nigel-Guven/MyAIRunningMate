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
    const loadDashboard = async () => {
      // 1. Verify a token exists locally before executing the batch API cascade
      const localToken = authStorage.get();
      if (!localToken) {
        setError('Session initializing. Please wait...');
        return;
      }

      try {
        setLoading(true);
        const data = await dashboardService.loadDashboard();
        setDashboard(data);
      } catch (err) {
        setError('Failed to load command center.');
      } finally {
        setLoading(false);
      }
    };
    
    loadDashboard();
  }, []);

  if (loading) return <div className="p-12 text-slate-500 font-mono animate-pulse uppercase">Synchronizing Command Center...</div>;
  if (error) return <div className="p-12 text-red-400">{error}</div>;

  const { primaryEvent, upcomingEvents, bestEfforts, latestWeight, weeklyInsights } = dashboard;

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
            <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-4">Weekly Volume</h4>

            {/* Main Multi-Sport Headers */}
            <div className="grid grid-cols-2 gap-4 mb-6">
              <div>
                <p className="text-[9px] font-bold text-slate-500 uppercase tracking-wider">Total Running</p>
                <div className="flex items-end gap-1 mt-1">
                  <span className="text-4xl font-black italic text-blue-400">
                    {((weeklyInsights?.running_distance_metres ?? 0) / 1000).toFixed(1)}
                  </span>
                  <span className="text-xs font-bold text-slate-500 mb-1">KM</span>
                </div>
              </div>

              <div className="border-l border-slate-800/80 pl-4">
                <p className="text-[9px] font-bold text-slate-500 uppercase tracking-wider">Total Swimming</p>
                <div className="flex items-end gap-1 mt-1">
                  <span className="text-4xl font-black italic text-purple-400">
                    {weeklyInsights?.swimming_distance_metres}
                  </span>
                  <span className="text-xs font-bold text-slate-500 mb-1">M</span>
                </div>
              </div>
            </div>

            <hr className="border-slate-800/60 my-4" />

            {/* Secondary Metrics Sub-Grid */}
            <div className="grid grid-cols-2 sm:grid-cols-3 gap-y-4 gap-x-2 text-left">
              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Elevation</p>
                <p className="text-sm font-black text-white font-mono">{weeklyInsights?.total_running_elevation_gain.toFixed(0)}m</p>
              </div>
              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Avg HR</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  { (weeklyInsights?.mean_average_heart_rate ?? 0) > 0 ? `${weeklyInsights?.mean_average_heart_rate} bpm` : '--'}
                </p>
              </div>
              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Training Effect</p>
                <p className="text-sm font-black text-slate-400 font-mono">
                  { (weeklyInsights?.mean_training_effect ?? 0) > 0 ? weeklyInsights?.mean_training_effect.toFixed(1) : '--' }
                </p>
              </div>
              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Eff. Factor</p>
                <p className="text-sm font-black text-slate-500 font-mono">
                  {weeklyInsights?.running_moving_efficiency.toFixed(0)}%
                </p>
              </div>
              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Total Time</p>
                <p className="text-sm font-black text-white font-mono">
                  { Math.floor(((weeklyInsights?.running_time_seconds ?? 0) + (weeklyInsights?.swimming_time_seconds ?? 0)) / 3600) }h 
                  { Math.floor((((weeklyInsights?.running_time_seconds ?? 0) + (weeklyInsights?.swimming_time_seconds ?? 0)) % 3600) / 60) }m
                </p>
              </div>
              <div>
                <p className="text-[9px] font-bold text-slate-600 uppercase">Rest Days</p>
                <p className="text-sm font-black text-emerald-400 font-mono">{weeklyInsights?.rest_days}</p>
              </div>
            </div>
          </div>

          {/* Location Footer tag */}
          <div className="mt-5 pt-3 border-t border-slate-800 text-[10px] text-slate-600 font-mono uppercase tracking-tight truncate">
            {(weeklyInsights?.locations ?? []).length > 0 ? weeklyInsights?.locations.join(' • ') : 'No locations recorded'}
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
              <div className="group p-4 rounded-2xl transition-all">
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
                <p className="pt-4 text-[10px] text-slate-600 font-mono uppercase">{event.event_location}</p>
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