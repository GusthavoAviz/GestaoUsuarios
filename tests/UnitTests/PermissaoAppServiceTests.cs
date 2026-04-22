using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.Services;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;
using Moq;
using Xunit;

namespace GestaoAcesso.UnitTests.Application;

/// <summary>
/// Testes de unidade para o serviço de aplicação de Permissão, incluindo herança de perfis.
/// </summary>
public class PermissaoAppServiceTests
{
    private readonly Mock<IPermissaoRepository> _permissaoRepositoryMock;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly IPermissaoAppService _permissaoAppService;

    public PermissaoAppServiceTests()
    {
        _permissaoRepositoryMock = new Mock<IPermissaoRepository>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _permissaoAppService = new PermissaoAppService(_permissaoRepositoryMock.Object, _usuarioRepositoryMock.Object);
    }

    private Unidade CriarUnidade() => new Unidade("Razao", "Fantasia", "123", "End", "Resp");

    [Fact]
    public async Task ValidarPermissaoAtivaAsync_DeveRetornarTrue_QuandoPossuirPermissaoEspecialAtiva()
    {
        // Arrange
        var azureId = Guid.NewGuid();
        var nomePermissao = "USUARIOS_EDITAR";
        var permissaoEspecial = new Permissao(azureId, nomePermissao, "Justificativa", DateTime.Now.AddDays(1), "Admin");
        var unidade = CriarUnidade();

        _permissaoRepositoryMock.Setup(r => r.ObterAtivasPorUsuarioAsync(azureId))
            .ReturnsAsync(new List<Permissao> { permissaoEspecial });

        _usuarioRepositoryMock.Setup(r => r.ObterPorAzureIdAsync(azureId))
            .ReturnsAsync(new Usuario(azureId, "123", "Teste", unidade, null, null));

        // Act
        var resultado = await _permissaoAppService.ValidarPermissaoAtivaAsync(azureId, nomePermissao);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public async Task ValidarPermissaoAtivaAsync_DeveRetornarFalse_QuandoPossuirPermissaoEspecialExpirada()
    {
        // Arrange
        var azureId = Guid.NewGuid();
        var nomePermissao = "USUARIOS_EDITAR";
        var permissaoEspecial = new Permissao(azureId, nomePermissao, "Justificativa", DateTime.Now.AddDays(-1), "Admin");
        var unidade = CriarUnidade();

        _permissaoRepositoryMock.Setup(r => r.ObterAtivasPorUsuarioAsync(azureId))
            .ReturnsAsync(new List<Permissao> { permissaoEspecial });

        _usuarioRepositoryMock.Setup(r => r.ObterPorAzureIdAsync(azureId))
            .ReturnsAsync(new Usuario(azureId, "123", "Teste", unidade, null, null));

        // Act
        var resultado = await _permissaoAppService.ValidarPermissaoAtivaAsync(azureId, nomePermissao);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public async Task ValidarPermissaoAtivaAsync_DeveRetornarTrue_QuandoHerdarDoPerfil()
    {
        // Arrange
        var azureId = Guid.NewGuid();
        var nomePermissao = "USUARIOS_VISUALIZAR";
        var unidade = CriarUnidade();
        var usuario = new Usuario(azureId, "123", "Teste", unidade, null, null);
        var app = new Aplicacao("App", "Desc");
        var perfil = new Perfil("Admin", "Desc", app);
        perfil.AdicionarPermissao(nomePermissao);
        usuario.AdicionarPerfil(perfil);

        _permissaoRepositoryMock.Setup(r => r.ObterAtivasPorUsuarioAsync(azureId))
            .ReturnsAsync(new List<Permissao>());

        _usuarioRepositoryMock.Setup(r => r.ObterPorAzureIdAsync(azureId))
            .ReturnsAsync(usuario);

        // Act
        var resultado = await _permissaoAppService.ValidarPermissaoAtivaAsync(azureId, nomePermissao);

        // Assert
        Assert.True(resultado);
    }
}
