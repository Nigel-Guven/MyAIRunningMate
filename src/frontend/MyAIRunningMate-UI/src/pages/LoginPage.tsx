import React, { useState } from 'react';
import { useNavigate } from 'react-router';
import logo from '../assets/applogo.png';
import { loginService } from '../services/api/login/login.service';
import { LoginHeader } from '../components/login/LoginHeader';
import { InputField } from '../components/login/InputField';

export function LoginPage() {
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      await loginService.login({ email, password });
      
      setTimeout(() => {
        navigate('/home');
      }, 100);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Login failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-slate-950 flex flex-col items-center justify-center p-6">
      <div className="w-full max-w-md space-y-8 bg-slate-900 border border-slate-800 rounded-2xl p-8 shadow-xl shadow-blue-900/10">
        
        {/* Header Component */}
        <LoginHeader logo={logo} />

        {/* Error Notification */}
        {error && (
          <div className="rounded-xl border border-red-500/20 bg-red-950/30 p-4 text-red-400 text-sm">
            {error}
          </div>
        )}

        {/* Form Container */}
        <form onSubmit={handleLogin} className="space-y-6">
          <InputField
            label="Email Address"
            type="email"
            value={email}
            placeholder="you@example.com"
            onChange={setEmail}
          />

          <InputField
            label="Password"
            type="password"
            value={password}
            placeholder="••••••••"
            onChange={setPassword}
          />

          <button
            type="submit"
            disabled={loading}
            className="w-full py-4 px-6 text-white font-bold rounded-xl bg-gradient-to-br from-blue-600 to-blue-800 hover:from-blue-700 hover:to-blue-900 shadow-lg shadow-blue-900/20 transition-all duration-200 flex items-center justify-center border border-blue-400/10 disabled:opacity-50"
          >
            {loading ? 'Signing in...' : 'Login'}
          </button>
        </form>
      </div>
    </div>
  );
}