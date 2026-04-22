using System.Threading.Tasks;
using GestaoAcesso.Application.ViewModels;
using GestaoAcesso.Domain.Entities;

namespace GestaoAcesso.Application.Interfaces;

/// <summary>
/// Interface do serviço de aplicação para Usuários.
/// </summary>
public interface IUsuarioAppService
{
    /// <summary>
    /// Obtém os dados de um usuário pelo CPF.
    /// </summary>
    Task<Usuario?> ObterPorCpfAsync(string cpf);

    /// <summary>
    /// Obtém todos os usuários cadastrados com dados consolidados.
    /// </summary>
    Task<IEnumerable<UsuarioViewModel>> ObterTodosAsync();

    /// <summary>
    /// Obtém o retorno completo do usuário (AD + Local + Permissões) via ID ou CPF.
    /// </summary>
    Task<UsuarioViewModel?> ObterUsuarioCompletoAsync(string idOuCpf);
}
