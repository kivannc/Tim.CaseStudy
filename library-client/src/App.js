import * as React from 'react';
import { Routes, Route, Outlet, Link } from 'react-router-dom';
import Navigation from './components/Navigation';
import Home from './pages/Home';
import DailyReport from './pages/DailyReport';

export default function App() {
  return (
    <div>
      <Routes>
        <Route path="/" element={<Navigation />}>
          <Route index element={<Home />} />
          <Route path="reports" element={<DailyReport />} />
          <Route path="*" element={<NoMatch />} />
        </Route>
      </Routes>
    </div>
  );
}

function NoMatch() {
  return (
    <div>
      <h2>Page Not Found!</h2>
      <p>
        <Link to="/">Go to the home page</Link>
      </p>
    </div>
  );
}
