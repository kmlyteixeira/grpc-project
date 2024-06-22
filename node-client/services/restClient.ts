import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5117',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default apiClient;
