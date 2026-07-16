export const TimeSeriesRecordTooltip = ({ active, payload, label }: any) => {
  if (active && payload && payload.length) {
    
    // Filter out average_heart_rate so it doesn't render in the tooltip box
    const filteredPayload = payload.filter(
      (entry: any) => entry.dataKey !== "average_heart_rate"
    );

    return (
      <div className="bg-slate-950 border border-slate-800 p-4 rounded-lg shadow-2xl min-w-[160px]">
        {/* Header */}
        <p className="text-slate-500 text-[10px] font-bold mb-2 uppercase tracking-widest">
          {label}
        </p>
        
        <div className="flex flex-col gap-1">
          {filteredPayload.map((entry: any, index: number) => {
            let textColorClass = "text-slate-200";
            let unit = "";
            let displayName = entry.name;

            switch (entry.dataKey) {
              case "heart_rate":
                textColorClass = "text-red-400";
                unit = "Bpm";
                displayName = "Heart Rate";
                break;
              case "cadence":
                textColorClass = "text-blue-400";
                unit = "Spm";
                displayName = "Cadence";
                break;
              case "power":
                textColorClass = "text-green-400";
                unit = "Watts";
                displayName = "Power";
                break;
            }

            return (
              <div key={index} className="flex flex-col">
                <div className="flex justify-between items-center gap-6">
                  <span className="text-[10px] font-medium text-slate-400 uppercase">
                    {displayName}
                  </span>
                  <p className={`${textColorClass} font-bold text-lg flex items-baseline gap-1`}>
                    {entry.value} 
                    <span className="text-[10px] font-medium text-slate-500 uppercase">
                      {unit}
                    </span>
                  </p>
                </div>
                
                {/* Add a divider below the item, unless it's the last one in the filtered array */}
                {index < filteredPayload.length - 1 && (
                  <div className="h-[1px] w-full bg-slate-800 my-1" />
                )}
              </div>
            );
          })}
        </div>
      </div>
    );
  }

  return null;
};