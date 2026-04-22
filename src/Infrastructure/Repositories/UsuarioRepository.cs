using System;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;
using GestaoAcesso.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoAcesso.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Usuários utilizando EF Core.
/// </summary>
public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Construtor do repositório de usuários.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca um usuário pelo CPF, incluindo navegações.
    /// </summary>
    /// <param name="cpf">CPF do usuário.</param>
    /// <returns>Usuário ou nulo.</returns>
    public async Task<Usuario?> ObterPorCpfAsync(string cpf)
    {
        return await _context.Usuarios
            .Include(u => u.UnidadePrincipal)
            .Include(u => u.UnidadesSecundarias)
            .Include(u => u.Perfis)
                .ThenInclude(p => p.Aplicacao)
            .FirstOrDefaultAsync(u => u.Cpf == cpf);
    }

    /// <summary>
    /// Busca um usuário pelo ID único do Azure (Microsoft Entra ID).
    /// </summary>
    /// <param name="azureId">ID do Azure.</param>
    /// <returns>Usuário ou nulo.</returns>
    public async Task<Usuario?> ObterPorAzureIdAsync(Guid azureId)
    {
        return await _context.Usuarios
            .Include(u => u.UnidadePrincipal)
            .Include(u => u.UnidadesSecundarias)
            .Include(u => u.Perfis)
            .FirstOrDefaultAsync(u => u.AzureUniqueId == azureId);
    }

    /// <summary>
    /// Lista todos os usuários cadastrados com suas respectivas unidades e perfis.
    /// </summary>
    /// <returns>Lista de usuários.</returns>
    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await _context.Usuarios
            .Include(u => u.UnidadePrincipal)
            .Include(u => u.UnidadesSecundarias)
            .Include(u => u.Perfis)
            .ToListAsync();
    }
}
