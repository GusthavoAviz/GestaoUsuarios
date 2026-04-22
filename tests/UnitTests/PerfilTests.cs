using GestaoAcesso.Domain.Entities;
using Xunit;

namespace GestaoAcesso.UnitTests.Domain;

/// <summary>
/// Testes de unidade para a entidade Perfil.
/// </summary>
public class PerfilTests
{
    [Fact]
    public void CriarPerfil_ComDadosValidos_DeveRetornarInstancia()
    {
        // Arrange
        var app = new Aplicacao("AppTeste", "App para testes");
        var nome = "Administrador";
        var descricao = "Acesso total ao sistema";

        // Act
        var perfil = new Perfil(nome, descricao, app);

        // Assert
        Assert.NotNull(perfil);
        Assert.Equal(nome, perfil.Nome);
        Assert.Equal(app, perfil.Aplicacao);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CriarPerfil_ComNomeInvalido_DeveLancarExcecao(string nomeInvalido)
    {
        // Act & Assert
        var app = new Aplicacao("AppTeste", "App para testes");
        Assert.Throws<ArgumentException>(() => new Perfil(nomeInvalido, "Descricao", app));
    }

    [Fact]
    public void CriarPerfil_SemAplicacao_DeveLancarExcecao()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Perfil("Admin", "Desc", null!));
    }
}
