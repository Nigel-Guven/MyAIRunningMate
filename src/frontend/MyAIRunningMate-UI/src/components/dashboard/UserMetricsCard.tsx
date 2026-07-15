import type { UserMetricsResponse } from "../../types/dashboard/userMetricsResponse";

interface UserMetricsCardProps {
  metrics: UserMetricsResponse | null;
}

interface MetricProps {
  icon: string; // Changed to string for emoji support
  title: string;
  value: string | number | undefined; // Safeguard if metrics is null
  unit?: string;
  children?: React.ReactNode;
}

const Metric = ({
  icon,
  title,
  value,
  unit,
  children,
}: MetricProps) => (
  <div className="rounded-2xl border border-slate-800 bg-slate-900/60 p-5">
    <div className="flex items-center gap-2 text-slate-400 text-xs uppercase tracking-wide">
      <span className="text-sm select-none" role="img" aria-label={title}>
        {icon}
      </span>
      {title}
    </div>

    <div className="mt-3 flex items-end gap-1">
      <span className="text-3xl font-bold text-white">{value ?? "—"}</span>
      {unit && (
        <span className="text-sm text-slate-400 mb-1">
          {unit}
        </span>
      )}
    </div>

    {children && (
      <div className="mt-3 space-y-1 text-sm text-slate-300">
        {children}
      </div>
    )}
  </div>
);

export default function UserMetricsCard({ metrics }: UserMetricsCardProps) {
  return (
    <div className="rounded-3xl border border-slate-800 bg-gradient-to-br from-slate-900 via-slate-900 to-slate-950 p-6 shadow-xl">

      <div className="flex items-center justify-between mb-6">
        <div>
          <h2 className="text-xl font-bold text-white">
            Current Fitness Profile
          </h2>

          <p className="text-slate-400 text-sm mt-1">
            Updated from your latest Garmin activity
          </p>
        </div>

        <div className="flex items-center gap-2 rounded-full border border-emerald-800 bg-emerald-950/40 px-4 py-2">
          <span className="text-sm" role="img" aria-label="award">🏆</span>
          <span className="text-emerald-300 text-sm font-semibold">
            {metrics?.training_level || "No Data"}
          </span>
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-5 gap-5">

        <Metric
          icon="💓"
          title="VO₂ Max"
          value={metrics?.user_volumetric_oxygen_max?.toFixed(1)}
        >
          {metrics?.user_volumetric_oxygen_max_rating && (
            <span
              className="inline-flex rounded-full px-3 py-1 text-xs font-semibold text-white"
              style={{
                background: metrics?.fitness_rank_color || "#334155",
              }}
            >
              {metrics?.user_volumetric_oxygen_max_rating}
            </span>
          )}
        </Metric>

        <Metric
          icon="❤️"
          title="Max Heart Rate"
          value={metrics?.user_max_heart_rate}
          unit="bpm"
        >
          <div className="text-slate-400 text-xs">
            Maximum recorded heart rate
          </div>
        </Metric>

        <Metric
          icon="📈"
          title="Threshold HR"
          value={metrics?.user_lactate_threshold_heart_rate}
          unit="bpm"
        >
          {metrics?.threshold_percentage_power != null && (
            <div>
              <span className="font-semibold text-white">
                {metrics.threshold_percentage_power.toFixed(1)}%
              </span>
              <span className="text-slate-400 ml-1">
                of max HR
              </span>
            </div>
          )}
        </Metric>

        <Metric
          icon="⚡"
          title="Threshold Power"
          value={metrics?.user_lactate_threshold_power}
          unit="W"
        >
          {metrics?.power_to_weight_ratio != null && (
            <div className="font-medium">
              {metrics.power_to_weight_ratio.toFixed(2)} W/kg
            </div>
          )}
          {metrics?.power_rating && (
            <div className="text-indigo-300">
              {metrics.power_rating}
            </div>
          )}
        </Metric>

        <Metric
          icon="⚖️"
          title="Weight"
          value={metrics?.weight_kg?.toFixed(1)}
          unit="kg"
        />
      </div>

      <div className="mt-6 flex flex-col md:flex-row md:items-center md:justify-between gap-4 rounded-2xl border border-slate-800 bg-slate-900/70 px-5 py-4">

        <div>
          <div className="text-sm text-slate-400">
            Fitness Percentile
          </div>

          <div className="text-white font-semibold text-lg">
            Better than{" "}
            <span className="text-emerald-400">
              {metrics?.fitness_percentile ?? "0"}%
            </span>{" "}
            of runners in your age group
          </div>
        </div>

        <div className="text-sm text-slate-400">
          Lactate Threshold Speed{" "}
          <span className="text-white font-semibold ml-2">
            {metrics?.user_lactate_threshold_speed != null
              ? `${metrics.user_lactate_threshold_speed.toFixed(1)} km/h`
              : "—"}
          </span>
        </div>

      </div>

    </div>
  );
}