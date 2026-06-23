import { formatTime } from "../../services/helpers/formatTime";
import type { DashboardTypes } from "../../types/dashboard/dashboard.types";

interface WeeklyVolumeCardProps {
  insights: NonNullable<DashboardTypes['weeklyInsights']>;
}

export const WeeklyVolumeCard = ({ insights }: WeeklyVolumeCardProps) => {
  const segments = [
    { label: "Morning", count: insights.morning_activities, color: "bg-red-400" },
    { label: "Afternoon", count: insights.afternoon_activities, color: "bg-amber-400" },
    { label: "Evening", count: insights.evening_activities, color: "bg-blue-400" },
    { label: "Night", count: insights.night_activities, color: "bg-purple-400" },
  ];

  return (
    <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6 backdrop-blur-sm flex flex-col justify-between">
      <div>
        <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-5">Weekly Volume</h4>

        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <p className="text-[9px] font-bold text-slate-500 uppercase">Running</p>
            <div className="flex items-end gap-1 mt-1">
              <span className="text-4xl font-black italic text-blue-400">
                {(insights.running_distance_metres / 1000).toFixed(1)}
              </span>
              <span className="text-xs font-bold text-slate-500 mb-1">KM</span>
            </div>
            <p className="text-[10px] text-slate-600 mt-1 font-mono">Total Time: {formatTime(insights.running_time_seconds)}</p>
            <p className="text-[10px] text-slate-600 mt-1 font-mono">Moving Time: {formatTime(insights.running_moving_time_seconds)}</p>
          </div>

          <div className="border-l border-slate-800/80 pl-4">
            <p className="text-[9px] font-bold text-slate-500 uppercase">Swimming</p>
            <div className="flex items-end gap-1 mt-1">
              <span className="text-4xl font-black italic text-purple-400">{insights.swimming_distance_metres}</span>
              <span className="text-xs font-bold text-slate-500 mb-1">M</span>
            </div>
            <p className="text-[10px] text-slate-600 mt-1 font-mono">Swimming Time: {formatTime(insights.swimming_time_seconds)}</p>
          </div>
        </div>

        <hr className="border-slate-800/60 my-5" />

        <div className="mb-6">
          <p className="text-[9px] font-bold text-slate-500 uppercase tracking-wider mb-2">Activity Time Distribution</p>
          <div className="flex w-full h-3 rounded-full overflow-hidden bg-slate-800">
            {segments.reduce((acc, curr) => acc + curr.count, 0) > 0 ? (
              segments.map((s, idx) => s.count > 0 && (
                <div key={idx} className={`transition-all duration-300 ${s.color}`} style={{ flexGrow: s.count }} title={`${s.label}: ${s.count}`} />
              ))
            ) : (
              <div className="w-full h-full bg-slate-700" />
            )}
          </div>
          <div className="flex justify-between mt-2 text-[9px] text-slate-500 font-mono">
            {segments.map((s, idx) => (
              <span key={idx} className={s.count === 0 ? "opacity-40" : ""}>{s.label} {s.count}</span>
            ))}
          </div>
        </div>

        <div className="grid grid-cols-2 sm:grid-cols-3 gap-y-4 gap-x-3 text-left">
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Avg. Training Score</p>
            <p className="text-sm font-black text-slate-400 font-mono">{insights.mean_training_effect ? insights.mean_training_effect.toFixed(1) : "--"}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Total Training Score</p>
            <p className="text-sm font-black text-white font-mono">{insights.total_training_effect ? insights.total_training_effect.toFixed(1) : "--"}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Consistency</p>
            <p className="text-sm font-black text-orange-400 font-mono">{insights.training_consistency_score}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Avg. Heart Rate</p>
            <p className="text-sm font-black text-slate-400 font-mono">{insights.mean_average_heart_rate ? `${insights.mean_average_heart_rate} bpm` : "--"}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Max Heart Rate</p>
            <p className="text-sm font-black text-slate-400 font-mono">{insights.mean_max_heart_rate ? `${insights.mean_max_heart_rate} bpm` : "--"}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Moving %</p>
            <p className="text-sm font-black text-slate-400 font-mono">{insights.running_moving_efficiency.toFixed(0)}%</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Rest Days</p>
            <p className="text-sm font-black text-emerald-400 font-mono">{insights.rest_days}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Calories</p>
            <p className="text-sm font-black text-orange-400 font-mono">{insights.total_calories_burned}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Cal. Intensity</p>
            <p className="text-sm font-black text-slate-400 font-mono">{insights.caloric_intensity.toFixed(0)}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Elev. Gain</p>
            <p className="text-sm font-black text-white font-mono">{insights.total_running_elevation_gain.toFixed(0)}m</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Elev. Intensity</p>
            <p className="text-sm font-black text-slate-400 font-mono">{insights.elevation_intensity.toFixed(1)}</p>
          </div>
          <div>
            <p className="text-[9px] font-bold text-slate-600 uppercase">Time on Pause</p>
            <p className="text-sm font-black text-slate-500 font-mono">{(insights.running_time_break_seconds / 60).toFixed(0)}m</p>
          </div>
        </div>
      </div>

      <div className="mt-6 pt-4 border-t border-slate-800 text-[10px] text-slate-600 font-mono uppercase tracking-tight">
        {insights.locations.length > 0 ? (
          <div className="flex flex-col gap-1">
            {insights.locations.map((loc, idx) => <span key={idx} className="truncate">{loc}</span>)}
          </div>
        ) : "No locations recorded"}
      </div>
    </div>
  );
};