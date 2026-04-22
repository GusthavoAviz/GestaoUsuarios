using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestaoAcesso.API.Controllers;

/// <summary>
/// Controller para consulta de Unidades (popula seleções no front-end).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UnidadeController : ControllerBase
{
    private readonly IUnidadeAppService _unidadeAppService;

    /// <summary>
    /// Construtor do controller de unidades.
    /// </summary>
    /// <param name="unidadeAppService">Serviço de aplicação de unidades.</param>
    public UnidadeController(IUnidadeAppService unidadeAppService)
    {
        _unidadeAppService = unidadeAppService;
    }

    /// <summary>
    /// Lista todas as unidades ativas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnidadeViewModel>>> ListarAtivas()
    {
        var result = await _unidadeAppService.ObterTodasAtivasAsync();
        return Ok(result);
    }
}
