import { ChevronLeft, ChevronRight } from "lucide-react";

interface CalendarNavigationProps {
  monthLabel: string;
  onNavigate: (direction: number) => void;
}

export function CalendarNavigation({ monthLabel, onNavigate }: CalendarNavigationProps) {
  return (
    <div className="flex items-center gap-4 bg-slate-900 border border-slate-800 rounded-lg p-1 h-fit">
      <button
        onClick={() => onNavigate(-1)}
        className="hover:bg-slate-800 p-2 rounded text-slate-400"
      >
        <ChevronLeft />
      </button>
      <span className="font-bold text-slate-200 min-w-[120px] text-center">{monthLabel}</span>
      <button
        onClick={() => onNavigate(1)}
        className="hover:bg-slate-800 p-2 rounded text-slate-400"
      >
        <ChevronRight />
      </button>
    </div>
  );
}