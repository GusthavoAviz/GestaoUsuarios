import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { Search, Loader2, User, ArrowRight } from 'lucide-react';

interface Usuario {
  azureId: string;
  nome: string;
  cpf: string;
  rg: string;
  nit: string;
  unidadePrincipal: string;
  unidadesSecundarias: string[];
  perfis: string[];
  permissoesAtivas: string[];
  emailAd: string;
}

const Usuarios: React.FC = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);
  const [usuarioSelecionado, setUsuarioSelecionado] = useState<Usuario | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const carregarTodos = async () => {
    setLoading(true);
    setError('');
    try {
      const response = await api.get<Usuario[]>('/usuario');
      setUsuarios(response.data);
      setUsuarioSelecionado(null);
    } catch (err) {
      setError('Erro ao carregar lista de usuários.');
    } finally {
      setLoading(false);
    }
  };

  const buscarUsuario = async () => {
    if (!searchTerm) {
      carregarTodos();
      return;
    }
    
    setLoading(true);
    setError('');
    try {
      const response = await api.get<Usuario>(`/usuario/${searchTerm}`);
      if (response.data) {
        setUsuarioSelecionado(response.data);
        setUsuarios([]);
      }
    } catch (err) {
      setError('Usuário não encontrado.');
      setUsuarioSelecionado(null);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarTodos();
  }, []);

  return (
    <div className="page">
      <div className="page-header" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '2rem', flexWrap: 'wrap', gap: '1rem' }}>
        <h1>Gestão de Usuários</h1>
        <div className="search-bar" style={{ display: 'flex', gap: '0.5rem', width: '100%', maxWidth: '400px' }}>
          <input 
            type="text" 
            placeholder="Buscar por CPF ou ID Azure..." 
            value={searchTerm}
            style={{ flex: 1 }}
            onChange={(e) => setSearchTerm(e.target.value)}
            onKeyPress={(e) => e.key === 'Enter' && buscarUsuario()}
          />
          <button className="btn-primary" onClick={buscarUsuario} disabled={loading} style={{ padding: '0 1.25rem' }}>
            {loading ? <Loader2 className="animate-spin" size={18} /> : <Search size={18} />}
          </button>
        </div>
      </div>

      {error && <div className="alert-error" style={{ background: 'var(--danger-color)', color: 'white', padding: '0.75rem 1rem', borderRadius: 'var(--border-radius)', marginBottom: '1.5rem', fontSize: '0.875rem' }}>{error}</div>}

      {usuarioSelecionado && (
        <div className="card" style={{ borderLeft: '4px solid var(--primary-color)' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1.5rem' }}>
            <button className="btn-outline btn-sm" onClick={carregarTodos} style={{ padding: '0.25rem 0.75rem' }}>← Voltar para lista</button>
            <span className="tag tag-success">Resultado da Busca</span>
          </div>
          <UserCard usuario={usuarioSelecionado} detailed={true} />
        </div>
      )}

      {!usuarioSelecionado && (
        <div className="grid">
          {usuarios.map(u => (
            <div key={u.azureId} className="card clickable" onClick={() => setUsuarioSelecionado(u)} style={{ cursor: 'pointer', position: 'relative' }}>
              <UserCard usuario={u} detailed={false} />
              <div style={{ marginTop: '1rem', fontSize: '0.75rem', color: 'var(--primary-color)', display: 'flex', alignItems: 'center', gap: '0.25rem', fontWeight: 600 }}>
                Clique para detalhes <ArrowRight size={14} />
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

const UserCard = ({ usuario, detailed }: { usuario: Usuario, detailed: boolean }) => (
  <div className="user-card-content">
    <div style={{ display: 'flex', gap: '1rem', alignItems: 'center' }}>
      <div style={{ background: 'var(--bg-color)', padding: '0.75rem', borderRadius: '50%', color: 'var(--primary-color)', border: '1px solid var(--border-color)' }}>
        <User size={24} />
      </div>
      <div>
        <h3 style={{ marginBottom: '0.125rem' }}>{usuario.nome}</h3>
        <p style={{ fontSize: '0.875rem', color: 'var(--text-secondary)' }}>{usuario.emailAd}</p>
      </div>
    </div>

    <div className="user-grid" style={{ marginTop: '1.25rem', display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1rem', fontSize: '0.875rem' }}>
      <div>
        <h4 style={{ fontSize: '0.625rem' }}>Documento</h4>
        <p style={{ fontWeight: 500 }}>CPF {usuario.cpf}</p>
      </div>
      <div>
        <h4 style={{ fontSize: '0.625rem' }}>Unidade</h4>
        <span className="tag tag-primary">{usuario.unidadePrincipal}</span>
      </div>
    </div>

    {detailed && (
      <div className="details-expanded" style={{ marginTop: '1.5rem', paddingTop: '1.5rem', borderTop: '1px solid var(--border-color)' }}>
        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '1.5rem' }}>
          <div>
            <h4>Documentos Auxiliares</h4>
            <p style={{ fontSize: '0.875rem' }}>RG: {usuario.rg || 'N/A'}</p>
            <p style={{ fontSize: '0.875rem' }}>NIT: {usuario.nit || 'N/A'}</p>
          </div>
          <div>
            <h4>Perfis Atribuídos</h4>
            <div style={{ display: 'flex', flexWrap: 'wrap', gap: '0.4rem', marginTop: '0.4rem' }}>
              {usuario.perfis.map(p => <span key={p} className="tag tag-primary" style={{ borderRadius: '4px' }}>{p}</span>)}
            </div>
          </div>
        </div>
        <div style={{ marginTop: '1.5rem' }}>
          <h4>Permissões de Acesso Especiais</h4>
          <div style={{ display: 'flex', flexWrap: 'wrap', gap: '0.5rem', marginTop: '0.5rem' }}>
            {usuario.permissoesAtivas.length > 0 ? (
              usuario.permissoesAtivas.map(p => <span key={p} className="tag tag-success" style={{ borderRadius: '4px' }}>{p}</span>)
            ) : (
              <span style={{ fontSize: '0.875rem', color: 'var(--text-secondary)', fontStyle: 'italic' }}>Nenhuma permissão especial ativa.</span>
            )}
          </div>
        </div>
      </div>
    )}
  </div>
);

export default Usuarios;
