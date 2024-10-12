import React from 'react';
import AccountSummary from './AccountSummary';
import TransactionList from './TransactionList';
import TransactionForm from './TransactionForm';

const Dashboard = () => {
  return (
    <div className="p-6 max-w-4xl mx-auto">
      <h1 className="text-3xl font-bold mb-6 text-center">EclipseCapital Dashboard</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <AccountSummary />
        <TransactionForm />
      </div>
      <TransactionList />
    </div>
  );
};

export default Dashboard;