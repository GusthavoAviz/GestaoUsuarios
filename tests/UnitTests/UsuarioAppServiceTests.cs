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
/// Testes de unidade para o serviço de aplicação de Usuário.
/// </summary>
public class UsuarioAppServiceTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IPermissaoRepository> _permissaoRepositoryMock;
    private readonly IUsuarioAppService _usuarioAppService;

    public UsuarioAppServiceTests()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _permissaoRepositoryMock = new Mock<IPermissaoRepository>();
        _usuarioAppService = new UsuarioAppService(_usuarioRepositoryMock.Object, _permissaoRepositoryMock.Object);
    }

    private Unidade CriarUnidade() => new Unidade("Razao", "Fantasia", "123", "End", "Resp");

    [Fact]
    public async Task ObterUsuarioCompletoPorCpfAsync_DeveRetornarViewModel_ComTudoConsolidado()
    {
        // Arrange
        var cpf = "12345678901";
        var azureId = Guid.NewGuid();
        var unidade = CriarUnidade();
        var usuario = new Usuario(azureId, cpf, "Usuario Teste", unidade, "12345", "67890");
        
        var app = new Aplicacao("App1", "Desc");
        var perfil = new Perfil("Admin", "Desc", app);
        perfil.AdicionarPermissao("EDITAR_CONTEUDO");
        usuario.AdicionarPerfil(perfil);

        var permissaoEspecial = new Permissao(azureId, "ACESSO_EXTRA", "Justificativa", DateTime.UtcNow.AddDays(1), "Admin");

        _usuarioRepositoryMock.Setup(r => r.ObterPorCpfAsync(cpf))
            .ReturnsAsync(usuario);

        _permissaoRepositoryMock.Setup(r => r.ObterAtivasPorUsuarioAsync(azureId))
            .ReturnsAsync(new List<Permissao> { permissaoEspecial });

        // Act
        var result = await _usuarioAppService.ObterUsuarioCompletoAsync(cpf);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("12345", result.Rg);
        Assert.Equal("67890", result.Nit);
        Assert.Contains("EDITAR_CONTEUDO", result.PermissoesAtivas);
        Assert.Contains("ACESSO_EXTRA", result.PermissoesAtivas);
    }

    [Fact]
    public async Task ObterTodosAsync_DeveRetornarListaMapeada()
    {
        // Arrange
        var azureId = Guid.NewGuid();
        var unidade = CriarUnidade();
        var usuario = new Usuario(azureId, "123", "User Test", unidade);
        
        _usuarioRepositoryMock.Setup(r => r.ObterTodosAsync())
            .ReturnsAsync(new List<Usuario> { usuario });

        _permissaoRepositoryMock.Setup(r => r.ObterAtivasPorUsuarioAsync(azureId))
            .ReturnsAsync(new List<Permissao>());

        // Act
        var result = await _usuarioAppService.ObterTodosAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("User Test", result.First().Nome);
    }
}
