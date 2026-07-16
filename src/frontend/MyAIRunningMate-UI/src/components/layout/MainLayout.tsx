import { Sidebar } from './Sidebar';

export const MainLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="flex min-h-screen bg-slate-950 text-slate-50">
      {/* Sidebar - Desktop Only for now */}
      <Sidebar />

      {/* Reduced to p-2 (8px) and md:p-4 (16px) for maximum screen usage. */}
      <main className="ml-64 flex-1 p-2 md:p-4">
        {/* max-w-7xl removed so the grid can stretch fully on ultrawide monitors */}
        <div className="w-full mx-auto">
          {children}
        </div>
      </main>
    </div>
  );
};