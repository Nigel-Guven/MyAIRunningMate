import type { CalendarViewResponse } from "../../types/calendar/calendarViewResponse";
import type { TrainingPlanEventResponse } from "../../types/nexus/trainingPlanEventResponse";
import { CalendarActivityTile } from "./CalendarActivityTile";
import { TrainingPlanTarget } from "./TrainingPlanTarget";

interface CalendarCellProps {
  day: number;
  activities: CalendarViewResponse[];
  trainingEvent?: TrainingPlanEventResponse;
  isToday?: boolean;
}

// Utility Helpers
export const formatDistanceKm = (metres: number) => {
  if (!metres || metres <= 0) return '';
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

const getCellStyles = (isToday: boolean, hasActivities: boolean, hasTarget: boolean) => {
  if (isToday) {
    return 'border-yellow-500/50 bg-yellow-500/[0.04] shadow-[inset_0_0_12px_rgba(234,179,8,0.05)] ring-1 ring-yellow-500/20';
  }
  if (hasActivities) {
    return 'border-slate-800 bg-slate-900/40 hover:border-blue-500/50';
  }
  if (hasTarget) {
    return 'border-blue-500/20 bg-blue-500/5 hover:border-blue-500/40 opacity-90';
  }
  return 'border-slate-900/50 bg-slate-950/20 opacity-40';
};

const getDayNumberStyles = (isToday: boolean, hasActivities: boolean, hasTarget: boolean) => {
  if (isToday) return 'text-yellow-400 bg-yellow-400/10 px-1.5 py-0.5 rounded';
  if (hasActivities) return 'text-slate-400';
  if (hasTarget) return 'text-blue-400';
  return 'text-slate-700';
};

export const CalendarCell = ({ day, activities, trainingEvent, isToday = false }: CalendarCellProps) => {
  const hasCompletedActivities = activities.length > 0;
  const isRestDay = trainingEvent?.exercise_type === 'Rest';
  const hasActiveTarget = trainingEvent && !isRestDay;

  return (
    <div className={`min-h-[140px] rounded-lg border p-2 transition-all group flex flex-col justify-between ${getCellStyles(isToday, hasCompletedActivities, !!hasActiveTarget)}`}>
      <div>
        {/* Day Header Row */}
        <div className="flex justify-between items-center mb-1.5">
          <span className={`text-xs font-bold font-mono ${getDayNumberStyles(isToday, hasCompletedActivities, !!hasActiveTarget)}`}>
            {day}
          </span>
          {isRestDay && (
            <span className="text-[10px] text-slate-600 uppercase font-mono tracking-wider">Rest</span>
          )}
        </div>

        {/* Planned Target Segment Block */}
        {hasActiveTarget && (
          <TrainingPlanTarget 
            trainingEvent={trainingEvent} 
            exerciseEmoji={getExerciseEmoji(trainingEvent.exercise_type)}
            formattedDistance={formatDistanceKm(trainingEvent.distance_metres)}
          />
        )}

        {/* Logged/Completed Real Actions List */}
        <div className="space-y-1">
          {activities.map(act => (
            <CalendarActivityTile key={act.activity_id} act={act} />
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