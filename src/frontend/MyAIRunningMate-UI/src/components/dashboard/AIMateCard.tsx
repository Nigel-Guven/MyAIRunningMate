export default function NexusAIMate({ 
  message = "Analyzing your profile to generate personalized insights...", 
  status = "Analyzing Data" 
}) {
  return (
    <div className="rounded-3xl border border-slate-800 bg-slate-900 p-6 flex flex-col justify-between h-full">
      {/* Header with Pulse Indicator */}
      <div>
        <div className="flex items-center gap-2 mb-4">
          <span className="h-2 w-2 rounded-full bg-purple-500 animate-pulse" />
          <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em]">
            Nexus AI Mate
          </h4>
        </div>

        {/* AI Suggestion Text */}
        <p className="text-slate-300 italic text-sm leading-relaxed">
          "{message}"
        </p>
      </div>

      {/* Status Footer */}
      <div className="mt-4 pt-4 border-t border-slate-800 text-[10px] text-slate-600 font-mono uppercase">
        Status: {status}
      </div>
    </div>
  );
}