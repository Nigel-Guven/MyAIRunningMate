import type { CalendarViewResponse } from "../../types/calendar/calendarViewResponse";
import type { TrainingPlanEventResponse } from "../../types/nexus/trainingPlanEventResponse";
import { ActivityTile } from "./ActivityTile";

interface DayCellProps {
  day: number;
  activities: CalendarViewResponse[];
  trainingEvent?: TrainingPlanEventResponse;
  isToday?: boolean; // <-- Added prop definition
}

const formatDistanceKm = (metres: number) => {
  if (metres <= 0) return '';
  return `${(metres / 1000).toFixed(1)}k`;
};

const getExerciseEmoji = (type: string): string => {
  const t = type.toLowerCase();
  if (t.includes('run')) return '🏃‍♂️';
  if (t.includes('swim')) return '🏊‍♂️';
  if (t.includes('walk')) return '🚶‍♂️';
  if (t.includes('cycl') || t.includes('bike')) return '🚴‍♂️';
  if (t.includes('gym') || t.includes('train')) return '🏋️‍♂️';
  if (t.includes('hike')) return '🥾';
  return '🏁';
};

export const DayCell = ({ day, activities, trainingEvent, isToday }: DayCellProps) => {
  const hasCompletedActivities = activities.length > 0;
  const isRestDay = trainingEvent?.exercise_type === 'Rest';
  const hasActiveTarget = trainingEvent && !isRestDay;

  return (
    <div className={`
      min-h-[140px] rounded-lg border p-2 transition-all group flex flex-col justify-between
      ${isToday
        ? 'border-yellow-500/50 bg-yellow-500/[0.04] shadow-[inset_0_0_12px_rgba(234,179,8,0.05)] ring-1 ring-yellow-500/20'
        : hasCompletedActivities 
          ? 'border-slate-800 bg-slate-900/40 hover:border-blue-500/50' 
          : hasActiveTarget
            ? 'border-blue-500/20 bg-blue-500/5 hover:border-blue-500/40 opacity-90'
            : 'border-slate-900/50 bg-slate-950/20 opacity-40'}
    `}>
      <div>
        {/* Day Header Row */}
        <div className="flex justify-between items-center mb-1.5">
          <span className={`text-xs font-bold font-mono ${
            isToday 
              ? 'text-yellow-400 bg-yellow-400/10 px-1.5 py-0.5 rounded' 
              : hasCompletedActivities 
                ? 'text-slate-400' 
                : hasActiveTarget 
                  ? 'text-blue-400' 
                  : 'text-slate-700'
          }`}>
            {day}
          </span>
          
          {/* Subtle Rest indicator icon */}
          {isRestDay && (
            <span className="text-[10px] text-slate-600 uppercase font-mono tracking-wider">Rest</span>
          )}
        </div>

        {/* Planned Target Segment Block */}
        {hasActiveTarget && (
          <div className="mb-2 p-1.5 rounded bg-blue-500/10 border border-blue-500/20 text-[11px] text-blue-300 leading-tight">
            <div className="flex justify-between font-bold">
              <span className="truncate">
                {getExerciseEmoji(trainingEvent.exercise_type)} {trainingEvent.exercise_subtype}
              </span>
              {trainingEvent.distance_metres > 0 && (
                <span className="font-mono text-[10px] bg-blue-500/20 px-1 rounded text-white flex-shrink-0">
                  {formatDistanceKm(trainingEvent.distance_metres)}
                </span>
              )}
            </div>
            {trainingEvent.description && (
              <p className="text-[10px] text-slate-400 mt-0.5 line-clamp-2 italic">
                "{trainingEvent.description}"
              </p>
            )}
          </div>
        )}

        {/* Logged/Completed Real Actions List */}
        <div className="space-y-1">
          {activities.map(act => (
            <ActivityTile key={act.activity_id} act={act} />
          ))}
        </div>
      </div>
      
      {/* Dynamic bottom status indicator */}
      {isToday && !hasCompletedActivities ? (
        <div className="text-[9px] font-bold text-yellow-500/80 font-mono tracking-wide mt-2 uppercase flex items-center gap-1">
          <span className="animate-pulse text-base leading-none">·</span> Today
        </div>
      ) : hasActiveTarget && !hasCompletedActivities ? (
        <div className="text-[9px] font-bold text-slate-600 font-mono tracking-wide mt-2 uppercase">
          ○ Scheduled
        </div>
      ) : null}
    </div>
  );
};