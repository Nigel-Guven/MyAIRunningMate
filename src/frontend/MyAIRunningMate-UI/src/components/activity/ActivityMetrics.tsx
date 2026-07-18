import React, { type ReactNode } from "react";
import { HeartPulse, Zap, Thermometer, Footprints, ChessKing, ChessQueen, Utensils, Droplet, RefreshCw, ArrowUpDown, ArrowRightLeft, ArrowDownFromLine, LandPlot, WavesHorizontal, WavesLadder } from "lucide-react";
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

  function sweatLossInfo(estimated_sweat_loss: number | null): string {
    
    if(estimated_sweat_loss != null)
    {
      const litres  = estimated_sweat_loss/1000;

      return `Sweat Loss - About ${litres.toFixed(1)} bottles of water.`;
    }

      return "Sweat Loss"
    
  }

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
            icon=<ChessKing color="#ff0000" />
            />

            <Metric 
            label="Anaerobic" 
            value={metrics.anaerobic_training_effect} 
            icon=<ChessQueen color="#ff0000" />
            />

            <Metric 
            label="Calories" 
            value={`${metrics.total_calories} kcal`} 
            icon=<Utensils color="#e1ff00" />
            />

            <Metric 
            label={sweatLossInfo(metrics.estimated_sweat_loss)}
            value={
                metrics.estimated_sweat_loss
                ? `${metrics.estimated_sweat_loss} ml`
                : "Not Recorded"
            } 
            icon=<Droplet color="#1f77ea" />
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
                    icon=<RefreshCw color="#34f941" />
                />

                <Metric
                    label="Vertical Oscillation"
                    value={
                    metrics.average_vertical_oscillation
                        ? `${metrics.average_vertical_oscillation} mm`
                        : "—"
                    }
                    icon=<ArrowUpDown color="#34f941" />
                />

                <Metric
                    label="Step Length"
                    value={
                    metrics.step_length
                        ? `${metrics.step_length} mm`
                        : "—"
                    }
                    icon=<ArrowRightLeft color="#34f941" />
                />

                <Metric
                    label="Vertical Ratio"
                    value={
                    metrics.average_vertical_ratio
                        ? `${metrics.average_vertical_ratio}%`
                        : "—"
                    }
                    icon=<ArrowDownFromLine color="#34f941" />
                />

                <Metric
                    label="Stance Time"
                    value={
                    metrics.average_stance_time
                        ? `${metrics.average_stance_time} ms`
                        : "—"
                    }
                    icon=<LandPlot color="#27ece9" />
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
                    icon=<WavesHorizontal color="#4f27ec" />
                />

                <Metric
                    label="Pool Length"
                    value={
                    metrics.pool_length
                        ? `${metrics.pool_length} m`
                        : "—"
                    }
                    icon=<WavesLadder color="#4f27ec" />
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

const Metric = ({ label, value, icon }: { label: string, value: any, icon: ReactNode }) => (
  <div>
    <div className="text-xs text-slate-500 mb-0.5">
      {label}
    </div>
    <div className="flex items-center gap-1.5 text-lg font-bold text-white">
      {/* 
        Using [&>svg] ensures that whatever SVG you pass in 
        is forced to remain h-4 w-4 so it doesn't break your layout.
      */}
      <span className="shrink-0 flex items-center justify-center [&>svg]:h-4 [&>svg]:w-4">
        {icon}
      </span>
      <span>{value}</span>
    </div>
  </div>
);