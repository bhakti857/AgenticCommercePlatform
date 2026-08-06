import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { AuthProvider, useAuth } from "./contexts/AuthContext";
import Login from "./components/Auth/Login";
import Register from "./components/Auth/Register";
import ProductList from "./components/Products/ProductList";
import OrderForm from "./components/Orders/OrderForm";
import Chat from "./components/Agent/Chat";

const PrivateRoute = ({ children }: { children: JSX.Element }) => {
  const { token } = useAuth();
  return token ? children : <Navigate to="/login" />;
};

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <div className="min-h-screen bg-gray-100">
          <nav className="bg-white shadow-md p-4">
            <div className="container mx-auto flex items-center justify-between">
              <div className="flex items-center gap-6">
                <a href="/" className="font-bold text-xl text-blue-600">
                  🛒 AI Commerce
                </a>
                <a href="/products" className="hover:text-blue-600">
                  Products
                </a>
                <a href="/orders" className="hover:text-blue-600">
                  Orders
                </a>
                <a href="/agent" className="hover:text-blue-600">
                  Agent
                </a>
              </div>
              <button
                onClick={() => {
                  localStorage.clear();
                  window.location.href = "/login";
                }}
                className="bg-red-500 text-white px-4 py-1 rounded hover:bg-red-600"
              >
                Logout
              </button>
            </div>
          </nav>

          <div className="container mx-auto py-6">
            <Routes>
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route
                path="/"
                element={
                  <PrivateRoute>
                    <div className="text-center py-20">
                      <h1 className="text-4xl font-bold text-gray-800">
                        Welcome to AI Commerce
                      </h1>
                      <p className="mt-4 text-gray-600 text-lg">
                        Your intelligent e-commerce assistant
                      </p>
                      <div className="mt-8 flex justify-center gap-4">
                        <a
                          href="/products"
                          className="bg-blue-600 text-white px-6 py-3 rounded-lg hover:bg-blue-700"
                        >
                          Browse Products
                        </a>
                        <a
                          href="/agent"
                          className="bg-green-600 text-white px-6 py-3 rounded-lg hover:bg-green-700"
                        >
                          Chat with Agent
                        </a>
                      </div>
                    </div>
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
                path="/orders"
                element={
                  <PrivateRoute>
                    <OrderForm />
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
          </div>
        </div>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
