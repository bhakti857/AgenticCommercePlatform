import React, { useState } from 'react';
import api from '../api/client';
import { AuthContext } from './auth-context';

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [token, setToken] = useState(localStorage.getItem('token'));
  const [user, setUser] = useState(() => {
    const saved = localStorage.getItem('user');
    return saved ? JSON.parse(saved) : null;
  });

  const login = async (email: string, password: string) => {
    const res = await api.post('/auth/login', { email, password });
    const { token, refreshToken, ...userData } = res.data;
    if (refreshToken) localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(userData));
    setToken(token);
    setUser(userData);
  };

  const register = async (data: any) => {
    const res = await api.post('/auth/register', data);
    const { token, refreshToken, ...userData } = res.data;
    if (refreshToken) localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify(userData));
    setToken(token);
    setUser(userData);
  };

  const logout = async () => {
    const refresh = localStorage.getItem('refreshToken');
    if (refresh) {
      try {
        await api.post('/auth/revoke', { refreshToken: refresh });
      } catch {
        // Best-effort — the token remains invalid once the JWT expires.
      }
    }
    localStorage.clear();
    setToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ token, user, login, register, logout }}>
      {children}
    </AuthContext.Provider>
  );
};