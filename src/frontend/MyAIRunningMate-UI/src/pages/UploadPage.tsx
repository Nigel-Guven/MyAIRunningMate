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

  // --- Strava Authorization State ---
  const [stravaLoading, setStravaLoading] = useState(false);
  const [syncMessage, setSyncMessage] = useState('');
  const [isStravaConnected, setIsStravaConnected] =
    useState(() =>
      stravaService.isConnectedLocally()
    );

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
      await stravaService.connect();
    } catch (err) {
      setSyncMessage('Connection Failed');
      setStravaLoading(false);
    }
  };

  useEffect(() => {
    const verify = async () => {
      try {
        const isConnected =
          await stravaService.verifyConnection();

        setIsStravaConnected(isConnected);
      } catch (err) {
        console.error(
          'Failed to verify Strava status',
          err
        );
      }
    };

    verify();

    const params = new URLSearchParams(
      window.location.search
    );

    if (params.get('sync') === 'success') {
      setSyncMessage('Strava Connection Verified');
      setIsStravaConnected(true);

      window.history.replaceState(
        {},
        document.title,
        window.location.pathname
      );
    }
  }, []);

  const handleFileChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => { 
    if (!isStravaConnected) return;

    const selected = e.target.files?.[0];

    if (!selected) return;

    setFile(selected);
    setStatus('idle');
  };

  const startUpload = async () => {
    if (!file) return;

    setStatus('uploading');
    setErrorMessage(null);

    try {
      const data =
        await ingestionService.uploadFitFile(
          file,
          (p) => {
            setProgress(p);

            if (p === 100) {
              setStatus('processing');
            }
          }
        );

      setResult(data);
      setStatus('success');

    } catch (err: any) {
      setStatus('error');

      if (!err.response) {
        setErrorMessage(
          'Backend unreachable'
        );
      } else if (
        err.response.status === 409
      ) {
        setErrorMessage(
          'Duplicate Activity detected'
        );
      } else {
        setErrorMessage(
          err.response?.data?.message ||
            'Unexpected error'
        );
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

      <div className="flex justify-between items-start">

        <div>
          <h2 className="text-3xl font-bold italic text-white tracking-tighter">
            INGESTION LAB
          </h2>

          <div className="flex items-center gap-2 mt-1">

            <span
              className={`h-2 w-2 rounded-full ${
                isStravaConnected
                  ? 'bg-green-500 animate-pulse'
                  : 'bg-red-500'
              }`}
            />

            <p className="text-sm font-mono text-slate-400">
              STRAVA STATUS:{' '}
              {isStravaConnected
                ? 'ACTIVE'
                : 'OFFLINE'}
            </p>

          </div>
        </div>

        <button
          onClick={handleStravaConnect}
          disabled={stravaLoading}
          className="px-6 py-2 rounded-lg text-xs font-black uppercase"
        >
          {stravaLoading
            ? 'Authorizing...'
            : isStravaConnected
            ? 'System Linked'
            : 'Connect Strava'}
        </button>

      </div>

      {/* MAIN AREA */}
      <div className="relative min-h-[420px] flex flex-col items-center justify-center">

        {!isStravaConnected ? (
          <div className="text-center">
            <h3>Handshake Required</h3>
          </div>
        ) : (
          <div className="w-full h-full flex flex-col items-center justify-center p-8">

            {status === 'idle' && (
              <>
                <h3>
                  {file
                    ? file.name
                    : 'Upload Training Data'}
                </h3>

                <input
                  type="file"
                  accept=".fit"
                  onChange={handleFileChange}
                />

                {file && (
                  <button onClick={startUpload}>
                    START PROCESSING
                  </button>
                )}
              </>
            )}

            {status === 'uploading' && (
              <p>Uploading... {progress}%</p>
            )}

            {status === 'processing' && (
              <p>Processing...</p>
            )}

            {status === 'success' && result && (
              <div>
                <h3>Success</h3>
                <p>{result.type}</p>
                <button onClick={resetLab}>
                  Reset
                </button>
              </div>
            )}

            {status === 'error' && (
              <div>
                <p>{errorMessage}</p>
                <button onClick={startUpload}>
                  Retry
                </button>
              </div>
            )}
          </div>
        )}
      </div>
    </div>
  );
};