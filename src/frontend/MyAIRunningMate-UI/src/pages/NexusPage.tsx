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
  'w-full bg-slate-900 border border-slate-600 rounded-lg p-1.5 text-white text-xs focus:outline-none focus:ring-2 focus:ring-blue-500/50';

const formatDistance = (metres: number) => {
  if (metres <= 0) return '—';
  if (metres >= 1000) return `${(metres / 1000).toFixed(1)} km`;
  return `${metres} m`;
};

type IndexedEvent = {
  event: TrainingPlanEventView;
  originalIndex: number;
};

const getPhasesForLength = (weeks: number) => {
  const basePhases = {
    4: [
      { name: 'BASE', start: 1, end: 1, bg: 'bg-cyan-500/10 text-cyan-400 border-cyan-500/30' },
      { name: 'BUILD', start: 2, end: 3, bg: 'bg-orange-500/10 text-orange-400 border-orange-500/30' },
      { name: 'PEAK', start: 4, end: 4, bg: 'bg-red-500/10 text-red-400 border-red-500/30' },
      { name: 'TAPER', start: 5, end: 5, bg: 'bg-yellow-500/10 text-yellow-400 border-yellow-500/30' },
    ],
    8: [
      { name: 'BASE', start: 1, end: 2, bg: 'bg-cyan-500/10 text-cyan-400 border-cyan-500/30' },
      { name: 'BUILD', start: 3, end: 5, bg: 'bg-orange-500/10 text-orange-400 border-orange-500/30' },
      { name: 'PEAK', start: 6, end: 7, bg: 'bg-red-500/10 text-red-400 border-red-500/30' },
      { name: 'TAPER', start: 8, end: 8, bg: 'bg-yellow-500/10 text-yellow-400 border-yellow-500/30' },
    ],
    12: [
      { name: 'BASE', start: 1, end: 2, bg: 'bg-cyan-500/10 text-cyan-400 border-cyan-500/30' },
      { name: 'BUILD', start: 3, end: 7, bg: 'bg-orange-500/10 text-orange-400 border-orange-500/30' },
      { name: 'PEAK', start: 8, end: 10, bg: 'bg-red-500/10 text-red-400 border-red-500/30' },
      { name: 'TAPER', start: 11, end: 12, bg: 'bg-yellow-500/10 text-yellow-400 border-yellow-500/30' },
    ],
  }[weeks] || [];

  return [
    ...basePhases,
    { name: 'RECOVERY', start: weeks + 1, end: weeks + 1, bg: 'bg-emerald-500/10 text-emerald-400 border-emerald-500/30' }
  ];
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
          // Reset distance out if changing strictly back to Rest
          distanceMetres: exerciseType === 'Rest' ? 0 : event.distanceMetres,
          description: exerciseType === 'Rest' ? 'Rest Day' : event.description,
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
      const normalized = normalizeTrainingPlan(generatedPlan);

      // --- HYDRATE ENTIRE HORIZON ARRAY (Including missing rest days) ---
      // This builds a continuous array state so every day can be modified
      if (normalized.startDate && normalized.trainingPlanEvents) {
        const startMs = new Date(normalized.startDate).getTime();
        const totalDays = (formData.schedule_length_weeks + 1) * 7;
        
        const eventMap = new Map<string, TrainingPlanEventView>();
        normalized.trainingPlanEvents.forEach((ev) => {
          const dateKey = new Date(ev.eventDate).toISOString().split('T')[0];
          eventMap.set(dateKey, ev);
        });

        const completeEvents: TrainingPlanEventView[] = [];
        for (let i = 0; i < totalDays; i++) {
          const targetDate = new Date(startMs + i * 24 * 60 * 60 * 1000);
          const dateKey = targetDate.toISOString().split('T')[0];
          
          if (eventMap.has(dateKey)) {
            completeEvents.push(eventMap.get(dateKey)!);
          } else {
            // Generate a mutable default state wrapper for structural empty slots
            completeEvents.push({
              eventDate: targetDate.toISOString(),
              exerciseType: 'Rest',
              exerciseSubtype: 'Rest',
              description: 'Rest Day',
              distanceMetres: 0,
            });
          }
        }
        normalized.trainingPlanEvents = completeEvents;
      }

      setPlan(normalized);
      setStatusMessage({
        type: 'success',
        text: 'Training plan generated. Complete calendar is open for edits.',
      });
    } catch {
      setStatusMessage({
        type: 'error',
        text: 'Failed to generate a training plan. Ensure backend microservices are up.',
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

  // Maps the fully populated state flat array cleanly into structural 7-day groups
  const organizedWeeks = useMemo(() => {
    if (!plan?.trainingPlanEvents?.length) return [];

    const totalPlanWeeks = formData.schedule_length_weeks + 1;
    const weeksArray = [];
    const phaseConfig = getPhasesForLength(formData.schedule_length_weeks);

    for (let w = 0; w < totalPlanWeeks; w++) {
      const weekNumber = w + 1;
      const currentPhase = phaseConfig.find(p => weekNumber >= p.start && weekNumber <= p.end) || {
        name: 'BUILD',
        bg: 'bg-slate-500/10 text-slate-400 border-slate-500/30'
      };

      const days = [];
      for (let d = 0; d < 7; d++) {
        const originalIndex = w * 7 + d;
        const event = plan.trainingPlanEvents[originalIndex];
        
        if (event) {
          days.push({
            date: new Date(event.eventDate),
            event,
            originalIndex
          });
        }
      }

      weeksArray.push({
        weekNumber,
        phase: currentPhase,
        days
      });
    }

    return weeksArray;
  }, [plan, formData.schedule_length_weeks]);

  return (
    <div className="max-w-7xl mx-auto space-y-8 px-4">
      <div>
        <h2 className="text-3xl font-bold">Nexus AI Mate</h2>
        <p className="text-slate-400">Configure your autonomous training architect.</p>
      </div>

      <form onSubmit={handleGenerate} className="p-6 rounded-2xl border border-blue-500/30 bg-blue-500/5 max-w-4xl">
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
              Microcycles: {formData.schedule_length_weeks} Weeks Phase Training + 1 Week Autonomous Recovery
            </p>
            {plan.description && (
              <p className="text-slate-300 mt-2 text-sm max-w-4xl leading-relaxed">{plan.description}</p>
            )}
          </div>

          <div className="space-y-4">
            <div className="flex justify-between items-center border-b border-slate-800 pb-2">
              <h4 className="text-lg font-semibold text-slate-200">Macrocycle Horizon Grid</h4>
              <span className="text-xs text-slate-500">Every single day (including Rest) is fully customizable.</span>
            </div>

            <div className="space-y-6">
              {organizedWeeks.map((week) => (
                <div key={week.weekNumber} className="flex gap-4 items-stretch">
                  
                  {/* Left Phase Bracketing Sidebar */}
                  <div className={`w-32 flex-shrink-0 flex flex-col justify-between p-3 rounded-xl border ${week.phase.bg}`}>
                    <div>
                      <div className="text-xs tracking-wider font-semibold opacity-60">WEEK {week.weekNumber}</div>
                      <div className="text-sm font-black tracking-wide mt-0.5">{week.phase.name}</div>
                    </div>
                    <div className="text-[10px] opacity-40 font-mono mt-4">
                      Days {(week.weekNumber - 1) * 7 + 1} - {week.weekNumber * 7}
                    </div>
                  </div>

                  {/* 7-Day Matrix Row */}
                  <div className="grid grid-cols-7 flex-1 gap-2">
                    {week.days.map(({ date, event, originalIndex }) => {
                      const exerciseType = normalizeExerciseType(event.exerciseType);
                      const subtypeOptions = getSubtypesForType(exerciseType);
                      const exerciseSubtype = normalizeExerciseSubtype(exerciseType, event.exerciseSubtype);
                      const isRest = exerciseType === 'Rest';

                      return (
                        <div 
                          key={originalIndex}
                          className={`p-2.5 rounded-xl border flex flex-col justify-between min-h-[15rem] shadow-sm transition-all duration-150 space-y-2 ${
                            isRest 
                              ? 'border-slate-800 bg-slate-950/40 opacity-70 hover:opacity-100 hover:border-slate-700' 
                              : 'border-slate-700 bg-slate-800/30 hover:border-slate-500'
                          }`}
                        >
                          <div className="flex justify-between items-start gap-1">
                            <span className={`text-[10px] font-bold font-mono ${isRest ? 'text-slate-500' : 'text-blue-400'}`}>
                              {date.toLocaleDateString(undefined, { weekday: 'short', day: 'numeric' })}
                            </span>
                            {!isRest && (
                              <span className="text-[10px] bg-blue-500/10 px-1.5 py-0.5 rounded text-slate-400 font-mono">
                                {formatDistance(event.distanceMetres)}
                              </span>
                            )}
                          </div>

                          <div className="space-y-1.5 flex-1 flex flex-col justify-start">
                            {/* Exercise Type Dropdown */}
                            <div>
                              <select
                                value={exerciseType}
                                onChange={(e) => updateExerciseType(originalIndex, e.target.value as ExerciseType)}
                                className={inputClassName}
                              >
                                {EXERCISE_TYPES.map((type) => (
                                  <option key={type} value={type}>{type}</option>
                                ))}
                              </select>
                            </div>

                            {/* Subtype Dropdown */}
                            <div>
                              <select
                                value={exerciseSubtype}
                                disabled={isRest}
                                className={`${inputClassName} disabled:opacity-40 disabled:bg-slate-950`}
                                onChange={(e) => updateExerciseSubtype(originalIndex, e.target.value)}
                              >
                                {subtypeOptions.map((subtype) => (
                                  <option key={subtype} value={subtype}>{subtype}</option>
                                ))}
                              </select>
                            </div>

                            {/* Workout Description */}
                            <div className="flex-1">
                              <textarea
                                value={event.description || ''}
                                onChange={(e) => updateEventField(originalIndex, 'description', e.target.value)}
                                rows={3}
                                placeholder={isRest ? 'Rest Day' : 'Workout details...'}
                                className={`${inputClassName} resize-none h-16 text-[11px] leading-tight`}
                              />
                            </div>
                          </div>

                          {/* Distance Input Field */}
                          <div>
                            <input
                              type="number"
                              min={0}
                              step={100}
                              disabled={isRest}
                              value={isRest ? '' : event.distanceMetres}
                              onChange={(e) => updateEventField(originalIndex, 'distanceMetres', e.target.value)}
                              className={`${inputClassName} font-mono disabled:opacity-30 disabled:bg-slate-950`}
                              placeholder={isRest ? '—' : 'Meters'}
                            />
                          </div>
                        </div>
                      );
                    })}
                  </div>

                </div>
              ))}
            </div>
          </div>

          <button
            type="button"
            onClick={handleFinalize}
            disabled={finalizing || generating}
            className="w-full bg-emerald-600 hover:bg-emerald-500 disabled:bg-emerald-900 disabled:text-slate-400 font-bold py-3 rounded-lg transition-colors mt-6"
          >
            {finalizing ? 'Finalizing plan…' : 'Finalize Training Plan'}
          </button>
        </section>
      )}
    </div>
  );
};