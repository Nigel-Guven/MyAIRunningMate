const getOrdinalSuffix = (day: number) => {
  if (day > 3 && day < 21) return 'th';
  switch (day % 10) {
    case 1:  return "st";
    case 2:  return "nd";
    case 3:  return "rd";
    default: return "th";
  }
};

export const formatDateLong = (dateStr: string) => {
  const date = new Date(dateStr);
  const month = date.toLocaleString('en-US', { month: 'long' });
  const day = date.getDate();
  const year = date.getFullYear();
  return `${month} ${day}${getOrdinalSuffix(day)}, ${year}`;
};