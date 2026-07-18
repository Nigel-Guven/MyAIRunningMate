import type { LapResponse } from "../../types/aggregates/lapResponse";

export const LapTable = ({ laps }: { laps: LapResponse[] }) => {
  const formatTime = (seconds: number) => {
    const mins = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);
    return `${mins}:${secs.toString().padStart(2, "0")}`;
  };

  // Infers activity type by checking if any lap has swimming-specific data
  const isSwimming = laps.some((lap) => lap.primary_stroke !== null);

  return (
    <div className="bg-slate-900 border border-slate-800 rounded-2xl p-5">
      <h2 className="text-lg font-bold mb-4">Laps</h2>

      <div className="overflow-auto max-h-[600px] relative rounded-lg border border-slate-800/60">
        <table className="w-full text-sm border-collapse">
          <thead className="text-slate-400 border-b border-slate-800 sticky top-0 bg-slate-900 z-10 shadow-[0_1px_0_0_rgba(30,41,59,1)]">
            <tr>
              <th className="text-left p-2">Lap</th>
              <th className="text-left p-2">Distance</th>
              <th className="text-left p-2">Time</th>
              <th className="text-left p-2">Avg HR</th>
              <th className="text-left p-2">Max HR</th>
              <th className="text-left p-2">Speed</th>
              <th className="text-left p-2">Cadence</th>
              
              {/* Conditionally render Swimming headers */}
              {isSwimming && (
                <>
                  <th className="text-left p-2">Stroke</th>
                  <th className="text-left p-2">Lengths</th>
                </>
              )}
            </tr>
          </thead>

          <tbody className="divide-y divide-slate-800/50">
            {laps.map((lap) => (
              <tr
                key={lap.lap_number}
                className="border-b border-slate-800/50"
              >
                <td className="p-3 text-slate-300">{lap.lap_number}</td>
                <td className="p-3 text-blue-400/90">{(lap.distance_metres / 1000).toFixed(2)} km</td>
                <td className="p-3 text-black-400/90">{formatTime(lap.duration_seconds)}</td>
                <td className="p-3 text-rose-400/90">{lap.average_heart_rate} bpm</td>
                <td className="p-3 text-red-400/90">{lap.max_heart_rate} bpm</td>
                <td className="p-3 text-white-400/90">{formatPace(lap.average_speed)}</td>
                <td className="p-3 text-orange-400/90">
                  {lap.average_cadence !== null 
                    ? (isSwimming ? lap.average_cadence : lap.average_cadence * 2) 
                    : "-"}
                  {!isSwimming && lap.average_cadence !== null && " spm"}
                </td>

                {isSwimming && (
                  <>
                    <td className="p-3 text-sky-400">{lap.primary_stroke}</td>
                    <td className="p-3 text-teal-400">{lap.number_of_lengths}</td>
                  </>
                )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

const formatPace = (speedMs: number) => {
  if (!speedMs || speedMs === 0) return "0:00";
  
  // Calculate seconds per kilometer
  const secondsPerKm = 1000 / speedMs;
  
  const mins = Math.floor(secondsPerKm / 60);
  const secs = Math.floor(secondsPerKm % 60);
  
  return `${mins}:${secs.toString().padStart(2, "0")} /km`;
};