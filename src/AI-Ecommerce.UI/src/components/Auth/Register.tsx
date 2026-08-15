import { useState } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import { Link, useNavigate } from 'react-router-dom';

export default function Register() {
  const [form, setForm] = useState({ email: '', password: '', firstName: '', lastName: '', userType: 4 });
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setSubmitting(true);
    try {
      await register(form);
      navigate('/');
    } catch (err: any) {
      setError(err?.response?.data ?? 'Registration failed. Please check your details and try again.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="mx-auto mt-6 max-w-md">
      <div className="card p-8">
        <h1 className="text-center text-2xl font-bold text-primary">Create your account</h1>
        <p className="mt-1 text-center text-sm text-secondary">Join AI Commerce in a few seconds</p>

        {error && (
          <p role="alert" className="mt-4 rounded-lg border border-red-200 bg-red-50 px-3 py-2 text-center text-sm text-red-700">
            {error}
          </p>
        )}

        <form onSubmit={handleSubmit} className="mt-6 space-y-4" noValidate>
          <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
            <div>
              <label htmlFor="firstName" className="label">First name</label>
              <input
                id="firstName"
                className="input-field"
                placeholder="Bhakti"
                value={form.firstName}
                onChange={e => setForm({ ...form, firstName: e.target.value })}
                autoComplete="given-name"
                required
              />
            </div>
            <div>
              <label htmlFor="lastName" className="label">Last name</label>
              <input
                id="lastName"
                className="input-field"
                placeholder="Raut"
                value={form.lastName}
                onChange={e => setForm({ ...form, lastName: e.target.value })}
                autoComplete="family-name"
                required
              />
            </div>
          </div>
          <div>
            <label htmlFor="reg-email" className="label">Email</label>
            <input
              id="reg-email"
              type="email"
              className="input-field"
              placeholder="you@example.com"
              value={form.email}
              onChange={e => setForm({ ...form, email: e.target.value })}
              autoComplete="email"
              required
            />
          </div>
          <div>
            <label htmlFor="reg-password" className="label">Password</label>
            <input
              id="reg-password"
              type="password"
              className="input-field"
              placeholder="••••••••"
              value={form.password}
              onChange={e => setForm({ ...form, password: e.target.value })}
              autoComplete="new-password"
              required
            />
          </div>
          <button type="submit" disabled={submitting} className="btn-primary w-full justify-center disabled:cursor-not-allowed disabled:opacity-60">
            {submitting ? 'Creating account…' : 'Register'}
          </button>
        </form>

        <p className="mt-5 text-center text-sm text-secondary">
          Already have an account?{' '}
          <Link to="/login" className="font-semibold text-accent hover:underline">
            Login
          </Link>
        </p>
      </div>
    </div>
  );
}
