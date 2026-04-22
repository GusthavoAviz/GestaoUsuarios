using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoAcesso.API.Controllers;

/// <summary>
/// Controller para gestão de Perfis de Acesso.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PerfilController : ControllerBase
{
    private readonly AppDbContext _context;

    public PerfilController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista todos os perfis com suas respectivas aplicações.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> ListarTodos()
    {
        var perfis = await _context.Perfis
            .Include(p => p.Aplicacao)
            .Select(p => new {
                p.Id,
                p.Nome,
                p.Descricao,
                AplicacaoNome = p.Aplicacao.Nome,
                QtdPermissoes = p.Permissoes.Count,
                p.Permissoes
            })
            .ToListAsync();
            
        return Ok(perfis);
    }

    /// <summary>
    /// Cria um novo perfil associado a uma aplicação.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Criar(PerfilInputModel input)
    {
        var app = await _context.Aplicacoes.FindAsync(input.AplicacaoId);
        if (app == null) return BadRequest("Aplicação não encontrada.");

        var perfil = new Perfil(input.Nome, input.Descricao, app);
        
        if (input.Permissoes != null)
        {
            foreach (var perm in input.Permissoes)
            {
                perfil.AdicionarPermissao(perm);
            }
        }

        _context.Perfis.Add(perfil);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ListarTodos), new { id = perfil.Id }, perfil);
    }
}

public record PerfilInputModel(string Nome, string Descricao, Guid AplicacaoId, List<string> Permissoes);
