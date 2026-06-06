import { useEffect, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import type { AnalyticsDashboardView } from '../types/views/analyticsView';
import { analyticsService } from '../services/api/analytics/analytics.service';

export const AnalyticsPage = () => {
  const [analytics, setAnalytics] = useState<AnalyticsDashboardView | null>(null);
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

  // Fallback if data array is missing or empty
  const summary = analytics?.summary;
  const chartData = (analytics?.year_weekly_volume || []).map((week, index) => ({
    name: `Wk ${index + 1}`,
    Running: parseFloat((week.total_running_distance_metres / 1000).toFixed(2)),
    Swimming: week.total_swimming_distance_metres, // kept in meters for chart legibility
  }));

  const statCards = [
    { 
      label: 'Yearly Running Distance', 
      value: summary?.year_running_distance ? `${(summary.year_running_distance / 1000).toFixed(1)} km` : '0 km', 
      icon: '🛣️' 
    },
    { 
      label: 'Yearly Swimming Distance', 
      value: summary?.year_swimming_distance ? `${summary.year_swimming_distance.toLocaleString()} m` : '0 m', 
      icon: '🏊' 
    },
    { 
      label: 'Active Days', 
      value: summary?.year_active_days.toString() || '0', 
      icon: '🔥' 
    },
    { 
      label: 'Avg Training Effect', 
      value: summary?.year_average_training_effect ? summary.year_average_training_effect.toFixed(1) : '0.0', 
      icon: '📈' 
    },
    { 
      label: 'Total Training Effect', 
      value: summary?.year_total_training_effect ? summary.year_total_training_effect.toFixed(1) : '0.0', 
      icon: '📊' 
    },
  ];

  return (
    <div className="space-y-8 p-6 max-w-7xl mx-auto text-slate-100">
      <div className="flex justify-between items-center">
        <h2 className="text-3xl font-bold tracking-tight bg-gradient-to-r from-indigo-400 to-cyan-400 bg-clip-text text-transparent">
          Analytics Vault
        </h2>
        <select 
          value={selectedYear} 
          onChange={(e) => setSelectedYear(Number(e.target.value))}
          className="bg-slate-900 border border-slate-800 text-slate-300 px-4 py-2 rounded-lg outline-none focus:border-indigo-500"
        >
          <option value={2026}>2026</option>
          <option value={2025}>2025</option>
          <option value={2024}>2024</option>
        </select>
      </div>
      
      {/* Dynamic Summary Cards */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4">
        {statCards.map(stat => (
          <div key={stat.label} className="p-5 rounded-xl bg-slate-900 border border-slate-800 hover:border-slate-700 transition-colors">
            <div className="flex justify-between items-start">
              <p className="text-slate-500 text-xs font-bold uppercase tracking-wider">{stat.label}</p>
              <span className="text-lg">{stat.icon}</span>
            </div>
            <p className="text-2xl font-bold mt-2 text-slate-100">{stat.value}</p>
          </div>
        ))}
      </div>

      {/* Recharts Weekly Volume Visualization */}
      <div className="p-6 rounded-xl bg-slate-900 border border-slate-800">
        <div className="mb-4">
          <h3 className="text-lg font-semibold text-slate-200">Weekly Training Volumes</h3>
          <p className="text-slate-500 text-xs">Comparing historical running loads (km) and pool intervals (m)</p>
        </div>
        <div className="h-80 w-full">
          {chartData.length === 0 ? (
            <div className="h-full w-full flex items-center justify-center text-slate-500 italic">
              No training sessions recorded for this timeframe.
            </div>
          ) : (
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={chartData} margin={{ top: 10, right: 10, left: -10, bottom: 0 }}>
                <CartesianGrid strokeDasharray="3 3" stroke="#1e293b" vertical={false} />
                <XAxis dataKey="name" stroke="#64748b" fontSize={12} tickLine={false} />
                
                {/* Primary Axis: Running (km) */}
                <YAxis yAxisId="left" stroke="#487500" fontSize={12} tickLine={false} axisLine={false} unit="k" />
                {/* Secondary Axis: Swimming (m) */}
                <YAxis yAxisId="right" orientation="right" stroke="#0a16c2" fontSize={12} tickLine={false} axisLine={false} unit="m" />
                
                <Tooltip 
                  contentStyle={{ backgroundColor: '#0f172a', borderColor: '#334155', borderRadius: '8px' }}
                  labelStyle={{ color: '#94a3b8', fontWeight: 'bold' }}
                />
                <Legend verticalAlign="top" height={36} iconType="circle" />
                
                <Bar yAxisId="left" dataKey="Running" fill="#6366f1" radius={[4, 4, 0, 0]} name="Running (km)" />
                <Bar yAxisId="right" dataKey="Swimming" fill="#06b6d4" radius={[4, 4, 0, 0]} name="Swimming (m)" />
              </BarChart>
            </ResponsiveContainer>
          )}
        </div>
      </div>

      {/* GitHub Style Heatmap Grid Placeholder */}
      <div className="h-48 w-full rounded-xl bg-slate-900 border border-slate-800 flex items-center justify-center text-slate-500 text-sm italic">
        [Yearly Activity Heatmap Grid coming next: Connecting address maps and active day counts]
      </div>
    </div>
  );
};