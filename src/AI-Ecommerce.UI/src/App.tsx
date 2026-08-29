import type { ReactNode } from 'react';
import { BrowserRouter, Routes, Route, Navigate, Link } from 'react-router-dom';
import { AuthProvider, useAuth } from './contexts/AuthContext';
import Header from './components/Layout/Header';
import Footer from './components/Layout/Footer';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import EmployeeRegister from './components/Auth/EmployeeRegister';
import ProductList from './components/Products/ProductList';
import CartPage from './components/Cart/CartPage';
import OrderTracking from './components/Orders/OrderTracking';
import ProfilePage from './components/Profile/ProfilePage';
import DashboardPage from './components/Dashboard/DashboardPage';
import MasterPage from './components/Masters/MasterPage';
import Chat from './components/Agent/Chat';

function PrivateRoute({ children }: { children: ReactNode }) {
  const { token } = useAuth();
  return token ? <>{children}</> : <Navigate to="/login" replace />;
}

const features = [
  {
    title: 'Browse products',
    desc: 'Explore a curated catalog with real-time stock and pricing.',
    href: '/products',
    cta: 'View products',
  },
  {
    title: 'Track your orders',
    desc: 'Place orders and keep tabs on totals, tax, and shipping.',
    href: '/orders',
    cta: 'Create an order',
  },
  {
    title: 'Chat with the AI agent',
    desc: 'Ask questions, get recommendations, and get help — instantly.',
    href: '/agent',
    cta: 'Start chatting',
  },
];

function Home() {
  return (
    <div className="space-y-16">
      <section className="mx-auto max-w-3xl text-center">
        <h1 className="text-4xl font-bold tracking-tight text-primary sm:text-5xl">
          Shop smarter with an AI-powered assistant
        </h1>
        <p className="mt-4 text-lg text-secondary">
          AI Commerce pairs a modern storefront with a built-in agent that helps you find
          products, manage orders, and get answers — all in one place.
        </p>
        <div className="mt-8 flex flex-col items-center justify-center gap-3 sm:flex-row">
          <Link to="/products" className="btn-primary">
            Browse products
          </Link>
          <Link to="/agent" className="btn-secondary">
            Chat with agent
          </Link>
        </div>
      </section>

      <section className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3" aria-label="Key features">
        {features.map(f => (
          <div key={f.title} className="card flex flex-col p-6">
            <h2 className="text-lg font-semibold text-primary">{f.title}</h2>
            <p className="mt-2 flex-1 text-sm text-secondary">{f.desc}</p>
            <Link
              to={f.href}
              className="mt-4 inline-flex items-center gap-1 text-sm font-semibold text-accent hover:underline"
            >
              {f.cta}
              <span aria-hidden="true">→</span>
            </Link>
          </div>
        ))}
      </section>
    </div>
  );
}

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <div className="flex min-h-screen flex-col bg-bg">
          <Header />
          <main className="mx-auto w-full max-w-6xl flex-1 px-4 py-10 sm:px-6 lg:px-8">
            <Routes>
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route
                path="/employeeregister"
                element={
                  <PrivateRoute>
                    <EmployeeRegister />
                  </PrivateRoute>
                }
              />
              <Route
                path="/"
                element={
                  <PrivateRoute>
                    <Home />
                  </PrivateRoute>
                }
              />
              <Route
                path="/products"
                element={
                  <PrivateRoute>
                    <ProductList />
                  </PrivateRoute>
                }
              />
              <Route
                path="/cart"
                element={
                  <PrivateRoute>
                    <CartPage />
                  </PrivateRoute>
                }
              />
              <Route
                path="/orders"
                element={
                  <PrivateRoute>
                    <OrderTracking />
                  </PrivateRoute>
                }
              />
              <Route
                path="/profile"
                element={
                  <PrivateRoute>
                    <ProfilePage />
                  </PrivateRoute>
                }
              />
              <Route
                path="/dashboard"
                element={
                  <PrivateRoute>
                    <DashboardPage />
                  </PrivateRoute>
                }
              />
              <Route
                path="/masters"
                element={
                  <PrivateRoute>
                    <MasterPage />
                  </PrivateRoute>
                }
              />
              <Route
                path="/masters/:entity"
                element={
                  <PrivateRoute>
                    <MasterPage />
                  </PrivateRoute>
                }
              />
              <Route
                path="/agent"
                element={
                  <PrivateRoute>
                    <Chat />
                  </PrivateRoute>
                }
              />
            </Routes>
          </main>
          <Footer />
        </div>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
