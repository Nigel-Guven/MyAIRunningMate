import React from 'react';
import { formatDateLong } from '../../services/helpers/dateFormatter' // Adjust path as needed

export const WeightTooltip = ({ active, payload, label }: any) => {
  if (active && payload && payload.length) {
    const lbs = payload[0].value;
    const kg = (lbs * 0.453592).toFixed(1);

    return (
      <div className="bg-slate-950 border border-slate-800 p-4 rounded-lg shadow-2xl">
        <p className="text-slate-500 text-[10px] font-bold mb-2 uppercase tracking-widest">
          {formatDateLong(label)}
        </p>
        <div className="flex flex-col gap-1">
          <p className="text-sky-400 font-bold text-lg flex items-baseline gap-2">
            {lbs} <span className="text-[10px] font-medium text-slate-500 uppercase">Lbs</span>
          </p>
          <div className="h-[1px] w-full bg-slate-800 my-1" />
          <p className="text-emerald-400 font-bold text-lg flex items-baseline gap-2">
            {kg} <span className="text-[10px] font-medium text-slate-500 uppercase">Kg</span>
          </p>
        </div>
      </div>
    );
  }

  return null;
};