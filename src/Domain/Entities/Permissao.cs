using System;

namespace GestaoAcesso.Domain.Entities;

/// <summary>
/// Entidade que representa uma Permissão Especial no sistema.
/// </summary>
public class Permissao
{
    /// <summary>
    /// Identificador único da permissão especial.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Identificador único do usuário no Azure AD.
    /// </summary>
    public Guid UsuarioAzureId { get; private set; }

    /// <summary>
    /// Nome da permissão especial.
    /// </summary>
    public string Nome { get; private set; } = null!;

    /// <summary>
    /// Justificativa técnica ou de negócio para a concessão da permissão especial.
    /// </summary>
    public string Justificativa { get; private set; } = null!;

    /// <summary>
    /// Data e hora em que a permissão especial expira.
    /// </summary>
    public DateTime DataExpiracao { get; private set; }

    /// <summary>
    /// Identificação do responsável que concedeu a permissão.
    /// </summary>
    public string ConcedidoPor { get; private set; } = null!;

    /// <summary>
    /// Data e hora em que a permissão foi concedida.
    /// </summary>
    public DateTime DataConcessao { get; private set; }

    protected Permissao() { }

    /// <summary>
    /// Construtor da entidade Permissao.
    /// </summary>
    public Permissao(Guid usuarioAzureId, string nome, string justificativa, DateTime dataExpiracao, string concedidoPor)
    {
        if (usuarioAzureId == Guid.Empty) throw new ArgumentException("UsuarioAzureId inválido.");
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome da permissão é obrigatório.");
        if (string.IsNullOrWhiteSpace(justificativa)) throw new ArgumentException("Justificativa é obrigatória para permissões especiais.");
        if (string.IsNullOrWhiteSpace(concedidoPor)) throw new ArgumentException("Auditoria: Quem concedeu a permissão é obrigatório.");

        UsuarioAzureId = usuarioAzureId;
        Nome = nome;
        Justificativa = justificativa;
        DataExpiracao = dataExpiracao;
        ConcedidoPor = concedidoPor;
        DataConcessao = DateTime.UtcNow;
    }

    /// <summary>
    /// Verifica se a permissão ainda está ativa.
    /// </summary>
    public bool EstaAtiva() => DateTime.UtcNow < DataExpiracao;
}
