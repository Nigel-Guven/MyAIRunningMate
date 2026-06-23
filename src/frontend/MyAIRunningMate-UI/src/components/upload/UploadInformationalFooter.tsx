export const UploadInformationalFooter = () => {
  return (
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
  );
};