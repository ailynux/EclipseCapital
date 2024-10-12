// src/services/api.js

const API_URL = 'https://localhost:5241/api';

export const getAccount = async (userId) => {
  const response = await fetch(`${API_URL}/account/${userId}`);
  if (!response.ok) throw new Error('Failed to fetch account');
  return response.json();
};

export const createAccount = async (userId) => {
  const response = await fetch(`${API_URL}/account`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ userId }),
  });
  if (!response.ok) throw new Error('Failed to create account');
  return response.json();
};

export const getTransactions = async (userId) => {
  const response = await fetch(`${API_URL}/transaction/${userId}`);
  if (!response.ok) throw new Error('Failed to fetch transactions');
  return response.json();
};

export const createTransaction = async (userId, amount, type) => {
  const response = await fetch(`${API_URL}/transaction`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ userId, amount, type }),
  });
  if (!response.ok) throw new Error('Failed to create transaction');
  return response.json();
};