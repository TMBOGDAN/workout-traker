import { useMemo, useState } from 'react';
import Login from './Login';
import Register from './Register';
import {
  Activity, ArrowDownRight, ArrowRight, ArrowUpRight, BarChart3, Bell,
  CalendarDays, Check, ChevronDown, Clock3, Dumbbell, Flame, LayoutDashboard,
  LogOut, Menu, MoreHorizontal, Plus, Search, Settings, Target, TrendingUp,
  Trophy, UserRound, Weight, X, Zap, Moon, Sun
} from 'lucide-react';

const initialWorkouts = [
  { id: 1, name: 'Push Strength', type: 'Piept & Triceps', date: 'Astăzi', duration: 52, volume: 8240, exercises: 6, color: '#54b8ff', icon: Dumbbell },
  { id: 2, name: 'Pull Power', type: 'Spate & Biceps', date: '19 Sep', duration: 61, volume: 9780, exercises: 7, color: '#6f8cff', icon: Zap },
  { id: 3, name: 'Leg Day', type: 'Picioare', date: '17 Sep', duration: 68, volume: 12450, exercises: 8, color: '#ff8b66', icon: Activity },
  { id: 4, name: 'Upper Focus', type: 'Upper body', date: '14 Sep', duration: 48, volume: 7620, exercises: 6, color: '#55d6ff', icon: Target },
  { id: 5, name: 'Full Body', type: 'Condiționare', date: '12 Sep', duration: 44, volume: 6890, exercises: 5, color: '#79a7ff', icon: Flame },
];

const weeklyData = [
  { day: 'L', value: 68 }, { day: 'M', value: 88 }, { day: 'M', value: 42 },
  { day: 'J', value: 96 }, { day: 'V', value: 62 }, { day: 'S', value: 76 }, { day: 'D', value: 24 },
];

const exerciseProgress = [
  { name: 'Bench press', current: 92.5, change: 8.2, max: 110 },
  { name: 'Back squat', current: 135, change: 5.9, max: 150 },
  { name: 'Deadlift', current: 165, change: 10, max: 180 },
];

function Logo() {
  return <div className="logo"><span className="logo-mark"><Activity size={21} strokeWidth={3} /></span><span>REP<span>FLOW</span></span></div>;
}

function Sidebar({ active, setActive, open, onClose, onLogout }) {
  const items = [
    { id: 'overview', label: 'Prezentare', icon: LayoutDashboard },
    { id: 'workouts', label: 'Antrenamente', icon: Dumbbell },
    { id: 'statistics', label: 'Statistici', icon: BarChart3 },
    { id: 'account', label: 'Contul meu', icon: UserRound },
  ];
  return <>
    {open && <button className="sidebar-backdrop" onClick={onClose} aria-label="Închide meniul" />}
    <aside className={`sidebar ${open ? 'open' : ''}`}>
      <div className="sidebar-top"><Logo /><button className="mobile-close" onClick={onClose}><X /></button></div>
      <p className="nav-label">MENIU PRINCIPAL</p>
      <nav>
        {items.map(({ id, label, icon: Icon }) => (
          <button key={id} className={active === id ? 'active' : ''} onClick={() => { setActive(id); onClose(); }}>
            <Icon size={19} /><span>{label}</span>{active === id && <span className="active-dot" />}
          </button>
        ))}
      </nav>
      <div className="sidebar-spacer" />
      <div className="weekly-goal">
        <div className="goal-ring"><span>4<span>/5</span></span></div>
        <div><strong>Obiectiv săptămânal</strong><small>Încă un antrenament!</small></div>
      </div>
      <nav className="secondary-nav">
        <button><Settings size={19} /><span>Setări</span></button>
        <button onClick={onLogout}><LogOut size={19} /><span>Deconectare</span></button>
      </nav>
      <div className="sidebar-user">
        <div className="avatar">AM</div><div><strong>Andrei M.</strong><small>Nivel intermediar</small></div><MoreHorizontal size={18} />
      </div>
    </aside>
  </>;
}

function LoggedOut({ onLogin }) {
  return <main className="logged-out-page">
    <section className="logged-out-card">
      <Logo />
      <div className="logged-out-icon"><LogOut size={28} /></div>
      <p className="overline">SESIUNE ÎNCHEIATĂ</p>
      <h1>Te-ai deconectat.</h1>
      <p>Sesiunea ta a fost închisă în siguranță.</p>
      <button className="primary-btn" onClick={onLogin}>Conectează-te din nou <ArrowRight size={17} /></button>
    </section>
  </main>;
}

function ThemeToggle({ theme, onToggle }) {
  const isDark = theme === 'dark';
  return <button
    className="theme-toggle"
    type="button"
    onClick={onToggle}
    aria-label={isDark ? 'Activează modul light' : 'Activează modul dark'}
    title={isDark ? 'Mod light' : 'Mod dark'}
  >
    <span className={isDark ? 'active' : ''}><Moon size={14} /></span>
    <span className={!isDark ? 'active' : ''}><Sun size={14} /></span>
  </button>;
}

function Header({ title, onMenu, theme, onToggleTheme }) {
  return <header className="topbar">
    <div className="title-wrap"><button className="menu-btn" onClick={onMenu}><Menu /></button><div><span className="eyebrow">REP FLOW /</span><h1>{title}</h1></div></div>
    <div className="top-actions">
      <label className="search"><Search size={17} /><input aria-label="Caută" placeholder="Caută..." /></label>
      <button className="icon-btn"><Bell size={19} /><i /></button>
      <ThemeToggle theme={theme} onToggle={onToggleTheme} />
      <div className="top-avatar">AM</div>
    </div>
  </header>;
}

function StatCard({ label, value, note, icon: Icon, accent, down }) {
  return <article className="stat-card">
    <div className="stat-icon" style={{ color: accent, background: `${accent}18` }}><Icon size={20} /></div>
    <div className="stat-copy"><small>{label}</small><strong>{value}</strong><span className={down ? 'down' : ''}>{down ? <ArrowDownRight /> : <ArrowUpRight />}{note}</span></div>
  </article>;
}

function WeeklyChart() {
  return <div className="weekly-chart">
    <div className="chart-grid"><i /><i /><i /><i /></div>
    <div className="bars">
      {weeklyData.map((item, index) => <div className="bar-col" key={`${item.day}-${index}`}>
        <div className={`bar ${index === 3 ? 'highlight' : ''}`} style={{ height: `${item.value}%` }}><span>{item.value} min</span></div>
        <small>{item.day}</small>
      </div>)}
    </div>
  </div>;
}

function RecentWorkout({ workout }) {
  const Icon = workout.icon;
  return <div className="recent-row">
    <div className="workout-icon" style={{ color: workout.color, background: `${workout.color}17` }}><Icon size={20} /></div>
    <div className="workout-title"><strong>{workout.name}</strong><small>{workout.type} · {workout.date}</small></div>
    <div className="workout-metric"><strong>{workout.duration}<small> min</small></strong><span>Durată</span></div>
    <div className="workout-metric"><strong>{workout.volume.toLocaleString('ro-RO')}<small> kg</small></strong><span>Volum</span></div>
    <div className="workout-metric hide-small"><strong>{workout.exercises}</strong><span>Exerciții</span></div>
    <button className="row-button"><ArrowRight size={18} /></button>
  </div>;
}

function Overview({ setActive, onNew }) {
  return <div className="page-stack">
    <section className="hero-row">
      <div><p className="overline">LUNI, 21 SEPTEMBRIE</p><h2>Hai să ridicăm ștacheta,<br /><em>Andrei.</em></h2><p>Ai finalizat 4 antrenamente săptămâna aceasta. Continuă tot așa.</p></div>
      <button className="primary-btn" onClick={onNew}><Plus size={19} /> Antrenament nou</button>
    </section>
    <section className="stat-grid">
      <StatCard label="ANTRENAMENTE" value="18" note="12.5% luna aceasta" icon={Dumbbell} accent="#d9ff43" />
      <StatCard label="VOLUM TOTAL" value="42.8K kg" note="8.2% luna aceasta" icon={TrendingUp} accent="#7c6cff" />
      <StatCard label="TIMP ACTIV" value="16h 24m" note="2h 10m săptămânal" icon={Clock3} accent="#47cfff" />
      <StatCard label="SERIE CURENTĂ" value="12 zile" note="Record: 18 zile" icon={Flame} accent="#ff7849" />
    </section>
    <section className="dashboard-grid">
      <article className="panel chart-panel">
        <div className="panel-head"><div><span className="overline">ACTIVITATE</span><h3>Săptămâna aceasta</h3></div><button className="select-btn">Ultimele 7 zile <ChevronDown size={15} /></button></div>
        <WeeklyChart />
        <div className="chart-footer"><div><strong>6h 48m</strong><span>Timp total</span></div><div><strong>38.4K kg</strong><span>Volum</span></div><div><strong>4</strong><span>Sesiuni</span></div></div>
      </article>
      <article className="panel next-panel">
        <div className="panel-head"><div><span className="overline">URMĂTORUL</span><h3>Push Strength</h3></div><span className="date-badge"><CalendarDays size={15} /> ASTĂZI</span></div>
        <div className="next-visual"><div className="rings"><span><Dumbbell size={36} /></span></div><div className="next-time"><small>ORA PLANIFICATĂ</small><strong>18:30</strong><span>~ 55 minute</span></div></div>
        <div className="exercise-chips"><span>Bench press</span><span>Incline DB</span><span>+4</span></div>
        <button className="wide-btn">Începe antrenamentul <ArrowRight size={18} /></button>
      </article>
    </section>
    <section className="panel recent-panel">
      <div className="panel-head"><div><span className="overline">ISTORIC</span><h3>Antrenamente recente</h3></div><button className="text-btn" onClick={() => setActive('workouts')}>Vezi toate <ArrowRight size={16} /></button></div>
      <div className="recent-list">{initialWorkouts.slice(0, 3).map(w => <RecentWorkout workout={w} key={w.id} />)}</div>
    </section>
  </div>;
}

function Workouts({ workouts, onNew }) {
  const [filter, setFilter] = useState('Toate');
  const filtered = filter === 'Toate' ? workouts : workouts.filter(w => w.type.includes(filter));
  return <div className="page-stack">
    <section className="hero-row compact"><div><p className="overline">JURNAL DE ANTRENAMENT</p><h2>Antrenamentele <em>tale.</em></h2><p>Planifică, urmărește și depășește fiecare obiectiv.</p></div><button className="primary-btn" onClick={onNew}><Plus size={19} /> Adaugă antrenament</button></section>
    <section className="workout-toolbar">
      <div className="filters">{['Toate', 'Piept', 'Spate', 'Picioare'].map(f => <button className={filter === f ? 'active' : ''} onClick={() => setFilter(f)} key={f}>{f}</button>)}</div>
      <span>{filtered.length} sesiuni</span>
    </section>
    <section className="workout-cards">
      {filtered.map((workout, index) => { const Icon = workout.icon; return <article className="workout-card" key={workout.id}>
        <div className="card-accent" style={{ background: workout.color }} />
        <div className="workout-card-top"><div className="workout-icon large" style={{ color: workout.color, background: `${workout.color}17` }}><Icon /></div><button><MoreHorizontal /></button></div>
        <span className="workout-index">SESIUNEA {String(workouts.length - index).padStart(2, '0')}</span><h3>{workout.name}</h3><p>{workout.type}</p>
        <div className="workout-card-stats"><div><Clock3 /><strong>{workout.duration}</strong><span>min</span></div><div><Weight /><strong>{(workout.volume / 1000).toFixed(1)}K</strong><span>kg</span></div><div><Activity /><strong>{workout.exercises}</strong><span>exerciții</span></div></div>
        <div className="card-date"><CalendarDays size={15} /> {workout.date}<button>Detalii <ArrowRight size={15} /></button></div>
      </article>; })}
    </section>
  </div>;
}

function Statistics() {
  return <div className="page-stack">
    <section className="hero-row compact"><div><p className="overline">ANALIZA PROGRESULUI</p><h2>Progresul în <em>cifre.</em></h2><p>Datele nu mint. Ești mai puternic decât luna trecută.</p></div><button className="select-btn big">Ultimele 30 zile <ChevronDown size={16} /></button></section>
    <section className="stat-grid stats-page">
      <StatCard label="MEDIE SĂPTĂMÂNALĂ" value="4.2 sesiuni" note="0.6 față de august" icon={CalendarDays} accent="#d9ff43" />
      <StatCard label="VOLUM MEDIU" value="9,460 kg" note="8.2% progres" icon={Weight} accent="#7c6cff" />
      <StatCard label="RECORDURI NOI" value="7 PR-uri" note="Top 14% utilizatori" icon={Trophy} accent="#f3bd4f" />
    </section>
    <section className="statistics-grid">
      <article className="panel volume-panel">
        <div className="panel-head"><div><span className="overline">VOLUM LUNAR</span><h3>Încărcare totală</h3></div><div className="legend"><i /> Volum (kg)</div></div>
        <div className="line-chart">
          <div className="y-labels"><span>50K</span><span>40K</span><span>30K</span><span>20K</span><span>10K</span><span>0</span></div>
          <svg viewBox="0 0 800 250" preserveAspectRatio="none" aria-label="Grafic volum lunar"><defs><linearGradient id="area" x1="0" y1="0" x2="0" y2="1"><stop offset="0%" stopColor="#d9ff43" stopOpacity=".28"/><stop offset="100%" stopColor="#d9ff43" stopOpacity="0"/></linearGradient></defs><path className="area" d="M0,210 C60,195 85,185 125,176 S205,188 250,153 S330,144 375,126 S455,137 500,92 S580,110 625,72 S705,80 800,24 L800,250 L0,250Z"/><path className="line" d="M0,210 C60,195 85,185 125,176 S205,188 250,153 S330,144 375,126 S455,137 500,92 S580,110 625,72 S705,80 800,24"/><circle cx="800" cy="24" r="6" /></svg>
          <div className="x-labels"><span>Apr</span><span>Mai</span><span>Iun</span><span>Iul</span><span>Aug</span><span>Sep</span></div>
        </div>
      </article>
      <article className="panel consistency-panel"><div className="panel-head"><div><span className="overline">CONSECVENTĂ</span><h3>Ritmul tău</h3></div><Flame className="panel-symbol" /></div><div className="big-score">87<span>%</span></div><p>Excelent! Te-ai antrenat conform planului în 26 din ultimele 30 de zile.</p><div className="heatmap">{Array.from({ length: 35 }, (_, i) => <i key={i} className={i % 9 === 0 || i === 31 ? 'low' : i % 6 === 0 ? 'mid' : 'high'} />)}</div><div className="heat-label"><span>Acum 5 săpt.</span><span>Astăzi</span></div></article>
    </section>
    <section className="panel progress-panel"><div className="panel-head"><div><span className="overline">EXERCIȚII PRINCIPALE</span><h3>Evoluția forței</h3></div><button className="text-btn">Vezi raportul <ArrowRight size={16} /></button></div>
      <div className="progress-table">{exerciseProgress.map(item => <div className="progress-row" key={item.name}><div className="round-icon"><Dumbbell size={18} /></div><strong>{item.name}</strong><div className="progress-track"><i style={{ width: `${item.current / item.max * 100}%` }} /></div><b>{item.current} kg</b><span><ArrowUpRight size={14} /> {item.change}%</span></div>)}</div>
    </section>
  </div>;
}

function Account() {
  const [saved, setSaved] = useState(false);
  return <div className="page-stack">
    <section className="hero-row compact"><div><p className="overline">PROFIL & PREFERINȚE</p><h2>Contul <em>tău.</em></h2><p>Personalizează experiența și obiectivele tale de fitness.</p></div></section>
    <section className="account-grid">
      <article className="panel profile-card"><div className="profile-cover"><span>MEMBRU DIN 2024</span></div><div className="profile-avatar">AM<button><Plus size={16} /></button></div><h3>Andrei Matei</h3><p>andrei.matei@example.com</p><div className="profile-level"><span>NIVEL 18</span><div><i /></div><small>1,240 XP până la nivelul următor</small></div><div className="profile-mini-stats"><div><strong>142</strong><span>Antrenamente</span></div><div><strong>12</strong><span>Serie curentă</span></div><div><strong>26</strong><span>Recorduri</span></div></div></article>
      <article className="panel settings-card"><div className="panel-head"><div><span className="overline">DATE PERSONALE</span><h3>Informații profil</h3></div><button className="edit-pill">Editează</button></div>
        <form onSubmit={e => { e.preventDefault(); setSaved(true); setTimeout(() => setSaved(false), 2500); }}>
          <div className="form-grid"><label>Prenume<input defaultValue="Andrei" /></label><label>Nume<input defaultValue="Matei" /></label><label>Email<input type="email" defaultValue="andrei.matei@example.com" /></label><label>Telefon<input defaultValue="+40 723 456 789" /></label><label>Înălțime<div className="unit-input"><input type="number" defaultValue="181" /><span>cm</span></div></label><label>Greutate<div className="unit-input"><input type="number" defaultValue="82" /><span>kg</span></div></label></div>
          <div className="goal-select"><div className="round-icon lime"><Target /></div><div><span>OBIECTIV PRINCIPAL</span><strong>Creștere în masă musculară</strong></div><ChevronDown /></div>
          <button className={`primary-btn save ${saved ? 'saved' : ''}`} type="submit">{saved ? <><Check /> Salvat</> : 'Salvează modificările'}</button>
        </form>
      </article>
    </section>
  </div>;
}

function NewWorkoutModal({ onClose, onAdd }) {
  const [name, setName] = useState(''); const [type, setType] = useState('Piept & Triceps');
  return <div className="modal-wrap" role="dialog" aria-modal="true"><button className="modal-bg" onClick={onClose} /><div className="modal"><div className="modal-head"><div><span className="overline">SESIUNE NOUĂ</span><h3>Adaugă antrenament</h3></div><button onClick={onClose}><X /></button></div><form onSubmit={e => { e.preventDefault(); if (!name.trim()) return; onAdd({ name, type }); }}><label>Numele antrenamentului<input autoFocus value={name} onChange={e => setName(e.target.value)} placeholder="ex. Push Strength" required /></label><label>Grupă musculară<select value={type} onChange={e => setType(e.target.value)}><option>Piept & Triceps</option><option>Spate & Biceps</option><option>Picioare</option><option>Upper body</option><option>Condiționare</option></select></label><div className="modal-actions"><button type="button" className="cancel-btn" onClick={onClose}>Anulează</button><button className="primary-btn" type="submit">Adaugă sesiunea <ArrowRight size={17} /></button></div></form></div></div>;
}

export default function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(() => sessionStorage.getItem('repflow-authenticated') === 'true');
  const [authView, setAuthView] = useState('login');
  const [active, setActive] = useState('overview');
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [workouts, setWorkouts] = useState(initialWorkouts);
  const [theme, setTheme] = useState(() => localStorage.getItem('repflow-theme') || 'dark');
  const titles = { overview: 'Prezentare', workouts: 'Antrenamente', statistics: 'Statistici', account: 'Contul meu' };
  const page = useMemo(() => {
    if (active === 'workouts') return <Workouts workouts={workouts} onNew={() => setModalOpen(true)} />;
    if (active === 'statistics') return <Statistics />;
    if (active === 'account') return <Account />;
    return <Overview setActive={setActive} onNew={() => setModalOpen(true)} />;
  }, [active, workouts]);
  const addWorkout = ({ name, type }) => {
    setWorkouts(prev => [{ id: Date.now(), name, type, date: 'Astăzi', duration: 0, volume: 0, exercises: 0, color: '#54b8ff', icon: Dumbbell }, ...prev]);
    setModalOpen(false); setActive('workouts');
  };
  const logout = () => {
    ['token', 'authToken', 'accessToken', 'user'].forEach(key => {
      localStorage.removeItem(key);
      sessionStorage.removeItem(key);
    });
    sessionStorage.setItem('repflow-authenticated', 'false');
    setSidebarOpen(false);
    setModalOpen(false);
    setIsAuthenticated(false);
  };
  const login = async ({ email, password }) => {
    const response = await fetch('http://localhost:5005/api/account/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
      throw new Error(await response.text() || 'Email sau parolă incorectă.');
    }

    sessionStorage.setItem('repflow-authenticated', 'true');
    setIsAuthenticated(true);
    setActive('overview');
  };
  const register = async ({ username, email, password }) => {
    const response = await fetch('http://localhost:5005/api/account/register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username, email, password }),
    });

    if (!response.ok) {
      throw new Error(await response.text() || 'Înregistrarea a eșuat.');
    }

    setAuthView('login');
  };
  const toggleTheme = () => setTheme(currentTheme => {
    const nextTheme = currentTheme === 'dark' ? 'light' : 'dark';
    localStorage.setItem('repflow-theme', nextTheme);
    return nextTheme;
  });

  if (!isAuthenticated) {
    return authView === 'register'
      ? <Register onRegister={register} onSwitchLogin={() => setAuthView('login')} />
      : <Login onLogin={login} onSwitchRegister={() => setAuthView('register')} />;
  }

  return <div className="app-shell" data-theme={theme}><Sidebar active={active} setActive={setActive} open={sidebarOpen} onClose={() => setSidebarOpen(false)} onLogout={logout} /><main><Header title={titles[active]} onMenu={() => setSidebarOpen(true)} theme={theme} onToggleTheme={toggleTheme} /><div className="page-content">{page}</div></main>{modalOpen && <NewWorkoutModal onClose={() => setModalOpen(false)} onAdd={addWorkout} />}</div>;
}
