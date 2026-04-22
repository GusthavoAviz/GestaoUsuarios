using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;

namespace GestaoAcesso.Domain.Interfaces;

/// <summary>
/// Interface de repositório para a entidade Permissão.
/// </summary>
public interface IPermissaoRepository
{
    /// <summary>
    /// Obtém todas as permissões ativas de um usuário.
    /// </summary>
    Task<IEnumerable<Permissao>> ObterAtivasPorUsuarioAsync(Guid azureId);
}
