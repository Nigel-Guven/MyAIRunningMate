export const getActivityStyles = (type: string) => {
  const t = type?.toLowerCase() || '';
  
  const baseStyles = 'bg-slate-900/50 border-slate-700 text-slate-200 border-l-[4px]';

  switch (t) {
    case 'run':
    case 'running':
      return `${baseStyles} border-l-lime-500 hover:bg-slate-800/80`;
    case 'swim':
    case 'swimming':
      return `${baseStyles} border-l-cyan-500 hover:bg-slate-800/80`;
    case 'cycle':
    case 'cycling':
      return `${baseStyles} border-l-amber-500 hover:bg-slate-800/80`;
    case 'hike':
    case 'hiking':
      return `${baseStyles} border-l-emerald-500 hover:bg-slate-800/80`;
    case 'ai_suggestion':
      return `${baseStyles} border-l-violet-500 hover:bg-slate-800/80`;
    default:
      return `${baseStyles} border-l-slate-500 hover:bg-slate-800/80`;
  }
};