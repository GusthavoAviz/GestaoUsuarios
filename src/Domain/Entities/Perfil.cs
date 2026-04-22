using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcesso.Domain.Entities;

/// <summary>
/// Entidade que representa um Perfil de Acesso no sistema.
/// </summary>
public class Perfil
{
    /// <summary>
    /// Identificador único do perfil.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Nome do perfil.
    /// </summary>
    public string Nome { get; private set; } = null!;

    /// <summary>
    /// Descrição detalhada do perfil.
    /// </summary>
    public string Descricao { get; private set; } = null!;

    /// <summary>
    /// Identificador da aplicação à qual o perfil pertence.
    /// </summary>
    public Guid AplicacaoId { get; private set; }

    /// <summary>
    /// Aplicação associada a este perfil.
    /// </summary>
    public Aplicacao Aplicacao { get; private set; } = null!;

    /// <summary>
    /// Lista de permissões/funcionalidades vinculadas a este perfil.
    /// </summary>
    public IList<string> Permissoes { get; private set; } = new List<string>();

    /// <summary>
    /// Construtor para o EF Core.
    /// </summary>
    protected Perfil() { }

    /// <summary>
    /// Construtor da entidade Perfil associada a uma aplicação.
    /// </summary>
    /// <param name="nome">Nome do perfil.</param>
    /// <param name="descricao">Descrição do perfil.</param>
    /// <param name="aplicacao">Aplicação a qual o perfil pertence.</param>
    public Perfil(string nome, string descricao, Aplicacao aplicacao)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do perfil é obrigatório.");
        if (aplicacao == null) throw new ArgumentException("A aplicação é obrigatória para o perfil.");

        Nome = nome;
        Descricao = descricao;
        Aplicacao = aplicacao;
    }

    /// <summary>
    /// Adiciona uma permissão específica ao perfil.
    /// </summary>
    /// <param name="nomePermissao">Código ou nome da permissão (ex: USUARIO_EDITAR).</param>
    public void AdicionarPermissao(string nomePermissao)
    {
        if (string.IsNullOrWhiteSpace(nomePermissao)) throw new ArgumentException("Nome da permissão inválido.");

        if (!Permissoes.Contains(nomePermissao.ToUpper()))
        {
            Permissoes.Add(nomePermissao.ToUpper());
        }
    }
}
