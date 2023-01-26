import * as React from 'react';
import { Routes, Route, Link } from 'react-router-dom';
import Navigation from './components/Navigation';
import Home from './pages/Home';
import DailyReport from './pages/DailyReport';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';

const queryClient = new QueryClient();

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Routes>
        <Route path="/" element={<Navigation />}>
          <Route index element={<Home />} />
          <Route path="reports" element={<DailyReport />} />
          <Route path="*" element={<NoMatch />} />
        </Route>
      </Routes>
      <ReactQueryDevtools initialIsOpen />
    </QueryClientProvider>
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
