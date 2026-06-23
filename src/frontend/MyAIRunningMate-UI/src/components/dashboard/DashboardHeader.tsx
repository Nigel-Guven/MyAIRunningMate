import type { DashboardTypes } from '../../types/dashboard/dashboard.types';
import logo from '../../assets/applogo.png';

interface DashboardHeaderProps {
  latestWeight: DashboardTypes['latestWeight'];
}

export const DashboardHeader = ({ latestWeight }: DashboardHeaderProps) => {
  return (
    <div className="flex justify-between items-end border-b border-slate-800 pb-6">
      <div className="flex items-center gap-4">
        <img src={logo} alt="Logo" className="h-14 w-14 rounded-xl shadow-lg shadow-blue-900/20" />
        <div>
          <h2 className="text-3xl font-black tracking-tighter uppercase italic">Command Center</h2>
          <p className="text-slate-400 font-medium">Welcome back, Nigel.</p>
        </div>
      </div>
      
      <div className="bg-slate-900/50 p-3 px-6 rounded-2xl border border-slate-800">
        <p className="text-[10px] text-slate-500 uppercase font-black tracking-widest text-right">Weight</p>
        <p className="text-2xl font-black text-blue-400 italic">
          {latestWeight?.weight_in_pounds ? (latestWeight.weight_in_pounds * 0.453592).toFixed(1) : '0.0'} 
          <span className="text-xs text-slate-600 not-italic ml-1">KG</span>
        </p>
      </div>
    </div>
  );
};