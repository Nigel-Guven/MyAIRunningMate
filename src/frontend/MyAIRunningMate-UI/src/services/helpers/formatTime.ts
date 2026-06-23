export const formatTime = (seconds: number | null | undefined): string => {
  if (seconds === null || seconds === undefined || seconds <= 0) return "--:--";

  const totalSeconds = Math.round(seconds);
  
  const hrs = Math.floor(totalSeconds / 3600);
  const mins = Math.floor((totalSeconds % 3600) / 60);
  const secs = totalSeconds % 60;
  
  const parts = [mins, secs].map(v => v.toString().padStart(2, '0'));
  
  if (hrs > 0) {
    parts[0] = mins.toString().padStart(2, '0');
    parts.unshift(hrs.toString().padStart(2, '0'));
  }
  
  return parts.join(':');
};