import L from "leaflet";
import { useEffect } from "react";
import { useMap } from "react-leaflet/hooks";

export const formatDuration = (
  seconds: number
): string => {

  const h =
    Math.floor(seconds / 3600);

  const m =
    Math.floor((seconds % 3600) / 60);

  const s =
    Math.floor(seconds % 60);

  return h > 0
    ? `${h}h ${m}m ${s}s`
    : `${m}m ${s}s`;
};

export const formatDistanceKm = (
  metres: number
): string => {

  return `${(
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

