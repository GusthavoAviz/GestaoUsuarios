using System;
using System.Threading.Tasks;

namespace GestaoAcesso.Application.Interfaces;

/// <summary>
/// Interface do serviço de aplicação para Permissões.
/// </summary>
public interface IPermissaoAppService
{
    /// <summary>
    /// Valida se o usuário possui uma permissão específica ativa.
    /// </summary>
    Task<bool> ValidarPermissaoAtivaAsync(Guid azureId, string nomePermissao);
}
