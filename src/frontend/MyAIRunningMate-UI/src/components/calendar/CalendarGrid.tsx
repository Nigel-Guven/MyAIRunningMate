import type { CalendarViewResponse } from "../../types/calendar/calendarViewResponse";
import type { TrainingPlanEventResponse } from "../../types/nexus/trainingPlanEventResponse";
import { DayCell } from "./Daycell";

interface CalendarGridProps {
  days: number[];
  blanks: number[];
  viewDate: Date;
  isLoading: boolean;
  groupedActivities: Map<number, CalendarViewResponse[]>;
  groupedTrainingEvents: Map<number, TrainingPlanEventResponse>;
}

const WEEKDAYS = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

export function CalendarGrid({
  days,
  blanks,
  viewDate,
  isLoading,
  groupedActivities,
  groupedTrainingEvents,
}: CalendarGridProps) {
  const today = new Date();
  const currentYear = viewDate.getFullYear();
  const currentMonth = viewDate.getMonth();

  return (
    <div
      key={`${currentYear}-${currentMonth}`}
      className={`grid grid-cols-7 gap-2 transition-opacity duration-300 ${
        isLoading ? 'opacity-40 pointer-events-none' : 'opacity-100'
      }`}
    >
      {WEEKDAYS.map((day) => (
        <div key={day} className="text-center text-[10px] font-black text-slate-600 uppercase py-2">
          {day}
        </div>
      ))}

      {blanks.map((i) => (
        <div key={`blank-${i}`} className="min-h-[140px]" />
      ))}

      {days.map((day) => {
        const isToday =
          day === today.getDate() &&
          currentMonth === today.getMonth() &&
          currentYear === today.getFullYear();

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
  );
}