import type { LapResponse } from "../../types/aggregates/lapResponse";

export const LapTable = ({laps}:{laps:LapResponse[]}) => {

  const formatTime = (seconds:number)=>{
    const mins=Math.floor(seconds/60);
    const secs=Math.floor(seconds%60);
    return `${mins}:${secs.toString().padStart(2,"0")}`;
  };


  return (
    <div className="bg-slate-900 border border-slate-800 rounded-2xl p-5 overflow-hidden">

      <h2 className="text-lg font-bold mb-4">
        Laps
      </h2>

      <div className="overflow-x-auto">
        <table className="w-full text-sm">

          <thead className="text-slate-400 border-b border-slate-800">
            <tr>
              <th className="text-left p-2">Lap</th>
              <th>Distance</th>
              <th>Time</th>
              <th>HR</th>
              <th>Cadence</th>
            </tr>
          </thead>

          <tbody>
          {laps.map(lap=>(
            <tr 
              key={lap.lap_number}
              className="border-b border-slate-800/50"
            >
              <td className="p-2">
                {lap.lap_number}
              </td>

              <td>
                {(lap.distance_metres/1000).toFixed(2)} km
              </td>

              <td>
                {formatTime(lap.duration_seconds)}
              </td>

              <td>
                {lap.average_heart_rate}
              </td>

              <td>
                {lap.average_cadence}
              </td>

            </tr>
          ))}
          </tbody>

        </table>
      </div>
    </div>
  );
};