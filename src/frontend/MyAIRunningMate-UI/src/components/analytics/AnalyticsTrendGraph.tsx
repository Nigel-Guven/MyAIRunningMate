import React from 'react';
import {
  ResponsiveContainer,
  LineChart,
  CartesianGrid,
  XAxis,
  YAxis,
  Tooltip,
  Legend,
  Line,
} from 'recharts';
import { AnalyticsTooltip } from './AnalyticsTooltip';

export interface TrendDataPoint {
  vo2: number;
  hr: number;
  power: number;
  speed: number;
  percent: number;
}

interface AnalyticsTrendGraphProps {
  data: TrendDataPoint[];
}

export const AnalyticsTrendGraph: React.FC<AnalyticsTrendGraphProps> = ({ data }) => {
  return (
    <div className="rounded-xl border border-slate-800 bg-slate-900 p-6">
      <div className="mb-4">
        <h3 className="text-lg font-semibold text-slate-200">
          Physiological Trends
        </h3>
        <p className="text-xs text-slate-500">
          Normalized trend comparison over the past year
        </p>
      </div>

      <div className="h-96">
        <ResponsiveContainer width="100%" height="100%">
          <LineChart data={data}>
            <CartesianGrid
              stroke="#334155"
              strokeDasharray="3 3"
              vertical={false}
            />

            <XAxis
              dataKey="month"
              tick={{ fill: "#94a3b8", fontSize: 12 }}
              stroke="#475569"
            />

            <YAxis
              domain={[0, 100]}
              tick={{ fill: "#94a3b8", fontSize: 12 }}
              stroke="#475569"
              tickFormatter={(v) => `${v}%`}
            />

            <Tooltip content={<AnalyticsTooltip />} />
            <Legend />

            <Line
              type="monotone"
              dataKey="vo2"
              name="VO₂ Max"
              stroke="#06b6d4"
              strokeWidth={3}
              dot={false}
            />

            <Line
              type="monotone"
              dataKey="hr"
              name="LT Heart Rate"
              stroke="#ef4444"
              strokeWidth={2}
              dot={false}
            />

            <Line
              type="monotone"
              dataKey="power"
              name="LT Power"
              stroke="#f59e0b"
              strokeWidth={2}
              dot={false}
            />

            <Line
              type="monotone"
              dataKey="speed"
              name="LT Speed"
              stroke="#22c55e"
              strokeWidth={2}
              dot={false}
            />

            <Line
              type="monotone"
              dataKey="percent"
              name="LT %"
              stroke="#a855f7"
              strokeWidth={2}
              dot={false}
            />
          </LineChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
};