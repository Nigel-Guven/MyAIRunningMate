export const UploadPage = () => {
  return (
    <div className="space-y-8">
      <div>
        <h2 className="text-3xl font-bold">Upload Activity</h2>
        <p className="text-slate-400">Add a new .fit file to your training history.</p>
      </div>

      {/* The Dropzone */}
      <div className="group relative flex min-h-[400px] cursor-pointer flex-col items-center justify-center rounded-2xl border-2 border-dashed border-slate-800 bg-slate-900/50 transition-all hover:border-blue-500 hover:bg-slate-900">
        <div className="flex flex-col items-center text-center">
          <div className="mb-4 flex h-16 w-16 items-center justify-center rounded-full bg-slate-800 text-3xl transition-transform group-hover:scale-110">
            📤
          </div>
          <h3 className="text-xl font-semibold">Drop your .fit file here</h3>
          <p className="mt-2 text-slate-500">or click to browse your computer</p>
        </div>
        
        {/* Hidden Input for clicks */}
        <input type="file" className="absolute inset-0 cursor-pointer opacity-0" accept=".fit" />
      </div>

      {/* Info Grid */}
      <div className="grid grid-cols-1 gap-6 md:grid-cols-3">
        <div className="rounded-xl border border-slate-800 p-4">
          <span className="text-blue-400">Step 1</span>
          <p className="mt-1 text-sm text-slate-400">Upload your raw Garmin or Wahoo file.</p>
        </div>
        <div className="rounded-xl border border-slate-800 p-4">
          <span className="text-blue-400">Step 2</span>
          <p className="mt-1 text-sm text-slate-400">Python extracts laps and swimming metrics.</p>
        </div>
        <div className="rounded-xl border border-slate-800 p-4">
          <span className="text-blue-400">Step 3</span>
          <p className="mt-1 text-sm text-slate-400">Gemini analyzes your training effect.</p>
        </div>
      </div>
    </div>
  );
};