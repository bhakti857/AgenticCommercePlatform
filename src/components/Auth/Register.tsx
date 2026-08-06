import { useState } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import { Link } from 'react-router-dom';

export default function Register() {
  const [form, setForm] = useState({
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    userType: 4
  });
  const { register } = useAuth();

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    register(form);
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded-lg shadow-md">
      <h2 className="text-2xl font-bold mb-6 text-center">Register</h2>
      <form onSubmit={handleSubmit}>
        <input className="w-full border p-2 mb-3 rounded" placeholder="First Name" value={form.firstName} onChange={e => setForm({...form, firstName: e.target.value})} />
        <input className="w-full border p-2 mb-3 rounded" placeholder="Last Name" value={form.lastName} onChange={e => setForm({...form, lastName: e.target.value})} />
        <input className="w-full border p-2 mb-3 rounded" placeholder="Email" value={form.email} onChange={e => setForm({...form, email: e.target.value})} />
        <input className="w-full border p-2 mb-3 rounded" type="password" placeholder="Password" value={form.password} onChange={e => setForm({...form, password: e.target.value})} />
        <button className="w-full bg-green-600 text-white p-2 rounded hover:bg-green-700">Register</button>
      </form>
      <p className="mt-3 text-center text-sm">Already have an account? <Link to="/login" className="text-blue-600">Login</Link></p>
    </div>
  );
}