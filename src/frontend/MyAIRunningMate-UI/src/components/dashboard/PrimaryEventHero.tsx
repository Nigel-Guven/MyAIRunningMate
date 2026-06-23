import { getDaysUntil } from "../../services/helpers/getDaysUntil";
import type { DashboardTypes } from "../../types/dashboard/dashboard.types";

interface PrimaryEventHeroProps {
  primaryEvent: DashboardTypes['primaryEvent'];
}

export const PrimaryEventHero = ({ primaryEvent }: PrimaryEventHeroProps) => {
  return (
    <div className="relative overflow-hidden rounded-3xl bg-gradient-to-br from-blue-600 via-blue-700 to-indigo-900 p-8 text-white shadow-2xl shadow-blue-900/40 border border-white/10">
      <div className="relative z-10 grid grid-cols-1 lg:grid-cols-2 gap-8 items-center">
        <div>
          <div className="flex items-center gap-3 mb-4">
            <span className="px-3 py-1 rounded-full bg-black/20 border border-white/20 text-[10px] font-black uppercase tracking-widest">
              {primaryEvent?.event_type || 'Race'} • {(primaryEvent?.distance_metres || 0) / 1000}KM
            </span>
          </div>
          <h3 className="text-5xl font-black italic tracking-tighter uppercase leading-none mb-4">
            {primaryEvent?.event_name || "No Objective Set"}
          </h3>
          <div className="flex flex-wrap gap-6 text-blue-100/80 mb-6">
            <div className="flex flex-col">
              <span className="text-[10px] uppercase font-bold opacity-60">Location</span>
              <span className="text-sm font-bold uppercase">{primaryEvent?.event_location || 'Unknown'}</span>
            </div>
            <div className="flex flex-col border-l border-white/20 pl-6">
              <span className="text-[10px] uppercase font-bold opacity-60">Date</span>
              <span className="text-sm font-bold uppercase">
                {primaryEvent ? new Date(primaryEvent.event_date).toLocaleDateString() : '--'}
              </span>
            </div>
          </div>
          {primaryEvent?.event_info && primaryEvent.event_info !== "null" && (
            <p className="text-blue-100/70 text-sm italic max-w-md leading-relaxed mb-6">"{primaryEvent.event_info}"</p>
          )}
          {primaryEvent?.event_url && primaryEvent.event_url !== "null" && (
            <a href={primaryEvent.event_url} target="_blank" rel="noreferrer" className="inline-flex items-center gap-2 text-[10px] font-black uppercase tracking-[0.2em] bg-white/10 hover:bg-white/20 px-4 py-2 rounded-lg transition-all">
              Event Website →
            </a>
          )}
        </div>

        <div className="flex justify-start lg:justify-end gap-12">
          <div className="text-center">
            <p className="text-7xl font-black italic leading-none">{primaryEvent ? getDaysUntil(primaryEvent.event_date) : '--'}</p>
            <p className="text-blue-200 text-[10px] uppercase font-bold tracking-widest mt-2">Days Remaining</p>
          </div>
          <div className="text-center border-l border-white/20 pl-12">
            <p className="text-7xl font-black italic leading-none text-blue-300">3:30</p>
            <p className="text-blue-200 text-[10px] uppercase font-bold tracking-widest mt-2">Target Time</p>
          </div>
        </div>
      </div>
      <div className="absolute -right-8 -bottom-8 text-[14rem] font-black opacity-10 italic select-none leading-none">GO</div>
    </div>
  );
};