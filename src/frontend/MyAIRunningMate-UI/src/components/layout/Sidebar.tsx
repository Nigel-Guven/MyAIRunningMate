import { NavLink } from 'react-router';

const menuItems = [
  { label: 'Command Center', href: '/', icon: '🏠' },
  { label: 'Activity Matrix', href: '/calendar', icon: '📅' },
  { label: 'Ingestion Lab', href: '/upload', icon: '📤' },
  { label: 'Analytics Vault', href: '/goals', icon: '🎯' },
  { label: 'Nexus AI Mate', href: '/nexus', icon: '🎯' },
];

export const Sidebar = () => {
  return (
    <aside className="fixed left-0 top-0 h-screen w-64 border-r border-slate-800 bg-slate-900 p-4 text-slate-300">
      <div className="mb-8 px-2 py-4">
        <h1 className="text-xl font-bold text-blue-400 italic">AIRunningMate</h1>
      </div>
      
      <nav className="space-y-2">
        {menuItems.map((item) => (
          <NavLink
            key={item.href}
            to={item.href} // Use 'to' instead of 'href'
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

    </aside>
  );
}