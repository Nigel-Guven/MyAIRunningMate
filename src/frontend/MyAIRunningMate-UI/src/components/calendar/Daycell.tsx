import type { CalendarViewDto } from "../../types/views/calendarView";
import type { TrainingPlanEventView } from "../../types/views/trainingPlanView";
import { ActivityTile } from "./ActivityTile";

interface DayCellProps {
  day: number;
  activities: CalendarViewDto[];
  trainingEvent?: TrainingPlanEventView; // Added target type structure
}

const formatDistanceKm = (metres: number) => {
  if (metres <= 0) return '';
  return `${(metres / 1000).toFixed(1)}k`;
};

// Maps exercise types to recognizable emojis for quick scannability
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

export const DayCell = ({ day, activities, trainingEvent }: DayCellProps) => {
  const hasCompletedActivities = activities.length > 0;
  const isRestDay = trainingEvent?.exerciseType === 'Rest';
  const hasActiveTarget = trainingEvent && !isRestDay;

  return (
    <div className={`
      min-h-[140px] rounded-lg border p-2 transition-all group flex flex-col justify-between
      ${hasCompletedActivities 
        ? 'border-slate-800 bg-slate-900/40 hover:border-blue-500/50' 
        : hasActiveTarget
          ? 'border-blue-500/20 bg-blue-500/5 hover:border-blue-500/40 opacity-90'
          : 'border-slate-900/50 bg-slate-950/20 opacity-40'}
    `}>
      <div>
        {/* Day Header Row */}
        <div className="flex justify-between items-center mb-1.5">
          <span className={`text-xs font-bold font-mono ${
            hasCompletedActivities ? 'text-slate-400' : hasActiveTarget ? 'text-blue-400' : 'text-slate-700'
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
                {getExerciseEmoji(trainingEvent.exerciseType)} {trainingEvent.exerciseSubtype}
              </span>
              {trainingEvent.distanceMetres > 0 && (
                <span className="font-mono text-[10px] bg-blue-500/20 px-1 rounded text-white flex-shrink-0">
                  {formatDistanceKm(trainingEvent.distanceMetres)}
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
      
      {/* If a target was planned but nothing was completed yet, render a subtle status reminder */}
      {hasActiveTarget && !hasCompletedActivities && (
        <div className="text-[9px] font-bold text-slate-600 font-mono tracking-wide mt-2 uppercase">
          ○ Scheduled
        </div>
      )}
    </div>
  );
};