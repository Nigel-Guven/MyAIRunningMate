import { useEffect, useState, useMemo } from 'react';
import { calendarService } from '../services/api/calendar/calendar.service';
import { buildCalendarMonth } from '../services/helpers/calendar.utils';

import type { CalendarViewResponse } from '../types/calendar/calendarViewResponse';
import type { TrainingPlanViewResponse } from '../types/nexus/trainingPlanViewResponse';
import type { TrainingPlanEventResponse } from '../types/nexus/trainingPlanEventResponse';
import { CalendarHeader } from '../components/calendar/CalendarHeader';
import { CalendarNavigation } from '../components/calendar/CalendarNavigation';
import { CalendarGrid } from '../components/calendar/CalendarGrid';

export const CalendarPage = () => {
  const [activities, setActivities] = useState<CalendarViewResponse[]>([]);
  const [trainingPlan, setTrainingPlan] = useState<TrainingPlanViewResponse | null>(null);
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
        if (active) setIsLoading(false);
      }
    };

    load();
    return () => { active = false; };
  }, [viewDate]);

  const { days, blanks, monthLabel } = useMemo(() => buildCalendarMonth(viewDate), [viewDate]);

  const groupedActivities = useMemo(() => {
    const map = new Map<number, CalendarViewResponse[]>();
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
    const map = new Map<number, TrainingPlanEventResponse>();
    if (!trainingPlan?.events?.length) return map;

    const currentYear = viewDate.getFullYear();
    const currentMonth = viewDate.getMonth();

    trainingPlan.events.forEach((ev) => {
      const eventDate = new Date(ev.event_date + 'Z');
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
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-end border-b border-slate-800 pb-6 gap-4">
        <CalendarHeader trainingPlan={trainingPlan} />
        <CalendarNavigation monthLabel={monthLabel} onNavigate={navigateMonth} />
      </div>

      <CalendarGrid
        days={days}
        blanks={blanks}
        viewDate={viewDate}
        isLoading={isLoading}
        groupedActivities={groupedActivities}
        groupedTrainingEvents={groupedTrainingEvents}
      />
    </div>
  );
};