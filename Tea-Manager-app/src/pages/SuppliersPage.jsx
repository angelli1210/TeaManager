import { useEffect, useState } from 'react';
import { supplierService } from '../services/api';
import ConfirmModal from '../components/common/ConfirmModal';

const emptyForm = { supplierId: '', supplierName: '', country: '', contactEmail: '', phone: '' };

export default function SuppliersPage() {
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
    supplierService.getAll()
      .then(r => setSuppliers(r.data))
      .catch(() => setError('Failed to load suppliers.'))
      .finally(() => setLoading(false));
  };

  useEffect(() => { fetchAll(); }, []);

  const validate = () => {
    const e = {};
    if (!form.supplierId) e.supplierId = 'Required';
    if (!form.supplierName || form.supplierName.length < 2) e.supplierName = 'Min 2 characters';
    if (!form.country) e.country = 'Required';
    if (!form.contactEmail || !/\S+@\S+\.\S+/.test(form.contactEmail)) e.contactEmail = 'Valid email required';
    setFormErrors(e);
    return Object.keys(e).length === 0;
  };

  const openCreate = () => { setForm(emptyForm); setFormErrors({}); setEditingId(null); setModalOpen(true); };
  const openEdit = (s) => {
    setForm({ supplierId: s.supplierId, supplierName: s.supplierName, country: s.country, contactEmail: s.contactEmail, phone: s.phone || '' });
    setFormErrors({}); setEditingId(s.supplierId); setModalOpen(true);
  };

  const handleSave = async () => {
    if (!validate()) return;
    setSaving(true);
    try {
      const payload = { ...form, supplierId: Number(form.supplierId) };
      if (editingId) await supplierService.update(editingId, payload);
      else await supplierService.create(payload);
      setModalOpen(false);
      fetchAll();
    } catch (err) {
      setFormErrors({ api: err.response?.data?.message || 'Save failed.' });
    } finally { setSaving(false); }
  };

  const handleDelete = async () => {
    try {
      await supplierService.delete(deleteModal.id);
      setDeleteModal({ open: false, id: null, name: '' });
      fetchAll();
    } catch { setError('Delete failed.'); }
  };

  const filtered = suppliers.filter(s => s.supplierName.toLowerCase().includes(search.toLowerCase()) || s.country?.toLowerCase().includes(search.toLowerCase()));

  return (
    <div>
      <div className="flex items-center justify-between mb-5">
        <div>
          <h2 className="text-xl font-bold text-gray-900">Suppliers</h2>
          <p className="text-sm text-gray-400 mt-0.5">Your global supplier network</p>
        </div>
        <button onClick={openCreate} className="bg-green-600 hover:bg-green-700 text-white text-sm font-semibold px-5 py-2.5 rounded-xl transition-all flex items-center gap-2">
          <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" /></svg>
          Add Supplier
        </button>
      </div>

      {error && <p className="text-red-500 text-sm mb-3">{error}</p>}

      <div className="bg-white rounded-2xl border border-gray-100 shadow-sm overflow-hidden">
        <div className="p-4 border-b border-gray-100 flex items-center justify-between">
          <input value={search} onChange={e => setSearch(e.target.value)} type="text" placeholder="Search suppliers..." className="w-64 text-sm border border-gray-200 rounded-xl px-3 py-2 focus:outline-none focus:ring-2 focus:ring-green-500" />
          <span className="text-xs text-gray-400">{filtered.length} suppliers</span>
        </div>
        {loading ? <div className="p-10 text-center text-gray-400 text-sm">Loading...</div> : (
          <table className="w-full text-sm">
            <thead>
              <tr className="text-left text-xs text-gray-400 bg-gray-50 border-b border-gray-100">
                {['ID', 'Supplier Name', 'Country', 'Contact Email', 'Phone', 'Actions'].map(h => <th key={h} className="px-5 py-3 font-semibold">{h}</th>)}
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-50">
              {filtered.map(s => (
                <tr key={s.supplierId} className="hover:bg-gray-50 transition-colors">
                  <td className="px-5 py-3.5 text-gray-400 text-xs">#{s.supplierId}</td>
                  <td className="px-5 py-3.5 font-semibold text-gray-800">{s.supplierName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{s.country}</td>
                  <td className="px-5 py-3.5 text-gray-500">{s.contactEmail}</td>
                  <td className="px-5 py-3.5 text-gray-500">{s.phone}</td>
                  <td className="px-5 py-3.5">
                    <div className="flex gap-3">
                      <button onClick={() => openEdit(s)} className="text-blue-500 hover:text-blue-700 text-xs font-semibold">Edit</button>
                      <button onClick={() => setDeleteModal({ open: true, id: s.supplierId, name: s.supplierName })} className="text-red-400 hover:text-red-600 text-xs font-semibold">Delete</button>
                    </div>
                  </td>
                </tr>
              ))}
              {filtered.length === 0 && <tr><td colSpan={6} className="px-5 py-10 text-center text-gray-400 text-sm">No suppliers found.</td></tr>}
            </tbody>
          </table>
        )}
      </div>

      {modalOpen && (
        <div className="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center z-50">
          <div className="bg-white rounded-3xl shadow-2xl w-full max-w-lg p-7 mx-4 max-h-[90vh] overflow-y-auto">
            <div className="flex items-center justify-between mb-6">
              <h3 className="text-lg font-bold text-gray-900">{editingId ? 'Edit Supplier' : 'Add New Supplier'}</h3>
              <button onClick={() => setModalOpen(false)} className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center">
                <svg className="w-4 h-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" /></svg>
              </button>
            </div>
            {formErrors.api && <p className="text-red-500 text-sm mb-4">{formErrors.api}</p>}
            <div className="grid grid-cols-2 gap-4">
              {[
                ...(!editingId ? [{ label: 'Supplier ID', field: 'supplierId', type: 'number', required: true }] : []),
                { label: 'Supplier Name', field: 'supplierName', placeholder: 'e.g. GreenLeaf Co.', required: true, full: !!editingId },
                { label: 'Country', field: 'country', placeholder: 'e.g. China', required: true },
                { label: 'Email', field: 'contactEmail', type: 'email', placeholder: 'email@supplier.com', required: true },
                { label: 'Phone', field: 'phone', placeholder: '+86-21-5555' },
              ].map(({ label, field, type = 'text', placeholder, required, full }) => (
                <div key={field} className={full ? 'col-span-2' : ''}>
                  <label className="block text-xs font-semibold text-gray-600 mb-1.5">{label} {required && <span className="text-red-400">*</span>}</label>
                  <input type={type} value={form[field]} onChange={e => setForm({ ...form, [field]: e.target.value })} placeholder={placeholder} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" />
                  {formErrors[field] && <p className="text-red-400 text-xs mt-1">{formErrors[field]}</p>}
                </div>
              ))}
            </div>
            <div className="flex justify-end gap-3 mt-6">
              <button onClick={() => setModalOpen(false)} className="px-5 py-2.5 text-sm font-semibold text-gray-600 border border-gray-200 rounded-xl hover:bg-gray-50">Cancel</button>
              <button onClick={handleSave} disabled={saving} className="px-6 py-2.5 text-sm font-semibold bg-green-600 text-white rounded-xl hover:bg-green-700 disabled:opacity-50">
                {saving ? 'Saving...' : 'Save Supplier'}
              </button>
            </div>
          </div>
        </div>
      )}

      <ConfirmModal isOpen={deleteModal.open} itemName={deleteModal.name} onConfirm={handleDelete} onCancel={() => setDeleteModal({ open: false, id: null, name: '' })} />
    </div>
  );
}
