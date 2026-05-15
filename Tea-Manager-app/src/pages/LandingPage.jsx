import { useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { LandingNavbar } from '../components/layout/Navbar';

// App mockup shown on the right side of hero
function AppMockup() {
  return (
    <div className="relative w-full max-w-[560px]">
      {/* Main window */}
      <div className="rounded-2xl overflow-hidden bg-white" style={{ boxShadow: '0 40px 80px rgba(0,0,0,0.18), 0 0 0 1px rgba(0,0,0,0.05)' }}>
        {/* Chrome bar */}
        <div className="bg-gray-100 px-4 py-3 flex items-center gap-2 border-b border-gray-200">
          <div className="w-3 h-3 rounded-full bg-red-400" />
          <div className="w-3 h-3 rounded-full bg-yellow-400" />
          <div className="w-3 h-3 rounded-full bg-green-400" />
          <div className="flex-1 flex justify-center">
            <div className="bg-white rounded-md px-4 py-1 text-xs text-gray-400 border border-gray-200 w-48 text-center">teamanager.app/dashboard</div>
          </div>
        </div>
        {/* Content */}
        <div className="flex" style={{ height: 340 }}>
          {/* Mini sidebar */}
          <div className="w-44 bg-white border-r border-gray-100 flex flex-col py-3 px-2 gap-1 flex-shrink-0">
            <div className="flex items-center gap-1.5 px-2 py-1.5 mb-2">
              <div className="w-5 h-5 bg-green-600 rounded-md flex items-center justify-center">
                <svg width="10" height="12" viewBox="0 0 28 32" fill="none"><path d="M14 1C14 1 9 11 9 18C9 22 11.2 25.5 14 27.5" stroke="white" strokeWidth="3.5" strokeLinecap="round" /></svg>
              </div>
              <span className="text-xs font-bold text-gray-800">TeaManager</span>
            </div>
            {['Overview', 'Products', 'Brands', 'Suppliers', 'Orders'].map((item, i) => (
              <div key={item} className={`px-2 py-1.5 rounded-lg flex items-center gap-2 ${i === 0 ? 'bg-green-50 border-r-2 border-green-500' : ''}`}>
                <div className={`w-2 h-2 rounded-sm ${i === 0 ? 'bg-green-500' : 'bg-gray-300'}`} />
                <span className={`text-xs ${i === 0 ? 'font-semibold text-green-700' : 'text-gray-500'}`}>{item}</span>
              </div>
            ))}
          </div>
          {/* Main content */}
          <div className="flex-1 bg-gray-50 p-4 overflow-hidden">
            <div className="grid grid-cols-3 gap-2 mb-3">
              {[['Products', '24', '↑ 3 this month'], ['Brands', '8', '↑ 1 new'], ['Orders', '47', '↑ 12 this month']].map(([label, val, sub]) => (
                <div key={label} className="bg-white rounded-xl p-3 border border-gray-100">
                  <p className="text-[9px] text-gray-400 font-medium uppercase tracking-wide">{label}</p>
                  <p className="text-xl font-bold text-gray-900">{val}</p>
                  <p className="text-[9px] text-green-500">{sub}</p>
                </div>
              ))}
            </div>
            <div className="bg-white rounded-xl border border-gray-100 overflow-hidden">
              <div className="px-3 py-2 border-b border-gray-100 flex items-center justify-between">
                <span className="text-xs font-semibold text-gray-700">Recent Products</span>
                <span className="text-[10px] text-green-600 font-medium">View all →</span>
              </div>
              <table className="w-full text-[10px]">
                <thead><tr className="bg-gray-50 text-gray-400">
                  <th className="px-3 py-1.5 text-left font-medium">Name</th>
                  <th className="px-3 py-1.5 text-left font-medium">Brand</th>
                  <th className="px-3 py-1.5 text-left font-medium">Price</th>
                  <th className="px-3 py-1.5 text-left font-medium">Stock</th>
                </tr></thead>
                <tbody className="divide-y divide-gray-50">
                  {[['Dragon Well', 'TianHu', '$45', '120', 'green'], ['Darjeeling', 'HillsTop', '$62', '34', 'yellow'], ['Earl Grey', 'RoyalBlend', '$28', '200', 'green'], ['Sencha', 'MatsuGreen', '$38', '8', 'red']].map(([name, brand, price, stock, color]) => (
                    <tr key={name}>
                      <td className="px-3 py-2 font-medium text-gray-800">{name}</td>
                      <td className="px-3 py-2 text-gray-500">{brand}</td>
                      <td className="px-3 py-2 font-medium">{price}</td>
                      <td className="px-3 py-2">
                        <span className={`px-1.5 py-0.5 rounded text-[9px] ${color === 'green' ? 'bg-green-100 text-green-700' : color === 'yellow' ? 'bg-yellow-100 text-yellow-700' : 'bg-red-100 text-red-600'}`}>{stock}</span>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
      {/* Floating revenue card */}
      <div className="absolute -left-14 top-16 anim-float-2">
        <div className="glass rounded-2xl px-4 py-3 shadow-xl w-44">
          <p className="text-[10px] text-gray-400 mb-1">Total Revenue</p>
          <p className="text-lg font-bold text-gray-900">$84,200</p>
          <div className="flex items-center gap-1 mt-1">
            <span className="text-[10px] text-green-600 font-semibold">↑ 18.2%</span>
            <span className="text-[10px] text-gray-400">vs last month</span>
          </div>
        </div>
      </div>
      {/* Floating order card */}
      <div className="absolute -right-10 bottom-20 anim-float">
        <div className="glass rounded-2xl px-4 py-3 shadow-xl w-40">
          <div className="flex items-center gap-2 mb-2">
            <div className="w-6 h-6 rounded-full bg-green-500 flex items-center justify-center">
              <svg className="w-3 h-3 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" /></svg>
            </div>
            <span className="text-xs font-semibold text-gray-700">Order Placed</span>
          </div>
          <p className="text-[10px] text-gray-500">50× Dragon Well Premium</p>
          <p className="text-[10px] text-gray-400 mt-0.5">just now</p>
        </div>
      </div>
    </div>
  );
}

export default function LandingPage() {
  const navigate = useNavigate();
  const revealRefs = useRef([]);

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => entries.forEach(e => { if (e.isIntersecting) e.target.classList.add('visible'); }),
      { threshold: 0.1 }
    );
    revealRefs.current.forEach(el => el && observer.observe(el));
    return () => observer.disconnect();
  }, []);

  const addReveal = (el) => { if (el && !revealRefs.current.includes(el)) revealRefs.current.push(el); };

  return (
    <div className="bg-[#FAFAF8] overflow-x-hidden">
      <LandingNavbar />

      {/* HERO */}
      <section className="relative min-h-screen flex items-center overflow-hidden pt-20">
        {/* Blobs */}
        <div className="absolute top-20 right-[-10%] w-[500px] h-[500px] bg-green-100 opacity-60 hero-blob rounded-full blur-3xl pointer-events-none" />
        <div className="absolute bottom-10 left-[-5%] w-[350px] h-[350px] bg-emerald-50 opacity-80 hero-blob-2 rounded-full blur-3xl pointer-events-none" />
        {/* Dot grid */}
        <div className="absolute inset-0 pointer-events-none" style={{ backgroundImage: 'radial-gradient(circle, #d1fae5 1px, transparent 1px)', backgroundSize: '32px 32px', opacity: 0.4 }} />

        <div className="relative w-full max-w-7xl mx-auto px-10 flex items-center gap-16 py-24">
          {/* Left */}
          <div className="flex-1 max-w-2xl">
            <div className="anim-fade-up inline-flex items-center gap-2 bg-green-50 border border-green-200 text-green-700 text-xs font-semibold px-3 py-1.5 rounded-full mb-6">
              <span className="w-1.5 h-1.5 rounded-full bg-green-500 animate-pulse" />
              Now supporting 50+ tea origins
            </div>
            <h1 className="anim-fade-up-2 text-5xl font-extrabold leading-[1.1] text-gray-900 mb-0">
              Manage Your Tea<br />Business
            </h1>
            <div className="shimmer-text italic text-5xl font-extrabold mb-4 anim-fade-up-2">
              efficiently<br />and accurately
            </div>
            <p className="anim-fade-up-3 text-gray-500 text-lg leading-relaxed mb-8 max-w-md">
              The all-in-one platform for managing your tea products, brands, suppliers and orders — beautifully designed and lightning fast.
            </p>
            <div className="anim-fade-up-4 flex items-center gap-4">
              <button onClick={() => navigate('/dashboard')} className="bg-green-600 hover:bg-green-700 text-white font-semibold px-8 py-3.5 rounded-2xl transition-all hover:shadow-xl hover:shadow-green-200 text-sm">
                Get Start — It's free
              </button>
              <button className="flex items-center gap-2 text-sm font-medium text-gray-700 hover:text-gray-900 transition-colors">
                <span className="w-9 h-9 flex items-center justify-center rounded-full bg-white shadow-md border border-gray-100">
                  <svg className="w-4 h-4 ml-0.5" fill="currentColor" viewBox="0 0 20 20"><path d="M6.3 2.841A1.5 1.5 0 004 4.11V15.89a1.5 1.5 0 002.3 1.269l9.344-5.89a1.5 1.5 0 000-2.538L6.3 2.84z" /></svg>
                </span>
                Watch demo
              </button>
            </div>
            <div className="anim-fade-up-4 flex items-center gap-4 mt-10">
              <div className="flex -space-x-2">
                {['L', 'W', 'T', '+'].map((l, i) => (
                  <div key={i} className={`w-8 h-8 rounded-full border-2 border-white flex items-center justify-center text-white text-xs font-bold ${['bg-green-400', 'bg-emerald-500', 'bg-teal-400', 'bg-green-600'][i]}`}>{l}</div>
                ))}
              </div>
              <p className="text-sm text-gray-500"><span className="font-semibold text-gray-800">500+</span> tea businesses trust us</p>
            </div>
          </div>
          {/* Right: mockup */}
          <div className="flex-1 flex justify-center anim-float">
            <AppMockup />
          </div>
        </div>
      </section>

      {/* STATS */}
      <section className="bg-green-600 py-10">
        <div className="max-w-4xl mx-auto grid grid-cols-4 gap-8 text-center px-10">
          {[['500+', 'Businesses'], ['50+', 'Tea Origins'], ['10k+', 'Products Managed'], ['99.9%', 'Uptime']].map(([num, label]) => (
            <div key={label} ref={addReveal} className="reveal">
              <p className="text-3xl font-extrabold text-white">{num}</p>
              <p className="text-green-200 text-sm mt-1">{label}</p>
            </div>
          ))}
        </div>
      </section>

      {/* FEATURES */}
      <section id="features" className="py-24 bg-white px-10">
        <div className="max-w-6xl mx-auto">
          <div ref={addReveal} className="reveal text-center mb-16">
            <span className="inline-block bg-green-50 text-green-700 text-xs font-semibold px-3 py-1.5 rounded-full mb-4 border border-green-200">Features</span>
            <h2 className="text-4xl font-extrabold text-gray-900">Everything you need to<br />run your tea business</h2>
            <p className="text-gray-500 mt-4 text-lg max-w-lg mx-auto">From product catalog to supplier orders — manage it all in one beautiful dashboard.</p>
          </div>
          <div className="grid grid-cols-3 gap-6">
            {[
              { bg: 'bg-green-100', ic: 'text-green-600', title: 'Product Management', desc: 'Track every tea product with detailed info — origin, price, harvest year, and real-time stock levels.', path: 'M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4' },
              { bg: 'bg-blue-100', ic: 'text-blue-600', title: 'Brand Directory', desc: 'Maintain a complete directory of tea brands with contact info, country of origin, and business details.', path: 'M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.994 1.994 0 013 12V7a4 4 0 014-4z' },
              { bg: 'bg-purple-100', ic: 'text-purple-600', title: 'Supplier Network', desc: 'Build and manage your global supplier network. Track relationships, contacts and sourcing history.', path: 'M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0' },
              { bg: 'bg-orange-100', ic: 'text-orange-500', title: 'Order Tracking', desc: 'Create and track supplier orders from pending to delivered. Know exactly what\'s on the way.', path: 'M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2' },
              { bg: 'bg-pink-100', ic: 'text-pink-500', title: 'Real-time Overview', desc: "Get a bird's-eye view of your entire operation — stats, trends, and key metrics at a glance.", path: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z' },
            ].map(({ bg, ic, title, desc, path }) => (
              <div key={title} ref={addReveal} className="reveal card-hover bg-gray-50 rounded-2xl p-7 border border-gray-100">
                <div className={`w-12 h-12 ${bg} rounded-2xl flex items-center justify-center mb-5`}>
                  <svg className={`w-6 h-6 ${ic}`} fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d={path} /></svg>
                </div>
                <h3 className="text-lg font-bold text-gray-900 mb-2">{title}</h3>
                <p className="text-gray-500 text-sm leading-relaxed">{desc}</p>
              </div>
            ))}
            <div ref={addReveal} className="reveal card-hover bg-green-600 rounded-2xl p-7 text-white">
              <div className="w-12 h-12 bg-white/20 rounded-2xl flex items-center justify-center mb-5">
                <svg className="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 10V3L4 14h7v7l9-11h-7z" /></svg>
              </div>
              <h3 className="text-lg font-bold mb-2">Built for Speed</h3>
              <p className="text-green-100 text-sm leading-relaxed">Powered by ASP.NET Core + React — blazing fast API responses and a smooth, responsive UI.</p>
            </div>
          </div>
        </div>
      </section>

      {/* CTA */}
      <section className="py-24 bg-gray-900 relative overflow-hidden">
        <div className="absolute inset-0 pointer-events-none" style={{ background: 'radial-gradient(ellipse at 20% 50%, rgba(22,163,74,0.2) 0%, transparent 60%), radial-gradient(ellipse at 80% 50%, rgba(74,222,128,0.1) 0%, transparent 60%)' }} />
        <div ref={addReveal} className="reveal relative max-w-2xl mx-auto text-center px-10">
          <h2 className="text-4xl font-extrabold text-white mb-5">Ready to grow your<br />tea business?</h2>
          <p className="text-gray-400 text-lg mb-8">Join hundreds of tea businesses managing smarter with TeaManager.</p>
          <button onClick={() => navigate('/dashboard')} className="bg-green-500 hover:bg-green-400 text-white font-bold px-10 py-4 rounded-2xl text-base transition-all hover:shadow-2xl">
            Start for free today →
          </button>
        </div>
      </section>

      {/* FOOTER */}
      <footer className="bg-gray-950 py-8 px-10 flex items-center justify-between">
        <div className="flex items-center gap-2">
          <span className="text-sm font-bold text-white">Tea<span className="text-green-400">Manager</span></span>
          <span className="text-gray-600 text-xs">© 2026</span>
        </div>
        <p className="text-gray-600 text-xs">Built with ASP.NET Core + React</p>
      </footer>
    </div>
  );
}
