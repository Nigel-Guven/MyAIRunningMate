export const getActivityStyles = (type: string, distance: number) => {
  const t = type.toLowerCase();
  switch (t) {
    case 'run':
    case 'running':
      if (distance >= 8000) {
        return 'bg-fuchsia-500/15 border-fuchsia-400/40 text-fuchsia-300';
      }

      return 'bg-lime-500/15 border-lime-400/40 text-lime-300';
    case 'swim':
    case 'swimming':
      if (distance >= 1000) {
        return 'bg-blue-500/20 border-blue-400/40 text-blue-300';
      }
      
      return 'bg-cyan-500/15 border-cyan-400/40 text-cyan-300';
    case 'cycle':
    case 'cycling':
      return 'bg-amber-500/15 border-amber-400/40 text-amber-300';
    case 'hike':
    case 'hiking':
      return 'bg-emerald-700/20 border-emerald-500/30 text-emerald-300';
    case 'ai_suggestion':
      return 'bg-violet-500/15 border-violet-400/30 text-violet-300';
    default:
      return 'bg-slate-100/10 border-slate-100/30 text-slate-100';
  }
};