interface LoginHeaderProps {
  logo: string;
}

export function LoginHeader({ logo }: LoginHeaderProps) {
  return (
    <div className="flex items-center gap-4">
      <img
        src={logo}
        alt="App Logo"
        className="h-16 w-16 rounded-xl object-contain bg-slate-800 p-1 shadow-md shadow-blue-900/20"
      />
      <div>
        <h2 className="text-3xl font-black text-slate-100">
          AIRunningMate
        </h2>
        <p className="text-slate-400 text-xs tracking-widest uppercase mt-1">
          Command Center Access
        </p>
      </div>
    </div>
  );
}