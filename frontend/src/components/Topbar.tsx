import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useTheme } from '../contexts/ThemeContext';
import { Sun, Moon, Users, Building, Shield, ChevronDown, Monitor, Key } from 'lucide-react';

const Topbar: React.FC = () => {
  const { theme, setTheme } = useTheme();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);

  const toggleMenu = (menu: string) => {
    setActiveMenu(activeMenu === menu ? null : menu);
  };

  return (
    <header className="topbar">
      <div className="container topbar-content">
        <Link to="/" className="logo">
          <Shield size={24} color="var(--primary-color)" />
          <span>Gestão de Acesso</span>
        </Link>

        <nav className="nav">
          <div className="nav-item">
            <button className="nav-link" onClick={() => toggleMenu('cadastros')}>
              Cadastros <ChevronDown size={16} />
            </button>
            {activeMenu === 'cadastros' && (
              <div className="dropdown card">
                <Link to="/usuarios" onClick={() => setActiveMenu(null)}>
                  <Users size={16} /> Usuários
                </Link>
                <Link to="/unidades" onClick={() => setActiveMenu(null)}>
                  <Building size={16} /> Unidades
                </Link>
              </div>
            )}
          </div>

          <div className="nav-item">
            <button className="nav-link" onClick={() => toggleMenu('seguranca')}>
              Segurança <ChevronDown size={16} />
            </button>
            {activeMenu === 'seguranca' && (
              <div className="dropdown card">
                <Link to="/aplicacoes" onClick={() => setActiveMenu(null)}>
                  <Monitor size={16} /> Aplicações
                </Link>
                <Link to="/perfis" onClick={() => setActiveMenu(null)}>
                  <Shield size={16} /> Perfis
                </Link>
                <Link to="/permissoes" onClick={() => setActiveMenu(null)}>
                  <Key size={16} /> Permissões
                </Link>
              </div>
            )}
          </div>
        </nav>

        <div className="theme-toggle">
          <button 
            className={`btn-toggle ${theme === 'light' ? 'active' : ''}`} 
            onClick={() => setTheme('light')}
            title="Tema Claro"
          >
            <Sun size={18} />
          </button>
          <button 
            className={`btn-toggle ${theme === 'dark' ? 'active' : ''}`} 
            onClick={() => setTheme('dark')}
            title="Tema Escuro"
          >
            <Moon size={18} />
          </button>
          <button 
            className={`btn-toggle ${theme === 'system' ? 'active' : ''}`} 
            onClick={() => setTheme('system')}
            title="Tema do Sistema"
          >
            <Monitor size={18} />
          </button>
        </div>
      </div>

      <style>{`
        .topbar {
          background-color: var(--header-bg);
          border-bottom: 1px solid var(--border-color);
          box-shadow: var(--shadow);
          height: 64px;
          display: flex;
          align-items: center;
          position: sticky;
          top: 0;
          z-index: 1000;
        }
        .topbar-content {
          display: flex;
          align-items: center;
          justify-content: space-between;
          width: 100%;
        }
        .logo {
          display: flex;
          align-items: center;
          gap: 0.625rem;
          font-weight: 700;
          font-size: 1.125rem;
          color: var(--text-color);
        }
        .nav {
          display: flex;
          gap: 1.5rem;
        }
        .nav-item {
          position: relative;
        }
        .nav-link {
          background: transparent;
          color: var(--text-color);
          font-weight: 600;
          padding: 0.5rem 0.75rem;
        }
        .nav-link:hover {
          color: var(--primary-color);
        }
        .dropdown {
          position: absolute;
          top: calc(100% + 0.5rem);
          left: 0;
          min-width: 180px;
          display: flex;
          flex-direction: column;
          padding: 0.5rem !important;
          animation: slideDown 0.2s ease-out;
        }
        @keyframes slideDown {
          from { opacity: 0; transform: translateY(-10px); }
          to { opacity: 1; transform: translateY(0); }
        }
        .dropdown a {
          padding: 0.625rem 0.75rem;
          display: flex;
          align-items: center;
          gap: 0.75rem;
          font-size: 0.875rem;
          border-radius: var(--border-radius);
          font-weight: 500;
        }
        .dropdown a:hover {
          background-color: var(--surface-hover);
          color: var(--primary-color);
        }
        .theme-toggle {
          display: flex;
          background-color: var(--bg-color);
          padding: 4px;
          border-radius: 99px;
          border: 1px solid var(--border-color);
        }
        .btn-toggle {
          padding: 0.5rem;
          border-radius: 50%;
          color: var(--text-secondary);
          background: transparent;
        }
        .btn-toggle:hover {
          color: var(--text-color);
        }
        .btn-toggle.active {
          background-color: var(--surface-color);
          color: var(--primary-color);
          box-shadow: var(--shadow);
        }
      `}</style>
    </header>
  );
};

export default Topbar;
