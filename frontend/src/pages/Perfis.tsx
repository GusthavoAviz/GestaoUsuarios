import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { Shield, Loader2, Plus, Key } from 'lucide-react';

interface Perfil {
  id: string;
  nome: string;
  descricao: string;
  aplicacaoNome: string;
  qtdPermissoes: number;
  permissoes: string[];
}

const Perfis: React.FC = () => {
  const [perfis, setPerfis] = useState<Perfil[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get<Perfil[]>('/perfil').then(r => {
      setPerfis(r.data);
      setLoading(false);
    });
  }, []);

  if (loading) return <div className="center"><Loader2 className="animate-spin" size={48} /></div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Perfis de Acesso</h1>
        <button className="btn primary"><Plus size={18} /> Novo Perfil</button>
      </div>

      <div className="grid">
        {perfis.map(p => (
          <div key={p.id} className="card perfil-card">
            <div className="perfil-info">
              <div className="perfil-title">
                <Shield size={24} className="icon" />
                <div>
                  <h3>{p.nome}</h3>
                  <span className="app-badge">{p.aplicacaoNome}</span>
                </div>
              </div>
              <p className="desc">{p.descricao}</p>
              
              <div className="perms-summary">
                <h4><Key size={14} /> Permissões ({p.qtdPermissoes})</h4>
                <div className="tag-list">
                  {p.permissoes.map(perm => (
                    <span key={perm} className="tag sm">{perm}</span>
                  ))}
                </div>
              </div>
            </div>
            <div className="card-footer">
              <button className="btn sm">Editar</button>
              <button className="btn sm danger">Excluir</button>
            </div>
          </div>
        ))}
      </div>

      <style>{`
        .perfil-card {
          display: flex;
          flex-direction: column;
          justify-content: space-between;
          height: 100%;
        }
        .perfil-title {
          display: flex;
          gap: 1rem;
          align-items: center;
          margin-bottom: 1rem;
        }
        .icon { color: var(--primary-color); }
        .app-badge {
          font-size: 0.75rem;
          background: var(--border-color);
          padding: 2px 6px;
          border-radius: 4px;
          text-transform: uppercase;
        }
        .desc {
          font-size: 0.9rem;
          color: var(--secondary-color);
          margin-bottom: 1.5rem;
        }
        .perms-summary h4 {
          font-size: 0.85rem;
          display: flex;
          align-items: center;
          gap: 0.4rem;
          margin-bottom: 0.5rem;
          color: var(--text-color);
        }
        .tag-list {
          display: flex;
          flex-wrap: wrap;
          gap: 0.4rem;
        }
        .tag.sm {
          font-size: 0.7rem;
          background: var(--surface-color);
          border: 1px solid var(--border-color);
        }
        .card-footer {
          margin-top: 1.5rem;
          padding-top: 1rem;
          border-top: 1px solid var(--border-color);
          display: flex;
          gap: 0.5rem;
        }
        .btn.danger { color: var(--danger-color); }
        .btn.danger:hover { background: #fee2e2; }
      `}</style>
    </div>
  );
};

export default Perfis;
