import { useState, useEffect, useCallback } from 'react';
import { dashboardService } from '../services/api/dashboard/dashboard.service';
import { authStorage } from '../services/api/config/authStorage';
import type { DashboardTypes } from '../types/dashboard/dashboard.types';

export const useDashboard = () => {
  const [dashboard, setDashboard] = useState<DashboardTypes | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const refresh = useCallback(async () => {
    try {
      const data = await dashboardService.loadDashboard();
      setDashboard(data);
      setError(null);
    } catch (err) {
      setError('Failed to refresh data.');
      throw err;
    }
  }, []);

  useEffect(() => {
    let isMounted = true;
    const init = async () => {
      setLoading(true);
      if (!authStorage.get()) {
        setError('Session initializing...');
        setLoading(false);
        return;
      }
      try {
        const data = await dashboardService.loadDashboard();
        if (isMounted) setDashboard(data);
      } catch (err) {
        if (isMounted) setError('Failed to load dashboard.');
      } finally {
        if (isMounted) setLoading(false);
      }
    };
    init();
    return () => { isMounted = false; };
  }, [refresh]);

  return { dashboard, loading, error, refresh };
};