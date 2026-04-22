using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;

namespace GestaoAcesso.Domain.Interfaces;

/// <summary>
/// Interface de repositório para a entidade Usuario.
/// </summary>
public interface IUsuarioRepository
{
    /// <summary>
    /// Obtém um usuário pelo CPF.
    /// </summary>
    Task<Usuario?> ObterPorCpfAsync(string cpf);

    /// <summary>
    /// Obtém todos os usuários cadastrados.
    /// </summary>
    Task<IEnumerable<Usuario>> ObterTodosAsync();

    /// <summary>
    /// Obtém um usuário pelo AzureUniqueId.
    /// </summary>
    Task<Usuario?> ObterPorAzureIdAsync(Guid azureId);
}
