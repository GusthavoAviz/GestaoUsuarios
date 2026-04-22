using System;
using System.Collections.Generic;
using System.Linq;

namespace GestaoAcesso.Domain.Entities;

/// <summary>
/// Entidade que representa um Usuário no sistema.
/// </summary>
public class Usuario
{
    /// <summary>
    /// Identificador único do usuário no Azure AD.
    /// </summary>
    public Guid AzureUniqueId { get; private set; }

    /// <summary>
    /// Cadastro de Pessoa Física (CPF) do usuário.
    /// </summary>
    public string Cpf { get; private set; } = null!;

    /// <summary>
    /// Nome completo do usuário.
    /// </summary>
    public string Nome { get; private set; } = null!;

    /// <summary>
    /// Registro Geral (RG) do usuário (opcional).
    /// </summary>
    public string? Rg { get; private set; }

    /// <summary>
    /// Número de Identificação do Trabalhador (NIT) (opcional).
    /// </summary>
    public string? Nit { get; private set; }
    
    /// <summary>
    /// Identificador da unidade principal do usuário.
    /// </summary>
    public Guid UnidadePrincipalId { get; private set; }

    /// <summary>
    /// Unidade principal à qual o usuário está vinculado.
    /// </summary>
    public Unidade? UnidadePrincipal { get; private set; }

    /// <summary>
    /// Lista de unidades secundárias às quais o usuário tem acesso.
    /// </summary>
    public ICollection<Unidade> UnidadesSecundarias { get; private set; } = new List<Unidade>();

    /// <summary>
    /// Lista de perfis de acesso atribuídos ao usuário.
    /// </summary>
    public ICollection<Perfil> Perfis { get; private set; } = new List<Perfil>();

    protected Usuario() { }

    /// <summary>
    /// Construtor da entidade Usuario.
    /// </summary>
    public Usuario(Guid azureUniqueId, string cpf, string nome, Unidade unidadePrincipal, string? rg = null, string? nit = null)
    {
        if (azureUniqueId == Guid.Empty) throw new ArgumentException("AzureUniqueId inválido.");
        if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("CPF obrigatório.");
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome obrigatório.");
        if (unidadePrincipal == null) throw new ArgumentException("Unidade Principal é obrigatória.");

        AzureUniqueId = azureUniqueId;
        Cpf = cpf;
        Nome = nome;
        UnidadePrincipal = unidadePrincipal;
        UnidadePrincipalId = unidadePrincipal.Id;
        Rg = rg;
        Nit = nit;
    }

    /// <summary>
    /// Adiciona uma unidade secundária ao usuário.
    /// </summary>
    /// <param name="unidade">Unidade a ser adicionada.</param>
    public void AdicionarUnidadeSecundaria(Unidade unidade)
    {
        if (unidade == null) return;
        if (unidade.Id == UnidadePrincipalId) throw new ArgumentException("Unidade secundária não pode ser igual à principal.");
        
        if (!UnidadesSecundarias.Any(u => u.Id == unidade.Id))
        {
            UnidadesSecundarias.Add(unidade);
        }
    }

    /// <summary>
    /// Atribui um novo perfil de acesso ao usuário.
    /// </summary>
    /// <param name="perfil">Perfil a ser atribuído.</param>
    public void AdicionarPerfil(Perfil perfil)
    {
        if (perfil == null) return;
        if (!Perfis.Any(p => p.Id == perfil.Id))
        {
            Perfis.Add(perfil);
        }
    }
}
