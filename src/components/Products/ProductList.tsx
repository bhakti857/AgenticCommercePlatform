import { useEffect, useState } from 'react';
import api from '../../api/client';

export default function ProductList() {
  const [products, setProducts] = useState<any[]>([]);

  useEffect(() => {
    api.get('/products').then(res => setProducts(res.data));
  }, []);

  return (
    <div className="p-4">
      <h2 className="text-2xl font-bold mb-4">Products</h2>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {products.map((p: any) => (
          <div key={p.id} className="border p-4 rounded shadow">
            <h3 className="font-bold">{p.name}</h3>
            <p>${p.price}</p>
            <p className="text-sm text-gray-600">{p.category}</p>
            <p className="text-sm">Stock: {p.stockQuantity}</p>
          </div>
        ))}
      </div>
    </div>
  );
}