import type { TrainingPlanEventResponse } from '../../types/nexus/trainingPlanEventResponse';

interface TrainingPlanTargetProps {
  trainingEvent: TrainingPlanEventResponse;
  exerciseEmoji: string;
  formattedDistance: string;
}

export function TrainingPlanTarget({ trainingEvent, exerciseEmoji, formattedDistance }: TrainingPlanTargetProps) {
  return (
    <div className="mb-2 p-1.5 rounded bg-blue-500/10 border border-blue-500/20 text-[11px] text-blue-300 leading-tight">
      <div className="flex justify-between font-bold">
        <span className="truncate">
          {exerciseEmoji} {trainingEvent.exercise_subtype}
        </span>
        {trainingEvent.distance_metres > 0 && (
          <span className="font-mono text-[10px] bg-blue-500/20 px-1 rounded text-white flex-shrink-0">
            {formattedDistance}
          </span>
        )}
      </div>
      {trainingEvent.description && (
        <p className="text-[10px] text-slate-400 mt-0.5 line-clamp-2 italic">
          "{trainingEvent.description}"
        </p>
      )}
    </div>
  );
}