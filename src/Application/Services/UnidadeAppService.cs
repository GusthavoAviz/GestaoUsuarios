using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using GestaoAcesso.Domain.Interfaces;

namespace GestaoAcesso.Application.Services;

/// <summary>
/// Implementação do serviço de aplicação para Unidades.
/// </summary>
public class UnidadeAppService : IUnidadeAppService
{
    private readonly IUnidadeRepository _unidadeRepository;

    /// <summary>
    /// Construtor do serviço de aplicação para Unidades.
    /// </summary>
    /// <param name="unidadeRepository">Instância do repositório de unidades.</param>
    public UnidadeAppService(IUnidadeRepository unidadeRepository)
    {
        _unidadeRepository = unidadeRepository;
    }

    /// <summary>
    /// Obtém todas as unidades ativas formatadas para a visualização.
    /// </summary>
    /// <returns>Lista de ViewModels de unidades.</returns>
    public async Task<IEnumerable<UnidadeViewModel>> ObterTodasAtivasAsync()
    {
        var unidades = await _unidadeRepository.ObterTodasAsync();
        
        return unidades.Select(u => new UnidadeViewModel(
            u.Id,
            u.NomeFantasia,
            u.RazaoSocial,
            u.Cnpj
        ));
    }
}
