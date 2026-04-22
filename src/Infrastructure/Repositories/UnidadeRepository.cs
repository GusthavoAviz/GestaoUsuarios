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
/// Implementação do repositório para Unidades via EF Core.
/// </summary>
public class UnidadeRepository : IUnidadeRepository
{
    private readonly AppDbContext _context;
/// <summary>
/// Construtor do repositório de unidades.
/// </summary>
/// <param name="context">Contexto do banco de dados.</param>
public UnidadeRepository(AppDbContext context)
{
    _context = context;
}

/// <summary>
/// Lista todas as unidades com status Ativo.
/// </summary>
/// <returns>Lista de unidades ativas.</returns>
public async Task<IEnumerable<Unidade>> ObterTodasAtivasAsync()
{
    return await _context.Unidades
        .Where(u => u.Ativo)
        .ToListAsync();
}
    /// <summary>
    /// Obtém uma unidade pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID da unidade.</param>
    /// <returns>A unidade encontrada ou nulo.</returns>
    public async Task<Unidade?> ObterPorIdAsync(Guid id)
    {
        return await _context.Unidades.FirstOrDefaultAsync(u => u.Id == id);
    }
}
