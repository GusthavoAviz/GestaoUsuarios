using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.Services;
using GestaoAcesso.Application.ViewModels;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;
using Moq;
using Xunit;

namespace GestaoAcesso.UnitTests.Application;

/// <summary>
/// Testes de unidade para o CRUD de Aplicação.
/// </summary>
public class AplicacaoAppServiceTests
{
    private readonly Mock<IAplicacaoRepository> _aplicacaoRepositoryMock;
    private readonly IAplicacaoAppService _aplicacaoAppService;

    public AplicacaoAppServiceTests()
    {
        _aplicacaoRepositoryMock = new Mock<IAplicacaoRepository>();
        _aplicacaoAppService = new AplicacaoAppService(_aplicacaoRepositoryMock.Object);
    }

    [Fact]
    public async Task AdicionarAsync_DeveChamarRepositorioESalvar()
    {
        // Arrange
        var input = new AplicacaoInputModel("Sistema A", "Descricao A");

        // Act
        var id = await _aplicacaoAppService.AdicionarAsync(input);

        // Assert
        Assert.NotEqual(Guid.Empty, id);
        _aplicacaoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Aplicacao>()), Times.Once);
        _aplicacaoRepositoryMock.Verify(r => r.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarViewModel_QuandoExistir()
    {
        // Arrange
        var id = Guid.NewGuid();
        var aplicacao = new Aplicacao("App", "Desc");
        _aplicacaoRepositoryMock.Setup(r => r.ObterPorIdAsync(id)).ReturnsAsync(aplicacao);

        // Act
        var result = await _aplicacaoAppService.ObterPorIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("App", result.Nome);
    }
}
