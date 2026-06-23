import { useState } from 'react';
import { ingestionService } from '../services/api/ingestion/ingestion.service';
import type { IngestionViewResponse } from '../types/ingestion/ingestionViewResponse';
import logo from '../assets/applogo.png';
import { FileDropZone } from '../components/upload/FileDropZone';
import { UploadProgressZone } from '../components/upload/UploadProgressZone';
import { TelemetrySuccessZone } from '../components/upload/TelemetrySuccessZone';
import { UploadInformationalFooter } from '../components/upload/UploadInformationalFooter';

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

  const resetLaboratory = () => {
    setFile(null);
    setStatus('idle');
    setResult(null);
  };

  return (
    <div className="space-y-8 animate-in fade-in duration-700">
      
      {/* Header Area */}
      <div className="flex justify-between items-end border-b border-slate-800 pb-6">
        <div className="flex items-center gap-4">
          <img src={logo} alt="Logo" className="h-14 w-14 rounded-xl shadow-lg shadow-blue-900/20" />
          <div>
            <h2 className="text-3xl font-black tracking-tighter uppercase italic">Ingestion Lab</h2>
            <p className="text-slate-400 font-medium">Upload your .FIT files taken from Garmin Connect.</p>
          </div>
        </div>
      </div>

      {/* Main Ingestion Console */}
      <div className="relative min-h-[450px] rounded-3xl border border-slate-800 bg-slate-900/30 backdrop-blur-sm overflow-hidden flex flex-col items-center justify-center p-12">
        <div className="w-full max-w-2xl">
          
          {status === 'idle' && (
            <FileDropZone file={file} onFileChange={handleFileChange} onStartUpload={startUpload} />
          )}

          {(status === 'uploading' || status === 'processing') && (
            <UploadProgressZone status={status} progress={progress} />
          )}

          {status === 'success' && result && (
            <TelemetrySuccessZone result={result} onReset={resetLaboratory} />
          )}

          {/* Error fallback container kept inline for atomic layout */}
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

      {/* Footer Informational Banner */}
      <UploadInformationalFooter />

    </div>
  );
};