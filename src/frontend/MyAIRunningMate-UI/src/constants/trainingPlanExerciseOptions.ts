import type { TrainingPlanEventView, TrainingPlanView } from '../types/views/trainingPlanView';

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

export const getSubtypesForType = (exerciseType: ExerciseType): readonly string[] =>
  EXERCISE_SUBTYPES_BY_TYPE[exerciseType];

export const normalizeExerciseSubtype = (
  exerciseType: ExerciseType,
  value: string
): string => {
  const options = getSubtypesForType(exerciseType);
  const match = options.find((option) => normalizeKey(option) === normalizeKey(value));
  return match ?? options[0];
};

export const normalizeTrainingPlanEvent = (
  event: TrainingPlanEventView
): TrainingPlanEventView => {
  const exerciseType = normalizeExerciseType(event.exerciseType);
  return {
    ...event,
    exerciseType,
    exerciseSubtype: normalizeExerciseSubtype(exerciseType, event.exerciseSubtype),
  };
};

export const normalizeTrainingPlan = (plan: TrainingPlanView): TrainingPlanView => ({
  ...plan,
  trainingPlanEvents: plan.trainingPlanEvents.map(normalizeTrainingPlanEvent),
});
