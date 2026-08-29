import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../../api/client';
import { useAuth } from '../../contexts/AuthContext';

interface Summary {
  counts: {
    customers: number;
    employees: number;
    products: number;
    vendors: number;
    warehouses: number;
    rawMaterials: number;
    categories: number;
    departments: number;
    openOrders: number;
    pendingPayments: number;
  };
  ordersByStatus: { status: string; count: number }[];
  recentOrders: { salesOrderNo: string; totalAmount: number; paymentMethod: string; orderStatus: string; createdAt: string }[];
  lowStock: { productId: number; productName: string; available: number }[];
  pendingApprovals: { productCode: string; productName: string; approval1At: boolean; approval2At: boolean; approval3At: boolean }[];
}

export default function DashboardPage() {
  const { user } = useAuth();
  const [data, setData] = useState<Summary | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    api
      .get('/dashboard/summary')
      .then(res => setData(res.data))
      .catch(() => setError('Could not load the dashboard.'));
  }, []);

  if (user?.accountType !== 'Employee') {
    return <div className="card mt-6 p-8 text-secondary">This page is for employees only.</div>;
  }

  if (error) return <p role="alert" className="rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">{error}</p>;
  if (!data) return <div className="text-sm text-secondary">Loading dashboard…</div>;

  const cards = [
    { label: 'Customers', value: data.counts.customers, to: '/masters/customer' },
    { label: 'Employees', value: data.counts.employees, to: '/masters/employee' },
    { label: 'Products', value: data.counts.products, to: '/masters/product' },
    { label: 'Vendors', value: data.counts.vendors, to: '/masters/vendor' },
    { label: 'Warehouses', value: data.counts.warehouses, to: '/masters/warehouse' },
    { label: 'Raw Materials', value: data.counts.rawMaterials, to: '/masters/rawmaterial' },
    { label: 'Categories', value: data.counts.categories, to: '/masters/category' },
    { label: 'Departments', value: data.counts.departments, to: '/masters/department' },
    { label: 'Open Orders', value: data.counts.openOrders, to: '/orders' },
    { label: 'Pending Payments', value: data.counts.pendingPayments, to: '/orders' },
  ];

  return (
    <div>
      <h1 className="text-2xl font-bold text-primary">Dashboard</h1>
      <p className="mt-1 text-sm text-secondary">Overview of the platform.</p>

      <div className="mt-6 grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-5">
        {cards.map(card => (
          <Link key={card.label} to={card.to} className="card p-5 transition hover:border-accent">
            <div className="text-2xl font-bold text-primary">{card.value}</div>
            <div className="mt-1 text-xs font-medium uppercase tracking-wide text-secondary">{card.label}</div>
          </Link>
        ))}
      </div>

      <div className="mt-6 grid gap-6 lg:grid-cols-2">
        <div className="card p-5">
          <h2 className="text-sm font-semibold text-primary">Orders by status</h2>
          {data.ordersByStatus.length === 0 ? (
            <p className="mt-3 text-sm text-secondary">No orders yet.</p>
          ) : (
            <ul className="mt-3 space-y-2 text-sm">
              {data.ordersByStatus.map(s => (
                <li key={s.status} className="flex justify-between">
                  <span className="text-secondary">{s.status}</span>
                  <span className="font-semibold text-primary">{s.count}</span>
                </li>
              ))}
            </ul>
          )}
        </div>

        <div className="card p-5">
          <h2 className="text-sm font-semibold text-primary">Low stock</h2>
          {data.lowStock.length === 0 ? (
            <p className="mt-3 text-sm text-secondary">All stocked up.</p>
          ) : (
            <ul className="mt-3 space-y-2 text-sm">
              {data.lowStock.map(s => (
                <li key={s.productId} className="flex justify-between">
                  <span className="text-secondary">{s.productName}</span>
                  <span className="font-semibold text-red-600">{s.available} left</span>
                </li>
              ))}
            </ul>
          )}
        </div>
      </div>

      <div className="card mt-6 overflow-x-auto p-0">
        <div className="p-5 text-sm font-semibold text-primary">Recent orders</div>
        {data.recentOrders.length === 0 ? (
          <div className="p-5 text-sm text-secondary">No orders yet.</div>
        ) : (
          <table className="w-full text-left text-sm">
            <thead className="border-y border-muted bg-bg text-xs uppercase tracking-wide text-secondary">
              <tr>
                <th className="px-5 py-3 font-semibold">Order</th>
                <th className="px-5 py-3 font-semibold">Date</th>
                <th className="px-5 py-3 font-semibold">Payment</th>
                <th className="px-5 py-3 font-semibold">Status</th>
                <th className="px-5 py-3 text-right font-semibold">Total</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-muted">
              {data.recentOrders.map(o => (
                <tr key={o.salesOrderNo}>
                  <td className="px-5 py-3 font-medium text-primary">{o.salesOrderNo}</td>
                  <td className="px-5 py-3 text-secondary">{new Date(o.createdAt).toLocaleString()}</td>
                  <td className="px-5 py-3">{o.paymentMethod}</td>
                  <td className="px-5 py-3">{o.orderStatus}</td>
                  <td className="px-5 py-3 text-right font-semibold">${o.totalAmount.toFixed(2)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}