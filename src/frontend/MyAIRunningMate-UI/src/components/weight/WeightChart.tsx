import React from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import type { WeightResponse } from '../../types/weight/weightResponse';
import { WeightTooltip } from './WeightTooltip';

interface WeightChartProps {
  weights: WeightResponse[];
  loading: boolean;
}

export const WeightChart: React.FC<WeightChartProps> = ({ weights, loading }) => {
  return (
    <div className="lg:col-span-2 p-6 rounded-xl bg-slate-900 border border-slate-800">
      <h3 className="text-slate-500 text-xs font-bold uppercase mb-6">
        Historical Trend
      </h3>

      <div className="h-80 w-full">
        {loading ? (
          <div className="h-full flex items-center justify-center text-slate-500">
            Loading history...
          </div>
        ) : (
          <ResponsiveContainer width="100%" height="100%">
            <LineChart data={weights}>
              <CartesianGrid
                strokeDasharray="3 3"
                vertical={false}
                stroke="#1e293b"
              />
              <XAxis
                dataKey="created_at"
                tickFormatter={(str) =>
                  new Date(str).toLocaleDateString('en-US', {
                    month: 'numeric',
                    day: 'numeric',
                    year: '2-digit',
                  })
                }
                fontSize={10}
                tick={{ fill: '#64748b' }}
                axisLine={false}
                tickLine={false}
                minTickGap={20}
              />
              <YAxis
                domain={['dataMin - 2', 'dataMax + 2']}
                fontSize={10}
                tick={{ fill: '#64748b' }}
                axisLine={false}
              />
              <Tooltip
                content={<WeightTooltip />}
                cursor={{
                  stroke: '#1e293b',
                  strokeWidth: 2,
                }}
              />
              <Line
                type="monotone"
                dataKey="weight_in_pounds"
                stroke="#38bdf8"
                strokeWidth={2}
                dot={{ fill: '#38bdf8', r: 3 }}
                activeDot={{ r: 5, strokeWidth: 0 }}
              />
            </LineChart>
          </ResponsiveContainer>
        )}
      </div>
    </div>
  );
};