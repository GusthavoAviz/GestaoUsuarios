using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestaoAcesso.API.Controllers;

/// <summary>
/// Controller para gestão de aplicações.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AplicacaoController : ControllerBase
{
    private readonly IAplicacaoAppService _aplicacaoAppService;

    public AplicacaoController(IAplicacaoAppService aplicacaoAppService)
    {
        _aplicacaoAppService = aplicacaoAppService;
    }

    /// <summary>
    /// Lista todas as aplicações cadastradas.
    /// </summary>
    /// <returns>Lista de aplicações.</returns>
    [HttpGet]
    public async Task<IActionResult> ObterTodas() => Ok(await _aplicacaoAppService.ObterTodasAsync());

    /// <summary>
    /// Obtém uma aplicação específica pelo seu ID.
    /// </summary>
    /// <param name="id">ID da aplicação.</param>
    /// <returns>ViewModel da aplicação.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var result = await _aplicacaoAppService.ObterPorIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Cria uma nova aplicação no sistema.
    /// </summary>
    /// <param name="input">Dados de entrada para criação.</param>
    /// <returns>ID da aplicação criada.</returns>
    [HttpPost]
    public async Task<IActionResult> Criar(AplicacaoInputModel input)
    {
        var id = await _aplicacaoAppService.AdicionarAsync(input);
        return CreatedAtAction(nameof(ObterPorId), new { id }, input);
    }

    /// <summary>
    /// Remove uma aplicação do sistema.
    /// </summary>
    /// <param name="id">ID da aplicação a ser removida.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _aplicacaoAppService.RemoverAsync(id);
        return NoContent();
    }
}
