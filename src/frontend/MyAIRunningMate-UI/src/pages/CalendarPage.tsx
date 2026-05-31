import { useEffect, useState, useMemo } from 'react';
import { calendarService } from '../services/api/calendar/calendar.service';
import { buildCalendarMonth } from '../services/helpers/calendar.utils';
import type { CalendarViewDto } from '../types/views/calendarView';
import type { TrainingPlanView, TrainingPlanEventView } from '../types/views/trainingPlanView';
import { DayCell } from '../components/calendar/Daycell';

export const CalendarPage = () => {
  const [activities, setActivities] = useState<CalendarViewDto[]>([]);
  const [trainingPlan, setTrainingPlan] = useState<TrainingPlanView | null>(null);
  const [viewDate, setViewDate] = useState(() => {
    const now = new Date();
    return new Date(now.getFullYear(), now.getMonth(), 1);
  });
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    let active = true;

    setActivities([]);
    setTrainingPlan(null);

    const load = async () => {
      try {
        setIsLoading(true);
        const targetMonth = viewDate.getMonth() + 1;
        const targetYear = viewDate.getFullYear();

        const [activitiesData, planData] = await Promise.all([
          calendarService.getMonthlyActivities(targetMonth, targetYear),
          calendarService.getTrainingPlan(targetMonth, targetYear),
        ]);

        if (!active) return;

        setActivities(activitiesData);
        setTrainingPlan(planData);
      } catch (err) {
        console.error('Error hydrating calendar matrix data maps:', err);
      } finally {
        if (active) {
          setIsLoading(false);
        }
      }
    };

    load();

    return () => {
      active = false;
    };
  }, [viewDate]);

  const { days, blanks, monthLabel } = useMemo(() => buildCalendarMonth(viewDate), [viewDate]);

  const groupedActivities = useMemo(() => {
    const map = new Map<number, CalendarViewDto[]>();
    if (activities.length === 0) return map;

    activities.forEach((activity) => {
      const day = new Date(activity.start_time).getDate();
      const existing = map.get(day);

      if (existing) {
        existing.push(activity);
      } else {
        map.set(day, [activity]);
      }
    });

    return map;
  }, [activities]);

  const groupedTrainingEvents = useMemo(() => {
    const map = new Map<number, TrainingPlanEventView>();
    if (!trainingPlan?.trainingPlanEvents?.length) return map;

    const currentYear = viewDate.getFullYear();
    const currentMonth = viewDate.getMonth();

    trainingPlan.trainingPlanEvents.forEach((ev) => {
      const eventDate = new Date(ev.eventDate);
      
      if (eventDate.getFullYear() === currentYear && eventDate.getMonth() === currentMonth) {
        map.set(eventDate.getDate(), ev);
      }
    });

    return map;
  }, [trainingPlan, viewDate]);

  const navigateMonth = (direction: number) => {
    setViewDate(new Date(viewDate.getFullYear(), viewDate.getMonth() + direction, 1));
  };

  return (
    <div className="p-6 space-y-6">
      <div className="flex justify-between items-center">
        <div>
          <h2 className="text-3xl font-bold text-white tracking-tight">Activity Matrix</h2>
          {trainingPlan && (
            <p className="text-xs font-medium text-blue-400 mt-1 uppercase tracking-wider">
              🏆 Active Track: {trainingPlan.title}
            </p>
          )}
        </div>

        <div className="flex items-center gap-4 bg-slate-900 border border-slate-800 rounded-lg p-1">
          <button
            onClick={() => navigateMonth(-1)}
            className="hover:bg-slate-800 p-2 rounded text-slate-400"
          >
            ‹
          </button>
          <span className="font-bold text-slate-200 min-w-[120px] text-center">{monthLabel}</span>
          <button
            onClick={() => navigateMonth(1)}
            className="hover:bg-slate-800 p-2 rounded text-slate-400"
          >
            ›
          </button>
        </div>
      </div>

      <div
        key={`${viewDate.getFullYear()}-${viewDate.getMonth()}`}
        className={`grid grid-cols-7 gap-2 transition-opacity duration-300 ${
          isLoading ? 'opacity-40 pointer-events-none' : 'opacity-100'
        }`}
      >
        {['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'].map((day) => (
          <div
            key={day}
            className="text-center text-[10px] font-black text-slate-600 uppercase py-2"
          >
            {day}
          </div>
        ))}

        {blanks.map((i) => (
          <div key={`blank-${i}`} className="min-h-[140px]" />
        ))}

        {days.map((day) => {
          const today = new Date();
          
          const isToday = 
            day === today.getDate() &&
            viewDate.getMonth() === today.getMonth() &&
            viewDate.getFullYear() === today.getFullYear();

          return (
            <DayCell
              key={day}
              day={day}
              activities={groupedActivities.get(day) || []}
              trainingEvent={groupedTrainingEvents.get(day)}
              isToday={isToday}
            />
          );
        })}
      </div>
    </div>
  );
};