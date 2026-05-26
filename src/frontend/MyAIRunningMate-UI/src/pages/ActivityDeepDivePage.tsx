import { useEffect, useMemo, useState, } from 'react';

import { useParams } from 'react-router';

import { activityService, } from '../services/api/activity/activity.service';

import type { AggregateArtifactViewDto, } from '../types/views/aggregateArtifactView';

import { formatDistanceKm, formatDuration, } from '../services/helpers/activity.utils';

import { StatBox, } from '../components/activity/StatBox';

import { ExternalLink, } from '../components/activity/ExternalLink';

import { MapContainer, TileLayer, Polyline } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import polyline from "@mapbox/polyline";

import {ResponsiveContainer,ComposedChart,Line, Bar, XAxis, YAxis, Tooltip, CartesianGrid, Legend, } from "recharts";

export const ActivityDeepDivePage = () => {

  const { id } = useParams();

  const [data, setData] = useState< AggregateArtifactViewDto | null >(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const coords = useMemo(() => {
    if (!data?.map?.map_polyline) return null;
    return polyline.decode(data.map.map_polyline);
  }, [data?.map?.map_polyline]);

  const lapChartData = useMemo(() => {
    return (
      data?.laps?.map((lap, idx) => ({
        lap: idx + 1,
        distance: lap.distance_metres,
        hr: lap.average_heart_rate,
        pace: Number((lap.duration_seconds / 60).toFixed(2)), // minutes
      })) || []
    );
  }, [data?.laps]);

  useEffect(() => {

    if (!id) return;

    const load = async () => {

      try {

        setLoading(true);

        const result =
          await activityService
            .getDetail(id);

        setData(result);

      } catch (err) {

        console.error(err);

        setError(
          'Failed to load activity.'
        );

      } finally {

        setLoading(false);
      }
    };

    load();

  }, [id]);

  if (loading) {

    return (
      <div className="p-10 text-slate-500 animate-pulse uppercase font-black">
        Decrypting Activity Data...
      </div>
    );
  }

  if (error) {

    return (
      <div className="p-10 text-red-500">
        {error}
      </div>
    );
  }

  if (!data) {

    return (
      <div className="p-10 text-red-500">
        Activity not found in Vault.
      </div>
    );
  }

  return (
    <div className="p-8 space-y-8 max-w-7xl mx-auto text-white">

      {/* HEADER */}
      <header className="flex justify-between items-end border-b border-slate-800 pb-6">

        <div>

          <p className="text-sky-500 text-xs font-black uppercase tracking-widest mb-2">
            {data.exercise_type}
          </p>

          <h1 className="text-4xl font-black">
            {data.name || 'Unnamed Artifact'}
          </h1>

          <p className="text-slate-500 mt-2">
            {new Date(
              data.start_time
            ).toLocaleString()}
          </p>

        </div>

        <div className="text-right">

          <span className="block text-slate-500 text-[10px] font-bold uppercase">
            Training Effect
          </span>

          <span className="text-4xl font-black text-emerald-500">
            {(data.training_effect ?? 0).toFixed(1)}
          </span>

        </div>
      </header>

      {/* STATS */}
      <div
        className={`grid grid-cols-1 gap-4 ${
          data.exercise_type === "running" ? "md:grid-cols-6" : "md:grid-cols-4"
        }`}
      >

        <StatBox
          label="Distance"
          value={formatDistanceKm(
            data.distance_metres
          )}
        />

        <StatBox
          label="Duration"
          value={formatDuration(
            data.duration_seconds
          )}
        />

        <StatBox
          label="Avg Heart Rate"
          value={
            data.average_heart_rate
              ? `${data.average_heart_rate} bpm`
              : 'N/A'
          }
        />

        <StatBox
          label="Max Heart Rate"
          value={
            data.max_heart_rate
              ? `${data.max_heart_rate} bpm`
              : 'N/A'
          }
        />

        {data.exercise_type === "running" && (
          <StatBox
            label="Average Pace / km"
            value={formatDuration(data.average_second_per_kilometre)}
          />
        )}

        {data.exercise_type === "running" && (
          <StatBox
            label="Elevation"
            value={
              data.total_elevation_gain
                ? `+${data.total_elevation_gain}m`
                : '0m'
            }
          />
        )}

      </div>

      {/* MAIN GRID */}
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">

        {/* LEFT */}
        <div className="lg:col-span-2 space-y-8">

          {coords?.length && (
            <div className="aspect-video rounded-xl overflow-hidden border border-slate-800">
              <MapContainer
                center={coords[0]}
                zoom={16}
                className="h-full w-full"
              >
                <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                <Polyline positions={coords} pathOptions={{ color: 'blue', weight: 4 }} />
              </MapContainer>
            </div>
          )}

          {/* LAP ANALYTICS */}
          <div className="bg-slate-900 border border-slate-800 rounded-xl p-6">

            <h3 className="text-slate-400 text-xs font-semibold uppercase tracking-wider mb-4">
              Lap Analytics
            </h3>

            <div className="h-[350px]">

              <ResponsiveContainer width="100%" height="100%">

                <ComposedChart data={lapChartData}>

                  <CartesianGrid strokeDasharray="3 3" stroke="#1e293b" />

                  <XAxis
                    dataKey="lap"
                    stroke="#94a3b8"
                  />

                  <YAxis
                    yAxisId="left"
                    stroke="#38bdf8"
                    label={{
                      value: "Pace (min)",
                      angle: -90,
                      position: "insideLeft",
                      fill: "#38bdf8",
                    }}
                  />

                  <YAxis
                    yAxisId="right"
                    orientation="right"
                    stroke="#ef4444"
                    label={{
                      value: "HR",
                      angle: 90,
                      position: "insideRight",
                      fill: "#ef4444",
                    }}
                  />

                  <Tooltip
                    contentStyle={{
                      backgroundColor: "#0f172a",
                      border: "1px solid #334155",
                    }}
                    formatter={(value: any, name: any) => {
                      // Return early if either expected piece of data is missing
                      if (value === undefined || value === null || !name) {
                        return [value, name];
                      }

                      if (name === "pace") {
                        const totalSeconds = Math.round(Number(value) * 60);
                        const mins = Math.floor(totalSeconds / 60);
                        const secs = totalSeconds % 60;
                        const paddedSecs = String(secs).padStart(2, '0');

                        return [`${mins}m ${paddedSecs}s`, "Pace"];
                      }

                      if (name === "hr") {
                        return [`${value} bpm`, "Heart Rate"];
                      }

                      return [value, name];
                    }}
                    labelFormatter={(label) => `Lap ${label}`}
                  />

                  <Legend />

                  {/* Pace */}
                  <Bar
                    yAxisId="left"
                    dataKey="pace"
                    fill="#38bdf8"
                    radius={[4, 4, 0, 0]}
                    name="Pace"
                  />

                  {/* Heart Rate */}
                  <Line
                    yAxisId="right"
                    type="monotone"
                    dataKey="hr"
                    stroke="#ef4444"
                    strokeWidth={3}
                    dot={{ r: 4 }}
                    name="Heart Rate"
                  />

                </ComposedChart>

              </ResponsiveContainer>

            </div>
          </div>

          {/* LAPS */}
          <div className="bg-slate-900 border border-slate-800 rounded-xl overflow-hidden">

            <table className="w-full text-left text-sm">

              <thead className="bg-slate-950 text-slate-500 uppercase text-[10px] font-black">

                <tr>
                  <th className="p-4">Lap</th>
                  <th className="p-4">Distance</th>
                  <th className="p-4">Pace</th>
                  <th className="p-4">HR</th>
                </tr>

              </thead>

              <tbody className="divide-y divide-slate-800">

                {data.laps?.map(
                  (lap, idx) => (

                    <tr
                      key={idx}
                      className="hover:bg-slate-800/50"
                    >

                      <td className="p-4 font-bold">
                        {idx + 1}
                      </td>

                      <td className="p-4">
                        {data.exercise_type === "swimming"
                          ? `${lap.distance_metres}m`
                          : `${(lap.distance_metres / 1000).toFixed(2)}k`}
                      </td>

                      <td className="p-4">
                        {formatDuration(lap.duration_seconds) ?? '--'}
                      </td>

                      <td className="p-4">
                        {lap.average_heart_rate ?? '--'}
                      </td>

                    </tr>
                  )
                )}

              </tbody>

            </table>

          </div>
        </div>

        {/* RIGHT */}
        <div className="space-y-4">

          <div className="p-6 bg-slate-900 border border-slate-800 rounded-xl">
            <h3 className="text-slate-400 text-xs font-semibold uppercase tracking-wider mb-4">
              External Activities
            </h3>

            <div className="space-y-3">
              {data.strava_id && (
                <ExternalLink
                  label="Strava"
                  href={`https://www.strava.com/activities/${data.strava_id}`}
                />
              )}

              {data.garmin_activity_id && (
                <ExternalLink
                  label="Garmin"
                  href={`https://connect.garmin.com/modern/activity/${data.garmin_activity_id}`}
                />
              )}
            </div>
          </div>

          {(data.achievement_count ?? 0) > 0 && (

            <div className="p-6 bg-amber-500/10 border border-amber-500/20 rounded-xl">

              <span className="text-amber-500 font-black text-xl">
                🏆 {data.achievement_count} Achievements
              </span>

            </div>
          )}

          {(data.kudos_count ?? 0) > 0 && (

            <div className="p-6 bg-amber-500/10 border border-amber-500/20 rounded-xl">

              <span className="text-amber-500 font-black text-xl">
                👍 {data.kudos_count} Kudos
              </span>

            </div>
          )}

          {(data.athlete_count ?? 0) > 0 && (

            <div className="p-6 bg-amber-500/10 border border-amber-500/20 rounded-xl">

              <span className="text-amber-500 font-black text-xl">
                🏃 {data.athlete_count} Athletes
              </span>

            </div>
          )}

          {(data.personal_record_count ?? 0) > 0 && (

            <div className="p-6 bg-amber-500/10 border border-amber-500/20 rounded-xl">

              <span className="text-amber-500 font-black text-xl">
                🏅 {data.personal_record_count} PR's Set
              </span>

            </div>
          )}

        </div>
      </div>
    </div>
  );
};