import { useEffect, useMemo, useState } from 'react';
import { weightService } from '../services/api/weight/weight.service';
import { WeightChart } from '../components/weight/WeightChart';
import { WeightForm } from '../components/weight/WeightForm';
import type { WeightResponse } from '../types/weight/weightResponse';

export const WeightPage = () => {
  const [weights, setWeights] = useState<WeightResponse[]>([]);
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
      setError('Could not retrieve weight history.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadWeights();
  }, []);

  const handleLogWeight = async (pounds: number) => {
    try {
      setError(null);
      await weightService.log(pounds);
      await loadWeights();
    } catch (err) {
      console.error(err);
      setError('Could not save weight entry.');
    }
  };

  const showStaleWarning = useMemo(() => {
    if (weights.length === 0) return false;

    const latestLogDate = new Date(weights[weights.length - 1].created_at);
    const now = new Date();
    
    const diffInMs = now.getTime() - latestLogDate.getTime();
    const diffInDays = diffInMs / (1000 * 60 * 60 * 24);

    return diffInDays > 7;
  }, [weights]);

  return (
    <div className="space-y-8 text-white">
      <h2 className="text-3xl font-bold">Weight Vault</h2>

      {error && (
        <div className="rounded-lg border border-red-500/20 bg-red-950/30 p-4 text-red-400 text-sm">
          {error}
        </div>
      )}

      {showStaleWarning && (
        <div className="rounded-lg border border-amber-500/20 bg-amber-950/30 p-4 text-amber-400 text-sm flex items-center gap-2">
          <span>⚠️</span>
          <span>It has been more than a week since you last logged. Check your weight now!</span>
        </div>
      )}

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        <WeightChart weights={weights





        } loading={loading} />
        <WeightForm weights={weights} onLogWeight={handleLogWeight} loading={loading} />
      </div>
    </div>
  );
};