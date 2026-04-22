import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ThemeProvider } from './contexts/ThemeContext';
import Layout from './components/Layout';
import Home from './pages/Home';
import Usuarios from './pages/Usuarios';
import Unidades from './pages/Unidades';
import Aplicacoes from './pages/Aplicacoes';
import Perfis from './pages/Perfis';
import Permissoes from './pages/Permissoes';
import './styles/global.css';

function App() {
  return (
    <ThemeProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path="usuarios" element={<Usuarios />} />
            <Route path="unidades" element={<Unidades />} />
            <Route path="aplicacoes" element={<Aplicacoes />} />
            <Route path="perfis" element={<Perfis />} />
            <Route path="permissoes" element={<Permissoes />} />
            <Route path="*" element={<div className="container"><h1>Página não encontrada</h1></div>} />
          </Route>
        </Routes>
      </BrowserRouter>

      <style>{`
        .card {
          background-color: var(--surface-color);
          border: 1px solid var(--border-color);
          padding: 2rem;
          border-radius: 8px;
          box-shadow: var(--shadow);
        }
        .page-header h1 {
          font-size: 1.8rem;
          color: var(--text-color);
        }
      `}</style>
    </ThemeProvider>
  );
}

export default App;
