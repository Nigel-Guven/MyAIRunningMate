import React, { useEffect, useState } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import { getWeightHistory, logWeight } from '../services/weightService';
import type { WeightEntry } from '../types/weight';
import { WeightTooltip } from '../components/layout/WeightToolTip';

export const WeightPage = () => {
  const [weights, setWeights] = useState<WeightEntry[]>([]);
  const [newWeight, setNewWeight] = useState('');

  const loadData = async () => {
    try {
      const data = await getWeightHistory();
      setWeights(data);
    } catch (err) {
      console.error("Vault Access Error: Could not retrieve history");
    }
  };

  useEffect(() => { loadData(); }, []);

  const handleLogWeight = async (e: React.FormEvent) => {
    e.preventDefault();
    const val = parseFloat(newWeight);
    if (isNaN(val) || val <= 0) return;

    try {
      await logWeight(val);
      setNewWeight('');
      await loadData();
    } catch (err) {
      alert("Encryption Error: Could not commit data to vault");
    }
  };

  const weightDiff = weights.length > 1 
    ? (weights[weights.length - 1].weight_pounds - weights[0].weight_pounds).toFixed(1) 
    : '0.0';

  return (
    <div className="space-y-8 text-white">
      <h2 className="text-3xl font-bold">Weight Vault</h2>


      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Chart Section */}
        <div className="lg:col-span-2 p-6 rounded-xl bg-slate-900 border border-slate-800">
          <h3 className="text-slate-500 text-xs font-bold uppercase mb-6">Historical Trend</h3>
          <div className="h-80 w-full">
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={weights}>
                <CartesianGrid strokeDasharray="3 3" vertical={false} stroke="#1e293b" />
                <XAxis 
                  dataKey="created_at" 
                  tickFormatter={(str) => new Date(str).toLocaleDateString('en-US', { month: 'short', day: 'numeric' })} 
                  fontSize={10}
                  tick={{ fill: '#64748b' }}
                  axisLine={false}
                  tickLine={false}
                  minTickGap={20} // Prevents dates from overlapping
                />
                <YAxis 
                  domain={['dataMin - 2', 'dataMax + 2']} 
                  fontSize={10} 
                  tick={{ fill: '#64748b' }}
                  axisLine={false}
                />
                <Tooltip 
                  content={<WeightTooltip />} 
                  cursor={{ stroke: '#1e293b', strokeWidth: 2 }} // Added a subtle vertical line on hover
                />
                <Line 
                  type="monotone" 
                  dataKey="weight_pounds" 
                  stroke="#38bdf8" 
                  strokeWidth={2} 
                  dot={{ fill: '#38bdf8', r: 3 }} 
                  activeDot={{ r: 5, strokeWidth: 0 }} 
                />
              </LineChart>
            </ResponsiveContainer>
          </div>
        </div>

        {/* Input Section */}
        <div className="p-6 rounded-xl bg-slate-900 border border-slate-800">
          <h3 className="text-slate-500 text-xs font-bold uppercase mb-6">New Entry</h3>
          <form onSubmit={handleLogWeight} className="space-y-4">
            <div>
              <label className="block text-sm text-slate-400 mb-2">Weight (lbs)</label>
              <input
                type="number"
                step="0.1"
                required
                placeholder="0.0"
                value={newWeight}
                onChange={(e) => setNewWeight(e.target.value)}
                className="w-full bg-slate-950 border border-slate-800 rounded-lg p-3 text-white focus:outline-none focus:border-sky-500 transition-colors"
              />
            </div>
            <button 
              type="submit" 
              className="w-full py-3 bg-sky-600 hover:bg-sky-500 text-white font-bold rounded-lg transition-all"
            >
              Commit to Vault
            </button>
          </form>
          
          <div className="mt-8 pt-8 border-t border-slate-800 text-center">
             <p className="text-slate-500 text-xs italic">
                Logs are timestamped automatically by the server.
             </p>
          </div>
        </div>
      </div>
    </div>
  );
};