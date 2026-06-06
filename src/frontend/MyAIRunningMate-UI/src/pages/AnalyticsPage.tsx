import { useEffect, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import type { AnalyticsDashboardView } from '../types/views/analyticsView';
import { analyticsService } from '../services/api/analytics/analytics.service';
import { COUNTY_COLORS } from '../components/analytics/irishCountyConfig';
import { CountyFlag } from '../components/analytics/customflags';

// 1. Define a clear structure for your location data
interface LocationMetric {
  address: string;
  country: 
    'Ireland' | 
    'Northern Ireland' | 
    'Scotland' |
    'Wales' | 
    'Germany' |
    'Spain' | 
    'Malta' |
    'Montenegro' |
    string; 
}

const ROUTE_LOCATION_METRICS: LocationMetric[] = [
  { address: "Howth, Dublin, Ireland", country: "Ireland" },
  { address: "Ashtown, Dublin, Ireland", country: "Ireland" },
  { address: "Tiknock, Dublin, Ireland", country: "Ireland" },
  { address: "Bray, Wicklow, Ireland", country: "Ireland" },
  { address: "Sallins, Kildare, Ireland", country: "Ireland" },
  { address: "Leixlip, Kildare, Ireland", country: "Ireland" },
  { address: "Lugnaquilla, Wicklow, Ireland", country: "Ireland" },
  { address: "Mullaghcleevaun, Wicklow, Ireland", country: "Ireland" },
  { address: "Blackstairs Mountain, Wexford, Ireland", country: "Ireland" },
  { address: "Cornahaltie, Fermanagh, Northern Ireland", country: "Northern Ireland" },
  { address: "Camaderry, Wicklow, Ireland", country: "Ireland" },
  { address: "Boyne Ramparts, Meath, Ireland", country: "Ireland" },
  { address: "Killary, Galway, Ireland", country: "Ireland" },
  { address: "Cuilcagh, Cavan, Ireland", country: "Ireland" },
  { address: "Scarr, Wicklow, Ireland", country: "Ireland" },
  { address: "Tonelagee, Wicklow, Ireland", country: "Ireland" },
  { address: "Luggala, Wicklow, Ireland", country: "Ireland" },
  { address: "Slieve Foye, Louth, Ireland", country: "Ireland" },
  { address: "Errigal, Donegal, Ireland", country: "Ireland" },
  { address: "Croagh Patrick, Mayo, Ireland", country: "Ireland" },
  { address: "Galtymore, Tipperary, Ireland", country: "Ireland" },
  { address: "Great Sugar Loaf, Wicklow, Ireland", country: "Ireland" },
  { address: "Prince Williams Seat, Dublin, Ireland", country: "Ireland" },
  { address: "Ben Nevis, Grampian, Scotland", country: "Scotland" },
  { address: "Snowdon, Gwynedd, Wales", country: "Wales" },
  { address: "Slieve Gullion, Down, Northern Ireland", country: "Northern Ireland" },
  { address: "Slieve Donard, Down, Northern Ireland", country: "Northern Ireland" },
  { address: "Carrauntoohil, Kerry, Ireland", country: "Ireland" },
  { address: "Raheny, Dublin, Ireland", country: "Ireland" },
  { address: "Mondello Park, Kildare, Ireland", country: "Ireland" },
  { address: "Whitehall, Dublin, Ireland", country: "Ireland" },
  { address: "North Circular Road, Dublin, Ireland", country: "Ireland" },
  { address: "Clongriffin, Dublin, Ireland", country: "Ireland" },
  { address: "Clontarf, Dublin, Ireland", country: "Ireland" },
  { address: "Altstadt, Bremen, Germany", country: "Germany" },
  { address: "Calle Menorca, Malaga, Spain", country: "Spain" },
  { address: "Cabra, Dublin, Ireland", country: "Ireland" },
  { address: "Beaumont, Dublin, Ireland", country: "Ireland" },
  { address: "Phibsborough, Dublin, Ireland", country: "Ireland" },
  { address: "Custom House Quay, Dublin, Ireland", country: "Ireland" },
  { address: "Phoenix Park, Dublin, Ireland", country: "Ireland" },
  { address: "Artane, Dublin, Ireland", country: "Ireland" },
  { address: "Slovenska Plaza, Budva, Montenegro", country: "Montenegro" },
  { address: "Becici Beach, Becici, Montenegro", country: "Montenegro" },
  { address: "Kamp Begovic, Veslo, Montenegro", country: "Montenegro" },
  { address: "Qawra Beach, Bugibba, Malta", country: "Malta" },
  { address: "Mistra Bay, Mistra, Malta", country: "Malta" },
  { address: "Playa de Huelin, Malaga, Spain", country: "Spain" },
];

const COUNTRY_ASSETS: Record<string, { flagUrl: string; badgeText: string }> = {
  "Ireland": { flagUrl: "https://flagcdn.com/w40/ie.png", badgeText: "IE Ireland" },
  "Northern Ireland": { flagUrl: "https://flagcdn.com/w40/gb-nir.png", badgeText: "NI Northern Ireland" },
  "Scotland": { flagUrl: "https://flagcdn.com/w40/gb-sct.png", badgeText: "SC Scotland" },
  "Wales": { flagUrl: "https://flagcdn.com/w40/gb-wls.png", badgeText: "WA Wales" },
  "Germany": { flagUrl: "https://flagcdn.com/w40/de.png", badgeText: "DE Germany" },
  "Spain": { flagUrl: "https://flagcdn.com/w40/es.png", badgeText: "ES Spain" },
  "Malta": { flagUrl: "https://flagcdn.com/w40/mt.png", badgeText: "MT Malta" },
  "Montenegro": { flagUrl: "https://flagcdn.com/w40/me.png", badgeText: "ME Montenegro" }
};

const getCountryMeta = (country: string) => {
  return COUNTRY_ASSETS[country] || {
    flagUrl: "https://flagcdn.com/w40/un.png",
    badgeText: `📍 ${country}`
  };
};

export const AnalyticsPage = () => {
  const [analytics, setAnalytics] = useState<AnalyticsDashboardView | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [selectedYear, setSelectedYear] = useState<number>(new Date().getFullYear());

  const uniqueCountries = Array.from(new Set(ROUTE_LOCATION_METRICS.map((item) => item.country)));

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

  const summary = analytics?.summary;
  const chartData = (analytics?.year_weekly_volume || []).map((week, index) => ({
    name: `Wk ${index + 1}`,
    Running: parseFloat((week.total_running_distance_metres / 1000).toFixed(2)),
    Swimming: week.total_swimming_distance_metres,
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
                <YAxis yAxisId="left" stroke="#487500" fontSize={12} tickLine={false} axisLine={false} unit="k" />
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

      <h2 className="text-3xl font-bold tracking-tight bg-gradient-to-r from-indigo-400 to-cyan-400 bg-clip-text text-transparent">
        My Running Passport
      </h2>

      {/* Geographic Analytics Segment */}
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        
        {/* Left: Address Registry */}
        <div className="lg:col-span-2 p-6 rounded-xl bg-slate-900 border border-slate-800 flex flex-col justify-between">
          <div>
            <h3 className="text-lg font-semibold text-slate-200 mb-1">Decoded Segment Locations</h3>
            <p className="text-slate-500 text-xs mb-4">Historical starting coordinates cross-referenced against global route maps</p>
          </div>
          
          <div className="grid grid-cols-12 px-3 py-2 text-[11px] font-bold uppercase tracking-wider text-slate-500 border-b border-slate-800/80 mb-2">
            <div className="col-span-12">Location</div>
          </div>

          <div className="overflow-y-auto max-h-64 space-y-1.5 pr-2 scrollbar-thin scrollbar-thumb-slate-800">
            {ROUTE_LOCATION_METRICS.map((item, idx) => {
              // 1. Check if the address contains a known county from your config keys
              const countyKey = Object.keys(COUNTY_COLORS).find(county => item.address.includes(county));
              const countyConfig = countyKey ? COUNTY_COLORS[countyKey] : null;

              // 2. Safely get the base country metadata
              const countryMeta = getCountryMeta(item.country);

              return (
                <div 
                  key={idx} 
                  className="grid grid-cols-12 items-center px-3 py-2.5 rounded-lg bg-slate-950/60 border border-slate-800/50 hover:border-slate-700/80 transition-all text-xs"
                >
                  <div className="col-span-12 flex items-center space-x-3 truncate">
                    <div className="flex items-center space-x-1.5 flex-shrink-0">
                      {/* Always display the primary country flag */}
                      <img 
                        src={countryMeta.flagUrl} 
                        alt="" 
                        className="w-6 h-4 object-cover rounded-sm border border-slate-800/60 shadow-sm"
                        loading="lazy"
                      />
                      
                      {/* If it's a known Irish/NI county, append the county colors right beside it */}
                      {countyConfig && (
                        <CountyFlag 
                          primaryColor={countyConfig.primaryColor}
                          secondaryColor={countyConfig.secondaryColor}
                          layout={countyConfig.layout}
                        />
                      )}
                    </div>

                    <span className="text-slate-300 font-medium truncate">{item.address}</span>
                  </div>
                </div>
              );
            })}
          </div>
        </div>

        {/* Right: Aggregated Achievements */}
        <div className="p-6 rounded-xl bg-slate-900 border border-slate-800 flex flex-col justify-between space-y-6">
          <div>
            <h3 className="text-lg font-semibold text-slate-200 mb-1">Global Passport Overview</h3>
            <p className="text-slate-500 text-xs">Accumulated performance tallies over geographic milestones</p>
          </div>

          <div className="grid grid-cols-2 gap-4 flex-grow items-center">
            <div className="p-4 rounded-xl bg-gradient-to-br from-indigo-950/40 to-slate-950 border border-indigo-900/30 flex flex-col justify-between h-24">
              <span className="text-slate-500 text-[10px] font-bold uppercase tracking-wider">Regions Tracked</span>
              <div className="flex items-baseline space-x-2">
                <span className="text-3xl font-extrabold text-indigo-400">{uniqueCountries.length}</span>
                <span className="text-sm" title="Active territories">🌍</span>
              </div>
            </div>

            <div className="p-4 rounded-xl bg-slate-950 border border-slate-800 flex flex-col justify-between h-24">
              <span className="text-slate-500 text-[10px] font-bold uppercase tracking-wider">Total Segment PRs</span>
              <span className="text-3xl font-extrabold text-cyan-400 font-mono">1</span>
            </div>

            <div className="p-4 rounded-xl bg-slate-950 border border-slate-800 flex flex-col justify-between h-24">
              <span className="text-slate-500 text-[10px] font-bold uppercase tracking-wider">Segment Trophies</span>
              <span className="text-3xl font-extrabold text-amber-500 font-mono">1</span>
            </div>

            <div className="p-4 rounded-xl bg-slate-950 border border-slate-800 flex flex-col justify-between h-24">
              <span className="text-slate-500 text-[10px] font-bold uppercase tracking-wider">Received Kudos</span>
              <span className="text-3xl font-extrabold text-orange-400 font-mono">1</span>
            </div>
          </div>

          {/* Active Territories List */}
          <div className="pt-2 border-t border-slate-800/60">
            <span className="text-[10px] text-slate-500 font-bold uppercase tracking-wider block mb-2">Footprint Records</span>
            <div className="flex flex-wrap gap-1.5">
              {uniqueCountries.map((country) => (
                <span key={country} className="px-2 py-0.5 rounded text-[11px] bg-slate-950 border border-slate-800 text-slate-400">
                  {getCountryMeta(country).badgeText}
                </span>
              ))}
            </div>
          </div>

        </div>
      </div>
    </div>
  );
};