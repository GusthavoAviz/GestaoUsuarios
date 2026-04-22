using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Application.ViewModels;

namespace GestaoAcesso.Application.Interfaces;

/// <summary>
/// Interface do serviço de aplicação para Unidades.
/// </summary>
public interface IUnidadeAppService
{
    /// <summary>
    /// Obtém todas as unidades ativas para seleção no front-end.
    /// </summary>
    Task<IEnumerable<UnidadeViewModel>> ObterTodasAtivasAsync();
}
