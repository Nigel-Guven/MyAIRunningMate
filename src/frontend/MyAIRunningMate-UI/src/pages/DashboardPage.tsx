import { useState, useEffect } from 'react';
import { DEFAULT_WEEKLY_INSIGHTS, type WeeklyInsightsResponse } from '../types/dashboard/weeklyInsightsResponse';
import { dashboardService, type InitialDashboardData } from '../services/api/dashboard/dashboard.service';
import { authStorage } from '../services/api/config/authStorage';
import { DashboardHeader } from '../components/dashboard/DashboardHeader';
import { PrimaryEventHero } from '../components/dashboard/PrimaryEventHero';
import { WeeklyVolumeCard } from '../components/dashboard/WeeklyVolumeCard';
import { PersonalRecordsCard } from '../components/dashboard/PersonalRecordsCard';
import UpcomingSchedule from '../components/dashboard/UpcomingEventsCard';
import AIMateCard from '../components/dashboard/AIMateCard';
import type { EventViewResponse } from '../types/dashboard/eventViewResponse';
import type { BestEffortResponse } from '../types/dashboard/bestEffortResponse';

const initialStaticState: InitialDashboardData = {
  primaryEvent: null,
  upcomingEvents: [] as EventViewResponse[],
  bestEfforts: [] as BestEffortResponse[],
  latestWeight: null,
};

export const DashboardPage = () => {

  const [staticData, setStaticData] = useState<InitialDashboardData>(initialStaticState);
  const [weeklyInsights, setWeeklyInsights] = useState<WeeklyInsightsResponse | null>(null);

  const [loadingInitial, setLoadingInitial] = useState(true);
  const [loadingInsights, setLoadingInsights] = useState(false);
  const [error, setError] = useState<string | null>(null);
  
  const [weekOffset, setWeekOffset] = useState(0);

  useEffect(() => {
    const initDashboard = async () => {
      const localToken = authStorage.get();

      if (!localToken) {
        setError("Session initializing. Please wait...");
        return;
      }

      try {
        setLoadingInitial(true);
        const data = await dashboardService.loadInitialDashboard();
        setStaticData(data);
      } catch {
        setError("Failed to load command center.");
      } finally {
        setLoadingInitial(false);
      }
    };

    initDashboard();
  }, []);

  useEffect(() => {
    const fetchInsights = async () => {
      const localToken = authStorage.get();
      if (!localToken) return;

      try {
        setLoadingInsights(true);
        const insights = await dashboardService.getWeeklyInsights(weekOffset);
        setWeeklyInsights(insights);
      } catch (err) {
        console.error("Failed to fetch insights:", err);
      } finally {
        setLoadingInsights(false);
      }
    };

    fetchInsights();
  }, [weekOffset]);

  if (loadingInitial) {
    return (
      <div className="p-12 text-slate-500 font-mono animate-pulse uppercase">
        Synchronizing Command Center...
      </div>
    );
  }

  if (error) return <div className="p-12 text-red-400">{error}</div>;

  const { primaryEvent, upcomingEvents, bestEfforts, latestWeight } = staticData;
  const insights = weeklyInsights || DEFAULT_WEEKLY_INSIGHTS;

  return (
    <div className="space-y-8 animate-in fade-in duration-700 pb-12">
      <DashboardHeader latestWeight={latestWeight} />
      
      <PrimaryEventHero primaryEvent={primaryEvent} />

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <div className="relative">
          {/* Subtle loading overlay for the Weekly Card when shifting weeks */}
          {loadingInsights && (
            <div className="absolute inset-0 bg-slate-950/20 backdrop-blur-[1px] rounded-3xl z-10 flex items-center justify-center">
              <span className="text-[10px] font-mono text-slate-400 uppercase animate-pulse">
                Updating Week...
              </span>
            </div>
          )}
          <WeeklyVolumeCard
            insights={insights}
            weekLabel={dashboardService.getWeekLabel(weekOffset)}
            onPreviousWeek={() => setWeekOffset(o => o - 1)}
            onNextWeek={() => setWeekOffset(o => Math.min(o + 1, 0))}
            canGoNext={weekOffset < 0}
          />
        </div>

        {/* AI Mate Card */}
        <AIMateCard message="This is a sample AI suggestion." status="Thinking..." />

        {/* Upcoming Schedule Card */}
        <div className="w-full max-w-sm">
          <UpcomingSchedule upcomingEvents={upcomingEvents} />
        </div>
      </div>

      <PersonalRecordsCard bestEfforts={bestEfforts} />
    </div>
  );
};