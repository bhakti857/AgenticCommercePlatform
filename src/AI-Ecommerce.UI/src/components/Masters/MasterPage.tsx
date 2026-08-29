import { useCallback, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../../api/client';
import type { MasterConfig, MasterField } from '../../config/masterConfigs';
import { masterConfigs, masterOrder } from '../../config/masterConfigs';

export default function MasterPage() {
  const { entity } = useParams();
  const config = entity ? masterConfigs[entity] : undefined;

  if (!config) {
    return (
      <div>
        <h1 className="text-2xl font-bold text-primary">Master Data</h1>
        <div className="card mt-6 grid gap-3 p-6 sm:grid-cols-2 lg:grid-cols-3">
          {masterOrder.map(key => (
            <a key={key} href={`/masters/${key}`} className="rounded-lg border border-muted p-4 text-sm font-medium text-primary hover:border-accent hover:text-accent">
              {masterConfigs[key].title}
            </a>
          ))}
        </div>
      </div>
    );
  }

  return <MasterCrud key={config.key} config={config} />;
}

function MasterCrud({ config }: { config: MasterConfig }) {
  const [tab, setTab] = useState<'list' | 'add'>('list');
  const [rows, setRows] = useState<any[]>([]);
  const [form, setForm] = useState<Record<string, any>>({});
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);
  const [options, setOptions] = useState<Record<string, any[]>>({});

  const tableColumns = config.fields.filter(
    f => !f.createOnly && f.type !== 'password' && f.name !== config.idField,
  );
  const formFields = config.fields.filter(f => f.name !== config.idField);

  const load = useCallback(async () => {
    setLoading(true);
    try {
      const res = await api.get(config.endpoint);
      setRows(res.data);
    } catch {
      setMessage({ type: 'error', text: 'Could not load records.' });
    } finally {
      setLoading(false);
    }
  }, [config]);

  useEffect(() => {
    load();
  }, [load]);

  useEffect(() => {
    const sources = config.fields.filter(f => f.optionSource);
    if (sources.length === 0) return;
    Promise.all(
      sources.map(async f => {
        const res = await api.get(f.optionSource!);
        return { key: f.name, items: res.data };
      }),
    ).then(all => {
      const map: Record<string, any[]> = {};
      all.forEach(a => (map[a.key] = a.items));
      setOptions(map);
    });
  }, [config]);

  const labelFor = (field: MasterField, value: any) => {
    if (field.options) return field.options.find(o => String(o.value) === String(value))?.label ?? String(value);
    if (field.optionSource) {
      const labelField = field.name.replace(/Id$/, 'Name');
      return options[field.name]?.find((o: any) => String(o[field.name]) === String(value))?.[labelField] ?? String(value);
    }
    return value;
  };

  const startAdd = () => {
    const initial: Record<string, any> = {};
    formFields.forEach(f => {
      if (f.defaultValue !== undefined) initial[f.name] = f.defaultValue;
      else if (f.type === 'number') initial[f.name] = '';
      else if (f.type === 'select') initial[f.name] = '';
      else initial[f.name] = '';
    });
    setForm(initial);
    setEditingId(null);
    setMessage(null);
    setTab('add');
  };

  const startEdit = (row: any) => {
    const values: Record<string, any> = {};
    formFields.forEach(f => {
      if (!f.createOnly) values[f.name] = row[f.name] ?? '';
    });
    setForm(values);
    setEditingId(row[config.idField]);
    setMessage(null);
    setTab('add');
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    setMessage(null);
    try {
      const payload: Record<string, any> = {};
      formFields.forEach(f => {
        if (editingId != null && f.createOnly) return;
        const raw = form[f.name];
        if (f.type === 'number') {
          payload[f.name] = raw === '' || raw === null ? null : Number(raw);
        } else {
          payload[f.name] = raw;
        }
      });

      if (editingId != null) {
        await api.put(`${config.endpoint}/${editingId}`, payload);
      } else {
        await api.post(config.endpoint, payload);
      }
      setMessage({ type: 'success', text: editingId != null ? 'Record updated.' : 'Record created.' });
      setTab('list');
      load();
    } catch (err: any) {
      setMessage({ type: 'error', text: err?.response?.data?.message ?? err?.response?.data ?? 'Save failed.' });
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (row: any) => {
    if (!window.confirm(`Delete this record?`)) return;
    try {
      await api.delete(`${config.endpoint}/${row[config.idField]}`);
      setMessage({ type: 'success', text: 'Record deleted.' });
      load();
    } catch {
      setMessage({ type: 'error', text: 'Delete failed.' });
    }
  };

  return (
    <div>
      <div className="flex flex-wrap items-center justify-between gap-3">
        <div>
          <h1 className="text-2xl font-bold text-primary">{config.title}</h1>
          <p className="mt-1 text-sm text-secondary">Maintain {config.title.toLowerCase()} records.</p>
        </div>
        <div className="flex gap-2">
          <button
            onClick={() => setTab('list')}
            className={`rounded-lg px-4 py-2 text-sm font-semibold ${tab === 'list' ? 'bg-primary text-white' : 'border border-muted text-secondary'}`}
          >
            List
          </button>
          <button
            onClick={startAdd}
            className={`rounded-lg px-4 py-2 text-sm font-semibold ${tab === 'add' ? 'bg-primary text-white' : 'border border-muted text-secondary'}`}
          >
            Add
          </button>
        </div>
      </div>

      {message && (
        <p role="alert" className={`mt-4 rounded-lg border px-4 py-3 text-sm ${message.type === 'success' ? 'border-emerald-200 bg-emerald-50 text-emerald-700' : 'border-red-200 bg-red-50 text-red-700'}`}>
          {message.text}
        </p>
      )}

      {tab === 'list' && (
        <div className="card mt-6 overflow-x-auto p-0">
          {loading ? (
            <div className="p-6 text-sm text-secondary">Loading…</div>
          ) : rows.length === 0 ? (
            <div className="p-6 text-sm text-secondary">No records yet. Use the Add tab to create one.</div>
          ) : (
            <table className="w-full text-left text-sm">
              <thead className="border-b border-muted bg-bg text-xs uppercase tracking-wide text-secondary">
                <tr>
                  {tableColumns.map(c => (
                    <th key={c.name} className="px-4 py-3 font-semibold">{c.label}</th>
                  ))}
                  <th className="px-4 py-3 text-right font-semibold">Actions</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-muted">
                {rows.map(row => (
                  <tr key={row[config.idField]} className="hover:bg-bg">
                    {tableColumns.map(c => (
                      <td key={c.name} className="px-4 py-3">
                        {typeof row[c.name] === 'boolean' ? (row[c.name] ? 'Yes' : 'No') : labelFor(c, row[c.name])}
                      </td>
                    ))}
                    <td className="px-4 py-3 text-right">
                      <button onClick={() => startEdit(row)} className="mr-2 rounded px-2 py-1 text-xs font-semibold text-accent hover:underline">
                        Edit
                      </button>
                      <button onClick={() => handleDelete(row)} className="rounded px-2 py-1 text-xs font-semibold text-red-600 hover:underline">
                        Delete
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      )}

      {tab === 'add' && (
        <form onSubmit={handleSubmit} className="card mt-6 max-w-2xl space-y-4 p-6">
          {editingId != null && (
            <p className="text-sm text-secondary">Editing {config.title} record #{editingId}.</p>
          )}
          {formFields.map(field => {
            if (editingId != null && field.createOnly) return null;
            return (
              <div key={field.name}>
                <label htmlFor={field.name} className="label">
                  {field.label} {field.required && <span className="text-red-600">*</span>}
                </label>
                {field.type === 'select' ? (
                  <select
                    id={field.name}
                    className="input-field"
                    value={form[field.name] ?? ''}
                    required={field.required}
                    onChange={e => {
                      const opts = field.options ?? options[field.name] ?? [];
                      const opt = opts.find(o => String(o.value ?? o[field.name]) === e.target.value);
                      setForm({ ...form, [field.name]: opt?.value ?? opt?.[field.name] ?? e.target.value });
                    }}
                  >
                    <option value="">Select…</option>
                    {(field.options ?? []).map(o => (
                      <option key={String(o.value)} value={String(o.value)}>{o.label}</option>
                    ))}
                    {(field.optionSource ? options[field.name] ?? [] : []).map((o: any) => {
                      const labelField = field.name.replace(/Id$/, 'Name');
                      return (
                        <option key={String(o[field.name])} value={String(o[field.name])}>
                          {o[labelField] ?? o[field.name]}
                        </option>
                      );
                    })}
                  </select>
                ) : field.type === 'textarea' ? (
                  <textarea
                    id={field.name}
                    className="input-field"
                    rows={3}
                    value={form[field.name] ?? ''}
                    required={field.required}
                    onChange={e => setForm({ ...form, [field.name]: e.target.value })}
                  />
                ) : (
                  <input
                    id={field.name}
                    type={field.type === 'password' ? 'password' : field.type === 'number' ? 'number' : 'text'}
                    step={field.type === 'number' ? 'any' : undefined}
                    className="input-field"
                    value={form[field.name] ?? ''}
                    required={field.required}
                    onChange={e => setForm({ ...form, [field.name]: e.target.value })}
                  />
                )}
              </div>
            );
          })}

          <div className="flex gap-3 pt-2">
            <button type="submit" disabled={saving} className="btn-primary disabled:cursor-not-allowed disabled:opacity-60">
              {saving ? 'Saving…' : editingId != null ? 'Update' : 'Save'}
            </button>
            <button type="button" onClick={() => setTab('list')} className="btn-secondary">
              Cancel
            </button>
          </div>
        </form>
      )}
    </div>
  );
}