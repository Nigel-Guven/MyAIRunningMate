namespace MyAIRunningMate.Domain.Models;

public class YearlyStatistics
{
        public int YearlyRunningDistance { get; set; }
        public int YearlySwimmingDistance { get; set; }
        public int YearlyActiveDays { get; set; }
        public double? YearlyAverageTrainingEffect { get; set; }
        public double? YearlyTotalTrainingEffect { get; set; }
}