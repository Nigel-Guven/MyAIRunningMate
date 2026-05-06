import React, { useState } from 'react';
import { apiClient } from '../services/apiClient';
import logo from '../assets/applogo.png';

export function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      const credentials = { email, password };

      const response = await apiClient.post('session/login', credentials);
      const { token, user_id, is_strava_connected } = response.data;

      localStorage.setItem('token', token);
      localStorage.setItem('userId', user_id);
      localStorage.setItem('is_strava_connected', String(is_strava_connected));

      apiClient.defaults.headers.common['Authorization'] = `Bearer ${token}`;

      window.location.href = '/home';
    } catch (err: any) {
      console.error('Login failed:', err);
      setError(
        err.response?.data?.message || 
        'Login failed. Please check your credentials and try again.'
      );
    } finally {
      setLoading(false);
    }
  };
  
  return (
    <div className="min-h-screen bg-slate-950 flex flex-col items-center justify-center p-6">
      <div className="w-full max-w-md space-y-8 bg-slate-900 border border-slate-800 rounded-2xl p-8 shadow-xl shadow-blue-900/10">
        
        {/* Header Section */}
        <div className="flex items-center gap-4">
          <img 
            src={logo} 
            alt="App Logo" 
            className="h-16 w-16 rounded-xl object-contain bg-slate-800 p-1 shadow-md shadow-blue-900/20" 
          />
          <div>
            <h2 className="text-3xl font-black text-slate-100">AIRunningMate</h2>
            <p className="text-slate-400 text-xs tracking-widest uppercase mt-1">Command Center Access</p>
          </div>
        </div>

        {/* Error Message Box */}
        {error && (
          <div className="rounded-xl border border-red-500/20 bg-red-950/30 p-4 text-red-400 text-sm">
            {error}
          </div>
        )}

        {/* Form Section */}
        <form onSubmit={handleLogin} className="space-y-6">
          <div>
            <label className="block text-xs font-bold uppercase tracking-widest text-slate-500 mb-2">
              Email Address
            </label>
            <input 
              type="email" 
              value={email} 
              onChange={(e) => setEmail(e.target.value)} 
              required 
              placeholder="you@example.com"
              className="w-full px-4 py-3 rounded-xl bg-slate-950 border border-slate-800 text-slate-300 focus:border-blue-600 focus:outline-none focus:ring-1 focus:ring-blue-600 transition font-mono text-sm"
            />
          </div>

          <div>
            <label className="block text-xs font-bold uppercase tracking-widest text-slate-500 mb-2">
              Password
            </label>
            <input 
              type="password" 
              value={password} 
              onChange={(e) => setPassword(e.target.value)} 
              required 
              placeholder="••••••••"
              className="w-full px-4 py-3 rounded-xl bg-slate-950 border border-slate-800 text-slate-300 focus:border-blue-600 focus:outline-none focus:ring-1 focus:ring-blue-600 transition font-mono text-sm"
            />
          </div>

          <button 
            type="submit" 
            disabled={loading} 
            className="w-full py-4 px-6 text-white font-bold rounded-xl bg-gradient-to-br from-blue-600 to-blue-800 hover:from-blue-700 hover:to-blue-900 shadow-lg shadow-blue-900/20 transition-all duration-200 flex items-center justify-center border border-blue-400/10"
          >
            {loading ? (
              <span className="flex items-center gap-2">
                <svg className="animate-spin h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                  <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                  <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                </svg>
                Signing in...
              </span>
            ) : (
              'Login'
            )}
          </button>
        </form>

      </div>
    </div>
  );
}