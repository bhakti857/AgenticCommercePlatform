import { useCallback, useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import api from '../../api/client';
import { useAuth } from '../../contexts/AuthContext';

interface OrderItem {
  productId: number;
  productCode: string;
  productName: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
}

interface SalesOrder {
  salesOrderId: number;
  salesOrderNo: string;
  orderDate: string;
  subTotal: number;
  taxAmount: number;
  shippingCost: number;
  totalAmount: number;
  paymentMethod: string;
  paymentStatus: string;
  orderStatus: string;
  shippingAddress: string | null;
  shippingCity: string | null;
  shippingState: string | null;
  shippingPincode: string | null;
  shippedDate: string | null;
  deliveredDate: string | null;
  cancelledDate: string | null;
  items: OrderItem[];
}

const STATUS_FLOW = ['Placed', 'Processing', 'Shipped', 'Delivered'];

export default function OrderTracking() {
  const { user } = useAuth();
  const location = useLocation();
  const [orders, setOrders] = useState<SalesOrder[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [expanded, setExpanded] = useState<Set<number>>(new Set());
  const [message, setMessage] = useState<string | null>(null);

  const isEmployee = user?.accountType === 'Employee';

  const load = useCallback(async () => {
    try {
      const res = await api.get(isEmployee ? '/sales-orders/all' : '/sales-orders');
      setOrders(res.data);
    } catch {
      setError('Could not load your orders.');
    } finally {
      setLoading(false);
    }
  }, [isEmployee]);

  useEffect(() => {
    if (location.state?.placed) {
      setMessage(`Order ${location.state.placed} placed successfully! Track it below.`);
    }
    load();
  }, [load, location.state]);

  const toggle = (id: number) => {
    setExpanded(prev => {
      const next = new Set(prev);
      if (next.has(id)) next.delete(id);
      else next.add(id);
      return next;
    });
  };

  const updateStatus = async (order: SalesOrder, status: string) => {
    try {
      await api.patch(`/sales-orders/${order.salesOrderId}/status`, { status });
      setMessage(`Order ${order.salesOrderNo} → ${status}`);
      setTimeout(() => setMessage(null), 2500);
      load();
    } catch (err: any) {
      setMessage(err?.response?.data ?? 'Status update failed.');
      setTimeout(() => setMessage(null), 3000);
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold text-primary">Order Tracking</h1>
      <p className="mt-1 text-sm text-secondary">
        {isEmployee ? 'All sales orders across customers.' : 'Track the progress of your orders.'}
      </p>

      {message && (
        <p role="status" className="mt-4 rounded-lg border border-emerald-200 bg-emerald-50 px-4 py-3 text-sm text-emerald-700">
          {message}
        </p>
      )}
      {error && (
        <p role="alert" className="mt-4 rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">{error}</p>
      )}

      {loading && <div className="mt-6 text-sm text-secondary">Loading orders…</div>}

      {!loading && orders.length === 0 && (
        <div className="card mt-6 p-8 text-center text-secondary">No orders yet.</div>
      )}

      <div className="mt-6 space-y-4">
        {orders.map(order => {
          const stepIndex = STATUS_FLOW.indexOf(order.orderStatus);
          const isCancelled = order.orderStatus === 'Cancelled';
          return (
            <div key={order.salesOrderId} className="card p-5">
              <div className="flex flex-wrap items-center justify-between gap-3">
                <div>
                  <div className="font-semibold text-primary">{order.salesOrderNo}</div>
                  <div className="text-xs text-secondary">{new Date(order.orderDate).toLocaleString()}</div>
                </div>
                <div className="flex items-center gap-3">
                  <span className={`rounded-full px-3 py-1 text-xs font-semibold ${isCancelled ? 'bg-red-100 text-red-700' : 'bg-emerald-100 text-emerald-700'}`}>
                    {order.orderStatus}
                  </span>
                  <span className="rounded-full bg-bg px-3 py-1 text-xs font-semibold text-secondary">
                    {order.paymentMethod} · {order.paymentStatus}
                  </span>
                  <span className="text-lg font-bold text-primary">${order.totalAmount.toFixed(2)}</span>
                </div>
              </div>

              {/* Progress bar */}
              {!isCancelled && (
                <div className="mt-4 flex items-center gap-1">
                  {STATUS_FLOW.map((s, i) => (
                    <div key={s} className="flex flex-1 items-center gap-1">
                      <div className={`h-2 flex-1 rounded-full ${i <= stepIndex ? 'bg-accent' : 'bg-muted'}`} />
                      <span className={`text-[10px] font-medium ${i <= stepIndex ? 'text-accent' : 'text-secondary'}`}>{s}</span>
                    </div>
                  ))}
                </div>
              )}

              {isEmployee && !isCancelled && order.orderStatus !== 'Delivered' && (
                <div className="mt-3 flex items-center gap-2">
                  <label className="text-xs font-medium text-secondary">Update status:</label>
                  <select
                    className="input-field w-44"
                    value={order.orderStatus}
                    onChange={e => updateStatus(order, e.target.value)}
                  >
                    {STATUS_FLOW.map(s => <option key={s} value={s}>{s}</option>)}
                    <option value="Cancelled">Cancelled</option>
                  </select>
                </div>
              )}

              <button onClick={() => toggle(order.salesOrderId)} className="mt-3 text-sm font-semibold text-accent hover:underline">
                {expanded.has(order.salesOrderId) ? 'Hide items ▲' : 'Show items ▼'}
              </button>

              {expanded.has(order.salesOrderId) && (
                <div className="mt-3 rounded-lg border border-muted p-4">
                  <table className="w-full text-left text-sm">
                    <thead className="text-xs uppercase tracking-wide text-secondary">
                      <tr>
                        <th className="pb-2 font-semibold">Item</th>
                        <th className="pb-2 font-semibold">Qty</th>
                        <th className="pb-2 text-right font-semibold">Unit Price</th>
                        <th className="pb-2 text-right font-semibold">Total</th>
                      </tr>
                    </thead>
                    <tbody className="divide-y divide-muted">
                      {order.items.map(item => (
                        <tr key={item.productId}>
                          <td className="py-2">
                            <span className="font-medium text-primary">{item.productName}</span>
                            <div className="text-xs text-secondary">{item.productCode}</div>
                          </td>
                          <td className="py-2">{item.quantity}</td>
                          <td className="py-2 text-right">${item.unitPrice.toFixed(2)}</td>
                          <td className="py-2 text-right font-semibold">${item.totalPrice.toFixed(2)}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                  <div className="mt-2 text-xs text-secondary">
                    {order.shippingAddress && (
                      <span>Ship to: {order.shippingAddress}, {order.shippingCity}, {order.shippingState} {order.shippingPincode}</span>
                    )}
                    {order.shippedDate && <span className="ml-3">Shipped: {new Date(order.shippedDate).toLocaleString()}</span>}
                    {order.deliveredDate && <span className="ml-3">Delivered: {new Date(order.deliveredDate).toLocaleString()}</span>}
                  </div>
                </div>
              )}
            </div>
          );
        })}
      </div>
    </div>
  );
}