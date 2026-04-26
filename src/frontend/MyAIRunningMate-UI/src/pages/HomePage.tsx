import logo from '../assets/applogo.png';

export const HomePage = () => {
  return (
    <div className="space-y-8">
      {/* Header Section */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <img src={logo} alt="Logo" className="h-16 w-16 rounded-xl" />
          <div>
            <h2 className="text-3xl font-bold">Command Center</h2>
            <p className="text-slate-400">Welcome back, Nigel.</p>
          </div>
        </div>
        <div className="text-right">
          <p className="text-sm text-slate-500 uppercase font-bold">Current Weight</p>
          <p className="text-2xl font-mono text-blue-400">78.5 kg</p>
        </div>
      </div>

      {/* Bento Grid */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        
        {/* Next Event - Large Card */}
        <div className="md:col-span-2 rounded-2xl bg-gradient-to-br from-blue-600 to-blue-800 p-8 text-white shadow-lg shadow-blue-900/20">
          <p className="text-blue-100 uppercase text-xs font-bold tracking-widest">Upcoming Event</p>
          <h3 className="text-4xl font-black mt-2">Dublin Half Marathon</h3>
          <div className="mt-8 flex gap-8">
            <div>
              <p className="text-3xl font-bold">24</p>
              <p className="text-blue-200 text-xs uppercase">Days Left</p>
            </div>
            <div className="border-l border-blue-400/30 pl-8">
              <p className="text-3xl font-bold">Sub 1:45</p>
              <p className="text-blue-200 text-xs uppercase">Target Goal</p>
            </div>
          </div>
        </div>

        {/* AI Insight Card */}
        <div className="rounded-2xl border border-slate-800 bg-slate-900 p-6">
          <h4 className="text-sm font-bold text-slate-500 uppercase mb-4">Nexus AI Mate</h4>
          <p className="text-slate-300 italic">
            "Your training load is up 12% this week. Focus on pool recovery tomorrow to keep your shins fresh for Sunday's long run."
          </p>
        </div>

        {/* Strava Best Efforts */}
        <div className="rounded-2xl border border-slate-800 bg-slate-900 p-6">
          <h4 className="text-sm font-bold text-slate-500 uppercase mb-4">Best Efforts</h4>
          <div className="space-y-4">
             <div className="flex justify-between items-center">
                <span className="text-slate-400">5k</span>
                <span className="font-mono font-bold">19:42</span>
             </div>
             <div className="flex justify-between items-center border-t border-slate-800 pt-4">
                <span className="text-slate-400">10k</span>
                <span className="font-mono font-bold">42:10</span>
             </div>
          </div>
        </div>

        {/* Training Load Placeholder */}
        <div className="md:col-span-2 rounded-2xl border border-slate-800 bg-slate-900 p-6 flex items-center justify-center text-slate-600">
           [Weekly Volume Chart Component will go here]
        </div>

      </div>
    </div>
  );
};