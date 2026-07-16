import React, { useMemo } from "react";
import {
  MapContainer,
  TileLayer,
 Polyline,
  Marker,
  Popup,
} from "react-leaflet";
import L from "leaflet";
import polyline from "@mapbox/polyline";

import "leaflet/dist/leaflet.css";

import markerIcon from "leaflet/dist/images/marker-icon.png";
import markerShadow from "leaflet/dist/images/marker-shadow.png";

import {
  FitBounds,
  MapResizeFix,
} from "../../services/helpers/activity.utils";

const DefaultIcon = L.icon({
  iconUrl: markerIcon,
  shadowUrl: markerShadow,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
});

L.Marker.prototype.options.icon = DefaultIcon;

interface ActivityMapProps {
  mapPolyline: string;
  locationName: string;
}

export const ActivityMap: React.FC<ActivityMapProps> = ({
  mapPolyline,
  locationName,
}) => {
  const coordinates = useMemo(() => {
    if (!mapPolyline) return [];

    try {
      // Try normal precision (Strava / Google)
      let coords = polyline.decode(mapPolyline);

      // If obviously invalid, try precision 6 (Mapbox/OSRM)
      const first = coords[0];

      if (
        first &&
        (Math.abs(first[0]) > 90 ||
          Math.abs(first[1]) > 180)
      ) {
        coords = polyline.decode(mapPolyline, 6);
      }

      return coords.filter(
        ([lat, lng]) =>
          Number.isFinite(lat) &&
          Number.isFinite(lng) &&
          Math.abs(lat) <= 90 &&
          Math.abs(lng) <= 180
      ) as [number, number][];
    } catch (err) {
      console.error("Polyline decode failed", err);
      return [];
    }
  }, [mapPolyline]);

  if (coordinates.length === 0) {
    return (
      <div className="flex h-96 items-center justify-center rounded-2xl border border-slate-800 bg-slate-900 text-slate-400">
        No map coordinates available.
      </div>
    );
  }

  const startPoint = coordinates[0];
  const endPoint = coordinates[coordinates.length - 1];

  return (
    <div className="w-full rounded-2xl border border-slate-800 bg-slate-900 p-4 shadow-xl">
      <h3 className="mb-3 text-lg font-semibold text-white">
        Activity Route
      </h3>

      <div className="h-[500px] w-full overflow-hidden rounded-xl border border-slate-800">
        <MapContainer
          key={mapPolyline}
          center={startPoint}
          zoom={13}
          scrollWheelZoom
          style={{
            width: "100%",
            height: "100%",
          }}
        >
          <MapResizeFix />

          <FitBounds coordinates={coordinates} />

          <TileLayer
            url="https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png"
            attribution="&copy; OpenStreetMap contributors &copy; CARTO"
          />

          <Polyline
            positions={coordinates}
            pathOptions={{
              color: "#22c55e",
              weight: 6,
              opacity: 1,
            }}
          />

          <Marker position={startPoint}>
            <Popup>Start</Popup>
          </Marker>

          <Marker position={endPoint}>
            <Popup>Finish</Popup>
          </Marker>
        </MapContainer>
      </div>

      <div className="mt-2 text-right text-xs text-slate-400">
        Route in <span className="text-white font-medium">{locationName}</span>
      </div>
    </div>
  );
};