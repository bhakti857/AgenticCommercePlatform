import { useEffect, useState } from 'react';
import api from '../../api/client';
import { useAuth } from '../../contexts/AuthContext';

interface Profile {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  addressLine: string;
  city: string;
  state: string;
  country: string;
  pincode: string;
}

export default function ProfilePage() {
  const { user } = useAuth();
  const [form, setForm] = useState<Profile>({
    email: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    addressLine: '',
    city: '',
    state: '',
    country: '',
    pincode: '',
  });
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);

  useEffect(() => {
    if (user?.accountType === 'Employee') {
      setLoading(false);
      return;
    }
    api
      .get('/profile')
      .then(res => setForm(res.data))
      .catch(() => setMessage({ type: 'error', text: 'Could not load your profile.' }))
      .finally(() => setLoading(false));
  }, [user]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    setMessage(null);
    try {
      await api.put('/profile', form);
      setMessage({ type: 'success', text: 'Profile updated.' });
    } catch (err: any) {
      setMessage({ type: 'error', text: err?.response?.data ?? 'Update failed.' });
    } finally {
      setSaving(false);
    }
  };

  const set = (name: keyof Profile) => (e: React.ChangeEvent<HTMLInputElement>) =>
    setForm({ ...form, [name]: e.target.value });

  if (user?.accountType === 'Employee') {
    return (
      <div>
        <h1 className="text-2xl font-bold text-primary">Profile</h1>
        <div className="card mt-6 p-8 text-secondary">
          Employee accounts don't maintain a shipping profile. Your details are managed in the Employee Master.
        </div>
      </div>
    );
  }

  if (loading) return <div className="text-sm text-secondary">Loading profile…</div>;

  const field = (label: string, name: keyof Profile, type = 'text') => (
    <div>
      <label htmlFor={name} className="label">{label}</label>
      <input id={name} type={type} className="input-field" value={form[name]} onChange={set(name)} />
    </div>
  );

  return (
    <div>
      <h1 className="text-2xl font-bold text-primary">My Profile</h1>
      <p className="mt-1 text-sm text-secondary">Manage your contact and shipping address.</p>

      {message && (
        <p role="alert" className={`mt-4 rounded-lg border px-4 py-3 text-sm ${message.type === 'success' ? 'border-emerald-200 bg-emerald-50 text-emerald-700' : 'border-red-200 bg-red-50 text-red-700'}`}>
          {message.text}
        </p>
      )}

      <form onSubmit={handleSubmit} className="card mt-6 max-w-2xl space-y-4 p-6">
        <div className="grid gap-4 sm:grid-cols-2">
          {field('Email', 'email', 'email')}
          {field('Phone Number', 'phoneNumber')}
          {field('First Name', 'firstName')}
          {field('Last Name', 'lastName')}
        </div>
        <div>
          <label htmlFor="addressLine" className="label">Address</label>
          <input id="addressLine" className="input-field" value={form.addressLine} onChange={set('addressLine')} />
        </div>
        <div className="grid gap-4 sm:grid-cols-2">
          {field('City', 'city')}
          {field('State', 'state')}
          {field('Country', 'country')}
          {field('Pincode', 'pincode')}
        </div>
        <button type="submit" disabled={saving} className="btn-primary disabled:cursor-not-allowed disabled:opacity-60">
          {saving ? 'Saving…' : 'Save Profile'}
        </button>
      </form>
    </div>
  );
}