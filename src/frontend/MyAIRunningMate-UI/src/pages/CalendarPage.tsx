import { useEffect, useState, useMemo } from 'react';
import { getMonthlyActivities } from '../services/api';
import { getActivityStyles } from '../components/calendar/styles';
import type { AggregateArtifactDto } from '../types/aggregateArtifact';

export const CalendarPage = () => {
  const [activities, setActivities] = useState<AggregateArtifactDto[]>([]);
  const [viewDate, setViewDate] = useState(new Date(2026, 3, 1)); // Starts at April 2026
  const [isLoading, setIsLoading] = useState(false)


  useEffect(() => {
    let isCurrent = true;
    const controller = new AbortController();

    const loadData = async () => {

      setActivities([]);
      setIsLoading(true);
      try {
        const data = await getMonthlyActivities(
          viewDate.getMonth() + 1, 
          viewDate.getFullYear()
        );
        
        if (isCurrent) {
          setActivities(data);
          setIsLoading(false);
        }
      } catch (err) {
        if (isCurrent) setIsLoading(false);
      }
    };

    loadData();

    return () => {
      isCurrent = false;
      controller.abort();
    };
  }, [viewDate]);

 
  const { days, blanks, monthLabel } = useMemo(() => {
    const year = viewDate.getFullYear();
    const month = viewDate.getMonth();
    
    const daysInMonth = new Date(year, month + 1, 0).getDate();
    const firstDayIndex = new Date(year, month, 1).getDay();
    const offset = firstDayIndex === 0 ? 6 : firstDayIndex - 1;

    return {
      days: Array.from({ length: daysInMonth }, (_, i) => i + 1),
      blanks: Array.from({ length: offset }, (_, i) => i),
      monthLabel: viewDate.toLocaleString('default', { month: 'long', year: 'numeric' })
    };
  }, [viewDate]);

  const navigateMonth = (direction: number) => {
    setViewDate(new Date(viewDate.getFullYear(), viewDate.getMonth() + direction, 1));
  };

  return (
    <div className="p-6 space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-3xl font-bold text-white tracking-tight">Activity Matrix</h2>
        <div className="flex items-center gap-4 bg-slate-900 border border-slate-800 rounded-lg p-1">
          <button onClick={() => navigateMonth(-1)} className="hover:bg-slate-800 p-2 rounded text-slate-400">‹</button>
          <span className="font-bold text-slate-200 min-w-[120px] text-center">{monthLabel}</span>
          <button onClick={() => navigateMonth(1)} className="hover:bg-slate-800 p-2 rounded text-slate-400">›</button>
        </div>
      </div>

      <div 
        key={`grid-${viewDate.getFullYear()}-${viewDate.getMonth()}`}
        className={`grid grid-cols-7 gap-2 transition-opacity duration-300 ${isLoading ? 'opacity-40 pointer-events-none' : 'opacity-100'}`}>
        {['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'].map(day => (
          <div key={day} className="text-center text-[10px] font-black text-slate-600 uppercase py-2">{day}</div>
        ))}

        {blanks.map(i => (
          <div key={`blank-${i}`} className="min-h-[140px] rounded-lg bg-slate-950/20 border border-transparent" />
        ))}

        {days.map(day => (
          <DayCell 
            key={`${viewDate.toISOString()}-${day}`} 
            day={day} 
            activities={activities.filter(a => {
              const actDate = new Date(a.startTime);
              // Validate the full date context, not just the day number
              return (
                actDate.getDate() === day &&
                actDate.getMonth() === viewDate.getMonth() &&
                actDate.getFullYear() === viewDate.getFullYear()
              );
            })} 
          />
        ))}
      </div>
    </div>
  );
};

const DayCell = ({ day, activities }: { day: number, activities: AggregateArtifactDto[] }) => {
  const hasActivity = activities.length > 0;

  return (
    <div className={`
      min-h-[140px] rounded-lg border p-2 transition-all group
      ${hasActivity 
        ? 'border-slate-800 bg-slate-900/40 hover:border-blue-500/50' 
        : 'border-slate-900/50 bg-slate-950/20 opacity-50'}
    `}>
      <span className={`text-xs font-medium ${hasActivity ? 'text-slate-500' : 'text-slate-700'}`}>
        {day}
      </span>
      <div className="mt-2 space-y-1">
        {activities.map(act => (
          <ActivityTile key={act.garminActivityId} act={act} />
        ))}
      </div>
    </div>
  );
};

const ActivityTile = ({ act }: { act: AggregateArtifactDto }) => {
  const colorClasses = getActivityStyles(act.exerciseType);

  return (
    <div className={`p-1.5 rounded border cursor-pointer transition-colors ${colorClasses} hover:brightness-125`}>
      <div className="flex justify-between items-start">
        <span className="text-[9px] font-black uppercase truncate">{act.exerciseType}</span>
        {act.stravaId && <div className="w-1.5 h-1.5 rounded-full bg-orange-500" />}
      </div>
      <p className="text-xs font-bold">{(act.distanceMetres / 1000).toFixed(1)}k</p>
      <div className="text-[8px] opacity-70 mt-0.5 font-medium">
        TE {act.trainingEffect.toFixed(1)}
      </div>
    </div>
  );
};

