import { useNavigate } from "react-router";
import { formatDistanceKm, formatDuration } from "../../services/helpers/activity.utils";
import type { CalendarViewResponse } from "../../types/calendar/calendarViewResponse";
import { getActivityStyles } from "../../services/helpers/calendarStyles";

export const ActivityTile = ({ act }: { act: CalendarViewResponse }) => {
  const navigate = useNavigate();
  const colorClasses = getActivityStyles(act.exercise_type, act.distance_metres);
  const formattedDistance = formatDistanceKm(act.distance_metres);

  return (
    <button
      onClick={() => navigate(`/activity/${act.activity_id}`)}
      className={`w-full text-left p-1.5 rounded border cursor-pointer transition-colors ${colorClasses} hover:brightness-110 focus:outline-none focus:ring-1 focus:ring-offset-1 focus:ring-blue-500 block`}
      type="button"
    >
      {/* Header: Activity Type */}
      <div className="flex justify-between items-start mb-0.5">
        <span className="text-[9px] font-black uppercase truncate tracking-wider" title={act.exercise_type}>
          {act.exercise_type}
        </span>
      </div>

      {/* Main Stats: Distance & Duration */}
      <div className="flex justify-between items-baseline gap-1">
        <p className="text-xs font-bold whitespace-nowrap">
          {formattedDistance || 'Logged'}
        </p>
        <p className="text-[10px] font-semibold opacity-90 tokens">
          {formatDuration(act.duration_seconds)}
        </p>
      </div>

      {/* Footer: Training Effect */}
      <div className="text-[8px] opacity-75 mt-0.5 font-medium tracking-wide">
        TE {act.training_effect_status}
      </div>
    </button>
  );
};