export const AnalyticsPage = () => {
  return (
    <div className="space-y-8">
      <h2 className="text-3xl font-bold">Analytics Vault</h2>
      
      <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
        {[
          { label: 'Yearly Distance', value: '412.5 km', icon: '🛣️' },
          { label: 'Swimming Volume', value: '28,400 m', icon: '🏊' },
          { label: 'Active Days', value: '84', icon: '🔥' },
          { label: 'Avg Training Effect', value: '3.2', icon: '📈' },
        ].map(stat => (
          <div key={stat.label} className="p-6 rounded-xl bg-slate-900 border border-slate-800">
            <p className="text-slate-500 text-xs font-bold uppercase">{stat.label}</p>
            <p className="text-2xl font-bold mt-1">{stat.value}</p>
          </div>
        ))}
      </div>

      <div className="h-64 w-full rounded-xl bg-slate-900 border border-slate-800 flex items-center justify-center text-slate-500 italic">
        [Yearly Heatmap: Committing data from Supabase to GitHub-style grid]
      </div>
    </div>
  );
};