import React from 'react';

interface FileDropZoneProps {
  file: File | null;
  onFileChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onStartUpload: () => void;
}

export const FileDropZone = ({ file, onFileChange, onStartUpload }: FileDropZoneProps) => {
  return (
    <div className="text-center space-y-8">
      <div className="relative group mx-auto w-64 h-64 border-2 border-dashed border-slate-700 rounded-full flex flex-col items-center justify-center hover:border-blue-500/50 hover:bg-blue-500/5 transition-all duration-500">
        <input
          type="file"
          accept=".fit"
          onChange={onFileChange}
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
          onClick={onStartUpload}
          className="animate-in slide-in-from-bottom-4 duration-500 px-10 py-4 bg-blue-600 hover:bg-blue-500 text-white rounded-2xl font-black uppercase italic tracking-tighter text-lg shadow-xl shadow-blue-900/40"
        >
          Initiate Ingestion
        </button>
      )}
    </div>
  );
};