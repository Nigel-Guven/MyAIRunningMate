import type { EventViewResponse } from "../../types/dashboard/eventViewResponse";

export default function UpcomingSchedule({ upcomingEvents = [] as EventViewResponse[] }) {
  // Guard clause in case events data is missing or empty
  if (!upcomingEvents || upcomingEvents.length === 0) {
    return (
      <div className="rounded-3xl border border-slate-800 bg-slate-900 p-6 text-center text-sm text-slate-500">
        No upcoming events.
      </div>
    );
  }

  return (
    <div className="rounded-3xl border border-slate-800 bg-slate-900 p-6">
      <h4 className="text-[10px] font-black text-slate-500 uppercase tracking-[0.2em] mb-4">
        Next Up
      </h4>
      {upcomingEvents.map((event: EventViewResponse, i) => (
        <div 
          key={i} 
          className="flex items-center gap-4 mb-3 last:mb-0 p-2 rounded-xl hover:bg-white/5 transition-colors"
        >
          {/* Date Badge */}
          <div className="text-center bg-blue-500/10 p-2 rounded-lg min-w-[45px]">
            <p className="text-[10px] font-black text-blue-400 uppercase leading-none mb-1">
              {new Date(event.event_date).toLocaleString('default', { month: 'short' })}
            </p>
            <p className="text-lg font-black text-white leading-none">
              {new Date(event.event_date).getDate()}
            </p>
          </div>

          {/* Event Details */}
          <div className="group p-4 rounded-2xl transition-all w-full min-w-0">
            <p className="text-xs font-bold text-white leading-tight uppercase truncate">
              <a 
                href={event.event_url} 
                target="_blank" 
                rel="noopener noreferrer" 
                className="transition-colors duration-200 hover:text-blue-400"
              >
                {event.event_name}
              </a>
            </p>
            <p className="pt-2 text-[10px] text-slate-600 font-mono uppercase truncate">
              {event.event_location}
            </p>
          </div>
        </div>
      ))}
    </div>
  );
}