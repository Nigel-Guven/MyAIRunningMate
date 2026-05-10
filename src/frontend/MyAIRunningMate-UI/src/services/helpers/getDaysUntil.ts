export const getDaysUntil = (date: DateTime) => {
    const target = new Date(date).getTime();
    const now = new Date().getTime();
    const diff = target - now;
    return Math.ceil(diff / (1000 * 60 * 60 * 24));
  };