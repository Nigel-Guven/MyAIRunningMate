import { BrowserRouter, Routes, Route, Navigate, Outlet } from 'react-router';
import { MainLayout } from './components/layout/MainLayout';
import { DashboardPage } from './pages/DashboardPage';
import { CalendarPage } from './pages/CalendarPage';
import { UploadPage } from './pages/UploadPage';
import { AnalyticsPage } from './pages/AnalyticsPage';
import { NexusPage } from './pages/NexusPage';
import { WeightPage } from './pages/WeightPage';
import { LoginPage } from './pages/LoginPage';
import { ActivityDeepDivePage } from './pages/ActivityDeepDivePage';

const isAuthenticated = () => {
  return !!localStorage.getItem('token');
};

// Use <Outlet /> to render child routes nicely inside the layout
function ProtectedLayout() {
  if (!isAuthenticated()) {
    // Redirect instantly to login if the token is missing
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