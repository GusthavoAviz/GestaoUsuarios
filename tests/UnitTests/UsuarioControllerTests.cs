using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.API.Controllers;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GestaoAcesso.UnitTests.API;

public class UsuarioControllerTests
{
    private readonly Mock<IUsuarioAppService> _serviceMock;
    private readonly UsuarioController _controller;

    public UsuarioControllerTests()
    {
        _serviceMock = new Mock<IUsuarioAppService>();
        _controller = new UsuarioController(_serviceMock.Object);
    }

    [Fact]
    public async Task ListarTodos_DeveRetornarOk_ComListaDeUsuarios()
    {
        // Arrange
        var usuario = new UsuarioViewModel(Guid.NewGuid(), "User 1", "123", null, null, "Unidade", new List<string>(), new List<string>(), new List<string>());
        _serviceMock.Setup(s => s.ObterTodosAsync())
            .ReturnsAsync(new List<UsuarioViewModel> { usuario });

        // Act
        var result = await _controller.ListarTodos();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsAssignableFrom<IEnumerable<UsuarioViewModel>>(okResult.Value);
        Assert.Single(value);
    }

    [Fact]
    public async Task ObterPorIdOuCpf_DeveRetornarNotFound_QuandoUsuarioNaoExiste()
    {
        // Arrange
        _serviceMock.Setup(s => s.ObterUsuarioCompletoAsync(It.IsAny<string>()))
            .ReturnsAsync((UsuarioViewModel)null);

        // Act
        var result = await _controller.ObterPorIdOuCpf("123");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
