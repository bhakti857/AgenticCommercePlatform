import { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/client';

interface CartItem {
  cartItemId: number;
  productId: number;
  productCode: string;
  productName: string;
  unitPrice: number;
  quantity: number;
  lineTotal: number;
  available: number;
}

export default function CartPage() {
  const [items, setItems] = useState<CartItem[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(true);
  const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);
  const [showCheckout, setShowCheckout] = useState(false);
  const [paymentMethod, setPaymentMethod] = useState<'COD' | 'UPI'>('COD');
  const [paymentReference, setPaymentReference] = useState('');
  const [placing, setPlacing] = useState(false);
  const navigate = useNavigate();

  const load = useCallback(async () => {
    try {
      const res = await api.get('/cart');
      setItems(res.data.items);
      setTotal(res.data.total);
    } catch {
      setMessage({ type: 'error', text: 'Could not load your cart.' });
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    load();
  }, [load]);

  const updateQty = async (item: CartItem, quantity: number) => {
    if (quantity <= 0) return;
    if (quantity > item.available) {
      setMessage({ type: 'error', text: `Only ${item.available} in stock.` });
      return;
    }
    try {
      await api.put(`/cart/items/${item.cartItemId}`, { quantity });
      load();
    } catch (err: any) {
      setMessage({ type: 'error', text: err?.response?.data ?? 'Update failed.' });
    }
  };

  const remove = async (item: CartItem) => {
    await api.delete(`/cart/items/${item.cartItemId}`);
    load();
  };

  const clear = async () => {
    await api.delete('/cart');
    load();
  };

  const placeOrder = async () => {
    setPlacing(true);
    setMessage(null);
    try {
      const res = await api.post('/cart/checkout', {
        paymentMethod,
        paymentReference: paymentMethod === 'UPI' ? paymentReference : null,
      });
      navigate('/orders', { state: { placed: res.data.salesOrderNo } });
    } catch (err: any) {
      setMessage({ type: 'error', text: err?.response?.data ?? 'Could not place the order.' });
      setPlacing(false);
    }
  };

  if (loading) return <div className="text-sm text-secondary">Loading cart…</div>;

  const tax = total * 0.1;
  const shipping = total > 100 ? 0 : 10;
  const grandTotal = total + tax + shipping;

  return (
    <div>
      <div className="flex flex-wrap items-center justify-between gap-3">
        <div>
          <h1 className="text-2xl font-bold text-primary">Shopping Cart</h1>
          <p className="mt-1 text-sm text-secondary">Review your items and choose a payment method.</p>
        </div>
        {items.length > 0 && (
          <button onClick={clear} className="btn-secondary">Clear Cart</button>
        )}
      </div>

      {message && (
        <p role="alert" className={`mt-4 rounded-lg border px-4 py-3 text-sm ${message.type === 'success' ? 'border-emerald-200 bg-emerald-50 text-emerald-700' : 'border-red-200 bg-red-50 text-red-700'}`}>
          {message.text}
        </p>
      )}

      {items.length === 0 ? (
        <div className="card mt-6 p-8 text-center text-secondary">
          Your cart is empty.{' '}
          <button onClick={() => navigate('/products')} className="text-accent hover:underline">Browse products</button>
        </div>
      ) : (
        <div className="mt-6 grid gap-6 lg:grid-cols-3">
          <div className="card overflow-x-auto p-0 lg:col-span-2">
            <table className="w-full text-left text-sm">
              <thead className="border-b border-muted bg-bg text-xs uppercase tracking-wide text-secondary">
                <tr>
                  <th className="px-4 py-3 font-semibold">Product</th>
                  <th className="px-4 py-3 font-semibold">Price</th>
                  <th className="px-4 py-3 font-semibold">Qty</th>
                  <th className="px-4 py-3 text-right font-semibold">Total</th>
                  <th className="px-4 py-3" />
                </tr>
              </thead>
              <tbody className="divide-y divide-muted">
                {items.map(item => (
                  <tr key={item.cartItemId}>
                    <td className="px-4 py-3">
                      <div className="font-medium text-primary">{item.productName}</div>
                      <div className="text-xs text-secondary">{item.productCode}</div>
                    </td>
                    <td className="px-4 py-3">${item.unitPrice.toFixed(2)}</td>
                    <td className="px-4 py-3">
                      <input
                        type="number"
                        min={1}
                        max={item.available}
                        className="input-field w-20"
                        value={item.quantity}
                        onChange={e => updateQty(item, Number(e.target.value))}
                      />
                    </td>
                    <td className="px-4 py-3 text-right font-semibold text-primary">${item.lineTotal.toFixed(2)}</td>
                    <td className="px-4 py-3 text-right">
                      <button onClick={() => remove(item)} className="text-sm font-semibold text-red-600 hover:underline">
                        Remove
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <div className="card h-fit space-y-3 p-6">
            <h2 className="text-lg font-semibold text-primary">Order Summary</h2>
            <div className="flex justify-between text-sm"><span className="text-secondary">Subtotal</span><span>${total.toFixed(2)}</span></div>
            <div className="flex justify-between text-sm"><span className="text-secondary">Tax (10%)</span><span>${tax.toFixed(2)}</span></div>
            <div className="flex justify-between text-sm"><span className="text-secondary">Shipping</span><span>{shipping === 0 ? 'Free' : `$${shipping.toFixed(2)}`}</span></div>
            <div className="flex justify-between border-t border-muted pt-3 text-base font-bold text-primary"><span>Total</span><span>${grandTotal.toFixed(2)}</span></div>

            <button onClick={() => setShowCheckout(true)} className="btn-primary w-full justify-center">
              Proceed to Checkout
            </button>
          </div>
        </div>
      )}

      {showCheckout && (
        <div className="fixed inset-0 z-40 flex items-center justify-center bg-black/40 p-4" role="dialog" aria-modal="true">
          <div className="w-full max-w-md rounded-2xl bg-white p-6 shadow-xl">
            <h2 className="text-lg font-bold text-primary">Checkout</h2>
            <p className="mt-1 text-sm text-secondary">Total payable: <span className="font-semibold text-primary">${grandTotal.toFixed(2)}</span></p>

            <div className="mt-4 space-y-2">
              <label className={`flex cursor-pointer items-center gap-3 rounded-lg border p-3 ${paymentMethod === 'COD' ? 'border-accent bg-bg' : 'border-muted'}`}>
                <input type="radio" name="payment" checked={paymentMethod === 'COD'} onChange={() => setPaymentMethod('COD')} />
                <div>
                  <div className="text-sm font-semibold text-primary">Cash on Delivery</div>
                  <div className="text-xs text-secondary">Pay when your order arrives.</div>
                </div>
              </label>
              <label className={`flex cursor-pointer items-center gap-3 rounded-lg border p-3 ${paymentMethod === 'UPI' ? 'border-accent bg-bg' : 'border-muted'}`}>
                <input type="radio" name="payment" checked={paymentMethod === 'UPI'} onChange={() => setPaymentMethod('UPI')} />
                <div>
                  <div className="text-sm font-semibold text-primary">UPI</div>
                  <div className="text-xs text-secondary">Payment is kept pending — no real processing.</div>
                </div>
              </label>
            </div>

            {paymentMethod === 'UPI' && (
              <div className="mt-4">
                <label htmlFor="upi-ref" className="label">UPI Reference (optional)</label>
                <input id="upi-ref" className="input-field" placeholder="e.g. 9876543210@upi" value={paymentReference} onChange={e => setPaymentReference(e.target.value)} />
              </div>
            )}

            <div className="mt-6 flex gap-3">
              <button onClick={placeOrder} disabled={placing} className="btn-primary flex-1 justify-center disabled:cursor-not-allowed disabled:opacity-60">
                {placing ? 'Placing…' : 'Place Order'}
              </button>
              <button onClick={() => setShowCheckout(false)} className="btn-secondary">Cancel</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}