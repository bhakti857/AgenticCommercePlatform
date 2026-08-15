import { useEffect, useState } from 'react';
import api from '../../api/client';

interface Product {
  id: number;
  name: string;
  price: number;
  category: string;
  stockQuantity: number;
}

export default function ProductList() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let mounted = true;
    api
      .get('/products')
      .then(res => {
        if (mounted) setProducts(res.data);
      })
      .catch(() => {
        if (mounted) setError('Could not load products. Please try again later.');
      })
      .finally(() => {
        if (mounted) setLoading(false);
      });
    return () => {
      mounted = false;
    };
  }, []);

  return (
    <div>
      <h1 className="text-2xl font-bold text-primary">Products</h1>
      <p className="mt-1 text-sm text-secondary">Browse our current catalog and stock levels.</p>

      {loading && (
        <div className="mt-6 grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3" aria-busy="true" aria-live="polite">
          {Array.from({ length: 6 }).map((_, i) => (
            <div key={i} className="card h-36 animate-pulse p-4">
              <div className="h-4 w-2/3 rounded bg-muted" />
              <div className="mt-3 h-3 w-1/3 rounded bg-muted" />
              <div className="mt-4 h-3 w-1/2 rounded bg-muted" />
            </div>
          ))}
        </div>
      )}

      {!loading && error && (
        <p role="alert" className="mt-6 rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
          {error}
        </p>
      )}

      {!loading && !error && products.length === 0 && (
        <div className="card mt-6 p-8 text-center text-secondary">No products available right now.</div>
      )}

      {!loading && !error && products.length > 0 && (
        <div className="mt-6 grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3">
          {products.map(p => (
            <div key={p.id} className="card flex flex-col p-5">
              <h2 className="font-semibold text-primary">{p.name}</h2>
              <p className="mt-1 text-xs uppercase tracking-wide text-secondary">{p.category}</p>
              <p className="mt-3 text-xl font-bold text-accent">${p.price.toFixed(2)}</p>
              <p className={`mt-2 text-sm ${p.stockQuantity > 0 ? 'text-secondary' : 'text-red-600'}`}>
                {p.stockQuantity > 0 ? `In stock: ${p.stockQuantity}` : 'Out of stock'}
              </p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
