import React from "react";
import { HeartPulse, Zap, Thermometer, Footprints } from "lucide-react";
import type { ActivityMetricsResponse } from "../../types/aggregates/activityMetricsResponse";

interface ActivityMetricsProps {
  metrics: ActivityMetricsResponse;
}

export const ActivityMetrics: React.FC<ActivityMetricsProps> = ({ metrics }) => {
  const stats = [
    {
      label: "Heart Rate",
      value: `${metrics.average_heart_rate} bpm`,
      sub: `Max ${metrics.max_heart_rate}`,
      icon: HeartPulse,
    },
    {
      label: "Power",
      value: `${metrics.average_power} W`,
      sub: `Max ${metrics.max_power}`,
      icon: Zap,
    },
    {
      label: "Cadence",
      value: `${metrics.average_cadence} spm`,
      sub: `Max ${metrics.max_cadence}`,
      icon: Footprints,
    },
    {
      label: "Temperature",
      value: `${metrics.average_temperature}°C`,
      sub: `Max ${metrics.max_temperature}°C`,
      icon: Thermometer,
    },
  ];

  return (
    <div className="bg-slate-900 border border-slate-800 rounded-2xl p-5">
      <h2 className="text-lg font-bold mb-4">Activity Metrics</h2>

      <div className="space-y-4">
        {stats.map((stat) => {
          const Icon = stat.icon;

          return (
            <div
              key={stat.label}
              className="flex items-center justify-between"
            >
              <div className="flex items-center gap-3">
                <Icon className="h-5 w-5 text-emerald-400" />
                <span className="text-slate-400">
                  {stat.label}
                </span>
              </div>

              <div className="text-right">
                <div className="font-bold">
                  {stat.value}
                </div>
                <div className="text-xs text-slate-500">
                  {stat.sub}
                </div>
              </div>
            </div>
          );
        })}

        <div className="border-t border-slate-800 pt-4 space-y-6">

        {/* Training Summary */}
        <div className="grid grid-cols-2 gap-4">
            <Metric 
            label="Aerobic" 
            value={metrics.aerobic_training_effect} 
            />

            <Metric 
            label="Anaerobic" 
            value={metrics.anaerobic_training_effect} 
            />

            <Metric 
            label="Calories" 
            value={`${metrics.total_calories} kcal`} 
            />

            <Metric 
            label="Sweat Loss" 
            value={
                metrics.estimated_sweat_loss
                ? `${metrics.estimated_sweat_loss} ml`
                : "Not Recorded"
            } 
            />
        </div>


        {/* Running / Swimming Metrics */}
        <div className="border-t border-slate-800 pt-4">

            {metrics.average_swolf === null ? (
            <>
                <h3 className="font-semibold mb-3">
                Running Metrics
                </h3>

                <div className="grid grid-cols-2 gap-4">

                <Metric
                    label="Total Cycles"
                    value={metrics.total_cycles}
                />

                <Metric
                    label="Vertical Oscillation"
                    value={
                    metrics.average_vertical_oscillation
                        ? `${metrics.average_vertical_oscillation} mm`
                        : "—"
                    }
                />

                <Metric
                    label="Step Length"
                    value={
                    metrics.step_length
                        ? `${metrics.step_length} mm`
                        : "—"
                    }
                />

                <Metric
                    label="Vertical Ratio"
                    value={
                    metrics.average_vertical_ratio
                        ? `${metrics.average_vertical_ratio}%`
                        : "—"
                    }
                />

                <Metric
                    label="Stance Time"
                    value={
                    metrics.average_stance_time
                        ? `${metrics.average_stance_time} ms`
                        : "—"
                    }
                />

                </div>


                <p className="mt-4 text-sm text-slate-400">
                Running dynamics describe how efficiently you move.
                <br></br>
                <br></br>
                <span className="text-white font-medium">Total Cycles</span> is a rough measure of how many steps were taken.
                <br></br>
                <br></br>
                <span className="text-white font-medium">Vertical Oscillation</span> measures how much you bounce while
                running.
                <br></br>
                <br></br>
                <span className="text-white font-medium">Step Length</span> shows your stride distance.
                <br></br>
                <br></br>
                <span className="text-white font-medium">Vertical Ratio</span> compares bounce to forward movement.
                <br></br>
                <br></br>
                <span className="text-white font-medium">Stance Time</span> measures how long your foot remains on the ground.
                </p>
            </>
            ) : (
            <>
                <h3 className="font-semibold mb-3">
                Swimming Metrics
                </h3>

                <div className="grid grid-cols-2 gap-4">

                <Metric
                    label="SWOLF"
                    value={metrics.average_swolf}
                />

                <Metric
                    label="Pool Length"
                    value={
                    metrics.pool_length
                        ? `${metrics.pool_length} m`
                        : "—"
                    }
                />

                </div>


                <p className="mt-4 text-sm text-slate-400">
                Swimming dynamics measure how efficiently you swim.
                <br></br>
                <br></br>
                <span className="text-white font-medium">SWOLF</span> is a swimming efficiency score that combines
                stroke count and time taken to complete a pool length.
                <br></br>
                <br></br>
                Lower scores generally indicate more efficient swimming.
                </p>
            </>
            )}

        </div>

        </div>
      </div>
    </div>
  );
};


const Metric = ({label,value}: {label:string,value:any}) => (
  <div>
    <div className="text-xs text-slate-500">
      {label}
    </div>
    <div className="font-bold">
      {value}
    </div>
  </div>
);