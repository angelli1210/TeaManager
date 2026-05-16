import { useEffect, useState } from 'react';
import { brandService } from '../API/api';
import ConfirmModal from '../components/common/ConfirmModal';

const emptyForm = { brandName: '', country: '', foundedYear: '', email: '', phone: '', address: '', businessRegNumber: '', ownerName: '' };
const Field = ({ label, field, type = 'text', placeholder, required, min, max, form, setForm, formErrors }) => (
  <div>
    <label className="block text-xs font-semibold text-gray-600 mb-1.5">{label} {required && <span className="text-red-400">*</span>}</label>
    <input type={type} value={form[field]} onChange={e => setForm({ ...form, [field]: e.target.value })} placeholder={placeholder} min={min}
      max={max} className="w-full border border-gray-200 rounded-xl px-3.5 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-green-500" />
    {formErrors[field] && <p className="text-red-400 text-xs mt-1">{formErrors[field]}</p>}
  </div>
)

export default function BrandsPage() {
  const [brands, setBrands] = useState([]);
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
    brandService.getAll()
      .then(r => setBrands(r.data))
      .catch(() => setError('Failed to load brands.'))
      .finally(() => setLoading(false));
  };

  useEffect(() => { fetchAll(); }, []);

  const validate = () => {
    const e = {};
    if (!form.brandName || form.brandName.length < 2) e.brandName = 'Min 2 characters';
    if (!form.country) e.country = 'Required';
    if (!form.email || !/\S+@\S+\.\S+/.test(form.email)) e.email = 'Valid email required';
    if (!form.phone || !/^[\d\s\-+()]+$/.test(form.phone)) e.phone = 'Valid phone required';
    if (!form.ownerName) e.ownerName = 'Required';
    const currentYear = new Date().getFullYear();
    if (!form.foundedYear || form.foundedYear < 1800 || form.foundedYear > currentYear) e.foundedYear = `1800–${currentYear}`;
    setFormErrors(e);
    return Object.keys(e).length === 0;
  };

  const openCreate = () => { setForm(emptyForm); setFormErrors({}); setEditingId(null); setModalOpen(true); };
  const openEdit = (b) => {
    setForm({ brandId: b.brandId, brandName: b.brandName, country: b.country, foundedYear: b.foundedYear, email: b.email, phone: b.phone || '', address: b.address || '', businessRegNumber: b.businessRegNumber || '', ownerName: b.ownerName });
    setFormErrors({}); setEditingId(b.brandId); setModalOpen(true);
  };

  const handleSave = async () => {
    if (!validate()) return;
    setSaving(true);
    try {
      const payload = { ...form, foundedYear: Number(form.foundedYear) };
      if (editingId) await brandService.update(editingId, payload);
      else await brandService.create(payload);
      setModalOpen(false);
      fetchAll();
    } catch (err) {
      setFormErrors({ api: err.response?.data?.message || 'Save failed.' });
    } finally { setSaving(false); }
  };

  const handleDelete = async () => {
    try {
      await brandService.delete(deleteModal.id);
      setDeleteModal({ open: false, id: null, name: '' });
      fetchAll();
    } catch { setError('Delete failed.'); }
  };

  const filtered = brands.filter(b => b.brandName.toLowerCase().includes(search.toLowerCase()) || b.country?.toLowerCase().includes(search.toLowerCase()));

  return (
    <div>
      <div className="flex items-center justify-between mb-5">
        <div>
          <h2 className="text-xl font-bold text-gray-900">Brands</h2>
          <p className="text-sm text-gray-400 mt-0.5">Manage your brand directory</p>
        </div>
        <button onClick={openCreate} className="bg-green-600 hover:bg-green-700 text-white text-sm font-semibold px-5 py-2.5 rounded-xl transition-all flex items-center gap-2">
          <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" /></svg>
          Add Brand
        </button>
      </div>

      {error && <p className="text-red-500 text-sm mb-3">{error}</p>}

      <div className="bg-white rounded-2xl border border-gray-100 shadow-sm overflow-hidden">
        <div className="p-4 border-b border-gray-100 flex items-center justify-between">
          <input value={search} onChange={e => setSearch(e.target.value)} type="text" placeholder="Search brands..." className="w-64 text-sm border border-gray-200 rounded-xl px-3 py-2 focus:outline-none focus:ring-2 focus:ring-green-500" />
          <span className="text-xs text-gray-400">{filtered.length} brands</span>
        </div>
        {loading ? <div className="p-10 text-center text-gray-400 text-sm">Loading...</div> : (
          <table className="w-full text-sm">
            <thead>
              <tr className="text-left text-xs text-gray-400 bg-gray-50 border-b border-gray-100">
                {['ID', 'Brand Name', 'Country', 'Owner', 'Email', 'Founded', 'Actions'].map(h => <th key={h} className="px-5 py-3 font-semibold">{h}</th>)}
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-50">
              {filtered.map(b => (
                <tr key={b.brandId} className="hover:bg-gray-50 transition-colors">
                  <td className="px-5 py-3.5 text-gray-400 text-xs">#{b.brandId}</td>
                  <td className="px-5 py-3.5 font-semibold text-gray-800">{b.brandName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{b.country}</td>
                  <td className="px-5 py-3.5 text-gray-500">{b.ownerName}</td>
                  <td className="px-5 py-3.5 text-gray-500">{b.email}</td>
                  <td className="px-5 py-3.5 text-gray-500">{b.foundedYear}</td>
                  <td className="px-5 py-3.5">
                    <div className="flex gap-3">
                      <button onClick={() => openEdit(b)} className="text-blue-500 hover:text-blue-700 text-xs font-semibold">Edit</button>
                      <button onClick={() => setDeleteModal({ open: true, id: b.brandId, name: b.brandName })} className="text-red-400 hover:text-red-600 text-xs font-semibold">Delete</button>
                    </div>
                  </td>
                </tr>
              ))}
              {filtered.length === 0 && <tr><td colSpan={7} className="px-5 py-10 text-center text-gray-400 text-sm">No brands found.</td></tr>}
            </tbody>
          </table>
        )}
      </div>

      {modalOpen && (
        <div className="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center z-50">
          <div className="bg-white rounded-3xl shadow-2xl w-full max-w-lg p-7 mx-4 max-h-[90vh] overflow-y-auto">
            <div className="flex items-center justify-between mb-6">
              <h3 className="text-lg font-bold text-gray-900">{editingId ? 'Edit Brand' : 'Add New Brand'}</h3>
              <button onClick={() => setModalOpen(false)} className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center">
                <svg className="w-4 h-4 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" /></svg>
              </button>
            </div>
            {formErrors.api && <p className="text-red-500 text-sm mb-4">{formErrors.api}</p>}
            <div className="grid grid-cols-2 gap-4">
              <Field label="Brand Name" field="brandName" placeholder="e.g. TianHu" form={form} setForm={setForm} formErrors={formErrors} required />
              <Field label="Country" field="country" placeholder="e.g. China" form={form} setForm={setForm} formErrors={formErrors} required />
              <Field label="Founded Year" field="foundedYear" type="number" placeholder="1998" form={form} setForm={setForm} formErrors={formErrors} required min={1800} max={new Date().getFullYear()} />
              <Field label="Owner Name" field="ownerName" placeholder="e.g. Li Wei" form={form} setForm={setForm} formErrors={formErrors} required />
              <Field label="Email" field="email" type="email" placeholder="brand@email.com" form={form} setForm={setForm} formErrors={formErrors} required />
              <Field label="Phone" field="phone" placeholder="+86-21-5555" form={form} setForm={setForm} formErrors={formErrors} required />
              <div className="col-span-2"><Field label="Address" field="address" placeholder="Street address" form={form} setForm={setForm} formErrors={formErrors} /></div>
              <div className="col-span-2"><Field label="Business Reg. No." field="businessRegNumber" placeholder="REG-123456" form={form} setForm={setForm} formErrors={formErrors} /></div>
            </div>
            <div className="flex justify-end gap-3 mt-6">
              <button onClick={() => setModalOpen(false)} className="px-5 py-2.5 text-sm font-semibold text-gray-600 border border-gray-200 rounded-xl hover:bg-gray-50">Cancel</button>
              <button onClick={handleSave} disabled={saving} className="px-6 py-2.5 text-sm font-semibold bg-green-600 text-white rounded-xl hover:bg-green-700 disabled:opacity-50">
                {saving ? 'Saving...' : 'Save Brand'}
              </button>
            </div>
          </div>
        </div>
      )}

      <ConfirmModal isOpen={deleteModal.open} itemName={deleteModal.name} onConfirm={handleDelete} onCancel={() => setDeleteModal({ open: false, id: null, name: '' })} />
    </div>
  );
}
