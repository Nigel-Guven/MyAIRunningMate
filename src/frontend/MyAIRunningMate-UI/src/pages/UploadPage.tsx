import { useState, useEffect } from 'react';
import { apiClient } from '../services/apiClient';
import { uploadFitFile } from '../services/ingestionService';
import type { IngestionViewResult } from '../types/ingestionView';

export const UploadPage = () => {
  const [file, setFile] = useState<File | null>(null);
  const [progress, setProgress] = useState(0);
  const [status, setStatus] = useState<'idle' | 'uploading' | 'processing' | 'success' | 'error'>('idle');
  const [result, setResult] = useState<IngestionViewResult | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  // --- Strava Authorization State ---
  const [stravaLoading, setStravaLoading] = useState(false);
  const [syncMessage, setSyncMessage] = useState('');
  const [isStravaConnected, setIsStravaConnected] = useState(() => {
    return localStorage.getItem('is_strava_connected') === 'true';
  });

  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    if (params.get('sync') === 'success') {
      setSyncMessage('Strava Connection Verified');
      localStorage.setItem('is_strava_connected', 'true');
      setIsStravaConnected(true);
      window.history.replaceState({}, document.title, window.location.pathname);
    }
  }, []);

  const handleStravaConnect = async () => {
    setStravaLoading(true);
    try {
      const response = await apiClient.get('strava/connect');
      if (response.data?.url) {
        window.location.href = response.data.url;
      }
    } catch (error: any) {
      setSyncMessage('Connection Failed');
      setStravaLoading(false);
    }
  };

  useEffect(() => {
    const checkStatus = async () => {
      try {
        // Create a quick endpoint in C# that calls HasStravaConnectionAsync
        const response = await apiClient.get('strava/status'); 
        const isConnected = response.data.isStravaConnected;

        setIsStravaConnected(isConnected);
        localStorage.setItem('is_strava_connected', String(isConnected));
      } catch (err) {
        console.error("Failed to verify Strava status", err);
      }
    };

    // Run the check on mount
    checkStatus();

    // Keep your existing URL param logic as a backup for immediate feedback
    const params = new URLSearchParams(window.location.search);
    if (params.get('sync') === 'success') {
      setSyncMessage('Strava Connection Verified');
      setIsStravaConnected(true);
      localStorage.setItem('is_strava_connected', 'true');
      window.history.replaceState({}, document.title, window.location.pathname);
    }
  }, []);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!isStravaConnected) return;
    if (e.target.files && e.target.files[0]) {
      setFile(e.target.files[0]);
      setStatus('idle');
    }
  };

  const startUpload = async () => {
    if (!file) return;
    setStatus('uploading');
    setErrorMessage(null);
    try {
      const data = await uploadFitFile(file, (p) => {
        setProgress(p);
        if (p === 100) setStatus('processing');
      });
      setResult(data);
      setStatus('success');
    } catch (err: any) {
      setStatus('error');
      if (!err.response) {
        setErrorMessage("C# API is unreachable. Check backend status.");
      } else if (err.response.status === 409) {
        setErrorMessage("Duplicate Activity: This file exists in the vault.");
      } else {
        setErrorMessage(err.response?.data?.message || "An unexpected error occurred.");
      }
    }
  };

  const resetLab = () => {
    setFile(null);
    setStatus('idle');
    setResult(null);
    setErrorMessage(null);
    setProgress(0);
  };

  return (
    <div className="space-y-8 animate-in fade-in duration-500">
      {/* Header Section */}
      <div className="flex justify-between items-start">
        <div>
          <h2 className="text-3xl font-bold italic text-white tracking-tighter">INGESTION LAB</h2>
          <div className="flex items-center gap-2 mt-1">
            <span className={`h-2 w-2 rounded-full ${isStravaConnected ? 'bg-green-500 animate-pulse' : 'bg-red-500'}`} />
            <p className="text-sm font-mono text-slate-400">
              STRAVA STATUS: {isStravaConnected ? 'ACTIVE' : 'OFFLINE'}
            </p>
          </div>
        </div>

        <button
          onClick={handleStravaConnect}
          disabled={stravaLoading}
          className={`px-6 py-2 rounded-lg text-xs font-black transition-all uppercase tracking-widest ${
            isStravaConnected 
              ? 'bg-slate-800 text-slate-400 border border-slate-700 cursor-default' 
              : 'bg-orange-600 text-white hover:bg-orange-500 shadow-[0_0_15px_rgba(234,88,12,0.3)]'
          }`}
        >
          {stravaLoading ? 'Authorizing...' : isStravaConnected ? 'System Linked' : 'Connect Strava'}
        </button>
      </div>

      {/* Main Lab Area */}
      <div className={`relative min-h-[420px] flex flex-col items-center justify-center rounded-3xl border-2 transition-all duration-500 ${
        !isStravaConnected 
          ? 'border-red-900/20 bg-black/40 grayscale' 
          : 'border-slate-800 bg-slate-900/50 border-dashed hover:border-blue-500/50'
      }`}>
        
        {!isStravaConnected ? (
          <div className="flex flex-col items-center text-center p-12 space-y-4">
            <div className="text-6xl mb-2 opacity-20">📡</div>
            <h3 className="text-xl font-bold text-red-500/80 uppercase tracking-widest">Handshake Required</h3>
            <p className="text-slate-500 max-w-xs text-sm leading-relaxed">
              Ingestion pipeline is locked. Connect your Strava account to authorize .FIT file mapping.
            </p>
          </div>
        ) : (
          <div className="w-full h-full flex flex-col items-center justify-center p-8">
            
            {/* IDLE STATE */}
            {status === 'idle' && (
              <>
                <div className="mb-6 flex h-24 w-24 items-center justify-center rounded-full bg-slate-800/50 text-5xl shadow-inner border border-slate-700">
                  {file ? '📊' : '☁️'}
                </div>
                <h3 className="text-2xl font-bold text-white mb-2">
                  {file ? file.name : "Upload Training Data"}
                </h3>
                <p className="text-slate-500 text-sm mb-8 italic">Drop .FIT file or click to browse</p>
                <input 
                  type="file" 
                  disabled={!isStravaConnected}
                  className="absolute inset-0 cursor-pointer opacity-0" 
                  accept=".fit" 
                  onChange={handleFileChange} 
                />
                {file && (
                  <button 
                    onClick={startUpload}
                    className="z-10 rounded-full bg-blue-600 px-12 py-4 font-black text-white transition-all hover:bg-blue-500 hover:scale-105 shadow-xl shadow-blue-900/20"
                  >
                    START PROCESSING
                  </button>
                )}
              </>
            )}

            {/* PROGRESS STATE */}
            {(status === 'uploading' || status === 'processing') && (
              <div className="w-full max-w-sm text-center">
                <div className="mb-6 inline-block h-12 w-12 animate-spin rounded-full border-4 border-slate-800 border-t-blue-500" />
                <h3 className="text-lg font-bold text-white mb-4 uppercase tracking-widest">
                  {status === 'uploading' ? 'Ingesting Fit Binary' : 'Executing Python Logic'}
                </h3>
                <div className="h-2 w-full bg-slate-800 rounded-full overflow-hidden">
                  <div 
                    className="h-full bg-blue-500 transition-all duration-500 shadow-[0_0_10px_#3b82f6]" 
                    style={{ width: `${progress}%` }} 
                  />
                </div>
                <p className="text-slate-500 text-xs mt-4 font-mono">{progress}% Complete</p>
              </div>
            )}

            {/* SUCCESS STATE */}
            {status === 'success' && result && (
              <div className="w-full max-w-md animate-in zoom-in-95 duration-300">
                <div className="rounded-2xl border border-green-500/30 bg-green-500/5 p-6 shadow-2xl shadow-green-900/10">
                  <div className="flex items-center justify-between mb-6">
                    <div>
                      <span className="text-[10px] font-black uppercase tracking-[0.2em] text-green-400">Vault Injection Successful</span>
                      <h3 className="text-2xl font-black text-white mt-1 uppercase tracking-tighter">{result.type}</h3>
                    </div>
                    <div className="text-4xl">✅</div>
                  </div>
                  
                  <div className="grid grid-cols-2 gap-4 border-t border-slate-800 pt-6">
                    <div>
                      <p className="text-[10px] uppercase font-bold text-slate-500 mb-1">Distance</p>
                      <p className="text-xl font-black text-white italic">
                        {(result.distance_metres / 1000).toFixed(2)} <span className="text-xs font-normal text-slate-500 not-italic">KM</span>
                      </p>
                    </div>
                    <div>
                      <p className="text-[10px] uppercase font-bold text-slate-500 mb-1">Intensity Score</p>
                      <p className="text-xl font-black text-blue-400 italic">{result.training_effect.toFixed(1)}</p>
                    </div>
                  </div>

                  <button 
                    onClick={resetLab}
                    className="mt-8 w-full rounded-xl bg-slate-800 py-3 text-sm font-bold text-slate-300 hover:bg-slate-700 transition-colors"
                  >
                    Process Next File
                  </button>
                </div>
              </div>
            )}

            {/* ERROR STATE */}
            {status === 'error' && (
              <div className="flex flex-col items-center p-8 animate-in slide-in-from-bottom-4">
                <div className="mb-4 flex h-20 w-20 items-center justify-center rounded-full bg-red-500/10 text-4xl border border-red-500/20">
                  ⚠️
                </div>
                <h3 className="text-xl font-bold text-red-500 uppercase tracking-tight">Pipeline Breach</h3>
                <p className="mt-2 text-slate-400 text-center text-sm max-w-[250px]">
                  {errorMessage}
                </p>
                <div className="mt-8 flex gap-3">
                  <button onClick={resetLab} className="px-6 py-2 rounded-lg border border-slate-700 text-slate-400 text-xs font-bold hover:bg-slate-800 transition-colors">
                    CANCEL
                  </button>
                  <button onClick={startUpload} className="px-6 py-2 rounded-lg bg-red-600 text-white text-xs font-bold hover:bg-red-500 transition-all">
                    RETRY
                  </button>
                </div>
              </div>
            )}

          </div>
        )}
      </div>

      {/* Visual Workflow Footer */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6 opacity-60">
        {[
          { label: 'Phase 01', title: 'OAuth Handshake', active: isStravaConnected },
          { label: 'Phase 02', title: 'Binary Parsing', active: status === 'processing' || status === 'success' },
          { label: 'Phase 03', title: 'Vault Injection', active: status === 'success' }
        ].map((phase, i) => (
          <div key={i} className={`p-4 rounded-xl border transition-colors duration-500 ${phase.active ? 'border-blue-500/30 bg-blue-500/5' : 'border-slate-800'}`}>
            <p className={`text-[10px] font-bold uppercase mb-1 ${phase.active ? 'text-blue-400' : 'text-slate-500'}`}>{phase.label}</p>
            <p className={`text-sm font-bold ${phase.active ? 'text-white' : 'text-slate-400'}`}>{phase.title}</p>
          </div>
        ))}
      </div>
    </div>
  );
};