import { Link, NavLink, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useAuth } from '../../contexts/AuthContext';

const navLinks = [
  { to: '/', label: 'Home', roles: ['Customer', 'Employee'] },
  { to: '/products', label: 'Products', roles: ['Customer', 'Employee'] },
  { to: '/cart', label: 'Cart', roles: ['Customer'] },
  { to: '/orders', label: 'Orders', roles: ['Customer', 'Employee'] },
  { to: '/dashboard', label: 'Dashboard', roles: ['Employee'] },
  { to: '/masters', label: 'Masters', roles: ['Employee'] },
  { to: '/agent', label: 'Agent', roles: ['Employee'] },
  { to: '/profile', label: 'Profile', roles: ['Customer', 'Employee'] },
];

export default function Header() {
  const [menuOpen, setMenuOpen] = useState(false);
  const { token, user, logout } = useAuth();
  const navigate = useNavigate();

  const links = navLinks.filter(l => !user || l.roles.includes(user.accountType));

  const handleLogout = () => {
    logout();
    setMenuOpen(false);
    navigate('/login');
  };

  const linkClasses = ({ isActive }: { isActive: boolean }) =>
    `rounded-lg px-3 py-2 text-sm font-medium transition ${
      isActive ? 'bg-bg text-accent' : 'text-secondary hover:text-primary'
    }`;

  return (
    <header className="sticky top-0 z-30 border-b border-muted bg-surface/95 backdrop-blur">
      <div className="mx-auto flex max-w-6xl items-center justify-between px-4 py-3 sm:px-6 lg:px-8">
        <Link to="/" className="flex items-center gap-2 text-lg font-bold text-primary">
          <span aria-hidden="true" className="text-xl">🛒</span>
          AI Commerce
        </Link>

        {token && (
          <nav className="hidden items-center gap-1 md:flex" aria-label="Primary">
            {links.map(link => (
              <NavLink key={link.to} to={link.to} end={link.to === '/'} className={linkClasses}>
                {link.label}
              </NavLink>
            ))}
          </nav>
        )}

        <div className="hidden items-center gap-3 md:flex">
          {token ? (
            <>
              {user?.firstName && (
                <span className="text-sm text-secondary">Hi, {user.firstName}</span>
              )}
              <button onClick={handleLogout} className="btn-secondary">
                Logout
              </button>
            </>
          ) : (
            <>
              <Link to="/login" className="text-sm font-medium text-secondary hover:text-accent">
                Login
              </Link>
              <Link to="/register" className="btn-primary">
                Get started
              </Link>
            </>
          )}
        </div>

        <button
          type="button"
          className="inline-flex items-center justify-center rounded-lg p-2 text-secondary hover:bg-bg md:hidden"
          aria-expanded={menuOpen}
          aria-label="Toggle navigation menu"
          onClick={() => setMenuOpen(o => !o)}
        >
          <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2} aria-hidden="true">
            {menuOpen ? (
              <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" />
            ) : (
              <path strokeLinecap="round" strokeLinejoin="round" d="M4 6h16M4 12h16M4 18h16" />
            )}
          </svg>
        </button>
      </div>

      {menuOpen && (
        <div className="border-t border-muted bg-surface px-4 py-3 md:hidden">
          <nav className="flex flex-col gap-1" aria-label="Mobile">
            {token &&
              links.map(link => (
                <NavLink
                  key={link.to}
                  to={link.to}
                  end={link.to === '/'}
                  onClick={() => setMenuOpen(false)}
                  className={linkClasses}
                >
                  {link.label}
                </NavLink>
              ))}
            {token ? (
              <button onClick={handleLogout} className="btn-secondary mt-2">
                Logout
              </button>
            ) : (
              <>
                <Link
                  to="/login"
                  onClick={() => setMenuOpen(false)}
                  className="rounded-lg px-3 py-2 text-sm font-medium text-secondary"
                >
                  Login
                </Link>
                <Link
                  to="/register"
                  onClick={() => setMenuOpen(false)}
                  className="btn-primary mt-2 justify-center"
                >
                  Get started
                </Link>
              </>
            )}
          </nav>
        </div>
      )}
    </header>
  );
}
