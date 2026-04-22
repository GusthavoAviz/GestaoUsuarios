using System;
using GestaoAcesso.Domain.Entities;
using Xunit;

namespace GestaoAcesso.UnitTests.Domain;

/// <summary>
/// Testes de unidade para a entidade Usuario.
/// </summary>
public class UsuarioTests
{
    private Unidade CriarUnidade() => new Unidade("Razao", "Fantasia", "123", "End", "Resp");

    [Fact]
    public void CriarUsuario_ComDadosValidos_DeveRetornarInstancia()
    {
        // Arrange
        var azureId = Guid.NewGuid();
        var cpf = "12345678901";
        var nome = "Usuario Teste";
        var unidade = CriarUnidade();

        // Act
        var usuario = new Usuario(azureId, cpf, nome, unidade, "12345", "67890");

        // Assert
        Assert.NotNull(usuario);
        Assert.Equal(azureId, usuario.AzureUniqueId);
        Assert.Equal(cpf, usuario.Cpf);
        Assert.Equal("12345", usuario.Rg);
        Assert.Equal("67890", usuario.Nit);
        Assert.Equal(unidade.Id, usuario.UnidadePrincipalId);
    }

    [Fact]
    public void AdicionarPerfil_DeveIncluirPerfilNaLista_QuandoNovoPerfil()
    {
        // Arrange
        var unidade = CriarUnidade();
        var usuario = new Usuario(Guid.NewGuid(), "12345678901", "Teste", unidade, null, null);
        var app = new Aplicacao("App1", "App1 Desc");
        var perfil = new Perfil("Admin", "Administrador", app);

        // Act
        usuario.AdicionarPerfil(perfil);

        // Assert
        Assert.Single(usuario.Perfis);
        Assert.Contains(usuario.Perfis, p => p.Nome == "Admin");
    }
}
