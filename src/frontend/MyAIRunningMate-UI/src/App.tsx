import { BrowserRouter, Routes, Route, Navigate } from 'react-router';
import { MainLayout } from './components/layout/MainLayout';
import { HomePage } from './pages/HomePage';
import { CalendarPage } from './pages/CalendarPage';
import { UploadPage } from './pages/UploadPage';
import { AnalyticsPage } from './pages/AnalyticsPage';
import { NexusPage } from './pages/NexusPage';
import { WeightPage } from './pages/WeightPage';
import { LoginPage } from './pages/LoginPage';

const isAuthenticated = () => {
  return !!localStorage.getItem('token');
};

function ProtectedLayout() {
  if (!isAuthenticated()) {
    return <Navigate to="/login" replace />;
  }

  return (
    <MainLayout>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/calendar" element={<CalendarPage />} />
        <Route path="/upload" element={<UploadPage />} />
        <Route path="/goals" element={<AnalyticsPage />} />
        <Route path="/nexus" element={<NexusPage />} />
        <Route path="/weight" element={<WeightPage />} />
      </Routes>
    </MainLayout>
  );
}

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/*" element={<ProtectedLayout />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;