import { useNavigate } from "react-router";
import { formatDistanceKm, formatDuration } from "../../services/helpers/activity.utils";
import type { CalendarViewResponse } from "../../types/calendar/calendarViewResponse";
import { getActivityStyles } from "../../services/helpers/calendarStyles";

const TrainingEffectBadge = ({ 
  status, 
  aerobic, 
  anaerobic 
}: { 
  status: string; 
  aerobic: number; 
  anaerobic: number; 
}) => {
  let colorClasses = "text-slate-600 bg-slate-500/10 border-slate-500/20 dark:text-slate-300";

  switch (status?.toLowerCase()) {
    case "recovery":
      colorClasses = "text-green-700 bg-green-500/10 border-green-500/20 dark:text-green-400";
      break;
    case "base":
      colorClasses = "text-blue-700 bg-blue-500/10 border-blue-500/20 dark:text-blue-400";
      break;
    case "tempo":
      colorClasses = "text-orange-700 bg-orange-500/10 border-orange-500/20 dark:text-orange-400";
      break;
    case "threshold":
      colorClasses = "text-purple-700 bg-purple-500/10 border-purple-500/20 dark:text-purple-400";
      break;
    case "intensity":
      colorClasses = "text-rose-700 bg-rose-600/10 border-rose-600/20 dark:text-rose-400";
      break;
  }

  return (
    <div className={`mt-1.5 flex flex-col px-1.5 py-1 rounded border ${colorClasses}`}>
      <span className="text-[8.5px] font-black uppercase tracking-wider mb-0.5">
        {status}
      </span>
      <div className="flex justify-between items-center text-[8.5px] font-semibold opacity-90">
        <span>Aerobic Score: {aerobic?.toFixed(1) || '0.0'}</span>
        <span>Anaerobic Score: {anaerobic?.toFixed(1) || '0.0'}</span>
      </div>
    </div>
  );
};

export const CalendarActivityTile = ({ act }: { act: CalendarViewResponse }) => {
  const navigate = useNavigate();
  const colorClasses = getActivityStyles(act.exercise_type);
  const formattedDistance = formatDistanceKm(act.distance_metres);

  return (
    <button
      onClick={() => navigate(`/activity/${act.activity_id}`)}
      className={`w-full text-left p-1.5 rounded-md border cursor-pointer transition-all duration-200 ${colorClasses} hover:brightness-105 hover:shadow-sm focus:outline-none focus:ring-2 focus:ring-offset-1 focus:ring-blue-500 flex flex-col justify-between h-full min-h-[76px]`}
      type="button"
    >
      <div className="w-full">
        {/* Header: Activity Type */}
        <div className="flex justify-between items-start mb-1">
          <span className="text-[9px] font-black uppercase truncate tracking-wider opacity-80" title={act.exercise_type}>
            {act.exercise_type}
          </span>
        </div>

        {/* Main Stats: Distance & Duration */}
        <div className="flex justify-between items-baseline gap-1">
          <span className="text-xs font-bold whitespace-nowrap">
            {formattedDistance || 'Logged'}
          </span>
          <span className="text-[10px] font-semibold opacity-80">
            {formatDuration(act.duration_seconds)}
          </span>
        </div>
      </div>

      {/* Footer: Training Effect */}
      <div className="w-full">
        <TrainingEffectBadge 
          status={act.training_effect_status} 
          aerobic={act.aerobic_training_effect} 
          anaerobic={act.anaerobic_training_effect} 
        />
      </div>
    </button>
  );
};