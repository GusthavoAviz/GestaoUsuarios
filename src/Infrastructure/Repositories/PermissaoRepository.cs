using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;
using GestaoAcesso.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoAcesso.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Permissões utilizando EF Core.
/// </summary>
public class PermissaoRepository : IPermissaoRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Construtor do repositório de permissões.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public PermissaoRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém as permissões especiais ativas vinculadas a um usuário.
    /// </summary>
    /// <param name="azureId">ID único do Azure.</param>
    /// <returns>Lista de permissões ativas.</returns>
    public async Task<IEnumerable<Permissao>> ObterAtivasPorUsuarioAsync(Guid azureId)
    {
        var dataAtual = DateTime.UtcNow;
        return await _context.Permissoes
            .Where(p => p.UsuarioAzureId == azureId && p.DataExpiracao > dataAtual)
            .ToListAsync();
    }
}
