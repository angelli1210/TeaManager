import { useEffect, useState } from 'react';
import { orderService, productService, supplierService } from '../API/api';
import ConfirmModal from '../components/common/ConfirmModal';

const emptyForm = { supplierOrderId: '', productId: '', supplierId: '', quantity: '', orderDate: '' };

export default function OrdersPage() {
  const [orders, setOrders] = useState([]);
  const [products, setProducts] = useState([]);
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
    Promise.all([orderService.getAll(), productService.getAll(), supplierService.getAll()])
      .then(([o, p, s]) => { setOrders(o.data); setProducts(p.data); setSuppliers(s.data); })
      .catch(() => setError('Failed to load orders.'))
      .finally(() => setLoading(false));
  };

  useEffect(() => { fetchAll(); }, []);

  const validate = () => {
    const e = {};
    if (!form.supplierOrderId) e.supplierOrderId = 'Required';
    if (!form.productId) e.productId = 'Select a product';
    if (!form.supplierId) e.supplierId = 'Select a supplier';
    if (!form.quantity || form.quantity <= 0) e.quantity = 'Must be > 0';
    if (!form.orderDate) e.orderDate = 'Required';
    else if (new Date(form.orderDate) > new Date()) e.orderDate = 'Cannot be in the future';
    setFormErrors(e);
    return Object.keys(e).length === 0;
  };

  const openCreate = () => { setForm(emptyForm); setFormErrors({}); setEditingId(null); setModalOpen(true); };
  const openEdit = (o) => {
    setForm({ supplierOrderId: o.supplierOrderId, productId: o.productId, supplierId: o.supplierId, quantity: o.quantity, orderDate: o.orderDate?.substring(0, 10) || '' });
    setFormErrors({}); setEditingId(o.supplierOrderId); setModalOpen(true);
  };

  const handleSave = async () => {
    if (!validate()) return;
    setSaving(true);
    try {
      const payload = { ...form, supplierOrderId: Number(form.supplierOrderId), productId: Number(form.productId), supplierId: Number(form.supplierId), quantity: Number(form.quantity) };
      if (editingId) await orderService.update(editingId, payload);
      else await orderService.create(payload);
      setModalOpen(false);
      fetchAll();
    } catch (err) {
      setFormErrors({ api: err.response?.data?.message || 'Save failed.' });
    } finally { setSaving(false); }
  };

  const handleDelete = async () => {
    try {
      await orderService.delete(deleteModal.id);
      setDeleteModal({ open: false, id: null, name: '' });
      fetchAll();
    } catch { setError('Delete failed.'); }
  };

  const filtered = orders.filter(o => (o.productName || '').toLowerCase().includes(search.toLowerCase()) || (o.supplierName || '').toLowerCase().includes(search.toLowerCase()));

  return (
    <div>
      <div className="flex items-center justify-between mb-5">
        <div>
          <h2 className="text-xl font-bold text-gray-900">Supplier Orders</h2>
          <p className="text-sm text-gray-400 mt-0.5">Track all incoming orders</p>
        </div>
        <button onClick={openCreate} className="bg-green-600 hover:bg-green-700 text-white text-sm font-semibold px-5 py-2.5 rounded-xl transition-all flex items-center gap-2">
          <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" /></svg>
          New Order
        </button>
      </div>

      {error && <p className="text-red-500 text-sm mb-3">{error}</p>}

      <div className="bg-white rounded-2xl border border-gray-100 shadow-sm overflow-hidden">
        <div className="p-4 border-b border-gray-100 flex items-center justify-between">
          <input value={search} onChange={e => setSearch(e.target.value)} type="text" placeholder="Search orders..." className="w-64 text-sm border border-gray-200 rounded-xl px-3 py-2 focus:outline-none focus:ring-2 focus:ring-green-500" />
          <span className="text-xs text-gray-400">{filtered.length} orders</span>
        </div>
        {loading ? <div className="p-10 text-center text-gray-400 text-sm">Loading...</div> : (
          <table className="w-full text-sm">
            <thead>
              <tr className="text-left text-xs text-gray-400 bg-gray-50 border-b border-gray-100">
                {['ID', 'Product', 'Supplier', 'Qty', 'Date', 'Actions'].map(h => <th key={h} className="px-5 py-3 font-semibold">{h}</th>)}
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-50">
              {filtered.map(o => (
                <tr key={o.supplierOrderId} className="hover:bg-gray-50 transition-colors">
                  <td className="px-5 py-3.5 text-gray-400 text-xs">#{o.supplierOrderId}</td>
                  <td className="px-5 py-3.5 font-semibold text-gray-800">{o.productName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{o.supplierName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{o.quantity}</td>
                  <td className="px-5 py-3.5 text-gray-400 text-xs">{o.orderDate?.substring(0, 10)}</td>
                  <td className="px-5 py-3.5">
                    <div className="flex gap-3">
                      <button onClick={() => openEdit(o)} className="text-blue-500 hover:text-blue-700 text-xs font-semibold">Edit</button>
                      <button onClick={() => setDeleteModal({ open: true, id: o.supplierOrderId, name: `Order #${o.supplierOrderId}` })} className="text-red-400 hover:text-red-600 text-xs font-semibold">Delete</button>
                    </div>
                  </td>
                </tr>
              ))}
              {filtered.length === 0 && <tr><td colSpan={6} className="px-5 py-10 text-center text-gray-400 text-sm">No orders found.</td></tr>}
            </tbody>
          </table>
        )}
      </div>

      {modalOpen && (
        <div className="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center z-50">
          <div className="bg-white rounded-3xl shadow-2xl w-full max-w-lg p-7 mx-4 max-h-[90vh] overflow-y-auto">
            <div className="flex items-center justify-between mb-6">
              <h3 className="text-lg font-bold text-gray-900">{editingId ? 'Edit Order' : 'New Supplier Order'}</h3>
              <button onClick={() => setModalOpen(false)} className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center">
                <svg className="w-4 h-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" /></svg>
              </button>
            </div>
            {formErrors.api && <p className="text-red-500 text-sm mb-4">{formErrors.api}</p>}
            <div className="grid grid-cols-2 gap-4">
              {!editingId && (
                <div className="col-span-2">
                  <label className="block text-xs font-semibold text-gray-600 mb-1.5">Order ID <span className="text-red-400">*</span></label>
                  <input type="number" value={form.supplierOrderId} onChange={e => setForm({ ...form, supplierOrderId: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" />
                  {formErrors.supplierOrderId && <p className="text-red-400 text-xs mt-1">{formErrors.supplierOrderId}</p>}
                </div>
              )}
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Product <span className="text-red-400">*</span></label>
                <select value={form.productId} onChange={e => setForm({ ...form, productId: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500">
                  <option value="">Select product...</option>
                  {products.map(p => <option key={p.productId} value={p.productId}>{p.productName}</option>)}
                </select>
                {formErrors.productId && <p className="text-red-400 text-xs mt-1">{formErrors.productId}</p>}
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
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Quantity <span className="text-red-400">*</span></label>
                <input type="number" value={form.quantity} onChange={e => setForm({ ...form, quantity: e.target.value })} min="1" className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" placeholder="0" />
                {formErrors.quantity && <p className="text-red-400 text-xs mt-1">{formErrors.quantity}</p>}
              </div>
              <div>
                <label className="block text-xs font-semibold text-gray-600 mb-1.5">Order Date <span className="text-red-400">*</span></label>
                <input type="date" value={form.orderDate} min="2020-01-01" max={new Date().toISOString().split('T')[0]} onChange={e => setForm({ ...form, orderDate: e.target.value })} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" />
                {formErrors.orderDate && <p className="text-red-400 text-xs mt-1">{formErrors.orderDate}</p>}
              </div>
            </div>
            <div className="flex justify-end gap-3 mt-6">
              <button onClick={() => setModalOpen(false)} className="px-5 py-2.5 text-sm font-semibold text-gray-600 border border-gray-200 rounded-xl hover:bg-gray-50">Cancel</button>
              <button onClick={handleSave} disabled={saving} className="px-6 py-2.5 text-sm font-semibold bg-green-600 text-white rounded-xl hover:bg-green-700 disabled:opacity-50">
                {saving ? 'Saving...' : 'Save Order'}
              </button>
            </div>
          </div>
        </div>
      )}

      <ConfirmModal isOpen={deleteModal.open} itemName={deleteModal.name} onConfirm={handleDelete} onCancel={() => setDeleteModal({ open: false, id: null, name: '' })} />
    </div>
  );
}
