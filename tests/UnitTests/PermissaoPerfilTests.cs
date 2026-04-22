using System;
using GestaoAcesso.Domain.Entities;
using Xunit;

namespace GestaoAcesso.UnitTests.Domain;

/// <summary>
/// Testes de unidade para permissões vinculadas a um perfil.
/// </summary>
public class PermissaoPerfilTests
{
    [Fact]
    public void AdicionarPermissaoAoPerfil_DeveVincularCorretamente()
    {
        // Arrange
        var app = new Aplicacao("SistemaRH", "Gestão de Pessoas");
        var perfil = new Perfil("Gerente", "Acesso gerencial", app);
        var nomePermissao = "USUARIOS_EDITAR";

        // Act
        perfil.AdicionarPermissao(nomePermissao);

        // Assert
        Assert.Contains(nomePermissao, perfil.Permissoes);
    }

    [Fact]
    public void AdicionarPermissaoDuplicada_NaoDeveRepetirNaLista()
    {
        // Arrange
        var app = new Aplicacao("SistemaRH", "Gestão de Pessoas");
        var perfil = new Perfil("Gerente", "Acesso gerencial", app);
        var nomePermissao = "USUARIOS_VISUALIZAR";

        // Act
        perfil.AdicionarPermissao(nomePermissao);
        perfil.AdicionarPermissao(nomePermissao);

        // Assert
        Assert.Single(perfil.Permissoes);
    }
}
