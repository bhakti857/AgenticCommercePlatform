import { useState } from 'react';
import api from '../../api/client';

interface OrderItem {
  productId: number;
  quantity: number;
}

export default function OrderForm() {
  const [items, setItems] = useState<OrderItem[]>([{ productId: 1, quantity: 1 }]);
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState<{ type: 'success' | 'error'; text: string } | null>(null);

  const updateItem = (idx: number, field: keyof OrderItem, value: number) => {
    setItems(items.map((item, i) => (i === idx ? { ...item, [field]: value } : item)));
  };

  const addItem = () => setItems([...items, { productId: 1, quantity: 1 }]);
  const removeItem = (idx: number) => setItems(items.filter((_, i) => i !== idx));

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage(null);
    setSubmitting(true);
    try {
      await api.post('/orders', { items });
      setMessage({ type: 'success', text: 'Order placed successfully!' });
      setItems([{ productId: 1, quantity: 1 }]);
    } catch (err: any) {
      setMessage({
        type: 'error',
        text: err?.response?.data ?? 'Could not place the order. Please check the items and try again.',
      });
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="mx-auto max-w-2xl">
      <h1 className="text-2xl font-bold text-primary">Create an order</h1>
      <p className="mt-1 text-sm text-secondary">Add product IDs and quantities, then submit your order.</p>

      {message && (
        <p
          role="alert"
          className={`mt-4 rounded-lg border px-4 py-3 text-sm ${
            message.type === 'success'
              ? 'border-emerald-200 bg-emerald-50 text-emerald-700'
              : 'border-red-200 bg-red-50 text-red-700'
          }`}
        >
          {message.text}
        </p>
      )}

      <form onSubmit={handleSubmit} className="card mt-6 space-y-4 p-6">
        {items.map((item, idx) => (
          <div key={idx} className="flex items-end gap-3">
            <div className="flex-1">
              <label htmlFor={`product-${idx}`} className="label">Product ID</label>
              <input
                id={`product-${idx}`}
                type="number"
                min={1}
                className="input-field"
                value={item.productId}
                onChange={e => updateItem(idx, 'productId', Number(e.target.value))}
                required
              />
            </div>
            <div className="flex-1">
              <label htmlFor={`qty-${idx}`} className="label">Quantity</label>
              <input
                id={`qty-${idx}`}
                type="number"
                min={1}
                className="input-field"
                value={item.quantity}
                onChange={e => updateItem(idx, 'quantity', Number(e.target.value))}
                required
              />
            </div>
            {items.length > 1 && (
              <button
                type="button"
                onClick={() => removeItem(idx)}
                aria-label={`Remove item ${idx + 1}`}
                className="mb-0.5 rounded-lg px-3 py-2 text-sm font-medium text-red-600 hover:bg-red-50"
              >
                Remove
              </button>
            )}
          </div>
        ))}

        <button
          type="button"
          onClick={addItem}
          className="text-sm font-semibold text-accent hover:underline"
        >
          + Add another item
        </button>

        <div className="pt-2">
          <button type="submit" disabled={submitting} className="btn-primary w-full justify-center disabled:cursor-not-allowed disabled:opacity-60">
            {submitting ? 'Placing order…' : 'Place order'}
          </button>
        </div>
      </form>
    </div>
  );
}
