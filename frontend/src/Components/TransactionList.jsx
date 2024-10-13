import React, { useState, useEffect } from 'react';
import { getTransactions } from '../Services/api';

const TransactionList = () => {
  const [transactions, setTransactions] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchTransactions = async () => {
      try {
        const user = JSON.parse(localStorage.getItem('user'));
        if (!user || !user.id) {
          throw new Error('User not found in local storage');
        }
        const data = await getTransactions(user.id);
        setTransactions(data);
      } catch (error) {
        console.error('Failed to fetch transactions:', error);
        setError(error.message);
      }
    };

    fetchTransactions();
  }, []);

  if (error) return <div>Error: {error}</div>;
  if (transactions.length === 0) return <div>No transactions found.</div>;

  return (
    <div>
      <h2>Transactions</h2>
      <ul>
        {transactions.map((transaction) => (
          <li key={transaction.id}>
            {transaction.type}: ${transaction.amount.toFixed(2)} - {new Date(transaction.createdAt).toLocaleString()}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TransactionList;