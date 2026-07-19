import React from 'react';

interface AnalyticsPageHeaderProps {
  logo: string;
  selectedYear: number;
  setSelectedYear: (year: number) => void;
}

export const AnalyticsPageHeader: React.FC<AnalyticsPageHeaderProps> = ({
  logo,
  selectedYear,
  setSelectedYear,
}) => {
  return (
    <div className="flex justify-between items-end border-b border-slate-800 pb-6">
      <div className="flex items-center gap-4">
        <img 
          src={logo} 
          alt="Logo" 
          className="h-14 w-14 rounded-xl shadow-lg shadow-blue-900/20" 
        />
        <div>
          <h2 className="text-3xl font-black tracking-tighter uppercase italic">
            Analytics Vault
          </h2>
          <p className="text-slate-400 font-medium">
            View your yearly analytics.
          </p>
        </div>
      </div>
      
      <select 
        value={selectedYear} 
        onChange={(e) => setSelectedYear(Number(e.target.value))}
        className="bg-slate-900 border border-slate-800 text-slate-300 px-4 py-2 rounded-lg outline-none focus:border-indigo-500"
      >
        <option value={2026}>2026</option>
        <option value={2025}>2025</option>
      </select>
    </div>
  );
};