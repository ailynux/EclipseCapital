import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Link, Navigate } from 'react-router-dom';
import Dashboard from './Components/Dashboard';
import AccountSummary from './Components/AccountSummary';
import TransactionList from './Components/TransactionList';
import TransactionForm from './Components/TransactionForm';
import Login from './Components/Login';
import Register from './Components/Register';
import { checkAuthStatus } from './Services/api';
import './App.css';

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const verifyAuth = async () => {
      try {
        const userData = await checkAuthStatus();
        setIsAuthenticated(true);
        setUser(userData);
      } catch (error) {
        console.error('Authentication check failed:', error);
        setIsAuthenticated(false);
        setUser(null);
      } finally {
        setLoading(false);
      }
    };

    verifyAuth();
  }, []);

  const handleLogout = async () => {
    // Implement logout logic here (e.g., calling a logout API endpoint)
    setIsAuthenticated(false);
    setUser(null);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <Router>
      <div className="App">
        <nav className="bg-blue-600 p-4">
          <ul className="flex space-x-4 justify-center">
            <li><Link to="/" className="text-white hover:text-blue-200">Dashboard</Link></li>
            {isAuthenticated ? (
              <>
                <li><Link to="/account" className="text-white hover:text-blue-200">Account</Link></li>
                <li><Link to="/transactions" className="text-white hover:text-blue-200">Transactions</Link></li>
                <li><Link to="/new-transaction" className="text-white hover:text-blue-200">New Transaction</Link></li>
                <li><button onClick={handleLogout} className="text-white hover:text-blue-200">Logout</button></li>
              </>
            ) : (
              <>
                <li><Link to="/login" className="text-white hover:text-blue-200">Login</Link></li>
                <li><Link to="/register" className="text-white hover:text-blue-200">Register</Link></li>
              </>
            )}
          </ul>
        </nav>

        <Routes>
          <Route path="/login" element={<Login setIsAuthenticated={setIsAuthenticated} setUser={setUser} />} />
          <Route path="/register" element={<Register />} />
          <Route
            path="/"
            element={isAuthenticated ? <Dashboard user={user} /> : <Navigate to="/login" />}
          />
          <Route
            path="/account"
            element={isAuthenticated ? <AccountSummary user={user} /> : <Navigate to="/login" />}
          />
          <Route
            path="/transactions"
            element={isAuthenticated ? <TransactionList user={user} /> : <Navigate to="/login" />}
          />
          <Route
            path="/new-transaction"
            element={isAuthenticated ? <TransactionForm user={user} /> : <Navigate to="/login" />}
          />
        </Routes>
      </div>
    </Router>
  );
}

export default App;