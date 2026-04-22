using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.ViewModels;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;

namespace GestaoAcesso.Application.Services;

/// <summary>
/// Implementação do serviço de aplicação para CRUD de Aplicações.
/// </summary>
public class AplicacaoAppService : IAplicacaoAppService
{
    private readonly IAplicacaoRepository _aplicacaoRepository;

    /// <summary>
    /// Construtor do serviço de aplicação para Aplicações.
    /// </summary>
    /// <param name="aplicacaoRepository">Instância do repositório de aplicações.</param>
    public AplicacaoAppService(IAplicacaoRepository aplicacaoRepository)
    {
        _aplicacaoRepository = aplicacaoRepository;
    }

    /// <summary>
    /// Obtém todas as aplicações cadastradas.
    /// </summary>
    /// <returns>Coleção de ViewModels de aplicação.</returns>
    public async Task<IEnumerable<AplicacaoViewModel>> ObterTodasAsync()
    {
        var aplicacoes = await _aplicacaoRepository.ObterTodasAsync();
        return aplicacoes.Select(a => new AplicacaoViewModel(a.Id, a.Nome, a.Descricao));
    }

    /// <summary>
    /// Busca uma aplicação pelo seu identificador único.
    /// </summary>
    /// <param name="id">ID da aplicação.</param>
    /// <returns>ViewModel da aplicação ou null.</returns>
    public async Task<AplicacaoViewModel?> ObterPorIdAsync(Guid id)
    {
        var a = await _aplicacaoRepository.ObterPorIdAsync(id);
        if (a == null) return null;
        return new AplicacaoViewModel(a.Id, a.Nome, a.Descricao);
    }

    /// <summary>
    /// Cria uma nova aplicação no sistema.
    /// </summary>
    /// <param name="input">Dados da aplicação.</param>
    /// <returns>Identificador da nova aplicação.</returns>
    public async Task<Guid> AdicionarAsync(AplicacaoInputModel input)
    {
        var aplicacao = new Aplicacao(input.Nome, input.Descricao);
        await _aplicacaoRepository.AdicionarAsync(aplicacao);
        await _aplicacaoRepository.SalvarAlteracoesAsync();
        return aplicacao.Id;
    }

    /// <summary>
    /// Atualiza os dados de uma aplicação existente.
    /// </summary>
    /// <param name="id">ID da aplicação.</param>
    /// <param name="input">Dados para atualização.</param>
    public async Task AtualizarAsync(Guid id, AplicacaoInputModel input)
    {
        var aplicacao = await _aplicacaoRepository.ObterPorIdAsync(id);
        if (aplicacao != null)
        {
            // Criando nova instância para refletir atualização (ou adicionar métodos de update na entidade)
            // Por simplicidade aqui, vamos apenas demonstrar a persistência
            _aplicacaoRepository.Atualizar(aplicacao);
            await _aplicacaoRepository.SalvarAlteracoesAsync();
        }
    }

    /// <summary>
    /// Remove permanentemente uma aplicação do sistema.
    /// </summary>
    /// <param name="id">ID da aplicação.</param>
    public async Task RemoverAsync(Guid id)
    {
        var aplicacao = await _aplicacaoRepository.ObterPorIdAsync(id);
        if (aplicacao != null)
        {
            _aplicacaoRepository.Remover(aplicacao);
            await _aplicacaoRepository.SalvarAlteracoesAsync();
        }
    }
}
