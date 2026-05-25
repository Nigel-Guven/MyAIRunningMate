import React, { useMemo, useState } from 'react';
import {
  EXERCISE_TYPES,
  getSubtypesForType,
  normalizeExerciseSubtype,
  normalizeExerciseType,
  normalizeTrainingPlan,
  type ExerciseType,
} from '../constants/trainingPlanExerciseOptions';
import { nexusService } from '../services/api/nexus/nexus.service';
import type { TrainingPlanRequest } from '../services/api/nexus/nexus.types';
import type { TrainingPlanEventView, TrainingPlanView } from '../types/views/trainingPlanView';
import { formatDateLong } from '../services/helpers/dateFormatter';

const inputClassName =
  'w-full bg-slate-900 border border-slate-600 rounded-lg p-2 text-white text-sm focus:outline-none focus:ring-2 focus:ring-blue-500/50';

const formatDistance = (metres: number) => {
  if (metres <= 0) return '—';
  if (metres >= 1000) return `${(metres / 1000).toFixed(1)} km`;
  return `${metres} m`;
};

type IndexedEvent = {
  event: TrainingPlanEventView;
  originalIndex: number;
};

export const NexusPage = () => {
  const [formData, setFormData] = useState<TrainingPlanRequest>({
    primary_goal: '5k',
    experience_years: '1 or Less',
    running_level: 'Beginner',
    schedule_length_weeks: 4,
    pool_access: 'None',
  });

  const [generating, setGenerating] = useState(false);
  const [finalizing, setFinalizing] = useState(false);
  const [plan, setPlan] = useState<TrainingPlanView | null>(null);
  const [statusMessage, setStatusMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);

  const sortedEvents: IndexedEvent[] = useMemo(() => {
    if (!plan?.trainingPlanEvents?.length) return [];
    return plan.trainingPlanEvents
      .map((event, originalIndex) => ({ event, originalIndex }))
      .sort(
        (a, b) =>
          new Date(a.event.eventDate).getTime() - new Date(b.event.eventDate).getTime()
      );
  }, [plan]);

  const handleFormChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: name === 'schedule_length_weeks' ? parseInt(value, 10) : value,
    }));
  };

  const updateEventField = (
    originalIndex: number,
    field: 'description' | 'distanceMetres',
    value: string | number
  ) => {
    setPlan((prev) => {
      if (!prev) return prev;

      const events = prev.trainingPlanEvents.map((event, index) => {
        if (index !== originalIndex) return event;

        if (field === 'distanceMetres') {
          const metres = typeof value === 'number' ? value : parseInt(String(value), 10);
          return { ...event, distanceMetres: Number.isNaN(metres) ? 0 : Math.max(0, metres) };
        }

        return { ...event, description: String(value) };
      });

      return { ...prev, trainingPlanEvents: events };
    });
  };

  const updateExerciseType = (originalIndex: number, exerciseType: ExerciseType) => {
    setPlan((prev) => {
      if (!prev) return prev;

      const events = prev.trainingPlanEvents.map((event, index) => {
        if (index !== originalIndex) return event;

        return {
          ...event,
          exerciseType,
          exerciseSubtype: getSubtypesForType(exerciseType)[0],
        };
      });

      return { ...prev, trainingPlanEvents: events };
    });
  };

  const updateExerciseSubtype = (originalIndex: number, exerciseSubtype: string) => {
    setPlan((prev) => {
      if (!prev) return prev;

      const events = prev.trainingPlanEvents.map((event, index) => {
        if (index !== originalIndex) return event;

        const exerciseType = normalizeExerciseType(event.exerciseType);
        return {
          ...event,
          exerciseType,
          exerciseSubtype: normalizeExerciseSubtype(exerciseType, exerciseSubtype),
        };
      });

      return { ...prev, trainingPlanEvents: events };
    });
  };

  const handleGenerate = async (e: React.FormEvent) => {
    e.preventDefault();
    setGenerating(true);
    setStatusMessage(null);
    setPlan(null);

    try {
      const generatedPlan = await nexusService.generateTrainingPlan(formData);
      setPlan(normalizeTrainingPlan(generatedPlan));
      setStatusMessage({
        type: 'success',
        text: 'Training plan generated. Edit sessions below, then finalize.',
      });
    } catch {
      setStatusMessage({
        type: 'error',
        text: 'Failed to generate a training plan. Ensure you are signed in and both APIs are running.',
      });
    } finally {
      setGenerating(false);
    }
  };

  const handleFinalize = async () => {
    if (!plan) return;

    const normalizedPlan = normalizeTrainingPlan(plan);
    setPlan(normalizedPlan);
    setFinalizing(true);
    setStatusMessage(null);

    try {
      const result = await nexusService.finalizeTrainingPlan(normalizedPlan);
      setStatusMessage({ type: 'success', text: result.message });
    } catch {
      setStatusMessage({
        type: 'error',
        text: 'Failed to finalize the training plan. Check your edits and try again.',
      });
    } finally {
      setFinalizing(false);
    }
  };

  return (
    <div className="max-w-4xl space-y-8">
      <div>
        <h2 className="text-3xl font-bold">Nexus AI Mate</h2>
        <p className="text-slate-400">Configure your autonomous training architect.</p>
      </div>

      <form onSubmit={handleGenerate} className="p-6 rounded-2xl border border-blue-500/30 bg-blue-500/5">
        <h3 className="text-xl font-bold mb-4 flex items-center gap-2">
          <span>🧠</span> AI Training Plan Requirements
        </h3>

        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-slate-400 mb-1">Primary Goal</label>
            <select
              name="primary_goal"
              value={formData.primary_goal}
              onChange={handleFormChange}
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
                name="experience_years"
                value={formData.experience_years}
                onChange={handleFormChange}
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
                name="running_level"
                value={formData.running_level}
                onChange={handleFormChange}
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
                name="schedule_length_weeks"
                value={formData.schedule_length_weeks}
                onChange={handleFormChange}
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
                name="pool_access"
                value={formData.pool_access}
                onChange={handleFormChange}
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
            disabled={generating || finalizing}
            className="w-full bg-blue-600 hover:bg-blue-500 disabled:bg-blue-800 disabled:text-slate-400 font-bold py-3 rounded-lg transition-colors mt-4 flex justify-center items-center"
          >
            {generating ? 'Generating plan…' : 'Generate Training Plan'}
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
            <h4 className="text-lg font-semibold text-slate-200 mb-1">
              Schedule ({sortedEvents.length} sessions)
            </h4>
            <p className="text-sm text-slate-500 mb-3">
              Edit exercise details before finalizing your plan.
            </p>

            <ul className="space-y-4 max-h-[36rem] overflow-y-auto pr-1">
              {sortedEvents.map(({ event, originalIndex }) => {
                const exerciseType = normalizeExerciseType(event.exerciseType);
                const subtypeOptions = getSubtypesForType(exerciseType);
                const exerciseSubtype = normalizeExerciseSubtype(
                  exerciseType,
                  event.exerciseSubtype
                );

                return (
                <li
                  key={`${event.eventDate}-${originalIndex}`}
                  className="p-4 rounded-xl border border-slate-700/80 bg-slate-800/40 space-y-3"
                >
                  <div className="flex flex-wrap items-baseline justify-between gap-2">
                    <span className="font-medium text-blue-300">
                      {formatDateLong(event.eventDate)}
                    </span>
                    <span className="text-xs text-slate-500">
                      Preview: {formatDistance(event.distanceMetres)}
                    </span>
                  </div>

                  <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                    <div>
                      <label className="block text-xs font-medium text-slate-500 mb-1">
                        Exercise type
                      </label>
                      <select
                        value={exerciseType}
                        onChange={(e) =>
                          updateExerciseType(originalIndex, e.target.value as ExerciseType)
                        }
                        className={inputClassName}
                      >
                        {EXERCISE_TYPES.map((type) => (
                          <option key={type} value={type}>
                            {type}
                          </option>
                        ))}
                      </select>
                    </div>
                    <div>
                      <label className="block text-xs font-medium text-slate-500 mb-1">
                        Exercise subtype
                      </label>
                      <select
                        value={exerciseSubtype}
                        onChange={(e) =>
                          updateExerciseSubtype(originalIndex, e.target.value)
                        }
                        className={inputClassName}
                      >
                        {subtypeOptions.map((subtype) => (
                          <option key={subtype} value={subtype}>
                            {subtype}
                          </option>
                        ))}
                      </select>
                    </div>
                  </div>

                  <div>
                    <label className="block text-xs font-medium text-slate-500 mb-1">
                      Description
                    </label>
                    <textarea
                      value={event.description}
                      onChange={(e) =>
                        updateEventField(originalIndex, 'description', e.target.value)
                      }
                      rows={2}
                      className={`${inputClassName} resize-y min-h-[4rem]`}
                    />
                  </div>

                  <div className="max-w-xs">
                    <label className="block text-xs font-medium text-slate-500 mb-1">
                      Distance (metres)
                    </label>
                    <input
                      type="number"
                      min={0}
                      step={100}
                      value={event.distanceMetres}
                      onChange={(e) =>
                        updateEventField(originalIndex, 'distanceMetres', e.target.value)
                      }
                      className={inputClassName}
                    />
                  </div>
                </li>
                );
              })}
            </ul>
          </div>

          <button
            type="button"
            onClick={handleFinalize}
            disabled={finalizing || generating}
            className="w-full bg-emerald-600 hover:bg-emerald-500 disabled:bg-emerald-900 disabled:text-slate-400 font-bold py-3 rounded-lg transition-colors"
          >
            {finalizing ? 'Finalizing plan…' : 'Finalize Training Plan'}
          </button>
        </section>
      )}
    </div>
  );
};
