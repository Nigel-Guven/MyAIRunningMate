export const formatTime = (seconds: number | null) => {
    if (!seconds) return "--:--";
    const hrs = Math.floor(seconds / 3600);
    const mins = Math.floor((seconds % 3600) / 60);
    const secs = seconds % 60;
    const parts = [mins, secs].map(v => v.toString().padStart(2, '0'));
    if (hrs > 0) parts.unshift(hrs.toString());
    return parts.join(':');
  };