import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { Key, Loader2, Plus, Calendar, User, FileText } from 'lucide-react';

interface Permissao {
  id: string;
  usuarioAzureId: string;
  nome: string;
  justificativa: string;
  dataExpiracao: string;
  concedidoPor: string;
  dataConcessao: string;
}

const Permissoes: React.FC = () => {
  const [permissoes, setPermissoes] = useState<Permissao[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Busca todas as permissões especiais (implementar endpoint no backend se necessário)
    api.get<Permissao[]>('/permissao').catch(() => []).then(r => {
      setPermissoes(Array.isArray(r) ? r : []);
      setLoading(false);
    });
  }, []);

  if (loading) return <div className="center"><Loader2 className="animate-spin" size={48} /></div>;

  return (
    <div className="page">
      <div className="page-header">
        <h1>Permissões Especiais</h1>
        <button className="btn primary"><Plus size={18} /> Nova Permissão</button>
      </div>

      <div className="card">
        {permissoes.length === 0 ? (
          <div className="empty-state">
            <Key size={48} color="var(--border-color)" />
            <p>Nenhuma permissão especial ativa encontrada.</p>
          </div>
        ) : (
          <table className="table">
            <thead>
              <tr>
                <th>Funcionalidade</th>
                <th>Usuário (ID)</th>
                <th>Expira em</th>
                <th>Concedido por</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {permissoes.map(p => {
                const expirada = new Date(p.dataExpiracao) < new Date();
                return (
                  <tr key={p.id}>
                    <td>
                      <div className="cell-content">
                        <strong>{p.nome}</strong>
                        <span className="justificativa" title={p.justificativa}>
                          <FileText size={12} /> {p.justificativa}
                        </span>
                      </div>
                    </td>
                    <td><User size={14} /> {p.usuarioAzureId.substring(0, 8)}...</td>
                    <td>
                      <div className="cell-content">
                        <span><Calendar size={14} /> {new Date(p.dataExpiracao).toLocaleDateString()}</span>
                      </div>
                    </td>
                    <td>{p.concedidoPor}</td>
                    <td>
                      <span className={`tag ${expirada ? 'danger' : 'success'}`}>
                        {expirada ? 'Expirada' : 'Ativa'}
                      </span>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        )}
      </div>

      <style>{`
        .empty-state {
          display: flex;
          flex-direction: column;
          align-items: center;
          padding: 4rem;
          color: var(--secondary-color);
          gap: 1rem;
        }
        .justificativa {
          display: flex;
          align-items: center;
          gap: 0.25rem;
          font-size: 0.75rem;
          color: var(--secondary-color);
          max-width: 200px;
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
        }
        .cell-content {
          display: flex;
          flex-direction: column;
          gap: 0.25rem;
        }
        .tag.success { background-color: #d1fae5; color: #065f46; }
        .tag.danger { background-color: #fee2e2; color: #991b1b; }
      `}</style>
    </div>
  );
};

export default Permissoes;
