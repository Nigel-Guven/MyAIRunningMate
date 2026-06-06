export interface CountyConfig {
  name: string;
  primaryColor: string;
  secondaryColor: string;
  layout: 'vertical' | 'horizontal' | 'bar';
}

export const COUNTY_COLORS: Record<string, CountyConfig> = {
  "Dublin": {
    name: "Dublin",
    primaryColor: "#85b3e2",   // Sky Blue
    secondaryColor: "#003b6f", // Navy Blue
    layout: "horizontal"
  },
  "Kerry": {
    name: "Kerry",
    primaryColor: "#008542",   // Green
    secondaryColor: "#fdef42", // Gold
    layout: "bar"
  },
  "Kildare": {
    name: "Kildare",
    primaryColor: "#ffffff",   // All White
    secondaryColor: "#e2e8f0", // Subtle grey divider line
    layout: "horizontal"
  },
  "Down": {
    name: "Down",
    primaryColor: "#000000",   // Black
    secondaryColor: "#e31b23", // Red
    layout: "vertical"
  },
  "Mayo": {
    name: "Mayo",
    primaryColor: "#006643",   // Green
    secondaryColor: "#e31b23", // Red
    layout: "horizontal"
  },
  "Wicklow": {
    name: "Wicklow",
    primaryColor: "#005da4",   // Blue
    secondaryColor: "#fdef42", // Gold
    layout: "horizontal"       // Blue over Gold
  },
  "Tipperary": {
    name: "Tipperary",
    primaryColor: "#005da4",   // Blue
    secondaryColor: "#fdef42", // Gold
    layout: "bar"              // Blue flag with a distinct Gold central hoop
  },
  "Donegal": {
    name: "Donegal",
    primaryColor: "#008542",   // Green
    secondaryColor: "#fdef42", // Gold
    layout: "vertical"         // Green and Gold vertical split
  },
  "Louth": {
    name: "Louth",
    primaryColor: "#e31b23",   // Red
    secondaryColor: "#ffffff", // White
    layout: "horizontal"       // Red over White
  },
  "Meath": {
    name: "Meath",
    primaryColor: "#006643",   // Green
    secondaryColor: "#fdef42", // Gold
    layout: "bar"              // Traditional Green with a Gold stripe
  },
  "Cavan": {
    name: "Cavan",
    primaryColor: "#003b6f",   // Royal Blue
    secondaryColor: "#ffffff", // White
    layout: "horizontal"       // Blue over White
  },
  "Galway": {
    name: "Galway",
    primaryColor: "#7a1c3e",   // Maroon
    secondaryColor: "#ffffff", // White
    layout: "vertical"         // Maroon and White vertical split
  },
  "Wexford": {
    name: "Wexford",
    primaryColor: "#4b0082",   // Purple
    secondaryColor: "#fdef42", // Gold
    layout: "horizontal"       // Purple over Gold split
  },
  "Fermanagh": {
    name: "Fermanagh",
    primaryColor: "#008542",   // Green
    secondaryColor: "#ffffff", // White
    layout: "vertical"         // Green and White vertical split
  }
};