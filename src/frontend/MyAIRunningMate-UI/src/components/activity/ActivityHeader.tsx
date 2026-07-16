import React from 'react';
import { Calendar, MapPin, Activity} from 'lucide-react';
import GarminLogo from "../../assets/garmin-connect.png";
import StravaLogo from "../../assets/strava.png";

interface ActivityHeaderProps {
  garminActivityId: string;
  exerciseName: string;
  startTime: string;
  location: string;
  distanceMetres: number;
  movingTime: number;
  totalTime: number;
  totalAscent: number | null;
  totalDescent: number | null;
  numberOfLaps: number;
}

export const ActivityHeader: React.FC<ActivityHeaderProps> = ({
  garminActivityId,
  exerciseName,
  startTime,
  location,
  distanceMetres,
  movingTime,
  totalTime,
  totalAscent,
  totalDescent,
  numberOfLaps,
}) => {
  const distanceKm = (distanceMetres / 1000).toFixed(2);

  const formatTime = (seconds: number) => {
    const hrs = Math.floor(seconds / 3600);
    const mins = Math.floor((seconds % 3600) / 60);
    const secs = Math.floor(seconds % 60);

    if (hrs > 0) {
      return `${hrs}:${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
    }
    return `${mins}:${secs.toString().padStart(2, '0')}`;
  };

  const calculatePace = (seconds: number, meters: number) => {
    if (meters === 0) return '0:00';
    const km = meters / 1000;
    const paceMinDec = (seconds / 60) / km;
    const paceMins = Math.floor(paceMinDec);
    const paceSecs = Math.round((paceMinDec - paceMins) * 60);
    return `${paceMins}:${paceSecs.toString().padStart(2, '0')}`;
  };

  const formattedDate = new Date(startTime).toLocaleDateString('en-IE', {
    weekday: 'long',
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });

  return (
    <div className="w-full bg-slate-900 text-white p-6 rounded-2xl border border-slate-800 shadow-xl">
      <div className="flex flex-col md:flex-row md:items-center md:justify-between gap-4">
        {/* Title & Metadata */}
        <div>
          <div className="flex items-center gap-2">
            <span className="p-2 bg-emerald-500/10 text-emerald-400 rounded-lg">
              <Activity className="h-6 w-6" />
            </span>
            <h1 className="text-2xl font-bold tracking-tight">{exerciseName}</h1>
          </div>
          <div className="flex flex-wrap items-center gap-4 mt-2 text-sm text-slate-400">
            <span className="flex items-center gap-1">
              <Calendar className="h-4 w-4" />
              {formattedDate}
            </span>

            <span className="flex items-center gap-1">
              <MapPin className="h-4 w-4" />
              {location}
            </span>

            <a
              href={`https://connect.garmin.com/app/activity/${garminActivityId}`}
              target="_blank"
              rel="noopener noreferrer"
              className="flex items-center gap-1 text-emerald-400 hover:text-emerald-300"
            >
              <img src={GarminLogo} className="h-4 w-4" alt="Garmin" />
              Garmin
            </a>

            <a
              href="https://www.strava.com/athlete/training"
              target="_blank"
              rel="noopener noreferrer"
              className="flex items-center gap-1 text-orange-400 hover:text-orange-300"
            >
              <img src={StravaLogo} className="h-4 w-4" alt="Strava" />
              Strava
            </a>
          </div>
        </div>

        {/* Highlight Stats Grid */}
        <div className="grid grid-cols-2 sm:grid-cols-4 gap-4 bg-slate-950 p-4 rounded-xl border border-slate-800/80">
          <div className="px-2">
            <span className="text-xs text-slate-400 block mb-1">Distance</span>
            <span className="text-xl font-bold">{distanceKm} <span className="text-xs text-emerald-400">km</span></span>
          </div>
          <div className="px-2 border-l border-slate-800">
            <span className="text-xs text-slate-400 block mb-1">Elapsed Time</span>
            <span className="text-xl font-bold">{formatTime(totalTime)}</span>
          </div>
          <div className="px-2 border-l border-slate-800">
            <span className="text-xs text-slate-400 block mb-1">Moving Time</span>
            <span className="text-xl font-bold">{formatTime(movingTime)}</span>
          </div>
          <div className="px-2 border-l border-slate-800">
            <span className="text-xs text-slate-400 block mb-1">Avg Pace</span>
            <span className="text-xl font-bold">{calculatePace(totalTime, distanceMetres)} <span className="text-xs text-slate-400">/km</span></span>
          </div>
          <div className="px-2 border-l border-slate-800">
            <span className="text-xs text-slate-400 block mb-1">Laps</span>
            <span className="text-xl font-bold">{numberOfLaps}</span>
          </div>
          <div className="px-2 border-l border-slate-800">
            <span className="text-xs text-slate-400 block mb-1">Elevation Gain</span>
            <span className="text-xl font-bold">{totalAscent} m</span>
          </div>
          <div className="px-2 border-l border-slate-800">
            <span className="text-xs text-slate-400 block mb-1">Elevation Loss</span>
            <span className="text-xl font-bold">{totalDescent} m</span>
          </div>
        </div>
      </div>
    </div>
  );
};