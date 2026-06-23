import type { IngestionViewResponse } from "../../types/ingestion/ingestionViewResponse";

interface TelemetrySuccessZoneProps {
  result: IngestionViewResponse;
  onReset: () => void;
}

export const TelemetrySuccessZone = ({ result, onReset }: TelemetrySuccessZoneProps) => {
  return (
    <div className="animate-in zoom-in-95 duration-500 bg-slate-900/50 border border-green-500/20 rounded-3xl p-8 text-center space-y-6">
      <div className="w-16 h-16 bg-green-500/10 border border-green-500/30 rounded-full flex items-center justify-center mx-auto mb-2">
        <svg className="w-8 h-8 text-green-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="3" d="M5 13l4 4L19 7" />
        </svg>
      </div>
      <div>
        <h3 className="text-2xl font-black italic uppercase text-white tracking-tighter">Analysis Complete</h3>
        <p className="text-xs text-slate-400 font-mono uppercase mt-1">ID: {result.garmin_activity_id}</p>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-4 gap-4 py-2">
        <div className="bg-black/20 p-4 rounded-2xl border border-slate-800">
          <p className="text-[10px] text-slate-500 uppercase font-bold mb-1">Type</p>
          <p className="text-white text-lg font-black uppercase italic tracking-tighter">{result.exercise_type}</p>
        </div>
        <div className="bg-black/20 p-4 rounded-2xl border border-slate-800">
          <p className="text-[10px] text-slate-500 uppercase font-bold mb-1">Distance</p>
          <p className="text-white text-lg font-black italic tracking-tighter">{(result.distance_metres / 1000).toFixed(2)} km</p>
        </div>
        <div className="bg-black/20 p-4 rounded-2xl border border-slate-800">
          <p className="text-[10px] text-slate-500 uppercase font-bold mb-1">Duration</p>
          <p className="text-white text-lg font-black italic tracking-tighter">
            {Math.floor(result.duration_seconds / 60)}m {Math.floor(result.duration_seconds % 60)}s
          </p>
        </div>
        <div className="bg-black/20 p-4 rounded-2xl border border-slate-800">
          <p className="text-[10px] text-slate-500 uppercase font-bold mb-1">Laps</p>
          <p className="text-white text-lg font-black italic tracking-tighter">{result.number_of_laps}</p>
        </div>
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div className="bg-black/20 p-4 rounded-2xl border border-slate-800 flex justify-between items-center px-6">
          <span className="text-[10px] text-slate-500 uppercase font-bold">Training Effect</span>
          <span className="text-orange-400 font-black italic text-lg tracking-tight">{result.training_effect.toFixed(1)}</span>
        </div>
        <div className="bg-black/20 p-4 rounded-2xl border border-slate-800 flex justify-between items-center px-6">
          <span className="text-[10px] text-slate-500 uppercase font-bold">Status</span>
          <span className="text-green-400 font-black uppercase italic tracking-tighter text-xs">{result.activity_status}</span>
        </div>
      </div>

      <div className="pt-4">
        <button 
          onClick={onReset}
          className="text-[10px] font-black uppercase tracking-widest text-slate-500 hover:text-white transition-colors"
        >
          Close Laboratory
        </button>
      </div>
    </div>
  );
};