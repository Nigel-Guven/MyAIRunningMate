import logo from '../../assets/applogo.png';

export const DashboardHeader = () => {
  return (
    <div className="flex justify-between items-center border-b border-slate-800 pb-6">
      {/* Left Side: Logo and Greeting */}
      <div className="flex items-center gap-4">
        <img src={logo} alt="Logo" className="h-14 w-14 rounded-xl shadow-lg shadow-blue-900/20" />
        <div>
          <h2 className="text-3xl font-black tracking-tighter uppercase italic">Command Center</h2>
          <p className="text-slate-400 font-medium">Welcome back, Nigel.</p>
        </div>
      </div>
    </div>
  );
};