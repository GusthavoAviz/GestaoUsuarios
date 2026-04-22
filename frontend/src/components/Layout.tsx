import React from 'react';
import { Outlet } from 'react-router-dom';
import Topbar from './Topbar';

const Layout: React.FC = () => {
  return (
    <div className="layout">
      <Topbar />
      <main className="container main-content">
        <Outlet />
      </main>
      <style>{`
        .main-content {
          padding-top: 2rem;
          padding-bottom: 2rem;
        }
      `}</style>
    </div>
  );
};

export default Layout;
