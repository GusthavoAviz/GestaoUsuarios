using GestaoAcesso.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoAcesso.API.Controllers;

/// <summary>
/// Controller para gestão de usuários.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioAppService _usuarioAppService;

    /// <summary>
    /// Construtor do controller de usuários.
    /// </summary>
    /// <param name="usuarioAppService">Serviço de aplicação de usuários.</param>
    public UsuarioController(IUsuarioAppService usuarioAppService)
    {
        _usuarioAppService = usuarioAppService;
    }

    /// <summary>
    /// Lista todos os usuários cadastrados com dados simplificados.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ListarTodos()
    {
        var usuarios = await _usuarioAppService.ObterTodosAsync();
        return Ok(usuarios);
    }

    /// <summary>
    /// Obtém o retorno completo de um usuário pelo ID ou CPF.
    /// </summary>
    [HttpGet("{id_ou_cpf}")]
    public async Task<IActionResult> ObterPorIdOuCpf(string id_ou_cpf)
    {
        var result = await _usuarioAppService.ObterUsuarioCompletoAsync(id_ou_cpf);
        
        if (result == null) return NotFound("Usuário não encontrado.");
        
        return Ok(result);
    }
}
