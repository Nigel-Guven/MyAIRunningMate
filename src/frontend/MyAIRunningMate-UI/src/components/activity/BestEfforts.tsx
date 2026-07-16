import type { BestEffortResponse } from "../../types/dashboard/bestEffortResponse";


export const BestEfforts = ({
  efforts
}:{
  efforts:BestEffortResponse[] | null
}) => {

  const formatTime=(seconds:number)=>{
    const mins=Math.floor(seconds/60);
    const secs=Math.floor(seconds%60);

    return `${mins}:${secs.toString().padStart(2,"0")}`;
  };


  return (
    <div className="bg-slate-900 border border-slate-800 rounded-2xl p-5">

      <h2 className="text-lg font-bold mb-4">
        Best Efforts
      </h2>


      {!efforts || efforts.length===0 ? (
        <div className="text-slate-500 text-sm">
          No best efforts available
        </div>
      ) : (

        <div className="space-y-3">

        {efforts.map((effort,index)=>(
          <div
            key={index}
            className="flex justify-between items-center"
          >

            <span className="text-slate-400">
              {effort.distance_label}
            </span>

            <span className="font-bold">
              {effort.time_achievement != null ? formatTime(effort.time_achievement) : 0}
            </span>

          </div>
        ))}

        </div>
      )}

    </div>
  );
};