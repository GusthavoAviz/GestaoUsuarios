using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;
using GestaoAcesso.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoAcesso.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Aplicações utilizando EF Core.
/// </summary>
public class AplicacaoRepository : IAplicacaoRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Construtor do repositório de aplicações.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public AplicacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém uma aplicação pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID da aplicação.</param>
    /// <returns>A aplicação encontrada ou nulo.</returns>
    public async Task<Aplicacao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Aplicacoes
            .Include(a => a.Perfis)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    /// <summary>
    /// Lista todas as aplicações cadastradas.
    /// </summary>
    /// <returns>Lista de aplicações.</returns>
    public async Task<IEnumerable<Aplicacao>> ObterTodasAsync()
    {
        return await _context.Aplicacoes.ToListAsync();
    }

    /// <summary>
    /// Adiciona uma nova aplicação ao banco de dados.
    /// </summary>
    /// <param name="aplicacao">Entidade aplicação.</param>
    public async Task AdicionarAsync(Aplicacao aplicacao)
    {
        await _context.Aplicacoes.AddAsync(aplicacao);
    }

    /// <summary>
    /// Marca uma aplicação para atualização.
    /// </summary>
    /// <param name="aplicacao">Entidade aplicação.</param>
    public void Atualizar(Aplicacao aplicacao)
    {
        _context.Aplicacoes.Update(aplicacao);
    }

    /// <summary>
    /// Remove uma aplicação do banco de dados.
    /// </summary>
    /// <param name="aplicacao">Entidade aplicação.</param>
    public void Remover(Aplicacao aplicacao)
    {
        _context.Aplicacoes.Remove(aplicacao);
    }

    /// <summary>
    /// Persiste as alterações pendentes no banco de dados.
    /// </summary>
    /// <returns>Verdadeiro se houver sucesso.</returns>
    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
