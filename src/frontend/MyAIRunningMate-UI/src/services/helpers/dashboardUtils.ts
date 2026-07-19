export const getEconomyLevel = (score: number) => {
  if (score < 70) return { label: "Easy", color: "bg-green-500" };
  if (score < 80) return { label: "Good", color: "bg-yellow-500" };
  if (score < 90) return { label: "Strong", color: "bg-orange-500" };
  return { label: "Elite", color: "bg-red-500" };
};