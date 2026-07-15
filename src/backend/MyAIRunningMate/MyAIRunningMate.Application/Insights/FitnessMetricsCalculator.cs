namespace MyAIRunningMate.Application.Insights;

public class FitnessMetricsCalculator : IFitnessMetricsCalculator
{
    public string GetVo2MaxRating(double vo2Max) =>
        vo2Max switch
        {
            < 30 => "Recruit",
            < 35 => "Private",
            < 40 => "Private First Class",
            < 44 => "Corporal",
            < 48 => "Corporal Prime",
            < 49 => "Sergeant",
            < 50 => "Staff Sergeant",
            < 51 => "Gunnery Sergeant",
            < 53 => "Lieutenant",
            < 60 => "Major",
            _ => "Field Marshal"
        };

    public string GetFitnessRankColor(double vo2Max) =>
        vo2Max switch
        {
            < 30 => "#475569", // Recruit - Muted Slate
            < 35 => "#94a3b8", // Private - Tarnished Steel
            < 40 => "#64748b", // Private First Class - Dark Steel Blue
            < 44 => "#334155", // Corporal - Dark Gunmetal
            < 48 => "#4b5563", // Corporal Prime - Burnished Lead
            < 49 => "#3f6212", // Sergeant - Olive Drab
            < 50 => "#b45309", // Staff Sergeant - Weathered Brass
            < 51 => "#a16207", // Gunnery Sergeant - Dark Ochre
            < 53 => "#cbd5e1", // Lieutenant - Polished Silver
            < 60 => "#d97706", // Major - Burnished Gold
            _    => "#991b1b"  // Field Marshal - Prestige Crimson Red
        };

    public int GetFitnessPercentile(double vo2Max) =>
        vo2Max switch
        {
            < 35 => 10,
            < 40 => 25,
            < 45 => 50,
            < 50 => 70,
            < 55 => 85,
            _ => 95
        };

    public double CalculatePowerToWeight(
        int power,
        double weightKg) =>
        Math.Round(power / weightKg, 2);

    public string GetPowerRating(double wattsPerKg) =>
        wattsPerKg switch
        {
            < 3 => "Beginner",
            < 4 => "Intermediate",
            < 5 => "Advanced",
            < 6 => "Very Advanced",
            _ => "Elite"
        };

    public double CalculateThresholdPercentageOfMaxHr(
        int thresholdHr,
        int maxHr) =>
        Math.Round(
            (double)thresholdHr / maxHr * 100,
            1);

    public string GetTrainingLevel(
        double vo2Max,
        double powerToWeight) =>
        vo2Max switch
        {
            >= 55 when powerToWeight >= 5 => "Competitive",
            >= 45 when powerToWeight >= 4 => "Highly Trained",
            >= 40 => "Recreational Runner",
            _ => "Beginner"
        };
}