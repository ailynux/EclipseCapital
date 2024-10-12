import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Dashboard from './Components/Dashboard';
import AccountSummary from './Components/AccountSummary';
import TransactionList from './Components/TransactionList';
import TransactionForm from './Components/TransactionForm';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <nav className="bg-blue-600 p-4">
          <ul className="flex space-x-4 justify-center">
            <li><Link to="/" className="text-white hover:text-blue-200">Dashboard</Link></li>
            <li><Link to="/account" className="text-white hover:text-blue-200">Account</Link></li>
            <li><Link to="/transactions" className="text-white hover:text-blue-200">Transactions</Link></li>
            <li><Link to="/new-transaction" className="text-white hover:text-blue-200">New Transaction</Link></li>
          </ul>
        </nav>

        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/account" element={<AccountSummary />} />
          <Route path="/transactions" element={<TransactionList />} />
          <Route path="/new-transaction" element={<TransactionForm />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;