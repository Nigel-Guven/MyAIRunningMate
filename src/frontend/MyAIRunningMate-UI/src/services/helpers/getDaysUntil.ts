export const getDaysUntil = (date: Date | string) => {
    const target = new Date(date).getTime();
    const now = new Date().getTime();
    const diff = target - now;
    return Math.ceil(diff / (1000 * 60 * 60 * 24));
  };