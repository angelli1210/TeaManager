import { useEffect, useState } from 'react';
import { productService, brandService, supplierService, orderService } from '../API/api';
import {
  BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip,
  ResponsiveContainer, Cell,
  PieChart, Pie,
} from 'recharts';

//Color palette
const PALETTE = [
  '#4ade80', '#60a5fa', '#c084fc', '#fb923c', '#2dd4bf',
  '#f472b6', '#818cf8', '#facc15', '#f87171', '#38bdf8',
  '#a3e635', '#f97316', '#e879f9', '#34d399', '#fb7185',
  '#93c5fd',
];

const stockFill = (stock) =>
  stock > 50 ? '#4ade80' : stock > 10 ? '#facc15' : '#f87171';

// ── Bar Chart custom tooltip ──
const CustomBarTooltip = ({ active, payload }) => {
  if (!active || !payload?.length) return null;
  const p = payload[0].payload;
  const statusColor = p.stock > 50 ? '#16a34a' : p.stock > 10 ? '#ca8a04' : '#dc2626';
  const statusBg = p.stock > 50 ? '#dcfce7' : p.stock > 10 ? '#fef9c3' : '#fee2e2';
  const statusText = p.stock > 50 ? 'High' : p.stock > 10 ? 'Medium' : 'Low';
  return (
    <div style={{
      background: 'white', border: '1px solid #e5e7eb',
      borderRadius: 14, padding: '12px 14px',
      boxShadow: '0 12px 32px rgba(0,0,0,0.12)',
      minWidth: 210, fontFamily: 'inherit',
    }}>
      <p style={{ fontWeight: 700, color: '#111827', marginBottom: 8, paddingBottom: 7, borderBottom: '1px solid #f3f4f6', fontSize: 13 }}>
        📦 {p.fullName}
      </p>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 5, fontSize: 12, color: '#6b7280' }}>
        {[
          ['Brand', p.brand],
          ['Origin', p.origin],
          ['Price', `$${p.price?.toFixed(2)}`],
        ].map(([label, val]) => (
          <div key={label} style={{ display: 'flex', justifyContent: 'space-between', gap: 16 }}>
            <span>{label}</span>
            <span style={{ color: '#111827', fontWeight: 600 }}>{val}</span>
          </div>
        ))}
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', gap: 16 }}>
          <span>Stock</span>
          <span style={{ background: statusBg, color: statusColor, fontWeight: 700, fontSize: 11, padding: '2px 8px', borderRadius: 99 }}>
            {p.stock} units · {statusText}
          </span>
        </div>
      </div>
    </div>
  );
};

//  Donut Chart custom tooltip
const CustomDonutTooltip = ({ active, payload }) => {
  if (!active || !payload?.length) return null;
  const d = payload[0].payload;
  const color = PALETTE[d.index % PALETTE.length];
  return (
    <div style={{
      background: 'white', border: '1px solid #e5e7eb',
      borderRadius: 14, padding: '12px 14px',
      boxShadow: '0 12px 32px rgba(0,0,0,0.12)',
      minWidth: 195, fontFamily: 'inherit',
    }}>
      <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 8, paddingBottom: 7, borderBottom: '1px solid #f3f4f6' }}>
        <span style={{ width: 10, height: 10, borderRadius: '50%', background: color, flexShrink: 0, display: 'inline-block' }} />
        <span style={{ fontSize: 13, fontWeight: 700, color: '#111827' }}>🌍 {d.country}</span>
      </div>
      <div style={{ display: 'flex', flexDirection: 'column', gap: 5, fontSize: 12, color: '#6b7280' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', gap: 16 }}>
          <span>Brand{d.brandNames.length > 1 ? 's' : ''}</span>
          <span style={{ color: '#111827', fontWeight: 600 }}>{d.brandNames.join(', ')}</span>
        </div>
        <div style={{ display: 'flex', justifyContent: 'space-between', gap: 16 }}>
          <span>Count</span>
          <span style={{ color: '#111827', fontWeight: 600 }}>{d.value} brand{d.value > 1 ? 's' : ''}</span>
        </div>
      </div>
    </div>
  );
};

//  StatCard
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
  const [recentProducts, setRecent] = useState([]);
  const [stockData, setStockData] = useState([]);
  const [countryData, setCountryData] = useState([]);
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
        setCounts({
          products: p.data.length,
          brands: b.data.length,
          suppliers: s.data.length,
          orders: o.data.length,
        });
        setRecent(p.data.slice(0, 5));

        // Bar chart: Stock by product
        setStockData(p.data.map(prod => ({
          brand: prod.brandName,
          fullName: prod.productName,
          stock: prod.stock,
          price: prod.price,
          origin: prod.origin,
        })));

        // Donut chart: Brand distribution by country
        const map = {};
        b.data.forEach(brand => {
          const key = brand.country;
          if (!map[key]) map[key] = { country: key, value: 0, brandNames: [] };
          map[key].value++;
          map[key].brandNames.push(brand.brandName);
        });
        setCountryData(Object.values(map).map((d, i) => ({ ...d, index: i })));
      })
      .catch(() => setError('Failed to load dashboard data.'))
      .finally(() => setLoading(false));
  }, []);

  const stockColor = (stock) => {
    if (stock > 50) return 'bg-green-100 text-green-700';
    if (stock > 10) return 'bg-yellow-100 text-yellow-700';
    return 'bg-red-100 text-red-600';
  };

  const hour = new Date().getHours();
  const greeting = hour < 12 ? 'Good morning' : hour < 18 ? 'Good afternoon' : 'Good evening';

  return (
    <div>
      {/* Greeting */}
      <div className="mb-6">
        <h2 className="text-xl font-bold text-gray-900">{greeting}, Admin !</h2>
        <p className="text-sm text-gray-500 mt-0.5">Here's what's happening with your tea business today.</p>
      </div>

      {error && <p className="text-red-500 text-sm mb-3">{error}</p>}

      {loading ? (
        <div className="flex items-center justify-center h-40 text-gray-400 text-sm">Loading...</div>
      ) : (
        <>
          {/*StatCards*/}
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

          {/*Charts Row */}
          <div className="grid grid-cols-3 gap-4 mb-6">

            {/* Bar Chart */}
            <div className="col-span-2 bg-white rounded-2xl border border-gray-100 shadow-sm px-5 pt-5 pb-3 flex flex-col">
              <div className="flex items-center justify-between mb-2 flex-shrink-0">
                <div>
                  <h3 className="text-sm font-bold text-gray-800">Product Stock Levels</h3>
                  <p className="text-xs text-gray-400 mt-0.5">Current inventory per product</p>
                </div>
                <div className="flex items-center gap-3 text-xs text-gray-400">
                  {[['bg-green-300', 'High (>50)'], ['bg-yellow-300', 'Mid (11–50)'], ['bg-red-300', 'Low (≤10)']].map(([bg, label]) => (
                    <span key={label} className="flex items-center gap-1.5">
                      <span className={`w-2.5 h-2.5 rounded-sm ${bg} inline-block`} />{label}
                    </span>
                  ))}
                </div>
              </div>
              <div className="flex-1" style={{ minHeight: 220, marginTop: 8 }}>
                <ResponsiveContainer width="100%" height="100%">
                  <BarChart data={stockData} margin={{ top: 4, right: 4, left: -10, bottom: 30 }}>
                    <CartesianGrid strokeDasharray="3 3" stroke="#f3f4f6" vertical={false} />
                    <XAxis
                      dataKey="brand"
                      tick={{ fontSize: 10, fill: '#9ca3af' }}
                      tickLine={false}
                      axisLine={false}
                      angle={-30}
                      textAnchor="end"
                      interval={0}
                    />
                    <YAxis
                      tick={{ fontSize: 10, fill: '#9ca3af' }}
                      tickLine={false}
                      axisLine={false}
                      tickCount={8}
                    />
                    <Tooltip content={<CustomBarTooltip />} cursor={{ fill: 'rgba(0,0,0,0.04)', radius: 8 }} />
                    <Bar dataKey="stock" radius={[8, 8, 0, 0]} maxBarSize={48}>
                      {stockData.map((entry, i) => (
                        <Cell key={i} fill={stockFill(entry.stock)} />
                      ))}
                    </Bar>
                  </BarChart>
                </ResponsiveContainer>
              </div>
            </div>

            {/* Donut Chart */}
            <div className="bg-white rounded-2xl border border-gray-100 shadow-sm p-5 flex flex-col">
              <div className="mb-3 flex-shrink-0">
                <h3 className="text-sm font-bold text-gray-800">Brands by Country</h3>
                <p className="text-xs text-gray-400 mt-0.5">Geographic distribution</p>
              </div>
              <div className="flex-1 relative" style={{ minHeight: 160 }}>
                <ResponsiveContainer width="100%" height="100%">
                  <PieChart>
                    <Pie
                      data={countryData}
                      dataKey="value"
                      innerRadius="55%"
                      outerRadius="80%"
                      paddingAngle={2}
                      stroke="none"
                    >
                      {countryData.map((entry, i) => (
                        <Cell key={i} fill={PALETTE[i % PALETTE.length]} />
                      ))}
                    </Pie>
                    <Tooltip content={<CustomDonutTooltip />} wrapperStyle={{ zIndex: 50 }} />
                  </PieChart>
                </ResponsiveContainer>
                {/* Center label as HTML overlay (more reliable than Recharts <Label>) */}
                <div className="absolute inset-0 flex flex-col items-center justify-center pointer-events-none" style={{ zIndex: 1 }}>
                  <span className="text-[22px] font-bold text-gray-900 leading-none">
                    {countryData.length}
                  </span>
                  <span className="text-[11px] text-gray-400 mt-1">countries</span>
                </div>
              </div>
              {/* Custom legend */}
              <div className="mt-3 grid grid-cols-2 gap-x-3 gap-y-1.5 flex-shrink-0">
                {countryData.map((d, i) => (
                  <div key={d.country} className="flex items-center gap-1.5">
                    <span
                      style={{ background: PALETTE[i % PALETTE.length] }}
                      className="w-2 h-2 rounded-full flex-shrink-0 inline-block"
                    />
                    <span className="text-[10px] text-gray-500 truncate">{d.country}</span>
                  </div>
                ))}
              </div>
            </div>
          </div>

          {/* Recent Products Table */}
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
                      <span className={`px-2.5 py-1 rounded-full text-xs font-semibold ${stockColor(p.stock)}`}>
                        {p.stock}
                      </span>
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
