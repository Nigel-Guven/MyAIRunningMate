interface UploadProgressZoneProps {
  status: 'uploading' | 'processing';
  progress: number;
}

export const UploadProgressZone = ({ status, progress }: UploadProgressZoneProps) => {
  return (
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
  );
};