using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.API.Controllers;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GestaoAcesso.UnitTests.API;

public class UnidadeControllerTests
{
    private readonly Mock<IUnidadeAppService> _serviceMock;
    private readonly UnidadeController _controller;

    public UnidadeControllerTests()
    {
        _serviceMock = new Mock<IUnidadeAppService>();
        _controller = new UnidadeController(_serviceMock.Object);
    }

    [Fact]
    public async Task ListarAtivas_DeveRetornarOk_ComListaDeUnidades()
    {
        // Arrange
        var unidade = new UnidadeViewModel(Guid.NewGuid(), "Unidade 1", "Razao", "123");
        _serviceMock.Setup(s => s.ObterTodasAtivasAsync())
            .ReturnsAsync(new List<UnidadeViewModel> { unidade });

        // Act
        var result = await _controller.ListarAtivas();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IEnumerable<UnidadeViewModel>>(okResult.Value);
        Assert.Single(value);
    }
}
