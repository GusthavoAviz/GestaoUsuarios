using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.API.Controllers;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GestaoAcesso.UnitTests.API;

public class AplicacaoControllerTests
{
    private readonly Mock<IAplicacaoAppService> _serviceMock;
    private readonly AplicacaoController _controller;

    public AplicacaoControllerTests()
    {
        _serviceMock = new Mock<IAplicacaoAppService>();
        _controller = new AplicacaoController(_serviceMock.Object);
    }

    [Fact]
    public async Task ObterTodas_DeveRetornarOk_ComLista()
    {
        // Arrange
        var app = new AplicacaoViewModel(Guid.NewGuid(), "App 1", "Desc");
        _serviceMock.Setup(s => s.ObterTodasAsync())
            .ReturnsAsync(new List<AplicacaoViewModel> { app });

        // Act
        var result = await _controller.ObterTodas();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsAssignableFrom<IEnumerable<AplicacaoViewModel>>(okResult.Value);
        Assert.Single(value);
    }

    [Fact]
    public async Task ObterPorId_DeveRetornarNotFound_QuandoNaoExiste()
    {
        // Arrange
        _serviceMock.Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((AplicacaoViewModel?)null);

        // Act
        var result = await _controller.ObterPorId(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Criar_DeveRetornarCreated()
    {
        // Arrange
        var input = new AplicacaoInputModel("App 1", "Desc");
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.AdicionarAsync(input)).ReturnsAsync(id);

        // Act
        var result = await _controller.Criar(input);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(id, createdResult.RouteValues?["id"]);
    }

    [Fact]
    public async Task Remover_DeveRetornarNoContent()
    {
        // Act
        var result = await _controller.Remover(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
