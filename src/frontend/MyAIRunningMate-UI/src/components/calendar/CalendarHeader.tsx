import type { TrainingPlanViewResponse } from '../../types/nexus/trainingPlanViewResponse';
import logo from '../../assets/applogo.png';

interface CalendarHeaderProps {
  trainingPlan: TrainingPlanViewResponse | null;
}

export function CalendarHeader({ trainingPlan }: CalendarHeaderProps) {
  return (
    <div className="flex flex-col gap-2">
      <div className="flex items-center gap-4">
        <img src={logo} alt="Logo" className="h-14 w-14 rounded-xl shadow-lg shadow-blue-900/20" />
        <div>
          <h2 className="text-3xl font-black tracking-tighter uppercase italic">Activity Matrix</h2>
          <p className="text-slate-400 font-medium">Your exercise calendar.</p>
        </div>
      </div>
      {trainingPlan && (
        <p className="text-xs font-medium text-blue-400 mt-1 uppercase tracking-wider">
          🏆 Active Track: {trainingPlan.training_plan.title}
        </p>
      )}
    </div>
  );
}