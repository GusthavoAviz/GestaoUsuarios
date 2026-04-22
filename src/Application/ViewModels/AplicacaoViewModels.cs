using System;

namespace GestaoAcesso.Application.ViewModels;

/// <summary>
/// Modelo de visualização para a Aplicação.
/// </summary>
public record AplicacaoViewModel(Guid Id, string Nome, string Descricao);

/// <summary>
/// Modelo de entrada para criação de Aplicação.
/// </summary>
public record AplicacaoInputModel(string Nome, string Descricao);
