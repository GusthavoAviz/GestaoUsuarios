using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Infrastructure.Context;
using GestaoAcesso.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestaoAcesso.UnitTests.Infrastructure;

public class UsuarioRepositoryTests
{
    private DbContextOptions<AppDbContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task ObterTodosAsync_DeveRetornarTodosUsuarios()
    {
        // Arrange
        var options = CreateOptions();
        using (var context = new AppDbContext(options))
        {
            var unidade = new Unidade("Unidade 1", "U1", "123", "End", "Resp");
            context.Unidades.Add(unidade);
            
            context.Usuarios.Add(new Usuario(Guid.NewGuid(), "111", "User 1", unidade));
            context.Usuarios.Add(new Usuario(Guid.NewGuid(), "222", "User 2", unidade));
            await context.SaveChangesAsync();
        }

        using (var context = new AppDbContext(options))
        {
            var repository = new UsuarioRepository(context);

            // Act
            var result = await repository.ObterTodosAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }
    }

    [Fact]
    public async Task ObterPorCpfAsync_DeveRetornarUsuarioCorreto()
    {
        // Arrange
        var options = CreateOptions();
        var cpf = "333";
        using (var context = new AppDbContext(options))
        {
            var unidade = new Unidade("Unidade 1", "U1", "123", "End", "Resp");
            context.Usuarios.Add(new Usuario(Guid.NewGuid(), cpf, "User 3", unidade));
            await context.SaveChangesAsync();
        }

        using (var context = new AppDbContext(options))
        {
            var repository = new UsuarioRepository(context);

            // Act
            var result = await repository.ObterPorCpfAsync(cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cpf, result.Cpf);
        }
    }
}
