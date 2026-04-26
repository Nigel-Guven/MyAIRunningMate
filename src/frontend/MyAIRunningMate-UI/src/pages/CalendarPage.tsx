// src/pages/CalendarPage.tsx
export const CalendarPage = () => {
  // Logic to generate 31 days would go here
  const days = Array.from({ length: 31 }, (_, i) => i + 1);

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-3xl font-bold">Activity Matrix</h2>
        <div className="flex gap-2">
          <button className="bg-slate-800 p-2 rounded">‹</button>
          <span className="font-bold py-2 px-4">April 2026</span>
          <button className="bg-slate-800 p-2 rounded">›</button>
        </div>
      </div>

      <div className="grid grid-cols-7 gap-2">
        {['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'].map(d => (
          <div key={d} className="text-center text-xs font-bold text-slate-500 uppercase pb-2">{d}</div>
        ))}
        {days.map(day => (
          <div key={day} className="min-h-[120px] rounded-lg border border-slate-800 bg-slate-900/50 p-2 hover:border-blue-500 transition-colors cursor-pointer">
            <span className="text-sm text-slate-500">{day}</span>
            {/* Example Activity Tile */}
            {day === 12 && (
              <div className="mt-1 rounded bg-blue-600/20 p-1 border border-blue-600/50">
                <p className="text-[10px] font-bold text-blue-400">RUN • 10k</p>
                <p className="text-[9px] text-slate-300">TE: 3.5</p>
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};