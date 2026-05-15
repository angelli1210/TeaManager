import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const LeafIcon = () => (
  <svg width="26" height="30" viewBox="0 0 28 32" fill="none">
    <path d="M14 1C14 1 2 9 2 18C2 24.6 7.4 30 14 30C20.6 30 26 24.6 26 18C26 10 17 4 14 1Z" fill="#dcfce7"/>
    <path d="M14 1C14 1 9 11 9 18C9 22 11.2 25.5 14 27.5" stroke="#16a34a" strokeWidth="2.2" strokeLinecap="round"/>
    <path d="M14 1C14 1 21 8 23.5 15C24.8 18.5 24.2 22 22.5 24.5" stroke="#16a34a" strokeWidth="2.2" strokeLinecap="round"/>
  </svg>
);

// Landing page navbar
export function LandingNavbar() {
  const navigate = useNavigate();
  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    const onScroll = () => setScrolled(window.scrollY > 20);
    window.addEventListener('scroll', onScroll);
    return () => window.removeEventListener('scroll', onScroll);
  }, []);

  return (
    <nav className={`fixed top-0 left-0 right-0 z-50 flex items-center justify-between px-10 py-4 transition-all duration-300 ${scrolled ? 'shadow-md bg-[rgba(250,250,248,0.95)]' : 'bg-[#FAFAF8]'}`}>
      <div className="flex items-center gap-10">
        <div className="flex items-center gap-2">
          <LeafIcon />
          <span className="text-[1.15rem] font-bold tracking-tight text-gray-900">Tea<span className="text-green-600">Manager</span></span>
        </div>
        <div className="flex items-center gap-8 text-sm font-medium text-gray-500">
          <a href="#features" className="hover:text-gray-900 transition-colors">Features</a>
          <a href="#tutorial" className="hover:text-gray-900 transition-colors">Tutorial</a>
          <a href="#pricing" className="hover:text-gray-900 transition-colors">Pricing</a>
        </div>
      </div>
      <div className="flex items-center gap-4">
        <button onClick={() => navigate('/dashboard')} className="text-sm font-medium text-gray-600 hover:text-gray-900 transition-colors">Sign in</button>
        <button onClick={() => navigate('/dashboard')} className="bg-green-600 hover:bg-green-700 text-white text-sm font-semibold px-5 py-2.5 rounded-xl transition-all hover:shadow-lg hover:shadow-green-200">
          Get Started Today
        </button>
      </div>
    </nav>
  );
}

// Dashboard top bar
export function DashboardNavbar() {
  const navigate = useNavigate();
  return (
    <div className="bg-white border-b border-gray-200 px-6 py-3 flex items-center justify-between sticky top-0 z-40 shadow-sm">
      <div className="flex items-center gap-2">
        <LeafIcon />
        <span className="font-bold text-gray-900 text-lg">Tea<span className="text-green-600">Manager</span></span>
      </div>
      <div className="flex items-center gap-3">
        <span className="text-sm text-gray-500">Admin</span>
        <div className="w-8 h-8 rounded-full bg-green-600 text-white flex items-center justify-center text-sm font-bold">A</div>
        <button onClick={() => navigate('/')} className="text-xs text-gray-400 hover:text-gray-600 ml-2 transition-colors">Sign out</button>
      </div>
    </div>
  );
}
