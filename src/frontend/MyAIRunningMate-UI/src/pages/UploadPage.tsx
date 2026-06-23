import { useState } from 'react';
import { ingestionService } from '../services/api/ingestion/ingestion.service';
import type { IngestionViewResponse } from '../types/ingestion/ingestionViewResponse';
import logo from '../assets/applogo.png';

export const UploadPage = () => {
  const [file, setFile] = useState<File | null>(null);
  const [progress, setProgress] = useState(0);
  const [status, setStatus] = useState<'idle' | 'uploading' | 'processing' | 'success' | 'error'>('idle');
  const [result, setResult] = useState<IngestionViewResponse | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selected = e.target.files?.[0];
    if (selected) {
      setFile(selected);
      setStatus('idle');
    }
  };

  const startUpload = async () => {
    if (!file) return;
    setStatus('uploading');
    setErrorMessage(null);
    try {
      const data = await ingestionService.uploadFitFile(file, (p) => {
        setProgress(p);
        if (p === 100) setStatus('processing');
      });
      setResult(data);
      setStatus('success');
    } catch (err: any) {
      setStatus('error');
      setErrorMessage(err.response?.status === 409 ? 'Duplicate Activity detected' : 'System Error');
    }
  };

  return (
    <div className="space-y-8 animate-in fade-in duration-700">
      
      {/* 1. Header Area */}
      <div className="flex justify-between items-end border-b border-slate-800 pb-6">
        <div className="flex items-center gap-4">
          <img src={logo} alt="Logo" className="h-14 w-14 rounded-xl shadow-lg shadow-blue-900/20" />
          <div>
            <h2 className="text-3xl font-black tracking-tighter uppercase italic">Ingestion Lab</h2>
            <p className="text-slate-400 font-medium">Upload your .FIT  files taken from Garmin Connect.</p>
          </div>
        </div>
      </div>

      {/* 2. Main Ingestion Console */}
      <div className="relative min-h-[450px] rounded-3xl border border-slate-800 bg-slate-900/30 backdrop-blur-sm overflow-hidden flex flex-col items-center justify-center p-12">
        <div className="w-full max-w-2xl">
          
          {/* IDLE STATE / FILE PICKER */}
          {status === 'idle' && (
            <div className="text-center space-y-8">
              <div className="relative group mx-auto w-64 h-64 border-2 border-dashed border-slate-700 rounded-full flex flex-col items-center justify-center hover:border-blue-500/50 hover:bg-blue-500/5 transition-all duration-500">
                <input
                  type="file"
                  accept=".fit"
                  onChange={handleFileChange}
                  className="absolute inset-0 opacity-0 cursor-pointer"
                />
                <div className="text-slate-500 group-hover:text-blue-400 transition-colors">
                  <svg className="w-12 h-12 mb-4 mx-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M7 16a4 4 0 01-.88-7.903A5 5 0 1115.9 6L16 6a5 5 0 011 9.9M15 13l-3-3m0 0l-3 3m3-3v12" />
                  </svg>
                  <p className="text-[10px] font-black uppercase tracking-widest leading-tight px-6 text-center">
                    {file ? file.name : 'Drop .FIT file or click'}
                  </p>
                </div>
              </div>

              {file && (
                <button 
                  onClick={startUpload}
                  className="animate-in slide-in-from-bottom-4 duration-500 px-10 py-4 bg-blue-600 hover:bg-blue-500 text-white rounded-2xl font-black uppercase italic tracking-tighter text-lg shadow-xl shadow-blue-900/40"
                >
                  Initiate Ingestion
                </button>
              )}
            </div>
          )}

          {/* PROGRESS STATE (Uploading/Processing) */}
          {(status === 'uploading' || status === 'processing') && (
            <div className="text-center space-y-8">
              <div className="relative w-48 h-48 mx-auto">
                <div className="absolute inset-0 rounded-full border-4 border-slate-800" />
                <div 
                  className="absolute inset-0 rounded-full border-4 border-blue-500 border-t-transparent animate-spin"
                  style={{ animationDuration: '2s' }}
                />
                <div className="absolute inset-0 flex flex-col items-center justify-center">
                  <span className="text-3xl font-black italic text-white leading-none">
                    {status === 'uploading' ? `${progress}%` : 'SCAN'}
                  </span>
                  <span className="text-[10px] font-bold text-slate-500 uppercase mt-2 tracking-widest">
                    {status === 'uploading' ? 'Uploading' : 'Processing'}
                  </span>
                </div>
              </div>
              <p className="text-slate-400 font-mono text-xs uppercase animate-pulse">
                Decrypting activity headers...
              </p>
            </div>
          )}

          {/* SUCCESS STATE - Rich Telemetry Output */}
          {status === 'success' && result && (
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
                  onClick={() => { setFile(null); setStatus('idle'); setResult(null); }}
                  className="text-[10px] font-black uppercase tracking-widest text-slate-500 hover:text-white transition-colors"
                >
                  Close Laboratory
                </button>
              </div>
            </div>
          )}

          {/* ERROR STATE */}
          {status === 'error' && (
            <div className="text-center space-y-6">
              <div className="w-16 h-16 bg-red-500/10 border border-red-500/30 rounded-full flex items-center justify-center mx-auto">
                <span className="text-red-500 text-3xl font-black">!</span>
              </div>
              <h3 className="text-xl font-black italic uppercase text-white">Ingestion Failed</h3>
              <p className="text-red-400/80 font-mono text-xs uppercase px-4 py-2 bg-red-500/5 border border-red-500/10 rounded-lg inline-block">
                {errorMessage}
              </p>
              <div className="pt-4">
                <button 
                  onClick={() => setStatus('idle')}
                  className="px-8 py-3 bg-slate-800 text-white rounded-xl font-bold uppercase text-xs tracking-widest hover:bg-slate-700 transition-all"
                >
                  Abort & Retry
                </button>
              </div>
            </div>
          )}

        </div>
      </div>

      {/* 3. Footer Informational Banner */}
      <div className="flex items-center gap-4 p-6 bg-blue-900/10 border border-blue-900/20 rounded-3xl">
        <div className="p-3 bg-blue-500/10 rounded-xl">
          <svg className="w-6 h-6 text-blue-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
        </div>
        <p className="text-xs text-blue-200/60 italic leading-relaxed">
          Ingestion Lab processes native binary .FIT telemetry files directly. Once processed, your running analytics profiles, metrics, map lines, and volume totals update across your database instantaneously.
        </p>
      </div>

    </div>
  );
};