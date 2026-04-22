using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;

namespace GestaoAcesso.Domain.Interfaces;

/// <summary>
/// Interface de repositório para Unidades.
/// </summary>
public interface IUnidadeRepository
{
    /// <summary>
    /// Obtém todas as unidades cadastradas no sistema.
    /// </summary>
    /// <returns>Lista de unidades.</returns>
    Task<IEnumerable<Unidade>> ObterTodasAsync();

    /// <summary>
    /// Obtém uma unidade pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador da unidade.</param>
    /// <returns>A unidade encontrada ou null.</returns>
    Task<Unidade?> ObterPorIdAsync(Guid id);
}
