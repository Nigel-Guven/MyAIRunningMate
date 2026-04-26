// src/pages/CalendarPage.tsx
import React, { useEffect, useState } from 'react';
import { getMonthlyActivities } from '../services/api';
import type { AggregateArtifactDto } from '../types/aggregateArtifact';

export const CalendarPage = () => {
  const [activities, setActivities] = useState<AggregateArtifactDto[]>([]);
  const [viewDate, setViewDate] = useState(new Date(2026, 3, 1)); // Starts at April 2026

  useEffect(() => {
    const loadData = async () => {
      const data = await getMonthlyActivities(viewDate.getMonth() + 1, viewDate.getFullYear());
      setActivities(data);
    };
    loadData();
  }, [viewDate]);

 
  const daysInMonth = new Date(viewDate.getFullYear(), viewDate.getMonth() + 1, 0).getDate();

  const firstDayIndex = new Date(viewDate.getFullYear(), viewDate.getMonth(), 1).getDay();
  const offset = firstDayIndex === 0 ? 6 : firstDayIndex - 1;

  const navigateMonth = (direction: number) => {
    setViewDate(new Date(viewDate.getFullYear(), viewDate.getMonth() + direction, 1));
  };

  return (
    <div className="p-6 space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-3xl font-bold text-white tracking-tight">Activity Matrix</h2>
        <div className="flex items-center gap-4 bg-slate-900 border border-slate-800 rounded-lg p-1">
          <button onClick={() => navigateMonth(-1)} className="hover:bg-slate-800 p-2 rounded text-slate-400 transition-colors">‹</button>
          <span className="font-bold text-slate-200 min-w-[120px] text-center">
            {viewDate.toLocaleString('default', { month: 'long', year: 'numeric' })}
          </span>
          <button onClick={() => navigateMonth(1)} className="hover:bg-slate-800 p-2 rounded text-slate-400 transition-colors">›</button>
        </div>
      </div>

      <div className="grid grid-cols-7 gap-2">
        {['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'].map(day => (
          <div key={day} className="text-center text-[10px] font-black text-slate-600 uppercase py-2 tracking-widest">{day}</div>
        ))}

        {/* Blank days for calendar alignment */}
        {Array.from({ length: offset }).map((_, i) => (
          <div key={`blank-${i}`} className="min-h-[140px] rounded-lg bg-slate-950/20 border border-transparent" />
        ))}

        {/* Actual Days */}
        {Array.from({ length: daysInMonth }).map((_, i) => {
          const dayNum = i + 1;
          const dayActivities = activities.filter(a => new Date(a.startTime).getDate() === dayNum);

          return (
            <div key={dayNum} className="min-h-[140px] rounded-lg border border-slate-800 bg-slate-900/40 p-2 hover:border-blue-500/50 transition-all group">
              <span className="text-xs font-medium text-slate-600 group-hover:text-slate-400">{dayNum}</span>
              
              <div className="mt-2 space-y-1">
                {dayActivities.map(act => (
                  <div 
                    key={act.garminActivityId} 
                    className="p-1.5 rounded bg-blue-600/10 border border-blue-600/30 cursor-pointer hover:bg-blue-600/20"
                  >
                    <div className="flex justify-between items-start">
                      <span className="text-[9px] font-black text-blue-400 uppercase truncate">
                        {act.exerciseType}
                      </span>
                      {act.stravaId && <div className="w-1.5 h-1.5 rounded-full bg-orange-500" title="Synced with Strava" />}
                    </div>
                    <p className="text-xs font-bold text-slate-200">
                      {(act.distanceMetres / 1000).toFixed(1)}k
                    </p>
                    <div className="flex justify-between text-[8px] text-slate-500 mt-0.5">
                       <span>TE {act.trainingEffect.toFixed(1)}</span>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
};