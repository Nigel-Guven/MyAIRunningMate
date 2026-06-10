import { useNavigate } from "react-router";
import type { CalendarViewDto } from "../../types/views/calendarView";
import { getActivityStyles } from "./styles";
import { formatDuration } from "../../services/helpers/activity.utils";

export const ActivityTile = ({ act }: { act: CalendarViewDto }) => {
  const navigate = useNavigate();
  const colorClasses = getActivityStyles(act.type, act.distance_metres);

  return (
    <button
      onClick={() => navigate(`/activity/${act.activity_id}`)}
      className={`w-full text-left p-1.5 rounded border cursor-pointer transition-colors ${colorClasses} hover:brightness-110 focus:outline-none focus:ring-1 focus:ring-offset-1 focus:ring-blue-500 block`}
      type="button"
    >
      {/* Header: Activity Type */}
      <div className="flex justify-between items-start mb-0.5">
        <span className="text-[9px] font-black uppercase truncate tracking-wider" title={act.type}>
          {act.type}
        </span>
      </div>

      {/* Main Stats: Distance & Duration */}
      <div className="flex justify-between items-baseline gap-1">
        <p className="text-xs font-bold whitespace-nowrap">
          {(act.distance_metres / 1000).toFixed(1)}k
        </p>
        <p className="text-[10px] font-semibold opacity-90 tokens">
          {formatDuration(act.duration_seconds)}
        </p>
      </div>

      {/* Footer: Training Effect */}
      <div className="text-[8px] opacity-75 mt-0.5 font-medium tracking-wide">
        TE {act.training_effect.toFixed(1)}
      </div>
    </button>
  );
};