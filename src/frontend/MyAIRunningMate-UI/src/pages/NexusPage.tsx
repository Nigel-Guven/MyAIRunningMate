// src/pages/NexusPage.tsx
import React, { useState } from 'react';
import { nexusService } from '../services/api/nexus/nexus.service';
import type { TrainingPlanRequest } from '../services/api/nexus/nexus.types';

export const NexusPage = () => {
  const [formData, setFormData] = useState<TrainingPlanRequest>({
    primaryGoal: '5k',
    experienceYears: '1 or Less',
    runningLevel: 'Beginner',
    scheduleLengthWeeks: 4,
    poolAccess: 'None',
  });

  const [loading, setLoading] = useState(false);
  const [statusMessage, setStatusMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = e.target;
    
    setFormData((prev) => ({
      ...prev,
      
      [name]: name === 'scheduleLengthWeeks' ? parseInt(value, 10) : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setStatusMessage(null);

    try {
      const response = await nexusService.createTrainingPlan(formData);
      setStatusMessage({ type: 'success', text: response.message || 'Plan generated successfully!' });
    } catch (error) {
      setStatusMessage({ type: 'error', text: 'Failed to communicate with the architectural core. Please try again.' });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-4xl space-y-8">
      <div>
        <h2 className="text-3xl font-bold">Nexus AI Mate</h2>
        <p className="text-slate-400">Configure your autonomous training architect.</p>
      </div>

      <form onSubmit={handleSubmit} className="p-6 rounded-2xl border border-blue-500/30 bg-blue-500/5">
        <h3 className="text-xl font-bold mb-4 flex items-center gap-2">
          <span>🧠</span> AI Training Plan Requirements
        </h3>
        
        <div className="space-y-4">
          {/* Primary Goal Dropdown */}
          <div>
            <label className="block text-sm font-medium text-slate-400 mb-1">Primary Goal</label>
            <select 
              name="primaryGoal"
              value={formData.primaryGoal}
              onChange={handleChange}
              className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
            >
              <option value="5k">5k</option>
              <option value="10k">10k</option>
              <option value="Half Marathon">Half Marathon</option>
              <option value="Marathon">Marathon</option>
              <option value="General Fitness">General Fitness</option>
            </select>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {/* Running Experience Years */}
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Running Experience</label>
              <select 
                name="experienceYears"
                value={formData.experienceYears}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value="1 or Less">1 or Less years</option>
                <option value="2-3">2-3 years</option>
                <option value="4+ years">4+ years</option>
              </select>
            </div>

            {/* Running Level */}
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Running Level</label>
              <select 
                name="runningLevel"
                value={formData.runningLevel}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value="Beginner">Beginner</option>
                <option value="Intermediate">Intermediate</option>
                <option value="Advanced">Advanced</option>
                <option value="Expert">Expert</option>
              </select>
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {/* Schedule Length (Mapped directly to Integer Values) */}
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Schedule Length</label>
              <select 
                name="scheduleLengthWeeks"
                value={formData.scheduleLengthWeeks}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value={4}>4 weeks</option>
                <option value={8}>8 weeks</option>
                <option value={12}>12 weeks</option>
              </select>
            </div>

            {/* Pool Access */}
            <div>
              <label className="block text-sm font-medium text-slate-400 mb-1">Pool Access</label>
              <select 
                name="poolAccess"
                value={formData.poolAccess}
                onChange={handleChange}
                className="w-full bg-slate-800 border-slate-700 rounded-lg p-2 text-white"
              >
                <option value="None">None</option>
                <option value="25m Pool">25m Pool</option>
                <option value="50m Pool">50m Pool</option>
              </select>
            </div>
          </div>

          {/* Feedback Messages */}
          {statusMessage && (
            <div className={`p-3 rounded-lg text-sm ${statusMessage.type === 'success' ? 'bg-green-500/20 text-green-400 border border-green-500/30' : 'bg-red-500/20 text-red-400 border border-red-500/30'}`}>
              {statusMessage.text}
            </div>
          )}

          {/* Submit Button */}
          <button 
            type="submit"
            disabled={loading}
            className="w-full bg-blue-600 hover:bg-blue-500 disabled:bg-blue-800 disabled:text-slate-400 font-bold py-3 rounded-lg transition-colors mt-4 flex justify-center items-center"
          >
            {loading ? 'Processing Parameters...' : 'Generate Training Plan'}
          </button>
        </div>
      </form>
    </div>
  );
};