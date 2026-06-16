import { BrowserRouter, Routes, Route, Navigate, Outlet, useLocation } from 'react-router';
import { MainLayout } from './components/layout/MainLayout';
import { DashboardPage } from './pages/DashboardPage';
import { CalendarPage } from './pages/CalendarPage';
import { UploadPage } from './pages/UploadPage';
import { AnalyticsPage } from './pages/AnalyticsPage';
import { NexusPage } from './pages/NexusPage';
import { WeightPage } from './pages/WeightPage';
import { LoginPage } from './pages/LoginPage';
import { ActivityDeepDivePage } from './pages/ActivityDeepDivePage';
import { authStorage } from './services/api/config/authStorage';
import { useEffect, useState } from 'react';


function ProtectedLayout() {
  const [token, setToken] = useState<string | null>(() => authStorage.get());

  useEffect(() => {
    const handleAuthChange = () => {
      setToken(authStorage.get());
    };

    // Listen for the custom auth events
    window.addEventListener('auth-change', handleAuthChange);
    
    return () => {
      window.removeEventListener('auth-change', handleAuthChange);
    };
  }, []);

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return (
    <MainLayout>
      <Outlet />
    </MainLayout>
  );
}

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Public Route */}
        <Route path="/login" element={<LoginPage />} />

        {/* Protected Routes */}
        <Route element={<ProtectedLayout />}>
          {/* Handle the root path "/" */}
          {/* If logged in, automatically forward them to /home */}
          <Route path="/" element={<Navigate to="/home" replace />} />
          
          <Route path="/home" element={<DashboardPage />} />
          <Route path="/calendar" element={<CalendarPage />} />
          <Route path="/activity/:id" element={<ActivityDeepDivePage />} />
          <Route path="/upload" element={<UploadPage />} />
          <Route path="/goals" element={<AnalyticsPage />} />
          <Route path="/nexus" element={<NexusPage />} />
          <Route path="/weight" element={<WeightPage />} />
        </Route>

        {/* Catch-all: optionally redirect any 404s back to root */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;