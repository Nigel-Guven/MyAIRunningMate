import type { CalendarViewResponse } from "./calendarViewResponse";
import type { TrainingPlanViewResponse } from "../nexus/trainingPlanViewResponse";

export interface CalendarTypes {

  calendarViews: CalendarViewResponse[];
  trainingPlanViews: TrainingPlanViewResponse[];
}