using System;

namespace GestaoAcesso.Domain.Entities;

/// <summary>
/// Entidade que representa uma Unidade organizacional no sistema.
/// </summary>
public class Unidade
{
    protected Unidade() { }

    /// <summary>
    /// Identificador único da unidade.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Razão social da empresa ou unidade.
    /// </summary>
    public string RazaoSocial { get; private set; } = null!;

    /// <summary>
    /// Nome fantasia da unidade.
    /// </summary>
    public string NomeFantasia { get; private set; } = null!;

    /// <summary>
    /// Cadastro Nacional da Pessoa Jurídica (CNPJ).
    /// </summary>
    public string Cnpj { get; private set; } = null!;

    /// <summary>
    /// Endereço físico da unidade.
    /// </summary>
    public string Endereco { get; private set; } = null!;

    /// <summary>
    /// Usuário responsável pela unidade.
    /// </summary>
    public string UsuarioResponsavel { get; private set; } = null!;

    /// <summary>
    /// Indica se a unidade está ativa no sistema.
    /// </summary>
    public bool Ativo { get; private set; }

    /// <summary>
    /// Construtor da entidade Unidade.
    /// </summary>
    public Unidade(string razaoSocial, string nomeFantasia, string cnpj, string endereco, string usuarioResponsavel)
    {
        if (string.IsNullOrWhiteSpace(razaoSocial)) throw new ArgumentException("Razão Social é obrigatória.");
        if (string.IsNullOrWhiteSpace(cnpj)) throw new ArgumentException("CNPJ é obrigatório.");

        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        Endereco = endereco;
        UsuarioResponsavel = usuarioResponsavel;
        Ativo = true;
    }

    /// <summary>
    /// Desativa a unidade no sistema.
    /// </summary>
    public void Desativar() => Ativo = false;

    /// <summary>
    /// Ativa a unidade no sistema.
    /// </summary>
    public void Ativar() => Ativo = true;
}
