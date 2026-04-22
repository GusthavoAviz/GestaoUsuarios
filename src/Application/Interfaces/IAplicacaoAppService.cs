using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Application.ViewModels;

namespace GestaoAcesso.Application.Interfaces;

/// <summary>
/// Interface do serviço de aplicação para CRUD de Aplicações.
/// </summary>
public interface IAplicacaoAppService
{
    /// <summary>
    /// Obtém todas as aplicações cadastradas.
    /// </summary>
    /// <returns>Lista de ViewModels de aplicação.</returns>
    Task<IEnumerable<AplicacaoViewModel>> ObterTodasAsync();

    /// <summary>
    /// Obtém uma aplicação específica pelo seu ID.
    /// </summary>
    /// <param name="id">Identificador da aplicação.</param>
    /// <returns>ViewModel da aplicação encontrada ou null.</returns>
    Task<AplicacaoViewModel?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Cadastra uma nova aplicação no sistema.
    /// </summary>
    /// <param name="input">Dados de entrada da aplicação.</param>
    /// <returns>ID da nova aplicação criada.</returns>
    Task<Guid> AdicionarAsync(AplicacaoInputModel input);

    /// <summary>
    /// Atualiza os dados de uma aplicação existente.
    /// </summary>
    /// <param name="id">Identificador da aplicação.</param>
    /// <param name="input">Dados atualizados.</param>
    Task AtualizarAsync(Guid id, AplicacaoInputModel input);

    /// <summary>
    /// Remove uma aplicação do sistema.
    /// </summary>
    /// <param name="id">Identificador da aplicação.</param>
    Task RemoverAsync(Guid id);
}
