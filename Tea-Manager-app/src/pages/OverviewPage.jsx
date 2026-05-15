import { useEffect, useState } from 'react';
import { productService, brandService, supplierService, orderService } from '../API/api';

const StatCard = ({ label, value, sub, color, icon }) => (
  <div className="bg-white rounded-2xl p-5 border border-gray-100 shadow-sm card-hover">
    <div className="flex items-center justify-between mb-3">
      <p className="text-xs text-gray-400 font-semibold uppercase tracking-wide">{label}</p>
      <div className={`w-8 h-8 ${color} rounded-xl flex items-center justify-center`}>{icon}</div>
    </div>
    <p className="text-3xl font-extrabold text-gray-900">{value}</p>
    <p className="text-xs text-green-600 mt-1.5 font-medium">{sub}</p>
  </div>
);

export default function OverviewPage() {
  const [counts, setCounts] = useState({ products: 0, brands: 0, suppliers: 0, orders: 0 });
  const [recentProducts, setRecentProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  useEffect(() => {
    Promise.all([
      productService.getAll(),
      brandService.getAll(),
      supplierService.getAll(),
      orderService.getAll(),
    ])
      .then(([p, b, s, o]) => {
        setCounts({ products: p.data.length, brands: b.data.length, suppliers: s.data.length, orders: o.data.length });
        setRecentProducts(p.data.slice(0, 5));
      })
      .catch(() => setError('Failed to load dashboard data.'))
      .finally(() => setLoading(false));
  }, []);

  const stockColor = (stock) => {
    if (stock > 50) return 'bg-green-100 text-green-700';
    if (stock > 10) return 'bg-yellow-100 text-yellow-700';
    return 'bg-red-100 text-red-600';
  };

  //Greeting section
  const hour = new Date().getHours();//Greeting message based on current time
  const greeting = hour < 12 ? 'Good morning' : hour < 18 ? 'Good afternoon' : 'Good evening';
  return (
    <div>
      <div className="mb-6">
        <h2 className="text-xl font-bold text-gray-900">{greeting}, Admin !</h2>
        <p className="text-sm text-gray-500 mt-0.5">Here's what's happening with your tea business today.</p>
      </div>
      {error && <p className="text-red-500 text-sm mb-3">{error}</p>}
      {loading ? (
        <div className="flex items-center justify-center h-40 text-gray-400 text-sm">Loading...</div>
      ) : (
        <>
          <div className="grid grid-cols-4 gap-4 mb-6">
            <StatCard label="Products" value={counts.products} sub="Total products" color="bg-green-100"
              icon={<svg className="w-4 h-4 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" /></svg>}
            />
            <StatCard label="Brands" value={counts.brands} sub="Total brands" color="bg-blue-100"
              icon={<svg className="w-4 h-4 text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z" /></svg>}
            />
            <StatCard label="Suppliers" value={counts.suppliers} sub="Total suppliers" color="bg-purple-100"
              icon={<svg className="w-4 h-4 text-purple-600" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0" /></svg>}
            />
            <StatCard label="Orders" value={counts.orders} sub="Total orders" color="bg-orange-100"
              icon={<svg className="w-4 h-4 text-orange-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" /></svg>}
            />
          </div>

          <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-5">
            <h3 className="text-sm font-bold text-gray-800 mb-4">Recent Products</h3>
            <table className="w-full text-sm">
              <thead>
                <tr className="text-left text-xs text-gray-400 border-b border-gray-100">
                  <th className="pb-3 font-semibold">Product Name</th>
                  <th className="pb-3 font-semibold">Brand</th>
                  <th className="pb-3 font-semibold">Origin</th>
                  <th className="pb-3 font-semibold">Price</th>
                  <th className="pb-3 font-semibold">Stock</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-50">
                {recentProducts.map(p => (
                  <tr key={p.productId} className="hover:bg-gray-50 transition-colors">
                    <td className="py-3 font-medium text-gray-800">{p.productName}</td>
                    <td className="py-3 text-gray-500">{p.brandName}</td>
                    <td className="py-3 text-gray-500">{p.origin}</td>
                    <td className="py-3 font-semibold text-gray-800">${p.price.toFixed(2)}</td>
                    <td className="py-3">
                      <span className={`px-2.5 py-1 rounded-full text-xs font-semibold ${stockColor(p.stock)}`}>{p.stock}</span>
                    </td>
                  </tr>
                ))}
                {recentProducts.length === 0 && (
                  <tr>
                    <td colSpan={5} className="py-6 text-center text-gray-400 text-sm">No products yet.</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </>
      )}
    </div>
  );
}
