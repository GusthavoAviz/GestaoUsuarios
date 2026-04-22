using System;
using System.Collections.Generic;

namespace GestaoAcesso.Domain.Entities;

/// <summary>
/// Entidade que representa uma Aplicação cadastrada no sistema.
/// </summary>
public class Aplicacao
{
    /// <summary>
    /// Identificador único da aplicação.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Nome da aplicação.
    /// </summary>
    public string Nome { get; private set; } = null!;

    /// <summary>
    /// Descrição detalhada da aplicação.
    /// </summary>
    public string Descricao { get; private set; } = null!;

    /// <summary>
    /// Lista de perfis associados a esta aplicação.
    /// </summary>
    public ICollection<Perfil> Perfis { get; private set; } = new List<Perfil>();

    protected Aplicacao() { }

    /// <summary>
    /// Construtor da entidade Aplicacao.
    /// </summary>
    /// <param name="nome">Nome único da aplicação.</param>
    /// <param name="descricao">Descrição da finalidade da aplicação.</param>
    public Aplicacao(string nome, string descricao)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome da aplicação é obrigatório.");

        Nome = nome;
        Descricao = descricao;
    }
}
