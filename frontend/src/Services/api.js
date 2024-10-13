const API_URL = 'https://localhost:5241/api';

// Handle the response from the API and check for errors
const handleResponse = async (response) => {
  const contentType = response.headers.get('content-type');
  let responseBody = null;

  if (contentType && contentType.includes('application/json')) {
    responseBody = await response.json(); // Parse JSON if response is in JSON format
  } else {
    responseBody = await response.text(); // Fallback to text if not JSON
  }

  if (!response.ok) {
    console.error(`API call failed: ${response.status} ${response.statusText}`, responseBody);
    throw new Error(responseBody || `API call failed: ${response.status} ${response.statusText}`);
  }

  return responseBody;
};

// Retrieve the token from sessionStorage and set Authorization header
const getAuthHeader = () => {
  const token = sessionStorage.getItem('token');
  return token ? { 'Authorization': `Bearer ${token}` } : {};
};

// Login function to handle login requests
export const login = async (username, password) => {
  try {
    const response = await fetch(`${API_URL}/auth/login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username, password }),
    });
    const data = await handleResponse(response);
    sessionStorage.setItem('token', data.token); // Store the token
    return data;
  } catch (error) {
    console.error('Login failed:', error.message);
    throw new Error('Login failed. Please check your credentials.');
  }
};

// Register a new user
export const register = async (username, password) => {
  try {
    const response = await fetch(`${API_URL}/auth/register`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username, password }),
    });
    return handleResponse(response);
  } catch (error) {
    console.error('Registration failed:', error.message);
    throw new Error('Registration failed. Please try again.');
  }
};

// Check if the user is authenticated
export const checkAuthStatus = async () => {
  try {
    const response = await fetch(`${API_URL}/auth/status`, {
      headers: getAuthHeader(),
    });
    return handleResponse(response);
  } catch (error) {
    console.error('Auth status check failed:', error.message);
    throw new Error('Authentication check failed. Please log in.');
  }
};

// Fetch the user's account details
export const getAccount = async () => {
  try {
    const response = await fetch(`${API_URL}/account`, {
      headers: getAuthHeader(),
    });
    return handleResponse(response);
  } catch (error) {
    console.error('Fetching account failed:', error.message);
    throw new Error('Failed to retrieve account details.');
  }
};

// Fetch the user's transactions
export const getTransactions = async () => {
  try {
    const response = await fetch(`${API_URL}/transaction`, {
      headers: getAuthHeader(),
    });
    return handleResponse(response);
  } catch (error) {
    console.error('Fetching transactions failed:', error.message);
    throw new Error('Failed to retrieve transactions.');
  }
};

// Create a new transaction
export const createTransaction = async (amount, type) => {
  try {
    const response = await fetch(`${API_URL}/transaction`, {
      method: 'POST',
      headers: {
        ...getAuthHeader(),
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ amount, type }),
    });
    return handleResponse(response);
  } catch (error) {
    console.error('Transaction creation failed:', error.message);
    throw new Error('Failed to create transaction.');
  }
};
