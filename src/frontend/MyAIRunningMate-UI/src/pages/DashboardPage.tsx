import { useState, useEffect } from 'react';
import logo from '../assets/applogo.png';

import { dashboardService } from '../services/api/home/dashboard.service';
import type { DashboardData } from '../types/dashboard.types';

import { formatTime } from '../services/helpers/formatTime';

import { getDaysUntil } from '../services/helpers/getDaysUntil';

const initialState: DashboardData = {
  primaryEvent: null,
  upcomingEvents: [],
  bestEfforts: [],
  latestWeight: null,
};

export const HomePage = () => {

  const [dashboard, setDashboard] =
    useState<DashboardData>(
      initialState
    );

  const [loading, setLoading] =
    useState(true);

  const [error, setError] =
    useState<string | null>(null);

  useEffect(() => {

    const loadDashboard = async () => {

      try {
        setLoading(true);

        const data =
          await dashboardService
            .loadDashboard();

        setDashboard(data);

      } catch (err) {
        console.error(err);

        setError(
          'Failed to load command center.'
        );

      } finally {
        setLoading(false);
      }
    };

    loadDashboard();

  }, []);

  if (loading) {
    return (
      <div className="p-12 text-slate-500 font-mono animate-pulse">
        SYNCHRONIZING COMMAND CENTER...
      </div>
    );
  }

  if (error) {
    return (
      <div className="p-12 text-red-400">
        {error}
      </div>
    );
  }

  const { primaryEvent, upcomingEvents, bestEfforts, latestWeight, } = dashboard;

  return (
    <div className="space-y-8 animate-in fade-in duration-700">
      {/* Header Section */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <img src={logo} alt="Logo" className="h-16 w-16 rounded-xl shadow-lg shadow-blue-900/20" />
          <div>
            <h2 className="text-3xl font-bold tracking-tighter uppercase italic">Command Center</h2>
            <p className="text-slate-400 font-medium">Welcome back, Nigel.</p>
          </div>
        </div>
        
        <div className="flex items-center gap-8 bg-slate-900/50 p-4 rounded-2xl border border-slate-800">
          <div className="text-right">
            <p className="text-[10px] text-slate-500 uppercase font-black tracking-widest">Latest Weigh-in</p>
            <p className="text-2xl font-black text-blue-400 italic">
              {latestWeight?.weight_pounds ? (latestWeight.weight_pounds * 0.453592).toFixed(1) : '0.0'} 
              <span className="text-xs text-slate-600 not-italic ml-1">KG</span>
            </p>
          </div>
        </div>
      </div>

      {/* Bento Grid */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        
        {/* Primary Event - Countdown Card */}
        <div className="md:col-span-2 relative overflow-hidden rounded-3xl bg-gradient-to-br from-blue-600 to-blue-900 p-8 text-white shadow-2xl shadow-blue-900/40">
          <div className="relative z-10">
            <p className="text-blue-200 uppercase text-[10px] font-black tracking-[0.3em]">Primary Objective</p>
            <h3 className="text-5xl font-black mt-2 italic tracking-tighter uppercase">
              {primaryEvent?.name || "No Event Set"}
            </h3>
            
            <div className="mt-10 flex gap-12">
              <div>
                <p className="text-5xl font-black italic">{primaryEvent ? getDaysUntil(primaryEvent.event_date) : '--'}</p>
                <p className="text-blue-200 text-[10px] uppercase font-bold tracking-widest mt-1">Days Remaining</p>
              </div>
              <div className="border-l border-blue-400/30 pl-12">
                <p className="text-5xl font-black italic">SUB 1:45</p>
                <p className="text-blue-200 text-[10px] uppercase font-bold tracking-widest mt-1">Target Pace</p>
              </div>
            </div>
          </div>
          {/* Abstract background element */}
          <div className="absolute -right-12 -bottom-12 text-[12rem] font-black opacity-10 italic select-none">RUN</div>
        </div>

        {/* Best Efforts Column */}
        <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6 backdrop-blur-sm">
          <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-6">Personal Records</h4>
          <div className="space-y-3">
            {bestEfforts.map((effort) => (
              <div key={effort.distance_label} className="flex justify-between items-center p-3 rounded-xl bg-slate-800/30 border border-slate-800/50">
                <span className="text-sm font-bold text-slate-400">{effort.distance_label}</span>
                <span className="font-mono font-black text-blue-400 text-lg">{formatTime(effort.time_seconds)}</span>
              </div>
            ))}
          </div>
        </div>

        {/* AI Insight Card */}
        <div className="rounded-3xl border border-slate-800 bg-slate-900 p-6 flex flex-col justify-between">
          <div>
            <div className="flex items-center gap-2 mb-4">
              <span className="h-2 w-2 rounded-full bg-purple-500 animate-pulse" />
              <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em]">Nexus AI Mate</h4>
            </div>
            <p className="text-slate-300 italic leading-relaxed text-sm">
              "Ingestion complete. Your training load is up 12% this week. Focus on pool recovery tomorrow to keep your shins fresh for Sunday's long run."
            </p>
          </div>
          <div className="mt-6 pt-4 border-t border-slate-800">
             <span className="text-[10px] text-slate-600 font-mono uppercase font-bold">Status: Awaiting more data...</span>
          </div>
        </div>

        {/* Upcoming Timeline */}
        <div className="md:col-span-2 rounded-3xl border border-slate-800 bg-slate-900 p-6">
          <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-6">Upcoming Schedule</h4>
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {upcomingEvents.map((event, idx) => (
              <div key={idx} className="flex items-center gap-4 p-4 rounded-2xl border border-slate-800 bg-black/20">
                <div className="text-center min-w-[50px]">
                  <p className="text-xs font-black text-blue-500 uppercase">
                    {new Date(event.event_date).toLocaleString('default', { month: 'short' })}
                  </p>
                  <p className="text-xl font-black text-white">{new Date(event.event_date).getDate()}</p>
                </div>
                <div>
                  <p className="text-sm font-bold text-white leading-tight">{event.name}</p>
                  <p className="text-[10px] text-slate-500 uppercase font-bold">{event.location}</p>
                </div>
              </div>
            ))}
          </div>
        </div>

      </div>
    </div>
  );
};