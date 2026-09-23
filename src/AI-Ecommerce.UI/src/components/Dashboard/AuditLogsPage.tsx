import { useCallback, useEffect, useState } from 'react';
import api from '../../api/client';
import { useAuth } from '../../contexts/useAuth';

interface AuditEntry {
  logId: number;
  name: string;
  ipAddress: string | null;
  osFamily: string | null;
  osVersion: string | null;
  browserFamily: string | null;
  browserVersion: string | null;
  logDateTime: string;
}

export default function AuditLogsPage() {
  const { user } = useAuth();
  const [tab, setTab] = useState<'employee' | 'customer'>('employee');
  const [rows, setRows] = useState<AuditEntry[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const load = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const res = await api.get(`/audit/${tab}-logs?page=1&pageSize=100`);
      setRows(res.data.items ?? res.data);
    } catch {
      setError('Could not load audit logs.');
    } finally {
      setLoading(false);
    }
  }, [tab]);

  useEffect(() => {
    load();
    const t = window.setInterval(load, 15000);
    return () => window.clearInterval(t);
  }, [load]);

  if (user?.accountType !== 'Employee') {
    return <div className="card mt-6 p-8 text-secondary">This page is for employees only.</div>;
  }

  return (
    <div>
      <h1 className="text-2xl font-bold text-primary">Login Audit</h1>
      <p className="mt-1 text-sm text-secondary">
        Recent successful logins, refreshed automatically every 15 seconds.
      </p>

      <div className="mt-4 flex gap-2">
        {(['employee', 'customer'] as const).map(t => (
          <button
            key={t}
            onClick={() => setTab(t)}
            className={`rounded-lg px-4 py-2 text-sm font-medium transition ${
              tab === t ? 'bg-primary text-white' : 'border border-muted text-secondary hover:bg-bg'
            }`}
          >
            {t === 'employee' ? 'Employee logins' : 'Customer logins'}
          </button>
        ))}
      </div>

      <div className="card mt-4 overflow-x-auto p-0">
        {loading ? (
          <div className="p-5 text-sm text-secondary">Loading…</div>
        ) : error ? (
          <p role="alert" className="rounded-lg bg-red-50 px-4 py-3 text-sm text-red-700">{error}</p>
        ) : rows.length === 0 ? (
          <div className="p-5 text-sm text-secondary">No login activity yet.</div>
        ) : (
          <table className="w-full text-left text-sm">
            <thead className="border-y border-muted bg-bg text-xs uppercase tracking-wide text-secondary">
              <tr>
                <th className="px-5 py-3 font-semibold">Account</th>
                <th className="px-5 py-3 font-semibold">Login time</th>
                <th className="px-5 py-3 font-semibold">IP address</th>
                <th className="px-5 py-3 font-semibold">OS</th>
                <th className="px-5 py-3 font-semibold">Browser</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-muted">
              {rows.map(r => (
                <tr key={r.logId}>
                  <td className="px-5 py-3 font-medium text-primary">{r.name}</td>
                  <td className="px-5 py-3 text-secondary">{new Date(r.logDateTime).toLocaleString()}</td>
                  <td className="px-5 py-3 text-secondary">{r.ipAddress ?? '—'}</td>
                  <td className="px-5 py-3 text-secondary">
                    {r.osFamily ?? 'Unknown'}
                    {r.osVersion ? ` ${r.osVersion}` : ''}
                  </td>
                  <td className="px-5 py-3 text-secondary">
                    {r.browserFamily ?? 'Unknown'}
                    {r.browserVersion ? ` ${r.browserVersion}` : ''}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}