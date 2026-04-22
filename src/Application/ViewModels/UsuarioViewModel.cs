using System;
using System.Collections.Generic;

namespace GestaoAcesso.Application.ViewModels;

/// <summary>
/// Modelo de retorno completo (AD + Unidades + Perfis + Permissões).
/// </summary>
public record UsuarioViewModel(
    Guid AzureId,
    string Nome,
    string Cpf,
    string? Rg,
    string? Nit,
    string UnidadePrincipal,
    IEnumerable<string> UnidadesSecundarias,
    IEnumerable<string> Perfis,
    IEnumerable<string> PermissoesAtivas,
    string? EmailAd = null
);
