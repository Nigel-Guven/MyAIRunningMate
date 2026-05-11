import type { CalendarViewDto } from "../../types/views/calendarView";
import { ActivityTile } from "./ActivityTile";

export const DayCell = ({ day, activities }: { day: number, activities: CalendarViewDto[] }) => {
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
          <ActivityTile key={act.activity_id} act={act} />
        ))}
      </div>
    </div>
  );
};