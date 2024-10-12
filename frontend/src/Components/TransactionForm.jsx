import React, { useState } from 'react';
import { createTransaction } from '../Services/api';

const TransactionForm = () => {
  const [amount, setAmount] = useState('');
  const [type, setType] = useState('deposit');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createTransaction('user123', parseFloat(amount), type); // Replace with actual user ID
      setAmount('');
      setType('deposit');
      // You might want to refresh the transaction list or account summary here
      alert('Transaction created successfully!');
    } catch (error) {
      console.error('Failed to create transaction:', error);
      alert('Failed to create transaction. Please try again.');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="bg-white shadow rounded-lg p-6">
      <h2 className="text-xl font-semibold mb-4">New Transaction</h2>
      <div className="mb-4">
        <label htmlFor="amount" className="block text-sm font-medium text-gray-700 mb-2">Amount: </label>
        <input
          type="number"
          id="amount"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          required
          min="0.01"
          step="0.01"
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        />
      </div>
      <div className="mb-4">
        <label htmlFor="type" className="block text-sm font-medium text-gray-700 mb-2">Type: </label>
        <select
          id="type"
          value={type}
          onChange={(e) => setType(e.target.value)}
          className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
        >
          <option value="deposit">Deposit</option>
          <option value="withdrawal">Withdrawal</option>
        </select>
      </div>
      <button 
        type="submit"
        className="w-full bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
      >
        Submit Transaction
      </button>
    </form>
  );
};

export default TransactionForm;