export interface YearlyStatisticsDto {
  yearly_running_distance: number;
  yearly_swimming_distance: number;
  yearly_active_days: number;
  yearly_average_training_effect: number | null;
  yearly_total_training_effect: number | null;
}