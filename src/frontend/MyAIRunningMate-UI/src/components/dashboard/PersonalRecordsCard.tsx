import type { DashboardTypes } from '../../types/dashboard/dashboard.types';
import { formatTime } from '../../services/helpers/formatTime';

// Define the order and labels for the UI
const SWIMMING_CATEGORIES = ["100 Metre", "400 Metre", "750 Metre", "1km", "1500 Metre"];
const RUNNING_CATEGORIES = ["1km", "1 mile", "5k", "10k", "Half Marathon", "Marathon"];

interface PersonalRecordsCardProps {
  bestEfforts: DashboardTypes['bestEfforts'];
}

export const PersonalRecordsCard = ({ bestEfforts }: PersonalRecordsCardProps) => {
  
  // Helper to merge API data with the master list
  const getEffortsForCategory = (categories: string[], type: string) => {
    return categories.map((label) => {
      const record = bestEfforts.find(e => e.distance_label === label && e.exercise_type === type);
      return { label, time: record?.time_achievement || null };
    });
  };

  const renderGrid = (title: string, data: { label: string; time: number | null }[]) => (
    <div className="mb-8">
      <h5 className="text-xs font-bold text-slate-400 mb-4 uppercase tracking-wider">{title}</h5>
      <div className="grid grid-cols-2 md:grid-cols-6 gap-3">
        {data.map((item) => (
          <div key={item.label} className="p-3 rounded-xl bg-slate-950/50 border border-slate-800/60">
            <p className="text-[9px] font-bold text-slate-600 uppercase mb-1">{item.label}</p>
            <p className={`font-mono font-black text-lg ${item.time ? 'text-blue-400' : 'text-slate-700'}`}>
              {item.time ? formatTime(item.time) : "--:--"}
            </p>
          </div>
        ))}
      </div>
    </div>
  );

  return (
    <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6">
      <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-6">Personal Records</h4>
      {renderGrid("Swimming", getEffortsForCategory(SWIMMING_CATEGORIES, "swimming"))}
      {renderGrid("Running", getEffortsForCategory(RUNNING_CATEGORIES, "running"))}
    </div>
  );
};