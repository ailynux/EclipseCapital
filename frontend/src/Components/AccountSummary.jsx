import React, { useState, useEffect } from 'react';
import { getAccount } from '../Services/api';

const AccountSummary = () => {
  const [account, setAccount] = useState(null);

  useEffect(() => {
    const fetchAccount = async () => {
      try {
        const data = await getAccount('user123'); // Replace with actual user ID
        setAccount(data);
      } catch (error) {
        console.error('Failed to fetch account:', error);
      }
    };

    fetchAccount();
  }, []);

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