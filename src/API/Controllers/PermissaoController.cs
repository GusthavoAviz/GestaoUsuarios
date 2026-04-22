using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoAcesso.API.Controllers;

/// <summary>
/// Controller para validação de permissões.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PermissaoController : ControllerBase
{
    private readonly IPermissaoAppService _permissaoAppService;
    private readonly AppDbContext _context;

    /// <summary>
    /// Construtor do controller de permissões.
    /// </summary>
    /// <param name="permissaoAppService">Serviço de aplicação de permissões.</param>
    /// <param name="context">Contexto do banco de dados.</param>
    public PermissaoController(IPermissaoAppService permissaoAppService, AppDbContext context)
    {
        _permissaoAppService = permissaoAppService;
        _context = context;
    }

    /// <summary>
    /// Lista todas as permissões especiais cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Permissao>>> ListarTodas()
    {
        var permissoes = await _context.Permissoes.ToListAsync();
        return Ok(permissoes);
    }

    /// <summary>
    /// Validação de permissão ativa para um usuário.
    /// </summary>
    [HttpGet("validar")]
    public async Task<IActionResult> Validar([FromQuery] Guid azureId, [FromQuery] string permissao)
    {
        var result = await _permissaoAppService.ValidarPermissaoAtivaAsync(azureId, permissao);
        return Ok(result);
    }
}
