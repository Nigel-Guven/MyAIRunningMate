import { BrowserRouter, Routes, Route } from 'react-router';
import { MainLayout } from './components/layout/MainLayout';
import { HomePage } from './pages/HomePage';
import { CalendarPage } from './pages/CalendarPage';
import { UploadPage } from './pages/UploadPage';
import { AnalyticsPage } from './pages/AnalyticsPage';
import { NexusPage } from './pages/NexusPage';
import { WeightPage } from './pages/WeightPage';


function App() {
  return (
    <BrowserRouter>
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
    </BrowserRouter>
  );
}

export default App;