import React, { useEffect, useState } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import axios from 'axios';
import type { WeightEntry } from '../types/weight';


export const WeightPage = () => {
  const [weights, setWeights] = useState<WeightEntry[]>([]);
  const [newWeight, setNewWeight] = useState('');
  const userId = "00000000-0000-0000-0000-000000000000"; // Your hardcoded test ID

  const fetchWeights = async () => {
    try {
      const response = await axios.get(`http://localhost:7001/api/weight/history?userId=${userId}`);
      // Reverse the array so the oldest is on the left of the chart, newest on the right
      setWeights(response.data.reverse());
    } catch (err) {
      console.error("Error fetching weight data", err);
    }
  };

  useEffect(() => { fetchWeights(); }, []);

  const handleLogWeight = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await axios.post('http://localhost:7001/api/weight/log', {
        weight_pounds: parseFloat(newWeight),
        user_id: userId,
        created_at: new Date().toISOString()
      });
      setNewWeight('');
      fetchWeights(); // Refresh the list and chart
    } catch (err) {
      alert("Failed to log weight");
    }
  };

  const latestWeight = weights.length > 0 ? weights[weights.length - 1].weight_pounds : '--';

  return (
    <div style={{ padding: '2rem', maxWidth: '800px', margin: '0 auto' }}>
      <h2>Weight Tracking</h2>

      {/* Hero Stat */}
      <div style={{ background: '#f4f4f4', padding: '1.5rem', borderRadius: '8px', marginBottom: '2rem', textAlign: 'center' }}>
        <p style={{ margin: 0, fontSize: '0.9rem', color: '#666' }}>Latest Weight</p>
        <h1 style={{ margin: 0, fontSize: '3rem' }}>{latestWeight} <span style={{ fontSize: '1.2rem' }}>lbs</span></h1>
      </div>

      {/* Input Form */}
      <form onSubmit={handleLogWeight} style={{ marginBottom: '2rem', display: 'flex', gap: '10px' }}>
        <input
          type="number"
          step="0.1"
          placeholder="Enter weight in lbs"
          value={newWeight}
          onChange={(e) => setNewWeight(e.target.value)}
          style={{ padding: '8px', flex: 1, borderRadius: '4px', border: '1px solid #ccc' }}
        />
        <button type="submit" style={{ padding: '8px 16px', background: '#007bff', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>
          Log Weight
        </button>
      </form>

      {/* Chart */}
      <div style={{ height: '300px', width: '100%', background: 'white', padding: '1rem', borderRadius: '8px', border: '1px solid #eee' }}>
        <h3>Last 20 Entries</h3>
        <ResponsiveContainer width="100%" height="100%">
          <LineChart data={weights}>
            <CartesianGrid strokeDasharray="3 3" vertical={false} />
            <XAxis 
                dataKey="created_at" 
                tickFormatter={(str) => new Date(str).toLocaleDateString()} 
                fontSize={12}
            />
            <YAxis domain={['auto', 'auto']} fontSize={12} />
            <Tooltip />
            <Line 
                type="monotone" 
                dataKey="weight_pounds" 
                stroke="#007bff" 
                strokeWidth={2} 
                dot={{ r: 4 }} 
                activeDot={{ r: 6 }} 
            />
          </LineChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
};
