import React, { useMemo, useState } from 'react';
import { nexusService } from '../services/api/nexus/nexus.service';
import type { TrainingPlanRequest } from '../services/api/nexus/nexus.types';
import type { TrainingPlanView } from '../types/views/trainingPlanView';
import { formatDateLong } from '../services/helpers/dateFormatter';

const formatDistance = (metres: number) => {
  if (metres <= 0) return '—';
  if (metres >= 1000) return `${(metres / 1000).toFixed(1)} km`;
  return `${metres} m`;
};

export const NexusPage = () => {
  const [formData, setFormData] = useState<TrainingPlanRequest>({
    primaryGoal: '5k',
    experienceYears: '1 or Less',
    runningLevel: 'Beginner',
    scheduleLengthWeeks: 4,
    poolAccess: 'None',
  });

  const [loading, setLoading] = useState(false);
  const [plan, setPlan] = useState<TrainingPlanView | null>(null);
  const [statusMessage, setStatusMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);

  const sortedEvents = useMemo(() => {
    if (!plan?.trainingPlanEvents?.length) return [];
    return [...plan.trainingPlanEvents].sort(
      (a, b) => new Date(a.eventDate).getTime() - new Date(b.eventDate).getTime()
    );
  }, [plan]);

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: name === 'scheduleLengthWeeks' ? parseInt(value, 10) : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setStatusMessage(null);
    setPlan(null);

    try {
      const generatedPlan = await nexusService.generateTrainingPlan(formData);
      setPlan(generatedPlan);
      setStatusMessage({ type: 'success', text: 'Training plan generated. Review the schedule below.' });
    } catch {
      setStatusMessage({
        type: 'error',
        text: 'Failed to generate a training plan. Ensure you are signed in and both APIs are running.',
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-4xl space-y-8">
      <div>
        <h2 className="text-3xl font-bold">Nexus AI Mate</h2>
        <p className="text-slate-400">Configure your autonomous training architect.</p>
      </div>

      <form onSubmit={handleSubmit} className="p-6 rounded-2xl border border-blue-500/30 bg-blue-500/5">
        <h3 className="text-xl font-bold mb-4 flex items-center gap-2">
          <span>🧠</span> AI Training Plan Requirements
        </h3>

        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-slate-400 mb-1">Primary Goal</label>
            <select
              name="primaryGoal"
              value={formData.primaryGoal}
              onChange={handleChange}
              className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
            >
              <option value="5k">5k</option>
              <option value="10k">10k</option>
              <option value="Half Marathon">Half Marathon</option>
              <option value="Marathon">Marathon</option>
              <option value="General Fitness">General Fitness</option>
            </select>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Running Experience</label>
              <select
                name="experienceYears"
                value={formData.experienceYears}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value="1 or Less">1 or Less years</option>
                <option value="2-3">2-3 years</option>
                <option value="4+ years">4+ years</option>
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Running Level</label>
              <select
                name="runningLevel"
                value={formData.runningLevel}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value="Beginner">Beginner</option>
                <option value="Intermediate">Intermediate</option>
                <option value="Advanced">Advanced</option>
                <option value="Expert">Expert</option>
              </select>
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Schedule Length</label>
              <select
                name="scheduleLengthWeeks"
                value={formData.scheduleLengthWeeks}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value={4}>4 weeks</option>
                <option value={8}>8 weeks</option>
                <option value={12}>12 weeks</option>
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Pool Access</label>
              <select
                name="poolAccess"
                value={formData.poolAccess}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value="None">None</option>
                <option value="25m Pool">25m Pool</option>
                <option value="50m Pool">50m Pool</option>
              </select>
            </div>
          </div>

          {statusMessage && (
            <div
              className={`p-3 rounded-lg text-sm ${
                statusMessage.type === 'success'
                  ? 'bg-green-500/20 text-green-400 border border-green-500/30'
                  : 'bg-red-500/20 text-red-400 border border-red-500/30'
              }`}
            >
              {statusMessage.text}
            </div>
          )}

          <button
            type="submit"
            disabled={loading}
            className="w-full bg-blue-600 hover:bg-blue-500 disabled:bg-blue-800 disabled:text-slate-400 font-bold py-3 rounded-lg transition-colors mt-4 flex justify-center items-center"
          >
            {loading ? 'Generating plan…' : 'Generate Training Plan'}
          </button>
        </div>
      </form>

      {plan && (
        <section className="p-6 rounded-2xl border border-slate-700 bg-slate-900/50 space-y-6">
          <div>
            <h3 className="text-2xl font-bold text-white">{plan.title}</h3>
            <p className="text-slate-400 mt-1">
              {formatDateLong(plan.startDate)} — {formatDateLong(plan.endDate)}
            </p>
            {plan.description && (
              <p className="text-slate-300 mt-3 leading-relaxed">{plan.description}</p>
            )}
          </div>

          <div>
            <h4 className="text-lg font-semibold text-slate-200 mb-3">
              Schedule ({sortedEvents.length} sessions)
            </h4>
            <ul className="space-y-3 max-h-[32rem] overflow-y-auto pr-1">
              {sortedEvents.map((event, index) => (
                <li
                  key={`${event.eventDate}-${index}`}
                  className="p-4 rounded-xl border border-slate-700/80 bg-slate-800/40"
                >
                  <div className="flex flex-wrap items-baseline justify-between gap-2">
                    <span className="font-medium text-blue-300">
                      {formatDateLong(event.eventDate)}
                    </span>
                    <span className="text-sm text-slate-400">
                      {formatDistance(event.distanceMetres)}
                    </span>
                  </div>
                  <p className="mt-1 text-sm font-semibold uppercase tracking-wide text-slate-300">
                    {event.exerciseType}
                    {event.exerciseSubtype ? ` · ${event.exerciseSubtype}` : ''}
                  </p>
                  <p className="mt-2 text-slate-400 text-sm">{event.description}</p>
                </li>
              ))}
            </ul>
          </div>
        </section>
      )}
    </div>
  );
};
