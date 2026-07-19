import { useEffect, useState } from 'react';
import { analyticsService } from '../services/api/analytics/analytics.service';
import logo from '../assets/applogo.png';
import type { AnalyticsViewResponse } from '../types/analytics/analyticsViewResponse';
import { formatTime } from '../services/helpers/formatTime';
import { AnalyticsPageHeader } from '../components/analytics/AnalyticsPageHeader';
import { AnalyticsSummaryCard } from '../components/analytics/AnalyticsSummaryCard';
import { AnalyticsTrendGraph } from '../components/analytics/AnalyticsTrendGraph';

export const AnalyticsPage = () => {
  const [analytics, setAnalytics] = useState<AnalyticsViewResponse | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [selectedYear, setSelectedYear] = useState<number>(new Date().getFullYear());

  useEffect(() => {
    async function fetchDashboard() {
      try {
        setLoading(true);
        const data = await analyticsService.getDashboardData(selectedYear);
        setAnalytics(data);
      } catch (err) {
        console.error("Could not load dashboard data", err);
      } finally {
        setLoading(false);
      }
    }
    fetchDashboard();
  }, [selectedYear]);

  if (loading) {
    return (
      <div className="h-96 w-full flex items-center justify-center text-slate-400">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-indigo-500 mr-3"></div>
        Parsing training blocks...
      </div>
    );
  }

  const summary = analytics?.yearly_statistics;
  const normalizeSeries = (values: number[]) => {
    const min = Math.min(...values);
    const max = Math.max(...values);

    if (max === min) {
      return values.map(() => 50);
    }

    return values.map((v) => ((v - min) / (max - min)) * 100);
  };

  const trendData = analytics?.yearly_analytics;

  const normalized = {
    vo2: normalizeSeries(
      trendData?.oxygen_max_trends ?? []
    ),
    hr: normalizeSeries(
      trendData?.lactate_threshold_heart_rate_trends ?? []
    ),
    power: normalizeSeries(
      trendData?.lactate_threshold_power_trends ?? []
    ),
    speed: normalizeSeries(
      trendData?.lactate_threshold_speed_trends ?? []
    ),
    percent: normalizeSeries(
      trendData?.lactate_threshold_percentage_rates ?? []
    )
  };

  const chartData =
    trendData?.oxygen_max_trends.map((_, index) => ({
      month: index + 1,

      vo2: normalized.vo2[index],
      hr: normalized.hr[index],
      power: normalized.power[index],
      speed: normalized.speed[index],
      percent: normalized.percent[index],

      rawVo2: trendData.oxygen_max_trends[index],
      rawHr: trendData.lactate_threshold_heart_rate_trends[index],
      rawPower: trendData.lactate_threshold_power_trends[index],
      rawSpeed: trendData.lactate_threshold_speed_trends[index],
      rawPercent: trendData.lactate_threshold_percentage_rates[index],
    })) ?? [];

  const statCards = [
    { 
      label: 'Yearly Running Distance', 
      value: summary?.yearly_running_distance ? `${(summary.yearly_running_distance / 1000).toFixed(0)} kilometres` : '0 kilometres', 
      icon: '🛣️' 
    },
    { 
      label: 'Yearly Swimming Distance', 
      value: summary?.yearly_swimming_distance ? `${(summary.yearly_swimming_distance).toFixed(0)} metres` : '0 metres', 
      icon: '🏊' 
    },
    { 
      label: 'Active Days', 
      value: `${summary?.yearly_active_days} days` || '0 days', 
      icon: '🔥' 
    },
    { 
      label: 'Total Time Spent Active', 
      value: formatTime(summary?.yearly_time_spent_active), 
      icon: '' 
    },
    { 
      label: 'Yearly Body Batteries Used', 
      value: summary?.yearly_body_batteries_used ? summary.yearly_body_batteries_used /10 : '0.0', 
      icon: '🔋' 
    },
  ];

  return (
    <div className="space-y-8 p-6 max-w-7xl mx-auto text-slate-100">
      {/* 1. Header Area */}
      <AnalyticsPageHeader 
        logo={logo} 
        selectedYear={selectedYear} 
        setSelectedYear={setSelectedYear} 
      />
        

      {/* Dynamic Summary Cards */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4">
        {statCards.map((stat) => (
          <AnalyticsSummaryCard 
            key={stat.label} 
            label={stat.label}
            value={stat.value}
            icon={stat.icon}
          />
        ))}
      </div>

      {/* Recharts Yearly Trends */}
      <AnalyticsTrendGraph data={chartData} />
    </div>
  );
};