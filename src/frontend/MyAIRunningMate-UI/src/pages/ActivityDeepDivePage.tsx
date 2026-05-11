import { useEffect, useState, } from 'react';

import { useParams } from 'react-router';

import { activityService, } from '../services/api/activity/activity.service';

import type { AggregateArtifactViewDto, } from '../types/views/aggregateArtifactView';

import { formatDistanceKm, formatDuration, } from '../services/helpers/activity.utils';

import { StatBox, } from '../components/activity/StatBox';

import { ExternalLink, } from '../components/activity/ExternalLink';

export const ActivityDeepDivePage = () => {

  const { id } = useParams();

  const [data, setData] = useState< AggregateArtifactViewDto | null >(null);

  const [loading, setLoading] = useState(true);

  const [error, setError] = useState<string | null>(null);

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
            {data.training_effect.toFixed(1)}
          </span>

        </div>
      </header>

      {/* STATS */}
      <div className="grid grid-cols-1 md:grid-cols-4 gap-4">

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
          label="Elevation"
          value={
            data.total_elevation_gain
              ? `+${data.total_elevation_gain}m`
              : '0m'
          }
        />

      </div>

      {/* MAIN GRID */}
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">

        {/* LEFT */}
        <div className="lg:col-span-2 space-y-8">

          {data.map ? (

            <div className="aspect-video bg-slate-900 rounded-xl border border-slate-800 flex items-center justify-center italic text-slate-600">
              [Geomap visualization]
            </div>

          ) : (

            <div className="p-12 bg-slate-900/20 border border-slate-800 border-dashed rounded-xl text-center text-slate-600 uppercase text-xs font-bold">
              No Geospatial Data Available
            </div>
          )}

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
                        {(
                          lap.distance_metres /
                          1000
                        ).toFixed(2)}k
                      </td>

                      <td className="p-4">
                        {lap.duration_seconds ?? '--'}
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

            <h3 className="text-slate-500 text-[10px] font-black uppercase mb-4">
              External IDs
            </h3>

            <div className="space-y-3">

              <ExternalLink
                label="Strava"
                id={data.strava_id}
              />

              <ExternalLink
                label="Garmin"
                id={data.garmin_activity_id}
              />

            </div>

          </div>

          {(data.achievement_count ?? 0) > 0 && (

            <div className="p-6 bg-amber-500/10 border border-amber-500/20 rounded-xl">

              <span className="text-amber-500 font-black text-xl">
                🏆 {data.achievement_count} Achievements
              </span>

            </div>
          )}

        </div>
      </div>
    </div>
  );
};