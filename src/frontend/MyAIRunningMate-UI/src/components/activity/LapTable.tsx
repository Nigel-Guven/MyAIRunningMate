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
    <div className="bg-slate-900 border border-slate-800 rounded-2xl p-5 overflow-hidden">
      <h2 className="text-lg font-bold mb-4">Laps</h2>

      <div className="overflow-x-auto">
        <table className="w-full text-sm">
          <thead className="text-slate-400 border-b border-slate-800">
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

          <tbody>
            {laps.map((lap) => (
              <tr
                key={lap.lap_number}
                className="border-b border-slate-800/50"
              >
                <td className="text-left p-2">{lap.lap_number}</td>
                <td className="text-left p-2">{(lap.distance_metres / 1000).toFixed(2)} km</td>
                <td className="text-left p-2">{formatTime(lap.duration_seconds)}</td>
                <td className="text-left p-2">{lap.average_heart_rate} bpm</td>
                <td className="text-left p-2">{lap.max_heart_rate} bpm</td>
                <td className="text-left p-2">{formatPace(lap.average_speed)}</td>
                <td className="text-left p-2">
                  {lap.average_cadence !== null 
                    ? (isSwimming ? lap.average_cadence : lap.average_cadence * 2) 
                    : "-"}
                  {!isSwimming && lap.average_cadence !== null && " spm"}
                </td>

                {isSwimming && (
                  <>
                    <td className="text-left p-2">{lap.primary_stroke}</td>
                    <td className="text-left p-2">{lap.number_of_lengths}</td>
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