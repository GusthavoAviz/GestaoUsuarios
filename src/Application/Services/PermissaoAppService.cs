using System;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Domain.Interfaces;

namespace GestaoAcesso.Application.Services;

/// <summary>
/// Implementação do serviço de aplicação para validação de permissões (Hereditárias e Ad-hoc).
/// </summary>
public class PermissaoAppService : IPermissaoAppService
{
    private readonly IPermissaoRepository _permissaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    /// <summary>
    /// Construtor do serviço de aplicação para Permissões.
    /// </summary>
    /// <param name="permissaoRepository">Instância do repositório de permissões.</param>
    /// <param name="usuarioRepository">Instância do repositório de usuários.</param>
    public PermissaoAppService(IPermissaoRepository permissaoRepository, IUsuarioRepository usuarioRepository)
    {
        _permissaoRepository = permissaoRepository;
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Valida se o usuário possui uma permissão ativa (Especial ou via Perfil).
    /// </summary>
    public async Task<bool> ValidarPermissaoAtivaAsync(Guid azureId, string nomePermissao)
    {
        var nomeUpper = nomePermissao.ToUpper();

        // 1. Verificar Permissões Especiais (Justificadas/Temporárias)
        var permissoesEspeciais = await _permissaoRepository.ObterAtivasPorUsuarioAsync(azureId);
        if (permissoesEspeciais.Any(p => p.Nome.ToUpper() == nomeUpper && p.EstaAtiva()))
        {
            return true;
        }

        // 2. Verificar Permissões Hereditárias (via Perfil)
        var usuario = await _usuarioRepository.ObterPorAzureIdAsync(azureId);
        if (usuario != null)
        {
            return usuario.Perfis.Any(perfil => 
                perfil.Permissoes.Contains(nomeUpper));
        }

        return false;
    }
}
