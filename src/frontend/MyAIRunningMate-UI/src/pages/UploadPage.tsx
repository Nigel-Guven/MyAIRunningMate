import { useState, useEffect } from 'react';
import type { IngestionViewDto } from '../types/views/ingestionView';
import { ingestionService } from '../services/api/ingestion/ingestion.service';
import { stravaService } from '../services/api/strava/strava.service';

export const UploadPage = () => {
  const [file, setFile] = useState<File | null>(null);
  const [progress, setProgress] = useState(0);
  const [status, setStatus] = useState<'idle' | 'uploading' | 'processing' | 'success' | 'error'>('idle');
  const [result, setResult] = useState<IngestionViewDto | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [stravaLoading, setStravaLoading] = useState(false);
  const [isStravaConnected, setIsStravaConnected] = useState(() => stravaService.isConnectedLocally());

  // Logic remains the same as your original component...
  useEffect(() => {
    const verify = async () => {
      try {
        const isConnected = await stravaService.verifyConnection();
        setIsStravaConnected(isConnected);
      } catch (err) { console.error('Strava verification failed', err); }
    };
    verify();
  }, []);

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
      
      {/* 1. Header & System Status */}
      <div className="flex justify-between items-end border-b border-slate-800 pb-6">
        <div>
          <h2 className="text-4xl font-black italic text-white tracking-tighter uppercase leading-none">
            Ingestion Lab
          </h2>
          <div className="flex items-center gap-3 mt-3 bg-slate-900/80 px-3 py-1.5 rounded-lg border border-slate-800 w-fit">
            <div className={`h-2 w-2 rounded-full ${isStravaConnected ? 'bg-green-500 animate-pulse' : 'bg-red-500'}`} />
            <span className="text-[10px] font-black text-slate-400 uppercase tracking-widest">
              Strava Uplink: {isStravaConnected ? 'Established' : 'Disconnected'}
            </span>
          </div>
        </div>

        <button
          onClick={() => stravaService.connect()}
          disabled={stravaLoading}
          className={`px-6 py-2 rounded-xl text-[10px] font-black uppercase tracking-widest transition-all ${
            isStravaConnected 
            ? 'bg-slate-800 text-slate-400 border border-slate-700 cursor-default' 
            : 'bg-orange-600 text-white hover:bg-orange-500 shadow-lg shadow-orange-900/20'
          }`}
        >
          {isStravaConnected ? 'System Linked' : 'Authorize Strava'}
        </button>
      </div>

      {/* 2. Main Terminal Area */}
      <div className="relative min-h-[450px] rounded-3xl border border-slate-800 bg-slate-900/30 backdrop-blur-sm overflow-hidden flex flex-col items-center justify-center p-12">
        
        {!isStravaConnected ? (
          <div className="text-center space-y-4 max-w-sm">
            <div className="mx-auto w-16 h-16 rounded-full bg-red-500/10 border border-red-500/30 flex items-center justify-center mb-6">
              <span className="text-red-500 text-2xl">!</span>
            </div>
            <h3 className="text-xl font-bold text-white uppercase italic">Handshake Required</h3>
            <p className="text-slate-500 text-sm leading-relaxed">
              To ingest training telemetry, an active connection to Strava must be established for verification.
            </p>
          </div>
        ) : (
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
                    <p className="text-[10px] font-black uppercase tracking-widest leading-tight">
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

            {/* SUCCESS STATE */}
            {status === 'success' && result && (
              <div className="animate-in zoom-in-95 duration-500 bg-slate-900/50 border border-green-500/20 rounded-3xl p-8 text-center space-y-6">
                <div className="w-16 h-16 bg-green-500/10 border border-green-500/30 rounded-full flex items-center justify-center mx-auto mb-2">
                  <svg className="w-8 h-8 text-green-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="3" d="M5 13l4 4L19 7" />
                  </svg>
                </div>
                <h3 className="text-2xl font-black italic uppercase text-white tracking-tighter">Analysis Complete</h3>
                <div className="grid grid-cols-2 gap-4 py-4">
                  <div className="bg-black/20 p-4 rounded-2xl border border-slate-800">
                    <p className="text-[10px] text-slate-500 uppercase font-bold mb-1">Activity Type</p>
                    <p className="text-white font-black uppercase italic tracking-tighter">{result.type}</p>
                  </div>
                  <div className="bg-black/20 p-4 rounded-2xl border border-slate-800">
                    <p className="text-[10px] text-slate-500 uppercase font-bold mb-1">Status</p>
                    <p className="text-green-400 font-black uppercase italic tracking-tighter text-sm leading-none pt-1">DATABASE UPDATED</p>
                  </div>
                </div>
                <button 
                  onClick={() => { setFile(null); setStatus('idle'); }}
                  className="text-[10px] font-black uppercase tracking-widest text-slate-500 hover:text-white transition-colors"
                >
                  Close Laboratory
                </button>
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
        )}
      </div>

      {/* 3. Footer Insight */}
      <div className="flex items-center gap-4 p-6 bg-blue-900/10 border border-blue-900/20 rounded-3xl">
        <div className="p-3 bg-blue-500/10 rounded-xl">
          <svg className="w-6 h-6 text-blue-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
        </div>
        <p className="text-xs text-blue-200/60 italic leading-relaxed">
          Ingestion Lab processes .FIT telemetry files directly. Once verified against Strava, your personal records and weekly volume will update automatically across the command center.
        </p>
      </div>

    </div>
  );
};