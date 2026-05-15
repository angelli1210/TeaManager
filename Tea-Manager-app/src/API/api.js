import axios from 'axios';

const BASE_URL = 'http://localhost:5028/api';

const api = axios.create({
  baseURL: BASE_URL,
  headers: { 'Content-Type': 'application/json' },
});

// Products
export const productService = {
  getAll: () => api.get('/products'),
  getById: (id) => api.get(`/products/${id}`),
  create: (data) => api.post('/products', data),
  update: (id, data) => api.put(`/products/${id}`, data),
  delete: (id) => api.delete(`/products/${id}`),
};

// Brands
export const brandService = {
  getAll: () => api.get('/brands'),
  getById: (id) => api.get(`/brands/${id}`),
  create: (data) => api.post('/brands', data),
  update: (id, data) => api.put(`/brands/${id}`, data),
  delete: (id) => api.delete(`/brands/${id}`),
};

// Suppliers
export const supplierService = {
  getAll: () => api.get('/suppliers'),
  getById: (id) => api.get(`/suppliers/${id}`),
  create: (data) => api.post('/suppliers', data),
  update: (id, data) => api.put(`/suppliers/${id}`, data),
  delete: (id) => api.delete(`/suppliers/${id}`),
};

// Supplier Orders
export const orderService = {
  getAll: () => api.get('/supplierorders'),
  getById: (id) => api.get(`/supplierorders/${id}`),
  create: (data) => api.post('/supplierorders', data),
  update: (id, data) => api.put(`/supplierorders/${id}`, data),
  delete: (id) => api.delete(`/supplierorders/${id}`),
};

export default api;
