import type { TrainingPlanEventResponse } from "../types/nexus/trainingPlanEventResponse";
import type { TrainingPlanViewResponse } from "../types/nexus/trainingPlanViewResponse";


export const EXERCISE_TYPES = [
  'Run',
  'Swim',
  'Walk',
  'Training',
  'Cycling',
  'Hiking',
  'Rest',
] as const;

export type ExerciseType = (typeof EXERCISE_TYPES)[number];

export const EXERCISE_SUBTYPES_BY_TYPE: Record<ExerciseType, readonly string[]> = {
  Run: [
    'Easy Run',
    'Recovery Run',
    'Long Run',
    'Tempo Run',
    'Fartlek',
    'Progression Run',
    'Interval Run',
    'Hill Repeats',
  ],
  Swim: ['Easy Swim', 'Long Swim', 'Interval Swim'],
  Walk: ['Walk'],
  Training: ['Gym Training'],
  Cycling: ['Cycling'],
  Hiking: ['Hiking'],
  Rest: ['Rest'],
};

const normalizeKey = (value: string) => value.trim().toLowerCase().replace(/\s+/g, ' ');

export const isExerciseType = (value: string): value is ExerciseType =>
  EXERCISE_TYPES.some((type) => normalizeKey(type) === normalizeKey(value));

export const normalizeExerciseType = (value: string): ExerciseType => {
  if (isExerciseType(value)) {
    return EXERCISE_TYPES.find((type) => normalizeKey(type) === normalizeKey(value))!;
  }

  const key = normalizeKey(value);
  if (key.includes('swim')) return 'Swim';
  if (key.includes('walk')) return 'Walk';
  if (key.includes('cycl') || key.includes('bike')) return 'Cycling';
  if (key.includes('hike')) return 'Hiking';
  if (key.includes('gym') || key.includes('train') || key.includes('weight')) return 'Training';
  if (key.includes('run')) return 'Run';
  if (key.includes('rest')) return 'Rest';

  return 'Run';
};

export const getSubtypesForType = (exercise_type: ExerciseType): readonly string[] =>
  EXERCISE_SUBTYPES_BY_TYPE[exercise_type];

export const normalizeExerciseSubtype = (
  exercise_type: ExerciseType,
  value: string
): string => {
  const options = getSubtypesForType(exercise_type);
  const match = options.find((option) => normalizeKey(option) === normalizeKey(value));
  return match ?? options[0];
};

export const normalizeTrainingPlanEvent = (
  event: TrainingPlanEventResponse
): TrainingPlanEventResponse => {
  const exercise_type = normalizeExerciseType(event.exercise_type);
  return {
    ...event,
    exercise_type,
    exercise_subtype: normalizeExerciseSubtype(exercise_type, event.exercise_subtype),
  };
};

export const normalizeTrainingPlan = (plan: TrainingPlanViewResponse): TrainingPlanViewResponse => ({
  ...plan,
  events: plan.events.map(normalizeTrainingPlanEvent),
});
