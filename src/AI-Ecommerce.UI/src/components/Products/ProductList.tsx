import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../../api/client';

interface CatalogProduct {
  productId: number;
  productCode: string;
  productName: string;
  category: string;
  subCategory: string;
  unit: string;
  sellingPrice: number;
  gstPercent: number;
  availableQuantity: number;
}

export default function ProductList() {
  const [products, setProducts] = useState<CatalogProduct[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [message, setMessage] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    let mounted = true;
    api
      .get('/catalog')
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

  const addToCart = async (productId: number) => {
    try {
      await api.post('/cart/items', { productId, quantity: 1 });
      setMessage('Added to cart.');
      setTimeout(() => setMessage(null), 2000);
    } catch (err: any) {
      setMessage(err?.response?.data ?? 'Could not add to cart.');
      setTimeout(() => setMessage(null), 3000);
    }
  };

  return (
    <div>
      <div className="flex flex-wrap items-center justify-between gap-3">
        <div>
          <h1 className="text-2xl font-bold text-primary">Products</h1>
          <p className="mt-1 text-sm text-secondary">Browse our current catalog and stock levels.</p>
        </div>
        <button onClick={() => navigate('/cart')} className="btn-secondary">
          View Cart
        </button>
      </div>

      {message && (
        <p role="alert" className="mt-4 rounded-lg border border-emerald-200 bg-emerald-50 px-4 py-3 text-sm text-emerald-700">
          {message}
        </p>
      )}

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
            <div key={p.productId} className="card flex flex-col p-5">
              <h2 className="font-semibold text-primary">{p.productName}</h2>
              <p className="mt-1 text-xs uppercase tracking-wide text-secondary">
                {p.category}
                {p.subCategory ? ` / ${p.subCategory}` : ''}
              </p>
              <p className="mt-3 text-xl font-bold text-accent">${p.sellingPrice.toFixed(2)}</p>
              <p className={`mt-2 text-sm ${p.availableQuantity > 0 ? 'text-secondary' : 'text-red-600'}`}>
                {p.availableQuantity > 0 ? `In stock: ${p.availableQuantity} ${p.unit}` : 'Out of stock'}
              </p>
              <button
                onClick={() => addToCart(p.productId)}
                disabled={p.availableQuantity <= 0}
                className="btn-primary mt-4 w-full justify-center disabled:cursor-not-allowed disabled:opacity-60"
              >
                Add to Cart
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}