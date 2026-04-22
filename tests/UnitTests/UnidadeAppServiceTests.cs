using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.Services;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;
using Moq;
using Xunit;

namespace GestaoAcesso.UnitTests.Application;

/// <summary>
/// Testes de unidade para o serviço de aplicação de Unidades.
/// </summary>
public class UnidadeAppServiceTests
{
    private readonly Mock<IUnidadeRepository> _unidadeRepositoryMock;
    private readonly IUnidadeAppService _unidadeAppService;

    public UnidadeAppServiceTests()
    {
        _unidadeRepositoryMock = new Mock<IUnidadeRepository>();
        _unidadeAppService = new UnidadeAppService(_unidadeRepositoryMock.Object);
    }

    [Fact]
    public async Task ObterTodasAtivasAsync_DeveRetornarListaDeViewModels()
    {
        // Arrange
        var unidades = new List<Unidade>
        {
            new Unidade("Razao 1", "Fantasia 1", "123", "End 1", "Resp 1"),
            new Unidade("Razao 2", "Fantasia 2", "456", "End 2", "Resp 2")
        };

        _unidadeRepositoryMock.Setup(r => r.ObterTodasAsync())
            .ReturnsAsync(unidades);

        // Act
        var result = await _unidadeAppService.ObterTodasAtivasAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.NomeFantasia == "Fantasia 1");
    }
}
