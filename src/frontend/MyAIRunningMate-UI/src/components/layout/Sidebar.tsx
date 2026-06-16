import { NavLink, useNavigate } from 'react-router';
import appLogo from '../../assets/applogo.png';

const menuItems = [
  { label: 'Command Center', href: '/home', icon: '🏠' },
  { label: 'Activity Matrix', href: '/calendar', icon: '📅' },
  { label: 'Ingestion Lab', href: '/upload', icon: '📤' },
  { label: 'Analytics Vault', href: '/goals', icon: '🎯' },
  { label: 'Nexus AI Mate', href: '/nexus', icon: '🎯' },
  { label: 'Weight Tracker', href: '/weight', icon: '🎯' },
];

export const Sidebar = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');

    navigate('/login');
  };

  return (
    <aside className="fixed left-0 top-0 h-screen w-64 border-r border-slate-800 bg-slate-900 p-4 text-slate-300 flex flex-col justify-between">
      {/* Top Section: Logo & Navigation Links */}
      <div>
        <div className="mb-8 px-2 py-4 flex items-center gap-3">
          <img 
            src={appLogo} 
            alt="App Logo" 
            className="h-10 w-10 object-contain" 
          />
          <h1 className="text-xl font-bold text-blue-400 italic">AIRunningMate</h1>
        </div>
        
        <nav className="space-y-2">
          {menuItems.map((item) => (
            <NavLink
              key={item.href}
              to={item.href}
              className={({ isActive }) => `
                flex items-center gap-3 rounded-lg px-3 py-2 transition-all
                ${isActive 
                  ? 'bg-blue-600 text-white shadow-lg shadow-blue-900/20' 
                  : 'hover:bg-slate-800 hover:text-white text-slate-400'}
              `}
            >
              <span>{item.icon}</span>
              <span className="font-medium">{item.label}</span>
            </NavLink>
          ))}
        </nav>
      </div>

      {/* Bottom Section: Logout Button */}
      <div>
        <button
          onClick={handleLogout}
          className="w-full flex items-center justify-center gap-2 rounded-lg bg-red-600 px-3 py-2 text-sm font-medium text-white transition-all hover:bg-red-700"
        >
          Logout
        </button>
      </div>
    </aside>
  );
};