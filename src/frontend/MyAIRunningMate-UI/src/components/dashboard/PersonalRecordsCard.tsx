import { useState } from 'react';
import type { DashboardTypes } from '../../types/dashboard/dashboard.types';
import { formatTime } from '../../services/helpers/formatTime';

interface PersonalRecordsCardProps {
  bestEfforts: DashboardTypes['bestEfforts'];
  onUpdateEffort: (label: string, seconds: number) => Promise<void>;
}

export const PersonalRecordsCard = ({ bestEfforts, onUpdateEffort }: PersonalRecordsCardProps) => {
  const [editingLabel, setEditingLabel] = useState<string | null>(null);
  const [tempSeconds, setTempSeconds] = useState<string>("");

  const handleSave = async (label: string) => {
    const parsed = parseInt(tempSeconds);
    if (!isNaN(parsed)) {
      await onUpdateEffort(label, parsed);
    }
    setEditingLabel(null);
  };

  return (
    <div className="rounded-3xl border border-slate-800 bg-slate-900/50 p-6">
      <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-6">Personal Records</h4>
      <div className="grid grid-cols-2 md:grid-cols-5 gap-4">
        {bestEfforts.map((effort) => {
          const isEditing = editingLabel === effort.distance_label;

          return (
            <div 
              key={effort.distance_label} 
              className={`group p-4 rounded-2xl border transition-all ${
                isEditing ? "bg-slate-950 border-blue-500 shadow-lg shadow-blue-500/10" : "bg-black/20 border-slate-800/50 hover:border-blue-500/50"
              }`}
            >
              <p className="text-[10px] font-bold text-slate-500 uppercase mb-1">{effort.distance_label}</p>
              <div className="flex justify-between items-end h-7">
                {isEditing ? (
                  <div className="flex items-center gap-2 w-full">
                    <input
                      type="number"
                      value={tempSeconds}
                      onChange={(e) => setTempSeconds(e.target.value)}
                      placeholder="Secs"
                      className="w-full bg-slate-900 text-white font-mono font-bold text-sm px-2 py-1 rounded border border-slate-700 focus:outline-none focus:border-blue-500 [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
                      autoFocus
                      onKeyDown={(e) => e.key === 'Enter' && handleSave(effort.distance_label)}
                    />
                    <button onClick={() => handleSave(effort.distance_label)} className="text-[10px] bg-emerald-600 hover:bg-emerald-500 text-white px-2 py-1 rounded font-black uppercase tracking-tight">Save</button>
                    <button onClick={() => setEditingLabel(null)} className="text-[10px] bg-slate-800 hover:bg-slate-700 text-slate-400 px-2 py-1 rounded font-black uppercase tracking-tight">×</button>
                  </div>
                ) : (
                  <>
                    <span className="font-mono font-black text-blue-400 text-xl leading-none">
                      {effort.time_achievement !== null ? formatTime(effort.time_achievement) : "--:--"}
                    </span>
                    <button 
                      onClick={() => {
                        setTempSeconds(effort.time_achievement?.toString() || "");
                        setEditingLabel(effort.distance_label);
                      }}
                      className="opacity-0 group-hover:opacity-100 text-[9px] bg-blue-600 hover:bg-blue-500 text-white px-2 py-1 rounded font-black uppercase tracking-tighter transition-all cursor-pointer"
                    >
                      Set
                    </button>
                  </>
                )}
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
};