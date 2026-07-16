import React from 'react';
import { Battery, Zap, Clock, TrendingDown, Wind } from 'lucide-react';

interface UserSpecificDetailsProps {
  beginningBodyBattery: number;
  beginningBodyPotential: number;
  endingBodyBattery: number;
  endingPotential: number;
  userVolumetricOxygenMax: number;
  userMaxHeartRate: number;
  userLactateThresholdHeartRate: number;
  userLactateThresholdPower: number;
  userLactateThresholdSpeed: number;
  recoveryTimeMinutes: number;
}

export const UserSpecificDetails: React.FC<UserSpecificDetailsProps> = ({
  beginningBodyBattery,
  beginningBodyPotential,
  endingBodyBattery,
  endingPotential,
  userVolumetricOxygenMax,
  userMaxHeartRate,
  userLactateThresholdHeartRate,
  userLactateThresholdPower,
  userLactateThresholdSpeed,
  recoveryTimeMinutes,
}) => {
  const formatRecoveryTime = (minutes: number) => {
    const hours = Math.round(minutes / 60);
    if (hours >= 24) {
      const days = (hours / 24).toFixed(1);
      return `${days} Days`;
    }
    return `${hours} Hours`;
  };

  const bodyBatteryDrain = beginningBodyBattery - endingBodyBattery;

  return (
    <div className="grid grid-cols-1 md:grid-cols-1 gap-6 w-full">
      
      {/* Card 1: Garmin Body Battery & Stress Impact */}
      <div className="bg-slate-900 border border-slate-800 rounded-2xl p-6 flex flex-col justify-between">
        <div>
          <h3 className="text-lg font-semibold text-white flex items-center gap-2 mb-4">
            <Battery className="h-5 w-5 text-cyan-400" />
            Body Battery & Energy Metrics
          </h3>
          
          <div className="space-y-4">
            {/* Battery Level depletion */}
            <div>
              <div className="flex justify-between text-sm mb-1">
                <span className="text-slate-400">Charge State (Start vs End)</span>
                <span className="font-medium text-white">{beginningBodyBattery} → {endingBodyBattery}</span>
              </div>
              <div className="w-full bg-slate-950 h-3 rounded-full overflow-hidden relative border border-slate-800">
                <div 
                  className="bg-cyan-500 h-full absolute left-0" 
                  style={{ width: `${beginningBodyBattery}%` }} 
                />
                <div 
                  className="bg-red-500 h-full absolute left-0 transition-all duration-1000" 
                  style={{ width: `${endingBodyBattery}%` }} 
                />
              </div>
            </div>

            {/* Potential Depletion */}
            <div>
              <div className="flex justify-between text-sm mb-1">
                <span className="text-slate-400">Overall Energy Potential</span>
                <span className="font-medium text-white">{beginningBodyPotential}% → {endingPotential}%</span>
              </div>
              <div className="w-full bg-slate-950 h-3 rounded-full overflow-hidden border border-slate-800">
                <div 
                  className="bg-emerald-500 h-full transition-all" 
                  style={{ width: `${endingPotential}%` }} 
                />
              </div>
            </div>
          </div>
        </div>

        {/* Energy Summary Footer */}
        <div className="mt-6 pt-4 border-t border-slate-800/60 flex justify-between items-center text-sm">
          <span className="text-slate-400 flex items-center gap-1">
            <TrendingDown className="h-4 w-4 text-red-400" /> Total Active Drain
          </span>
          <span className="font-semibold text-red-400">-{bodyBatteryDrain} Points</span>
        </div>
      </div>

      {/* Card 2: Physiological Thresholds */}
      <div className="bg-slate-900 border border-slate-800 rounded-2xl p-6">
        <h3 className="text-lg font-semibold text-white flex items-center gap-2 mb-4">
          <Zap className="h-5 w-5 text-amber-400" />
          Physiological Thresholds
        </h3>

        <div className="grid grid-cols-2 gap-4">
          <div className="bg-slate-950 p-3 rounded-xl border border-slate-800/80">
            <span className="text-xs text-slate-500 block">VO2 Max</span>
            <span className="text-lg font-bold text-white">
              <Wind className="h-4 w-4 text-amber-400 inline mr-2" />
              {userVolumetricOxygenMax}
              </span>
          </div>

          <div className="bg-slate-950 p-3 rounded-xl border border-slate-800/80">
            <span className="text-xs text-slate-500 block">
              <p>Recovery Time</p>
            </span>
            <span className="text-lg font-bold text-white flex items-center gap-1">
              <Clock className="h-4 w-4 text-sky-400 inline mr-2" />
              {formatRecoveryTime(recoveryTimeMinutes)}
            </span>
          </div>

          <div className="bg-slate-950 p-3 rounded-xl border border-slate-800/80 col-span-2">
            <span className="text-xs text-slate-500 block mb-2">Lactate Threshold Markers</span>
            <div className="grid grid-cols-2 gap-2 text-center">
              <div className="border-r border-slate-800/80">
                <span className="text-[10px] text-slate-400 block">Max Heart Rate</span>
                <span className="text-sm font-semibold text-rose-400">{userMaxHeartRate} bpm</span>
              </div>
              <div className="border-r border-slate-800/80">
                <span className="text-[10px] text-slate-400 block">Threshold Heart Rate</span>
                <span className="text-sm font-semibold text-rose-400">{userLactateThresholdHeartRate} bpm</span>
              </div>
              <div className="border-r border-slate-800/80">
                <span className="text-[10px] text-slate-400 block">Threshold Power</span>
                <span className="text-sm font-semibold text-amber-400">{userLactateThresholdPower} W</span>
              </div>
              <div className="border-r border-slate-800/80">
                <span className="text-[10px] text-slate-400 block">Threshold Speed</span>
                <span className="text-sm font-semibold text-emerald-400">{userLactateThresholdSpeed} km/h</span>
              </div>
            </div>
          </div>
        </div>
      </div>

    </div>
  );
};