export const getActivityStyles = (type: string) => {
  const t = type.toLowerCase();
  switch (t) {
    case 'run':
    case 'running':
      return 'bg-emerald-500/10 border-emerald-500/30 text-emerald-400';
    case 'swim':
    case 'swimming':
      return 'bg-sky-500/10 border-sky-500/30 text-sky-400';
    default:
      return 'bg-slate-100/10 border-slate-100/30 text-slate-100';
  }
};