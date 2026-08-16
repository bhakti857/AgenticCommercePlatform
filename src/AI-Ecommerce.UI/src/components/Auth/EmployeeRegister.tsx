import { useState } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import { Link } from 'react-router-dom';
import api from '../../api/client';

const roleOptions = [
  { value: 5, label: 'User' },
  { value: 4, label: 'Junior' },
  { value: 3, label: 'Senior' },
  { value: 2, label: 'Admin' },
  { value: 1, label: 'Master Admin' },
];

export default function EmployeeRegister() {
  const { user } = useAuth();
  const isAuthorized = user?.accountType === 'Employee' && (user?.userTypeId === 1 || user?.userTypeId === 2);

  const [form, setForm] = useState({
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    departmentId: 2,
    userTypeId: 5,
  });
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setSuccess(null);
    setSubmitting(true);
    try {
      const res = await api.post('/auth/register-employee', form);
      setSuccess(`Account created for ${res.data.fullName} (${res.data.email}).`);
      setForm({ email: '', password: '', firstName: '', lastName: '', departmentId: 2, userTypeId: 5 });
    } catch (err: any) {
      setError(
        err?.response?.status === 403
          ? 'You do not have permission to create staff accounts.'
          : err?.response?.data ?? 'Could not create the account. Please check the details and try again.'
      );
    } finally {
      setSubmitting(false);
    }
  };

  if (!isAuthorized) {
    return (
      <div className="mx-auto mt-6 max-w-md">
        <div className="card p-8 text-center">
          <h1 className="text-xl font-bold text-primary">Access restricted</h1>
          <p className="mt-2 text-sm text-secondary">
            Only Master Admin or Master accounts can create staff accounts.
          </p>
          <Link to="/" className="mt-4 inline-block text-sm font-semibold text-accent hover:underline">
            Back to home
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className="mx-auto mt-6 max-w-md">
      <div className="card p-8">
        <h1 className="text-center text-2xl font-bold text-primary">Create a staff account</h1>
        <p className="mt-1 text-center text-sm text-secondary">
          Register Employee, Master, or Master Admin accounts.
        </p>

        {error && (
          <p role="alert" className="mt-4 rounded-lg border border-red-200 bg-red-50 px-3 py-2 text-center text-sm text-red-700">
            {error}
          </p>
        )}
        {success && (
          <p role="status" className="mt-4 rounded-lg border border-emerald-200 bg-emerald-50 px-3 py-2 text-center text-sm text-emerald-700">
            {success}
          </p>
        )}

        <form onSubmit={handleSubmit} className="mt-6 space-y-4" noValidate>
          <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
            <div>
              <label htmlFor="emp-firstName" className="label">First name</label>
              <input
                id="emp-firstName"
                className="input-field"
                value={form.firstName}
                onChange={e => setForm({ ...form, firstName: e.target.value })}
                autoComplete="given-name"
                required
              />
            </div>
            <div>
              <label htmlFor="emp-lastName" className="label">Last name</label>
              <input
                id="emp-lastName"
                className="input-field"
                value={form.lastName}
                onChange={e => setForm({ ...form, lastName: e.target.value })}
                autoComplete="family-name"
                required
              />
            </div>
          </div>
          <div>
            <label htmlFor="emp-email" className="label">Email</label>
            <input
              id="emp-email"
              type="email"
              className="input-field"
              placeholder="staff@example.com"
              value={form.email}
              onChange={e => setForm({ ...form, email: e.target.value })}
              autoComplete="email"
              required
            />
          </div>
          <div>
            <label htmlFor="emp-password" className="label">Temporary password</label>
            <input
              id="emp-password"
              type="password"
              className="input-field"
              value={form.password}
              onChange={e => setForm({ ...form, password: e.target.value })}
              autoComplete="new-password"
              required
            />
          </div>
          <div>
            <label htmlFor="emp-department" className="label">Department</label>
            <select
              id="emp-department"
              className="input-field"
              value={form.departmentId}
              onChange={e => setForm({ ...form, departmentId: Number(e.target.value) })}
            >
              <option value={1}>CEO</option>
              <option value={2}>Software Developer</option>
            </select>
          </div>
          <div>
            <label htmlFor="emp-role" className="label">Role</label>
            <select
              id="emp-role"
              className="input-field"
              value={form.userTypeId}
              onChange={e => setForm({ ...form, userTypeId: Number(e.target.value) })}
            >
              {roleOptions.map(opt => (
                <option key={opt.value} value={opt.value}>
                  {opt.label}
                </option>
              ))}
            </select>
          </div>
          <button type="submit" disabled={submitting} className="btn-primary w-full justify-center disabled:cursor-not-allowed disabled:opacity-60">
            {submitting ? 'Creating account…' : 'Create staff account'}
          </button>
        </form>
      </div>
    </div>
  );
}
