import React, { useState, useMemo } from 'react';
import type { WeightResponse } from '../../types/weight/weightResponse';

interface WeightFormProps {
  weights: WeightResponse[];
  onLogWeight: (pounds: number) => Promise<void>;
  loading: boolean;
}

export const WeightForm: React.FC<WeightFormProps> = ({ weights, onLogWeight, loading }) => {
  const [newWeight, setNewWeight] = useState('');

  const handleFormSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const pounds = parseFloat(newWeight);
    if (isNaN(pounds) || pounds <= 0) return;

    await onLogWeight(pounds);
    setNewWeight('');
  };

  const weightDiff = useMemo(() => {
  if (weights.length <= 1) return '0.0';
  
  // Newest entry minus the oldest entry in the vault
  return (
    weights[0].weight_in_pounds - weights[weights.length - 1].weight_in_pounds
  ).toFixed(1);
}, [weights]);

  return (
    <div className="p-6 rounded-xl bg-slate-900 border border-slate-800">
      <h3 className="text-slate-500 text-xs font-bold uppercase mb-6">
        New Entry
      </h3>

      <form onSubmit={handleFormSubmit} className="space-y-4">
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
            onChange={(e) => setNewWeight(e.target.value)}
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
          Logs are timestamped automatically by the server.
        </p>
        <p className="mt-2 text-sm text-slate-400">
          Net Change: {weightDiff} lbs
        </p>
      </div>
    </div>
  );
};