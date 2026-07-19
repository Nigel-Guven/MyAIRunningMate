import React from 'react';

export interface StatItem {
  label: string;
  value: string | number;
  icon: React.ReactNode;
}

export const AnalyticsSummaryCard: React.FC<StatItem> = ({ label, value, icon }) => {
  return (
    <div className="p-5 rounded-xl bg-slate-900 border border-slate-800 hover:border-slate-700 transition-colors">
      <div className="flex justify-between items-start">
        <p className="text-slate-500 text-xs font-bold uppercase tracking-wider">
          {label}
        </p>
        <span className="text-lg">{icon}</span>
      </div>
      <p className="text-2xl font-bold mt-2 text-slate-100">{value}</p>
    </div>
  );
};