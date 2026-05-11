import { useNavigate } from "react-router";
import type { CalendarViewDto } from "../../types/views/calendarView";
import { getActivityStyles } from "./styles";

export const ActivityTile = ({ act }: { act: CalendarViewDto }) => {
  const navigate = useNavigate();
  const colorClasses = getActivityStyles(act.type);

  return (
    <div 
      onClick={() => navigate(`/activity/${act.activity_id}`)}
      className={`p-1.5 rounded border cursor-pointer transition-colors ${colorClasses} hover:brightness-125`}
    >
      <div className="flex justify-between items-start">
        <span className="text-[9px] font-black uppercase truncate">{act.type}</span>
      </div>
      <p className="text-xs font-bold">{(act.distance_metres / 1000).toFixed(1)}k</p>
      <div className="text-[8px] opacity-70 mt-0.5 font-medium">
        TE {act.training_effect.toFixed(1)}
      </div>
    </div>
  );
};