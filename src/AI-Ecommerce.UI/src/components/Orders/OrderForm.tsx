import { useState } from 'react';
import api from '../../api/client';

export default function OrderForm() {
  const [items, setItems] = useState([{ productId: 1, quantity: 1 }]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await api.post('/orders', { items });
    alert('Order placed!');
  };

  return (
    <form onSubmit={handleSubmit} className="p-4">
      <h2 className="text-2xl font-bold mb-4">Create Order</h2>
      {items.map((item, idx) => (
        <div key={idx} className="flex gap-2 mb-2">
          <input type="number" value={item.productId} onChange={e => {
            const newItems = [...items];
            newItems[idx].productId = +e.target.value;
            setItems(newItems);
          }} className="border p-2 w-1/2" />
          <input type="number" value={item.quantity} onChange={e => {
            const newItems = [...items];
            newItems[idx].quantity = +e.target.value;
            setItems(newItems);
          }} className="border p-2 w-1/2" />
        </div>
      ))}
      <button className="bg-green-600 text-white p-2 rounded">Place Order</button>
    </form>
  );
}