import L from "leaflet";
import { useEffect } from "react";
import { useMap } from "react-leaflet/hooks";

export const formatDuration = (seconds: number): string => {
  const h = Math.floor(seconds / 3600);
  const m = Math.floor((seconds % 3600) / 60);
  const s = Math.floor(seconds % 60);

  // Always pad seconds to ensure it looks like :05 or :07
  const paddedS = s.toString().padStart(2, '0');

  if (h > 0) {
    // If there are hours, minutes must also be padded (e.g., 1:05:07)
    const paddedM = m.toString().padStart(2, '0');
    return `${h}:${paddedM}:${paddedS}`;
  }

  // If no hours, minutes don't need padding (e.g., 45:07 or 5:07)
  return `${m}:${paddedS}`;
};

export const formatDistanceKm = (
  metres: number
): string => {

  if( metres < 1000)
    return `${( metres).toFixed(0)} m`;

  else return `${(
    metres / 1000
  ).toFixed(2)} km`;
};

export function FitBounds({
  coordinates,
}: {
  coordinates: [number, number][];
}) {
  const map = useMap();

  useEffect(() => {
    if (coordinates.length < 2) return;

    const bounds = L.latLngBounds(coordinates);

    map.fitBounds(bounds, {
      padding: [40, 40],
      maxZoom: 16,
      animate: false,
    });
  }, [coordinates, map]);

  return null;
}

export function MapResizeFix() {
  const map = useMap();

  useEffect(() => {
    const resize = () => {
      map.invalidateSize();
    };

    resize();

    const timer = setTimeout(resize, 300);

    window.addEventListener("resize", resize);

    return () => {
      clearTimeout(timer);
      window.removeEventListener("resize", resize);
    };
  }, [map]);

  return null;
}

