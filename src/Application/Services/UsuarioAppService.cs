using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;

namespace GestaoAcesso.Application.Services;

/// <summary>
/// Implementação do serviço de aplicação para Usuários.
/// </summary>
public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPermissaoRepository _permissaoRepository;

    /// <summary>
    /// Construtor do serviço de aplicação para Usuários.
    /// </summary>
    /// <param name="usuarioRepository">Repositório de usuários.</param>
    /// <param name="permissaoRepository">Repositório de permissões.</param>
    public UsuarioAppService(IUsuarioRepository usuarioRepository, IPermissaoRepository permissaoRepository)
    {
        _usuarioRepository = usuarioRepository;
        _permissaoRepository = permissaoRepository;
    }

    /// <summary>
    /// Busca um usuário específico pelo CPF.
    /// </summary>
    /// <param name="cpf">Número do CPF.</param>
    /// <returns>Entidade de usuário ou null.</returns>
    public async Task<Usuario?> ObterPorCpfAsync(string cpf)
    {
        return await _usuarioRepository.ObterPorCpfAsync(cpf);
    }

    /// <summary>
    /// Obtém todos os usuários com dados consolidados para listagem.
    /// </summary>
    /// <returns>Coleção de ViewModels de usuário.</returns>
    public async Task<IEnumerable<UsuarioViewModel>> ObterTodosAsync()
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync();
        var listaCompleta = new List<UsuarioViewModel>();

        foreach (var usuario in usuarios)
        {
            var permissoesEspeciais = await _permissaoRepository.ObterAtivasPorUsuarioAsync(usuario.AzureUniqueId);
            
            var todasPermissoes = usuario.Perfis
                .SelectMany(p => p.Permissoes)
                .Concat(permissoesEspeciais.Select(p => p.Nome.ToUpper()))
                .Distinct();

            string emailSimuladoAd = $"{usuario.Nome.Replace(" ", ".").ToLower()}@dominio.com";

            listaCompleta.Add(new UsuarioViewModel(
                usuario.AzureUniqueId,
                usuario.Nome,
                usuario.Cpf,
                usuario.Rg,
                usuario.Nit,
                usuario.UnidadePrincipal?.NomeFantasia ?? "N/A",
                usuario.UnidadesSecundarias.Select(u => u.NomeFantasia),
                usuario.Perfis.Select(p => p.Nome),
                todasPermissoes,
                emailSimuladoAd
            ));
        }

        return listaCompleta;
    }

    /// <summary>
    /// Obtém os dados completos de um usuário (AD + Local) por ID ou CPF.
    /// </summary>
    /// <param name="idOuCpf">Identificador AzureId ou CPF do usuário.</param>
    /// <returns>ViewModel detalhada do usuário ou null.</returns>
    public async Task<UsuarioViewModel?> ObterUsuarioCompletoAsync(string idOuCpf)
    {
        Usuario? usuario;

        if (Guid.TryParse(idOuCpf, out Guid azureId))
        {
            usuario = await _usuarioRepository.ObterPorAzureIdAsync(azureId);
        }
        else
        {
            usuario = await _usuarioRepository.ObterPorCpfAsync(idOuCpf);
        }

        if (usuario == null) return null;

        var permissoesEspeciais = await _permissaoRepository.ObterAtivasPorUsuarioAsync(usuario.AzureUniqueId);
        
        var todasPermissoes = usuario.Perfis
            .SelectMany(p => p.Permissoes)
            .Concat(permissoesEspeciais.Select(p => p.Nome.ToUpper()))
            .Distinct();

        // Simulação de enriquecimento AD (Graph API)
        string emailSimuladoAd = $"{usuario.Nome.Replace(" ", ".").ToLower()}@dominio.com";

        return new UsuarioViewModel(
            usuario.AzureUniqueId,
            usuario.Nome,
            usuario.Cpf,
            usuario.Rg,
            usuario.Nit,
            usuario.UnidadePrincipal?.NomeFantasia ?? "N/A",
            usuario.UnidadesSecundarias.Select(u => u.NomeFantasia),
            usuario.Perfis.Select(p => p.Nome),
            todasPermissoes,
            emailSimuladoAd
        );
    }
}
