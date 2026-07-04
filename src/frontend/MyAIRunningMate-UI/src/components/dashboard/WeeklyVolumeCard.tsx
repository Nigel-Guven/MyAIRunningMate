import { formatTime } from "../../services/helpers/formatTime";
import type { DashboardTypes } from "../../types/dashboard/dashboard.types";
import { getEconomyLevel } from "../../services/helpers/dashboardUtils";

interface WeeklyVolumeCardProps {
  insights: NonNullable<DashboardTypes['weeklyInsights']>;
  onPreviousWeek: () => void;
  onNextWeek: () => void;
  canGoNext?: boolean;
  weekLabel: string;
}

export const WeeklyVolumeCard = ({ insights, onPreviousWeek, onNextWeek, canGoNext, weekLabel }: WeeklyVolumeCardProps) => {
  const segments = [
    { label: "Morning", count: insights.morning_activities, color: "bg-red-400" },
    { label: "Afternoon", count: insights.afternoon_activities, color: "bg-amber-400" },
    { label: "Evening", count: insights.evening_activities, color: "bg-blue-400" },
    { label: "Night", count: insights.night_activities, color: "bg-purple-400" },
  ];

  const eco = getEconomyLevel(insights.running_economy_index);

  const vo2Trend = insights.volumetric_oxygen_max_trend;
  const vo2Percent = insights.volumetric_oxygen_max_diff_percent;

  const vo2Arrow =
    vo2Percent > 0 ? "↑" : vo2Percent < 0 ? "↓" : "→";

  return (
    <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6 backdrop-blur-sm flex flex-col justify-between">
      <div>
        <div className="flex items-center justify-between mb-5">
          <div>
            <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em]">Weekly Volume</h4>
            <p className="text-[10px] text-slate-600 mt-1">{weekLabel}</p>
          </div>

          <div className="flex gap-2">
            <button onClick={onPreviousWeek} className="w-7 h-7 rounded-lg border border-slate-700 hover:border-slate-500 hover:bg-slate-800">
              &lt;
            </button>

            <button onClick={onNextWeek} disabled={!canGoNext} className="w-7 h-7 rounded-lg border border-slate-700 disabled:opacity-40 hover:border-slate-500 hover:bg-slate-800">
              &gt;
            </button>
          </div>
        </div>
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

        <div className="grid grid-cols-1 sm:grid-cols-3 gap-6 text-left">
          <div className="flex flex-col gap-4">
            <h3 className="text-xs uppercase text-slate-500 font-bold">Training Load</h3>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Total Training Score</p>
              <p className="text-sm font-black text-slate-400 font-mono">
                {insights.total_training_score ? insights.total_training_score.toFixed(1) : "0"}
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Total Calories Burned</p>
              <p className="text-sm font-black text-slate-400 font-mono">
                {insights.total_calories_burned} kcals
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Heart Rate Intensity</p>
              <p className="text-sm font-black text-slate-400 font-mono">
                {insights.heart_rate_intensity_score
                  ? `${insights.heart_rate_intensity_score.toFixed(1)}%`
                  : "--"}
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Recovery Time</p>
              <p className="text-sm font-black text-slate-400 font-mono">
                {(insights.recovery_time_generated / 60).toFixed(1)} hrs
              </p>
            </div>
          </div>
          <div className="flex flex-col gap-4">
            <h3 className="text-xs uppercase text-slate-500 font-bold">Consistency</h3>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Training Consistency</p>
              <p className="text-sm font-black text-white font-mono">
                {insights.training_consistency_score
                  ? insights.training_consistency_score.toFixed(3) + "%"
                  : "--"}
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Rest Days</p>
              <p className="text-sm font-black text-orange-400 font-mono">
                {insights.rest_days} days
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Body Batteries Used</p>
              <p className="text-sm font-black text-emerald-400 font-mono">
                🔋 {(insights.body_battery_depletion / 100)} BBs
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Body Battery Efficiency</p>
              <p className="text-sm font-black text-orange-400 font-mono">
                ⚡ {insights.body_battery_efficiency.toFixed(0)} sec/BB
              </p>
            </div>
          </div>
          <div className="flex flex-col gap-4">
            <h3 className="text-xs uppercase text-slate-500 font-bold">Performance</h3>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">VO₂ Max</p>

              <p className="text-sm font-black text-slate-400 font-mono">
                {vo2Trend.toFixed(2)}{" "}
                <span className={vo2Percent >= 0 ? "text-emerald-400" : "text-red-400"}>
                  {vo2Arrow} {Math.abs(vo2Percent).toFixed(2)}%
                </span>
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Running Economy</p>

              <p className="text-sm font-black text-white font-mono flex items-center gap-2">
                {insights.running_economy_index.toFixed(1)}%
                <span className={`w-2 h-2 rounded-full ${eco.color}`} />
                <span className="text-xs text-slate-400">{eco.label}</span>
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Moving Efficiency</p>
              <p className="text-sm font-black text-slate-400 font-mono">
                {insights.running_moving_efficiency.toFixed(1)}%
              </p>
            </div>

            <div>
              <p className="text-[9px] font-bold text-slate-600 uppercase">Time on Pause</p>
              <p className="text-sm font-black text-slate-500 font-mono">
                {(insights.paused_seconds / 60).toFixed(0)} mins
              </p>
            </div>
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