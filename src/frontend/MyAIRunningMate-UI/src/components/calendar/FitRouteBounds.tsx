import { useEffect } from 'react';
import { useMap } from 'react-leaflet';
import type { LatLngExpression, LatLngBoundsExpression } from 'leaflet';

interface FitRouteBoundsProps {
  coords: LatLngExpression[]; 
}

export function FitRouteBounds({ coords }: FitRouteBoundsProps) {
  const map = useMap();

  useEffect(() => {
    if (coords && coords.length > 0) {
      // Cast coords to LatLngBoundsExpression to resolve the union ambiguity
      map.fitBounds(coords as LatLngBoundsExpression, {
        padding: [30, 30], 
        maxZoom: 16        
      });
    }
  }, [coords, map]);

  return null;
}