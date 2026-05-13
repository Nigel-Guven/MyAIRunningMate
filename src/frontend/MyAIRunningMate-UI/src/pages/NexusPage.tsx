// src/pages/NexusPage.tsx
export const NexusPage = () => {
  return (
    <div className="max-w-4xl space-y-8">
      <div>
        <h2 className="text-3xl font-bold">Nexus AI Mate</h2>
        <p className="text-slate-400">Configure your autonomous training architect.</p>
      </div>

      <div className="p-6 rounded-2xl border border-blue-500/30 bg-blue-500/5">
        <h3 className="text-xl font-bold mb-4 flex items-center gap-2">
          <span>🧠</span> AI Training Plan Requirements
        </h3>
        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-slate-400 mb-1">Primary Goal</label>
            <input type="text" placeholder="e.g. Sub-1:45 Half Marathon" className="w-full bg-slate-800 border-slate-700 rounded-lg p-2" />
          </div>
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Schedule Length</label>
              <select className="w-full bg-slate-800 border-slate-700 rounded-lg p-2">
                <option>4 weeks</option>
                <option>6 weeks</option>
                <option>8 weeks</option>
                <option>12 weeks</option>
              </select>
            </div>
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Pool Access</label>
              <select className="w-full bg-slate-800 border-slate-700 rounded-lg p-2">
                <option>None</option>
                <option>25m Pool</option>
                <option>50m Pool</option>
              </select>
            </div>
          </div>
          <button className="w-full bg-blue-600 hover:bg-blue-500 font-bold py-3 rounded-lg transition-colors mt-4">
            Generate / Update Training Plan
          </button>
        </div>
      </div>
    </div>
  );
};