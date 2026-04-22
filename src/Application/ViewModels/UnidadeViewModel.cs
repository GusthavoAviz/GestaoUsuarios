using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoAcesso.Application.ViewModels;

/// <summary>
/// Modelo de retorno para seleção de Unidades no front-end.
/// </summary>
public record UnidadeViewModel(
    Guid Id,
    string NomeFantasia,
    string RazaoSocial,
    string Cnpj
);
