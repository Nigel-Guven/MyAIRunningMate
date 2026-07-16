import React, { useState } from "react";
import {
  LineChart,
  Line,
  XAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
} from "recharts";
import type { TimeSeriesRecordResponse } from "../../types/aggregates/timeSeriesRecordResponse";
import { WeightTooltip } from "../weight/WeightTooltip";
import { TimeSeriesRecordTooltip } from "./TimeSeriesRecordTooltip";

interface TimeSeriesChartProps {
  records: TimeSeriesRecordResponse[];
  averageHeartRate: number;
}

export const TimeSeriesGraph: React.FC<TimeSeriesChartProps> = ({
  records,
  averageHeartRate
}) => {

  const [visible, setVisible] = useState({
    heart_rate: true,
    cadence: true,
    power: true,
  });


  const toggleMetric = (
    metric: keyof typeof visible
  ) => {
    setVisible(prev => ({
      ...prev,
      [metric]: !prev[metric],
    }));
  };


  const chartData = records.map(record => ({
    ...record,
    time: new Date(record.timestamp)
      .toLocaleTimeString("en-IE", {
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
      }),
    average_heart_rate: averageHeartRate
  }));


  return (
    <div className="bg-slate-900 border border-slate-800 rounded-2xl p-5">

      <div className="flex justify-between items-center mb-5">

        <h2 className="text-lg font-bold">
          Activity Timeline
        </h2>


        <div className="flex gap-2">

          <Toggle
            label="Heart Rate"
            active={visible.heart_rate}
            color="text-red-400"
            onClick={() => toggleMetric("heart_rate")}
          />

          <Toggle
            label="Cadence"
            active={visible.cadence}
            color="text-blue-400"
            onClick={() => toggleMetric("cadence")}
          />

          <Toggle
            label="Power"
            active={visible.power}
            color="text-green-400"
            onClick={() => toggleMetric("power")}
          />

        </div>

      </div>


      <div className="h-[400px]">

        <ResponsiveContainer width="100%" height="100%">

          <LineChart data={chartData}>
            
            <defs>
                <linearGradient id="heartRateGradient" x1="0" y1="0" x2="0" y2="1">
                    <stop offset="0%" stopColor="#dc2626" />
                    <stop offset="50%" stopColor="#ef4444" />
                    <stop offset="100%" stopColor="#f9a8d4" />
                </linearGradient>
            </defs>

            <CartesianGrid 
              strokeDasharray="3 3"
              strokeOpacity={0.2}
            />

            <XAxis
              dataKey="distance_metres"
              tick={{ fontSize: 12 }}
              label={{
                angle:-90,
                position:"insideLeft"
              }}
            />


            <Tooltip
                content={<TimeSeriesRecordTooltip />}
                cursor={{
                    stroke: '#1e293b',
                    strokeWidth: 2,
                }}
            />


            {visible.heart_rate && (
              <Line
                type="monotone"
                dataKey="heart_rate"
                stroke="url(#heartRateGradient)"
                strokeWidth={2}
                dot={false}
                name="Heart Rate (bpm)"
              />
            )}

            <Line
                type="monotone"
                dataKey="average_heart_rate"
                stroke="#bc0ecc"
                strokeWidth={2}
                strokeDasharray="6 4"
                dot={false}
                name="Average HR"
            />


            {visible.cadence && (
              <Line
                type="monotone"
                dataKey="cadence"
                stroke="#1320d6"
                strokeWidth={2}
                dot={false}
                name="Cadence (spm)"
              />
            )}


            {visible.power && (
              <Line
                type="monotone"
                dataKey="power"
                stroke="#098824"
                strokeWidth={2}
                dot={false}
                name="Power (watts)"
              />
            )}

          </LineChart>

        </ResponsiveContainer>

      </div>

    </div>
  );
};



const Toggle = ({
  label,
  active,
  onClick,
  color,
}: {
  label:string;
  active:boolean;
  onClick:()=>void;
  color:string;
}) => (
  <button
    onClick={onClick}
    className={`
      px-3 py-1 rounded-lg text-xs border transition
      ${active 
        ? `${color} border-current bg-current/10`
        : "text-slate-500 border-slate-700 bg-slate-800"
      }
    `}
  >
    {label}
  </button>
);