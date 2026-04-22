using System;
using System.Linq;
using GestaoAcesso.Domain.Entities;
using Xunit;

namespace GestaoAcesso.UnitTests.Domain;

/// <summary>
/// Testes de unidade para a entidade Usuario (incluindo Regras de Unidades).
/// </summary>
public class UsuarioUnidadeTests
{
    private Unidade CriarUnidade(string nome) => 
        new Unidade(nome, nome, "00000000000191", "Rua Teste", "Responsavel");

    [Fact]
    public void CriarUsuario_SemUnidadePrincipal_DeveLancarExcecao()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Usuario(Guid.NewGuid(), "123", "Teste", null!, null, null));
    }

    [Fact]
    public void AdicionarUnidadeSecundaria_IgualAPrincipal_DeveLancarExcecao()
    {
        // Arrange
        var unidade = CriarUnidade("Unidade 1");
        var usuario = new Usuario(Guid.NewGuid(), "123", "Teste", unidade, null, null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => usuario.AdicionarUnidadeSecundaria(unidade));
    }

    [Fact]
    public void AdicionarUnidadeSecundaria_Valida_DeveIncluirNaLista()
    {
        // Arrange
        var principal = CriarUnidade("Principal");
        var secundaria = CriarUnidade("Secundaria");
        var usuario = new Usuario(Guid.NewGuid(), "123", "Teste", principal, null, null);

        // Act
        usuario.AdicionarUnidadeSecundaria(secundaria);

        // Assert
        Assert.Single(usuario.UnidadesSecundarias);
        Assert.Equal(secundaria.Id, usuario.UnidadesSecundarias.First().Id);
    }
}
