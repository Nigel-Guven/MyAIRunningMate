import React, { useEffect, useMemo, useState } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import { weightService } from '../services/api/weight/weight.service';
import { WeightTooltip } from '../components/layout/WeightTooltip';
import type { WeightResponse } from '../types/weight/weight.types';

export const WeightPage = () => {
  const [weights, setWeights] = useState<WeightResponse[]>([]);
  const [newWeight, setNewWeight] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const loadWeights = async () => {
    try {
      setLoading(true);
      setError(null);

      const data = await weightService.getHistory();

      setWeights(data);
    } catch (err) {
      console.error(err);

      setError(
        'Could not retrieve weight history.'
      );
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { loadWeights(); }, []);

  const handleLogWeight = async (e: React.FormEvent) => { 
    e.preventDefault();
    
    const pounds = parseFloat(newWeight);
    if (isNaN(pounds) || pounds <= 0) return;

    try {
      await weightService.log(pounds);

      setNewWeight('');
      await loadWeights();
    } catch (err) {
      console.error(err);

      setError(
        'Could not save weight entry.'
      );
    }
  };

  const weightDiff = useMemo(() => {
    if (weights.length <= 1) {
      return '0.0';
    }

    return (
      weights[weights.length - 1]
        .weight_in_pounds -
      weights[0].weight_in_pounds
    ).toFixed(1);

  }, [weights]);

  return (
    <div className="space-y-8 text-white">
      <h2 className="text-3xl font-bold">
        Weight Vault
      </h2>

      {error && (
        <div className="rounded-lg border border-red-500/20 bg-red-950/30 p-4 text-red-400 text-sm">
          {error}
        </div>
      )}

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">

        {/* Chart */}
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
              <ResponsiveContainer
                width="100%"
                height="100%"
              >
                <LineChart data={weights}>

                  <CartesianGrid
                    strokeDasharray="3 3"
                    vertical={false}
                    stroke="#1e293b"
                  />

                  <XAxis
                    dataKey="created_at"
                    tickFormatter={(str) =>
                      new Date(str).toLocaleDateString(
                        'en-US',
                        {
                          month: 'short',
                          day: 'numeric',
                        }
                      )
                    }
                    fontSize={10}
                    tick={{ fill: '#64748b' }}
                    axisLine={false}
                    tickLine={false}
                    minTickGap={20}
                  />

                  <YAxis
                    domain={[
                      'dataMin - 2',
                      'dataMax + 2',
                    ]}
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
                    dataKey="weight_pounds"
                    stroke="#38bdf8"
                    strokeWidth={2}
                    dot={{
                      fill: '#38bdf8',
                      r: 3,
                    }}
                    activeDot={{
                      r: 5,
                      strokeWidth: 0,
                    }}
                  />

                </LineChart>
              </ResponsiveContainer>
            )}
          </div>
        </div>

        {/* Form */}
        <div className="p-6 rounded-xl bg-slate-900 border border-slate-800">

          <h3 className="text-slate-500 text-xs font-bold uppercase mb-6">
            New Entry
          </h3>

          <form
            onSubmit={handleLogWeight}
            className="space-y-4"
          >
            <div>
              <label className="block text-sm text-slate-400 mb-2">
                Weight (lbs)
              </label>

              <input
                type="number"
                step="0.1"
                required
                placeholder="0.0"
                value={newWeight}
                onChange={(e) =>
                  setNewWeight(
                    e.target.value
                  )
                }
                className="w-full bg-slate-950 border border-slate-800 rounded-lg p-3 text-white focus:outline-none focus:border-sky-500 transition-colors"
              />
            </div>

            <button
              type="submit"
              disabled={loading}
              className="w-full py-3 bg-sky-600 hover:bg-sky-500 text-white font-bold rounded-lg transition-all disabled:opacity-50"
            >
              Commit to Vault
            </button>
          </form>

          <div className="mt-8 pt-8 border-t border-slate-800 text-center">
            <p className="text-slate-500 text-xs italic">
              Logs are timestamped automatically
              by the server.
            </p>

            <p className="mt-2 text-sm text-slate-400">
              Net Change: {weightDiff} lbs
            </p>
          </div>

        </div>
      </div>
    </div>
  );
};