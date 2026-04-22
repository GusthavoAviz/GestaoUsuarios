import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { Monitor, Loader2, Plus } from 'lucide-react';

interface Aplicacao {
  id: string;
  nome: string;
  descricao: string;
}

const Aplicacoes: React.FC = () => {
  const [apps, setApps] = useState<Aplicacao[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    api.get<Aplicacao[]>('/aplicacao').then(r => {
      setApps(r.data);
      setLoading(false);
    });
  }, []);

  if (loading) return <div className="center"><Loader2 className="animate-spin" size={48} /></div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Aplicações do Ecossistema</h1>
        <button className="btn primary"><Plus size={18} /> Nova Aplicação</button>
      </div>

      <div className="card">
        <table className="table">
          <thead>
            <tr>
              <th>Nome</th>
              <th>Descrição</th>
              <th>ID</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {apps.map(a => (
              <tr key={a.id}>
                <td><strong>{a.nome}</strong></td>
                <td>{a.descricao}</td>
                <td><code className="badge">{a.id.substring(0, 8)}</code></td>
                <td>
                  <button className="btn sm">Editar</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <style>{`
        .table {
          width: 100%;
          border-collapse: collapse;
          margin-top: 1rem;
        }
        .table th, .table td {
          text-align: left;
          padding: 1rem;
          border-bottom: 1px solid var(--border-color);
        }
        .table th {
          background-color: var(--surface-color);
          font-weight: 600;
          color: var(--secondary-color);
          font-size: 0.85rem;
          text-transform: uppercase;
        }
        .table tr:hover {
          background-color: var(--surface-color);
        }
        .btn {
          display: inline-flex;
          align-items: center;
          gap: 0.5rem;
          padding: 0.6rem 1.2rem;
          border-radius: 4px;
          font-weight: 500;
          transition: 0.2s;
        }
        .btn.primary { background-color: var(--primary-color); color: white; }
        .btn.sm { padding: 0.3rem 0.6rem; font-size: 0.8rem; border: 1px solid var(--border-color); }
        .center { display: flex; justify-content: center; padding: 5rem; }
        @keyframes spin { from { transform: rotate(0deg); } to { transform: rotate(360deg); } }
        .animate-spin { animation: spin 1s linear infinite; }
      `}</style>
    </div>
  );
};

export default Aplicacoes;
