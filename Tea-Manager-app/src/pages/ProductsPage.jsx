import { useEffect, useState } from 'react';
import { productService, brandService, supplierService } from '../API/api';
import ConfirmModal from '../components/common/ConfirmModal';

const emptyForm = { productId: '', productName: '', description: '', price: '', stock: '', harvestYear: '', origin: '', brandId: '', supplierId: '' };

export default function ProductsPage() {
  const [products, setProducts] = useState([]);
  const [brands, setBrands] = useState([]);
  const [suppliers, setSuppliers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [search, setSearch] = useState('');

  const [modalOpen, setModalOpen] = useState(false);
  const [editingId, setEditingId] = useState(null);
  const [form, setForm] = useState(emptyForm);
  const [formErrors, setFormErrors] = useState({});
  const [saving, setSaving] = useState(false);

  const [deleteModal, setDeleteModal] = useState({ open: false, id: null, name: '' });

  const fetchAll = () => {
    setLoading(true);
    Promise.all([productService.getAll(), brandService.getAll(), supplierService.getAll()])
      .then(([p, b, s]) => { setProducts(p.data); setBrands(b.data); setSuppliers(s.data); })
      .catch(() => setError('Failed to load data.'))
      .finally(() => setLoading(false));
  };

  useEffect(() => { fetchAll(); }, []);

  const validate = () => {
    const e = {};
    if (!form.productId) e.productId = 'Required';
    if (!form.productName || form.productName.length < 2) e.productName = 'Min 2 characters';
    if (!form.description) e.description = 'Required';
    if (!form.price || form.price <= 0) e.price = 'Must be > 0';
    if (form.stock === '' || form.stock < 0) e.stock = 'Must be ≥ 0';
    if (!form.harvestYear || form.harvestYear < 1900 || form.harvestYear > 2100) e.harvestYear = '1900–2100';
    if (!form.origin) e.origin = 'Required';
    if (!form.brandId) e.brandId = 'Select a brand';
    if (!form.supplierId) e.supplierId = 'Select a supplier';
    setFormErrors(e);
    return Object.keys(e).length === 0;
  };

  const openCreate = () => { setForm(emptyForm); setFormErrors({}); setEditingId(null); setModalOpen(true); };
  const openEdit = (p) => {
    setForm({ productId: p.productId, productName: p.productName, description: p.description, price: p.price, stock: p.stock, harvestYear: p.harvestYear, origin: p.origin, brandId: p.brandId, supplierId: p.supplierId });
    setFormErrors({}); setEditingId(p.productId); setModalOpen(true);
  };

  const handleSave = async () => {
    if (!validate()) return;
    setSaving(true);
    try {
      const payload = { ...form, productId: Number(form.productId), price: Number(form.price), stock: Number(form.stock), harvestYear: Number(form.harvestYear), brandId: Number(form.brandId), supplierId: Number(form.supplierId) };
      if (editingId) await productService.update(editingId, payload);
      else await productService.create(payload);
      setModalOpen(false);
      fetchAll();
    } catch (err) {
      setFormErrors({ api: err.response?.data?.message || 'Save failed.' });
    } finally { setSaving(false); }
  };

  const handleDelete = async () => {
    try {
      await productService.delete(deleteModal.id);
      setDeleteModal({ open: false, id: null, name: '' });
      fetchAll();
    } catch { setError('Delete failed.'); }
  };

  const stockColor = (s) => s > 50 ? 'bg-green-100 text-green-700' : s > 10 ? 'bg-yellow-100 text-yellow-700' : 'bg-red-100 text-red-600';
  const filtered = products.filter(p => p.productName.toLowerCase().includes(search.toLowerCase()) || p.origin?.toLowerCase().includes(search.toLowerCase()));

  return (
    <div>
      <div className="flex items-center justify-between mb-5">
        <div>
          <h2 className="text-xl font-bold text-gray-900">Products</h2>
          <p className="text-sm text-gray-400 mt-0.5">Manage your tea product catalog</p>
        </div>
        <button onClick={openCreate} className="bg-green-600 hover:bg-green-700 text-white text-sm font-semibold px-5 py-2.5 rounded-xl transition-all flex items-center gap-2">
          <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" /></svg>
          Add Product
        </button>
      </div>

      {error && <p className="text-red-500 text-sm mb-3">{error}</p>}

      <div className="bg-white rounded-2xl border border-gray-100 shadow-sm overflow-hidden">
        <div className="p-4 border-b border-gray-100 flex items-center justify-between">
          <input value={search} onChange={e => setSearch(e.target.value)} type="text" placeholder="Search products..." className="w-64 text-sm border border-gray-200 rounded-xl px-3 py-2 focus:outline-none focus:ring-2 focus:ring-green-500" />
          <span className="text-xs text-gray-400">{filtered.length} products</span>
        </div>

        {loading ? (
          <div className="p-10 text-center text-gray-400 text-sm">Loading...</div>
        ) : (
          <table className="w-full text-sm">
            <thead>
              <tr className="text-left text-xs text-gray-400 bg-gray-50 border-b border-gray-100">
                {['ID', 'Product Name', 'Brand', 'Supplier', 'Origin', 'Price', 'Stock', 'Actions'].map(h => (
                  <th key={h} className="px-5 py-3 font-semibold">{h}</th>
                ))}
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-50">
              {filtered.map(p => (
                <tr key={p.productId} className="hover:bg-gray-50 transition-colors">
                  <td className="px-5 py-3.5 text-gray-400 text-xs">#{p.productId}</td>
                  <td className="px-5 py-3.5 font-semibold text-gray-800">{p.productName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{p.brandName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{p.supplierName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{p.origin}</td>
                  <td className="px-5 py-3.5 font-semibold text-gray-800">${p.price.toFixed(2)}</td>
                  <td className="px-5 py-3.5"><span className={`px-2.5 py-1 rounded-full text-xs font-semibold ${stockColor(p.stock)}`}>{p.stock}</span></td>
                  <td className="px-5 py-3.5">
                    <div className="flex gap-3">
                      <button onClick={() => openEdit(p)} className="text-blue-500 hover:text-blue-700 text-xs font-semibold">Edit</button>
                      <button onClick={() => setDeleteModal({ open: true, id: p.productId, name: p.productName })} className="text-red-400 hover:text-red-600 text-xs font-semibold">Delete</button>
                    </div>
                  </td>
                </tr>
              ))}
              {filtered.length === 0 && <tr><td colSpan={8} className="px-5 py-10 text-center text-gray-400 text-sm">No products found.</td></tr>}
            </tbody>
          </table>
        )}
      </div>

      {/* Create / Edit Modal */}
      {modalOpen && (
        <div className="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center z-50">
          <div className="bg-white rounded-3xl shadow-2xl w-full max-w-lg p-7 mx-4 max-h-[90vh] overflow-y-auto">
            <div className="flex items-center justify-between mb-6">
              <h3 className="text-lg font-bold text-gray-900">{editingId ? 'Edit Product' : 'Add New Product'}</h3>
              <button onClick={() => setModalOpen(false)} className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center">
                <svg className="w-4 h-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" /></svg>
              </button>
            </div>
            {formErrors.api && <p className="text-red-500 text-sm mb-4">{formErrors.api}</p>}
            <div className="grid grid-cols-2 gap-4">
              {!editingId && (
                <div>
                  <label className="block text-xs font-semibold text-gray-600 mb-1.5">Product ID <span className="text-red-400">*</span></label>
                  <input type="number" value={form.productId} onChange={e => setForm({ ...form, productId: e.target.value })} min="1" className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" />
                  {formErrors.productId && <p className="text-red-400 text-xs mt-1">{formErrors.productId}</p>}
                </div>
              )}
              <div className={editingId ? 'col-span-2' : ''}>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Product Name <span className="text-red-400">*</span></label>
                <input type="text" value={form.productName} onChange={e => setForm({ ...form, productName: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" placeholder="e.g. Dragon Well Premium" />
                {formErrors.productName && <p className="text-red-400 text-xs mt-1">{formErrors.productName}</p>}
              </div>
              <div className="col-span-2">
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Description <span className="text-red-400">*</span></label>
                <textarea rows={2} value={form.description} onChange={e => setForm({ ...form, description: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500 resize-none" placeholder="Product description..." />
                {formErrors.description && <p className="text-red-400 text-xs mt-1">{formErrors.description}</p>}
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Price ($) <span className="text-red-400">*</span></label>
                <input type="number" step="0.01" min="0.01" value={form.price} onChange={e => setForm({ ...form, price: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" placeholder="0.00" />
                {formErrors.price && <p className="text-red-400 text-xs mt-1">{formErrors.price}</p>}
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Stock <span className="text-red-400">*</span></label>
                <input type="number" min="0" value={form.stock} onChange={e => setForm({ ...form, stock: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" placeholder="0" />
                {formErrors.stock && <p className="text-red-400 text-xs mt-1">{formErrors.stock}</p>}
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Brand <span className="text-red-400">*</span></label>
                <select value={form.brandId} onChange={e => setForm({ ...form, brandId: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500">
                  <option value="">Select brand...</option>
                  {brands.map(b => <option key={b.brandId} value={b.brandId}>{b.brandName}</option>)}
                </select>
                {formErrors.brandId && <p className="text-red-400 text-xs mt-1">{formErrors.brandId}</p>}
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Supplier <span className="text-red-400">*</span></label>
                <select value={form.supplierId} onChange={e => setForm({ ...form, supplierId: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500">
                  <option value="">Select supplier...</option>
                  {suppliers.map(s => <option key={s.supplierId} value={s.supplierId}>{s.supplierName}</option>)}
                </select>
                {formErrors.supplierId && <p className="text-red-400 text-xs mt-1">{formErrors.supplierId}</p>}
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Origin <span className="text-red-400">*</span></label>
                <input type="text" value={form.origin} onChange={e => setForm({ ...form, origin: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" placeholder="e.g. Hangzhou, China" />
                {formErrors.origin && <p className="text-red-400 text-xs mt-1">{formErrors.origin}</p>}
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Harvest Year <span className="text-red-400">*</span></label>
                <input type="number" value={form.harvestYear} onChange={e => setForm({ ...form, harvestYear: e.target.value })} min="1900" max="2026" className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" placeholder="2024" />
                {formErrors.harvestYear && <p className="text-red-400 text-xs mt-1">{formErrors.harvestYear}</p>}
              </div>
            </div>
            <div className="flex justify-end gap-3 mt-6">
              <button onClick={() => setModalOpen(false)} className="px-5 py-2.5 text-sm font-semibold text-gray-600 border border-gray-200 rounded-xl hover:bg-gray-50">Cancel</button>
              <button onClick={handleSave} disabled={saving} className="px-6 py-2.5 text-sm font-semibold bg-green-600 text-white rounded-xl hover:bg-green-700 disabled:opacity-50">
                {saving ? 'Saving...' : 'Save Product'}
              </button>
            </div>
          </div>
        </div>
      )}

      <ConfirmModal isOpen={deleteModal.open} itemName={deleteModal.name} onConfirm={handleDelete} onCancel={() => setDeleteModal({ open: false, id: null, name: '' })} />
    </div>
  );
}
