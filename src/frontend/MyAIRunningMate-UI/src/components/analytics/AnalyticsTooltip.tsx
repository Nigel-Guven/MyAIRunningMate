export const AnalyticsTooltip = ({ active, payload, label }: any) => {
  if (!active || !payload?.length) return null;

  const d = payload[0].payload;

  return (
    <div className="rounded-lg border border-slate-700 bg-slate-900 p-3 text-sm shadow-lg">
      <p className="mb-2 font-medium text-slate-200">Month {label}</p>

      <div className="space-y-1">
        <p className="text-cyan-400">
          VO₂ Max: <span className="text-white">{d.rawVo2}</span>
        </p>

        <p className="text-red-400">
          LT Heart Rate: <span className="text-white">{d.rawHr} bpm</span>
        </p>

        <p className="text-yellow-400">
          LT Power: <span className="text-white">{d.rawPower} W</span>
        </p>

        <p className="text-green-400">
          LT Speed: <span className="text-white">{d.rawSpeed} km/h</span>
        </p>

        <p className="text-purple-400">
          LT %: <span className="text-white">
            {(d.rawPercent * 100).toFixed(1)}%
          </span>
        </p>
      </div>
    </div>
  );
};