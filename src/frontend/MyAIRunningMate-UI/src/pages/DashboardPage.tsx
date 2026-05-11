import { useState, useEffect } from 'react';
import logo from '../assets/applogo.png';
import { dashboardService } from '../services/api/home/dashboard.service';
import type { DashboardData } from '../types/dashboard.types';
import { formatTime } from '../services/helpers/formatTime';
import { getDaysUntil } from '../services/helpers/getDaysUntil';
import type { BestEffortRequest } from '../types/bestefforts.types';

const initialState: DashboardData = {
  primaryEvent: null,
  upcomingEvents: [],
  bestEfforts: [],
  latestWeight: null,
};

export const DashboardPage = () => {
  const [dashboard, setDashboard] = useState<DashboardData>(initialState);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const handleUpdateEffort = async (label: string, seconds: number) => {
    try {
      const payload: BestEffortRequest = {
        distance_label: label,
        time_seconds: seconds,
        achieved_at: new Date().toISOString()
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

  const { primaryEvent, upcomingEvents, bestEfforts, latestWeight } = dashboard;

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
            {latestWeight?.weight_pounds ? (latestWeight.weight_pounds * 0.453592).toFixed(1) : '0.0'} 
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
              {primaryEvent?.name || "No Objective Set"}
            </h3>
            <div className="flex flex-wrap gap-6 text-blue-100/80 mb-6">
              <div className="flex flex-col">
                <span className="text-[10px] uppercase font-bold opacity-60">Location</span>
                <span className="text-sm font-bold uppercase">{primaryEvent?.location || 'Unknown'}</span>
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
              <a href={primaryEvent.event_url} target="_blank" className="inline-flex items-center gap-2 text-[10px] font-black uppercase tracking-[0.2em] bg-white/10 hover:bg-white/20 px-4 py-2 rounded-lg transition-all">
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
        
        {/* Weekly Volume */}
        <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6 backdrop-blur-sm">
          <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-4">Weekly Volume</h4>
          <div className="flex items-end gap-2 mb-4">
            <span className="text-4xl font-black italic text-white text-blue-400">42.8</span>
            <span className="text-slate-500 font-bold text-sm mb-1">/ 60 KM</span>
          </div>
          <div className="w-full h-2 bg-slate-800 rounded-full overflow-hidden">
            <div className="h-full bg-blue-500" style={{ width: '71%' }} />
          </div>
          <p className="text-[10px] text-slate-600 mt-4 font-mono uppercase font-bold">On track for peak mileage week</p>
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
              <p className="text-xs font-bold text-white leading-tight uppercase truncate">{event.name}</p>
              <p className="pt-4 text-[10px] text-slate-600 font-mono uppercase">{event.location}</p>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* 4. Personal Records (Bottom Wide Section) */}
      <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6">
        <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-6">Personal Records</h4>
        <div className="grid grid-cols-2 md:grid-cols-5 gap-4">
          {bestEfforts.map((effort) => (
            <div key={effort.distance_label} className="group p-4 rounded-2xl bg-black/20 border border-slate-800/50 hover:border-blue-500/50 transition-all">
              <p className="text-[10px] font-bold text-slate-500 uppercase mb-1">{effort.distance_label}</p>
              <div className="flex justify-between items-end">
                <span className="font-mono font-black text-blue-400 text-xl leading-none">
                  {formatTime(effort.time_seconds)}
                </span>
                <button 
                  onClick={() => {
                    const newTime = prompt(`Seconds for ${effort.distance_label}:`);
                    if (newTime) handleUpdateEffort(effort.distance_label, parseInt(newTime));
                  }}
                  className="opacity-0 group-hover:opacity-100 text-[9px] bg-blue-600 text-white px-2 py-1 rounded font-black uppercase tracking-tighter transition-all"
                >
                  Set
                </button>
              </div>
            </div>
          ))}
        </div>
      </div>

    </div>
  );
};