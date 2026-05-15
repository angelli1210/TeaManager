import { BrowserRouter, Routes, Route } from 'react-router-dom';
import LandingPage from './pages/LandingPage';
import { DashboardNavbar } from './components/layout/Navbar';
import Sidebar from './components/layout/Sidebar';
import OverviewPage from './pages/OverviewPage';
import ProductsPage from './pages/ProductsPage';
import BrandsPage from './pages/BrandsPage';
import SuppliersPage from './pages/SuppliersPage';
import OrdersPage from './pages/OrdersPage';

// Dashboard layout wraps all dashboard routes
function DashboardLayout({ children }) {
  return (
    <div className="min-h-screen flex flex-col bg-gray-50">
      <DashboardNavbar />
      <div className="flex flex-1">
        <Sidebar />
        <main className="flex-1 p-6">{children}</main>
      </div>
    </div>
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/dashboard" element={<DashboardLayout><OverviewPage /></DashboardLayout>} />
        <Route path="/dashboard/products" element={<DashboardLayout><ProductsPage /></DashboardLayout>} />
        <Route path="/dashboard/brands" element={<DashboardLayout><BrandsPage /></DashboardLayout>} />
        <Route path="/dashboard/suppliers" element={<DashboardLayout><SuppliersPage /></DashboardLayout>} />
        <Route path="/dashboard/orders" element={<DashboardLayout><OrdersPage /></DashboardLayout>} />
      </Routes>
    </BrowserRouter>
  );
}
