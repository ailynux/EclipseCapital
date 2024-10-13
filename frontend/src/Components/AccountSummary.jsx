import React, { useState, useEffect } from 'react';
import { getAccount } from '../Services/api';

const AccountSummary = () => {
  const [account, setAccount] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchAccount = async () => {
      try {
        const user = JSON.parse(localStorage.getItem('user'));
        if (!user || !user.id) {
          throw new Error('User not found in local storage');
        }
        const data = await getAccount(user.id);
        setAccount(data);
      } catch (error) {
        console.error('Failed to fetch account:', error);
        setError(error.message);
      }
    };

    fetchAccount();
  }, []);

  if (error) return <div>Error: {error}</div>;
  if (!account) return <div>Loading account information...</div>;

  return (
    <div>
      <h2>Account Summary</h2>
      <p>Balance: ${account.balance.toFixed(2)}</p>
      <p>Last Updated: {new Date(account.updatedAt).toLocaleString()}</p>
    </div>
  );
};

export default AccountSummary;