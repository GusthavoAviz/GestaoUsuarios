import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { Building2, Loader2, MapPin, ClipboardList } from 'lucide-react';

interface Unidade {
  id: string;
  nomeFantasia: string;
  razaoSocial: string;
  cnpj: string;
}

const Unidades: React.FC = () => {
  const [unidades, setUnidades] = useState<Unidade[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const carregarUnidades = async () => {
      try {
        const response = await api.get<Unidade[]>('/unidade');
        setUnidades(response.data);
      } catch (err) {
        setError('Erro ao carregar unidades.');
      } finally {
        setLoading(false);
      }
    };
    carregarUnidades();
  }, []);

  if (loading) return (
    <div className="center">
      <Loader2 className="animate-spin" size={48} color="var(--primary-color)" />
    </div>
  );

  return (
    <div className="page">
      <div className="page-header">
        <h1>Unidades Organizacionais</h1>
      </div>

      {error && <div className="alert error">{error}</div>}

      <div className="grid">
        {unidades.map(u => (
          <div key={u.id} className="card unit-card">
            <div className="unit-icon">
              <Building2 size={32} />
            </div>
            <div className="unit-info">
              <h3>{u.nomeFantasia}</h3>
              <p className="razao">{u.razaoSocial}</p>
              <div className="unit-details">
                <p><ClipboardList size={14} /> <strong>CNPJ:</strong> {u.cnpj}</p>
                <p><MapPin size={14} /> <strong>ID:</strong> {u.id.substring(0, 8)}</p>
              </div>
            </div>
          </div>
        ))}
      </div>

      <style>{`
        .unit-card {
          display: flex;
          gap: 1.5rem;
          align-items: flex-start;
          transition: transform 0.2s;
        }
        .unit-card:hover {
          transform: translateY(-5px);
          border-color: var(--primary-color);
        }
        .unit-icon {
          background-color: var(--border-color);
          padding: 1rem;
          border-radius: 8px;
          color: var(--primary-color);
        }
        .razao {
          font-size: 0.9rem;
          color: var(--secondary-color);
          margin-bottom: 1rem;
        }
        .unit-details p {
          display: flex;
          align-items: center;
          gap: 0.5rem;
          font-size: 0.85rem;
          color: var(--text-color);
          margin-bottom: 0.25rem;
        }
        .center {
          display: flex;
          justify-content: center;
          align-items: center;
          height: 50vh;
        }
        @keyframes spin {
          from { transform: rotate(0deg); }
          to { transform: rotate(360deg); }
        }
        .animate-spin {
          animation: spin 1s linear infinite;
        }
      `}</style>
    </div>
  );
};

export default Unidades;
