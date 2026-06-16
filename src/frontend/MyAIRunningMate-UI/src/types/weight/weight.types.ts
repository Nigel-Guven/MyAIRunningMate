import type { WeightRequest } from "./weightRequest";
import type { WeightResponse } from "./weightResponse";


export interface WeightTypes {
  latestWeight: WeightResponse | null;
  updateWeightRequest: WeightRequest 
}