import { useEffect, useState, useMemo } from 'react';
import { calendarService } from '../services/api/calendar/calendar.service';
import { buildCalendarMonth } from '../services/helpers/calendar.utils';
import type { CalendarViewDto } from '../types/views/calendarView';
import { DayCell } from '../components/calendar/Daycell';

export const CalendarPage = () => {
  const [activities, setActivities] = useState<CalendarViewDto[]>([]);
  const [viewDate, setViewDate] = useState(new Date(2026, 3, 1)); // Starts at April 2026
  const [isLoading, setIsLoading] = useState(false)


  useEffect(() => {
    let active = true;

    const load = async () => {

      try {
        setIsLoading(true);

        const data =
          await calendarService.getMonthlyActivities(
              viewDate.getMonth() + 1,
              viewDate.getFullYear()
            );

        if (!active) return;

        setActivities(data);

      } catch (err) {
        console.error(err);

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

 
  const {
    days,
    blanks,
    monthLabel,
  } = useMemo(
    () => buildCalendarMonth(viewDate),
    [viewDate]
  );

  const groupedActivities =
    useMemo(() => {

      const map = new Map<
        number,
        CalendarViewDto[]
      >();

      activities.forEach((activity) => {

        const day = new Date(
          activity.start_time
        ).getDate();

        const existing = map.get(day);

        if (existing) {
          existing.push(activity);
        } else {
          map.set(day, [activity]);
        }
      });

      return map;

    }, [activities]);

  const navigateMonth = (direction: number) => {
    setViewDate(new Date(viewDate.getFullYear(), viewDate.getMonth() + direction, 1));
  };

  return (
    <div className="p-6 space-y-6">

      <div className="flex justify-between items-center">

        <h2 className="text-3xl font-bold text-white tracking-tight">
          Activity Matrix
        </h2>

        <div className="flex items-center gap-4 bg-slate-900 border border-slate-800 rounded-lg p-1">

          <button
            onClick={() => navigateMonth(-1)}
            className="hover:bg-slate-800 p-2 rounded text-slate-400"
          >
            ‹
          </button>

          <span className="font-bold text-slate-200 min-w-[120px] text-center">
            {monthLabel}
          </span>

          <button
            onClick={() => navigateMonth(1)}
            className="hover:bg-slate-800 p-2 rounded text-slate-400"
          >
            ›
          </button>

        </div>
      </div>

      <div
        className={`grid grid-cols-7 gap-2 transition-opacity duration-300 ${
          isLoading
            ? 'opacity-40 pointer-events-none'
            : 'opacity-100'
        }`}
      >

        {[
          'Mon',
          'Tue',
          'Wed',
          'Thu',
          'Fri',
          'Sat',
          'Sun',
        ].map((day) => (
          <div
            key={day}
            className="text-center text-[10px] font-black text-slate-600 uppercase py-2"
          >
            {day}
          </div>
        ))}

        {blanks.map((i) => (
          <div
            key={`blank-${i}`}
            className="min-h-[140px]"
          />
        ))}

        {days.map((day) => (
          <DayCell
            key={day}
            day={day}
            activities={
              groupedActivities.get(day) || []
            }
          />
        ))}

      </div>
    </div>
  );
};

