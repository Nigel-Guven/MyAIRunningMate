export const buildCalendarMonth = (
  date: Date
) => {

  const year = date.getFullYear();

  const month = date.getMonth();

  const daysInMonth = new Date(
    year,
    month + 1,
    0
  ).getDate();

  const firstDayIndex = new Date(
    year,
    month,
    1
  ).getDay();

  const offset =
    firstDayIndex === 0
      ? 6
      : firstDayIndex - 1;

  return {
    days: Array.from(
      { length: daysInMonth },
      (_, i) => i + 1
    ),

    blanks: Array.from(
      { length: offset },
      (_, i) => i
    ),

    monthLabel: date.toLocaleString(
      'default',
      {
        month: 'long',
        year: 'numeric',
      }
    ),
  };
};