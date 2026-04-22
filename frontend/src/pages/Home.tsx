import React from 'react';
import { Shield, Users, Building, Monitor } from 'lucide-react';
import { Link } from 'react-router-dom';

const Home: React.FC = () => {
  return (
    <div className="home">
      <div className="hero card">
        <h1>Bem-vindo ao Gestão de Acesso</h1>
        <p>Sistema centralizado de identidades, unidades e permissões especiais.</p>
      </div>

      <div className="grid" style={{ marginTop: '2rem' }}>
        <Link to="/usuarios" className="card feature-card">
          <Users size={32} color="var(--primary-color)" />
          <h3>Usuários</h3>
          <p>Gerencie identidades locais e integre com Azure AD.</p>
        </Link>

        <Link to="/unidades" className="card feature-card">
          <Building size={32} color="var(--success-color)" />
          <h3>Unidades</h3>
          <p>Consulte e gerencie unidades organizacionais.</p>
        </Link>

        <Link to="/aplicacoes" className="card feature-card">
          <Monitor size={32} color="var(--info-color)" />
          <h3>Aplicações</h3>
          <p>Configure os sistemas do ecossistema.</p>
        </Link>
      </div>

      <style>{`
        .hero {
          text-align: center;
          background: linear-gradient(135deg, var(--primary-color), #0056b3);
          color: white;
          padding: 4rem 2rem;
        }
        .hero h1 { font-size: 2.5rem; margin-bottom: 1rem; }
        .hero p { font-size: 1.2rem; opacity: 0.9; }
        
        .feature-card {
          text-align: center;
          padding: 2rem;
          display: flex;
          flex-direction: column;
          align-items: center;
          gap: 1rem;
          transition: 0.3s;
        }
        .feature-card:hover {
          transform: translateY(-10px);
          border-color: var(--primary-color);
        }
        .feature-card h3 { margin: 0; }
        .feature-card p { font-size: 0.9rem; color: var(--secondary-color); margin: 0; }
      `}</style>
    </div>
  );
};

export default Home;
