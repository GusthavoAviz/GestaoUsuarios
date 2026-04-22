using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;

namespace GestaoAcesso.Domain.Interfaces;

/// <summary>
/// Interface de repositório para a entidade Aplicação.
/// </summary>
public interface IAplicacaoRepository
{
    /// <summary>
    /// Obtém uma aplicação pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador da aplicação.</param>
    /// <returns>A aplicação encontrada ou null.</returns>
    Task<Aplicacao?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Obtém todas as aplicações cadastradas no sistema.
    /// </summary>
    /// <returns>Lista de aplicações.</returns>
    Task<IEnumerable<Aplicacao>> ObterTodasAsync();

    /// <summary>
    /// Adiciona uma nova aplicação ao repositório.
    /// </summary>
    /// <param name="aplicacao">Entidade da aplicação.</param>
    Task AdicionarAsync(Aplicacao aplicacao);

    /// <summary>
    /// Atualiza os dados de uma aplicação existente.
    /// </summary>
    /// <param name="aplicacao">Entidade da aplicação com dados atualizados.</param>
    void Atualizar(Aplicacao aplicacao);

    /// <summary>
    /// Remove uma aplicação do sistema.
    /// </summary>
    /// <param name="aplicacao">Entidade da aplicação a ser removida.</param>
    void Remover(Aplicacao aplicacao);

    /// <summary>
    /// Persiste todas as alterações pendentes no banco de dados.
    /// </summary>
    /// <returns>True se as alterações foram salvas com sucesso.</returns>
    Task<bool> SalvarAlteracoesAsync();
}
