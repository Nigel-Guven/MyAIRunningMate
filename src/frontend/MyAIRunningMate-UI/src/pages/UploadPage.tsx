import { useState } from 'react';
import { uploadFitFile } from '../services/ingestionService';
import type { IngestionViewResult } from '../types/ingestionView';

export const UploadPage = () => {
  const [file, setFile] = useState<File | null>(null);
  const [progress, setProgress] = useState(0);
  const [status, setStatus] = useState<'idle' | 'uploading' | 'processing' | 'success' | 'error'>('idle');
  const [result, setResult] = useState<IngestionViewResult | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
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
      setStatus('error'); // Ensure the status is set to error
      
      if (!err.response) {
        setErrorMessage("C# API is unreachable. Check if the backend is running.");
      } else if (err.response.status === 409) {
        setErrorMessage("Duplicate Activity: This file has already been ingested.");
      } else if (err.response.status === 500) {
        setErrorMessage("Python Processing Error: The .fit file might be corrupted or unsupported.");
      } else {
        setErrorMessage("An unexpected error occurred during ingestion.");
      }
    }
  };

  return (
    <div className="space-y-8 animate-in fade-in duration-500">
      <div>
        <h2 className="text-3xl font-bold italic text-white">Ingestion Lab</h2>
        <p className="text-slate-400">Process raw .FIT data through the C#/Python pipeline.</p>
      </div>

      {/* Main Interactive Area */}
      <div className="relative min-h-[400px] flex flex-col items-center justify-center rounded-2xl border-2 border-dashed border-slate-800 bg-slate-900/50 transition-all">
        
        {status === 'idle' && (
          <div className="flex flex-col items-center text-center p-12">
            <div className="mb-4 flex h-20 w-20 items-center justify-center rounded-full bg-slate-800 text-4xl transition-transform hover:scale-110">
              {file ? '📄' : '📤'}
            </div>
            <h3 className="text-xl font-semibold">
              {file ? file.name : "Drop your .fit file here"}
            </h3>
            <p className="mt-2 text-slate-500 mb-6">Standard Garmin .FIT files</p>
            <p className="mt-2 text-slate-500 mb-6">You can get an activity file by manually downloading the file from Garmin Connect, then extracting the .zip file.</p>

            <input 
              type="file" 
              className="absolute inset-0 cursor-pointer opacity-0" 
              accept=".fit" 
              onChange={handleFileChange} 
            />
            
            {file && (
              <button 
                onClick={startUpload}
                className="z-10 rounded-full bg-blue-600 px-10 py-3 font-bold text-white transition-all hover:bg-blue-500 hover:shadow-[0_0_20px_rgba(37,99,235,0.4)]"
              >
                Analyze Activity
              </button>
            )}
          </div>
        )}

        {(status === 'uploading' || status === 'processing') && (
          <div className="flex flex-col items-center w-full max-w-md px-8 text-center">
            <div className="mb-6 h-12 w-12 animate-spin rounded-full border-4 border-slate-700 border-t-blue-500"></div>
            <h3 className="text-xl font-bold mb-2">
              {status === 'uploading' ? 'Transmitting Data...' : 'Python Processing...'}
            </h3>
            <p className="text-slate-400 text-sm mb-6">
              {status === 'uploading' ? `Sending file to C# API: ${progress}%` : 'Extracting laps and calculating training effect'}
            </p>
            <div className="w-full bg-slate-800 h-1.5 rounded-full overflow-hidden">
              <div 
                className="bg-blue-500 h-full transition-all duration-300" 
                style={{ width: `${progress}%` }}
              />
            </div>
          </div>
        )}

        {status === 'success' && result && (
          <div className="w-full p-8 animate-in zoom-in-95 duration-300">
            <div className="mx-auto max-w-lg rounded-xl border border-green-500/30 bg-green-500/5 p-6">
              <div className="flex items-center justify-between mb-6">
                <div>
                  <span className="text-xs font-bold uppercase tracking-widest text-green-400">Successfully Ingested</span>
                  <h3 className="text-2xl font-black uppercase tracking-tighter mt-1">{result.type}</h3>
                </div>
                <div className="text-4xl">✅</div>
              </div>

              <div className="grid grid-cols-2 gap-y-6 border-t border-slate-800 pt-6">
                <div>
                  <p className="text-[10px] uppercase font-bold text-slate-500">Garmin Activity ID</p>
                  <p className="font-mono text-sm text-slate-300">{result.garmin_id}</p>
                </div>
                <div>
                  <p className="text-[10px] uppercase font-bold text-slate-500">Start Time</p>
                  <p className="text-sm text-slate-300">{new Date(result.start_time).toLocaleString()}</p>
                </div>
                <div>
                  <p className="text-[10px] uppercase font-bold text-slate-500">Distance</p>
                  <p className="text-xl font-black text-white">
                    {(result.distance_metres / 1000).toFixed(2)} <span className="text-xs font-normal text-slate-500">km</span>
                  </p>
                </div>
                <div>
                  <p className="text-[10px] uppercase font-bold text-slate-500">Training Effect</p>
                  <p className="text-xl font-black text-blue-400">{result.training_effect.toFixed(1)}</p>
                </div>
              </div>

              <button 
                onClick={() => { setStatus('idle'); setFile(null); }}
                className="mt-8 w-full rounded-lg border border-slate-700 py-2 text-sm font-semibold hover:bg-slate-800 transition-colors"
              >
                Upload Another File
              </button>
            </div>
          </div>
        )}

        {status === 'error' && (
          <div className="flex flex-col items-center p-12 animate-in slide-in-from-bottom-4">
            <div className="mb-4 flex h-16 w-16 items-center justify-center rounded-full bg-red-500/20 text-3xl">
              ⚠️
            </div>
            <h3 className="text-xl font-bold text-red-500">Ingestion Failed</h3>
            <p className="mt-2 text-slate-400 max-w-xs text-center">
              {errorMessage}
            </p>
            
            <div className="mt-8 flex gap-4">
              <button 
                onClick={() => { setStatus('idle'); setFile(null); }}
                className="px-6 py-2 rounded-lg border border-slate-700 hover:bg-slate-800 transition-colors"
              >
                Cancel
              </button>
              <button 
                onClick={startUpload}
                className="px-6 py-2 rounded-lg bg-red-600 font-bold hover:bg-red-500 transition-colors"
              >
                Try Again
              </button>
            </div>
          </div>
        )}

      </div>

      <div className="grid grid-cols-1 gap-6 md:grid-cols-3">
        <div className="rounded-xl border border-slate-800 p-4 bg-slate-900/30">
          <span className="text-blue-400 font-bold text-xs uppercase">Step 1</span>
          <p className="mt-1 text-sm text-slate-400 italic">Upload your raw Garmin .FIT file.</p>
        </div>
        <div className="rounded-xl border border-slate-800 p-4 bg-slate-900/30">
          <span className="text-blue-400 font-bold text-xs uppercase">Step 2</span>
          <p className="mt-1 text-sm text-slate-400 italic">Python parses and extracts activity and related lap metrics.</p>
        </div>
        <div className="rounded-xl border border-slate-800 p-4 bg-slate-900/30">
          <span className="text-blue-400 font-bold text-xs uppercase">Step 3</span>
          <p className="mt-1 text-sm text-slate-400 italic">The file data is now visible on your calendar.</p>
        </div>
      </div>
    </div>
  );
};