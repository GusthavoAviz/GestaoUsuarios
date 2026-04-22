using System;
using System.Linq;
using System.Collections.Generic;
using GestaoAcesso.Application.Interfaces;
using GestaoAcesso.Application.Services;
using GestaoAcesso.Domain.Entities;
using GestaoAcesso.Domain.Interfaces;
using GestaoAcesso.Infrastructure.Context;
using GestaoAcesso.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Banco de Dados (PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Injeção de Dependência - Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPermissaoRepository, PermissaoRepository>();
builder.Services.AddScoped<IAplicacaoRepository, AplicacaoRepository>();
builder.Services.AddScoped<IUnidadeRepository, UnidadeRepository>();

// 3. Injeção de Dependência - Serviços de Aplicação
builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>();
builder.Services.AddScoped<IPermissaoAppService, PermissaoAppService>();
builder.Services.AddScoped<IAplicacaoAppService, AplicacaoAppService>();
builder.Services.AddScoped<IUnidadeAppService, UnidadeAppService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Aplicar migrações automáticas e Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try 
    {
        var context = services.GetRequiredService<AppDbContext>();
        logger.LogInformation("Iniciando migrações automáticas...");
        await context.Database.MigrateAsync();
        logger.LogInformation("Migrações concluídas com sucesso.");

        // Verifica se o usuário principal já existe para evitar duplicidade no seed
        if (!context.Usuarios.Any(u => u.Cpf == "12345678901"))
        {
            logger.LogInformation("Dados de teste não encontrados. Iniciando Seeding...");
            
            // 1. Unidades
            var unidadeMatriz = context.Unidades.FirstOrDefault(u => u.Cnpj == "12345678000190") 
                                ?? new Unidade("Empresa Matriz LTDA", "Matriz São Paulo", "12345678000190", "Av. Paulista, 1000", "Admin");
            
            var unidadeFilial = context.Unidades.FirstOrDefault(u => u.Cnpj == "87654321000100")
                                ?? new Unidade("Empresa Filial S.A.", "Filial Rio", "87654321000100", "Av. Atlântica, 500", "Gerente");
            
            if (unidadeMatriz.Id == Guid.Empty) context.Unidades.Add(unidadeMatriz);
            if (unidadeFilial.Id == Guid.Empty) context.Unidades.Add(unidadeFilial);
            await context.SaveChangesAsync();

            // 2. Aplicações
            var appErp = context.Aplicacoes.FirstOrDefault(a => a.Nome == "ERP_SISTEMA")
                         ?? new Aplicacao("ERP_SISTEMA", "Sistema de Gestão de Recursos");
            
            var appRh = context.Aplicacoes.FirstOrDefault(a => a.Nome == "RH_PORTAL")
                        ?? new Aplicacao("RH_PORTAL", "Portal do Colaborador");
            
            if (appErp.Id == Guid.Empty) context.Aplicacoes.Add(appErp);
            if (appRh.Id == Guid.Empty) context.Aplicacoes.Add(appRh);
            await context.SaveChangesAsync();

            // 3. Perfis
            var perfilAdmin = context.Perfis.FirstOrDefault(p => p.Nome == "ADMIN_TOTAL")
                              ?? new Perfil("ADMIN_TOTAL", "Acesso administrativo completo", appErp);
            
            if (perfilAdmin.Id == Guid.Empty)
            {
                perfilAdmin.AdicionarPermissao("USUARIOS_VISUALIZAR");
                perfilAdmin.AdicionarPermissao("USUARIOS_EDITAR");
                perfilAdmin.AdicionarPermissao("CONFIGURACOES_SISTEMA");
                context.Perfis.Add(perfilAdmin);
            }

            var perfilOperador = context.Perfis.FirstOrDefault(p => p.Nome == "OPERADOR")
                                 ?? new Perfil("OPERADOR", "Acesso operacional básico", appRh);
            
            if (perfilOperador.Id == Guid.Empty)
            {
                perfilOperador.AdicionarPermissao("PONTO_REGISTRAR");
                perfilOperador.AdicionarPermissao("FERIAS_CONSULTAR");
                context.Perfis.Add(perfilOperador);
            }
            
            await context.SaveChangesAsync();

            // 4. Usuários
            var usuario1 = new Usuario(
                Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"), 
                "12345678901", 
                "Gusthavo Engenheiro", 
                unidadeMatriz, 
                "12.345.678-9", 
                "9876543210"
            );
            usuario1.AdicionarPerfil(perfilAdmin);
            usuario1.AdicionarUnidadeSecundaria(unidadeFilial);

            var usuario2 = new Usuario(
                Guid.NewGuid(), 
                "98765432100", 
                "Ana Gerente", 
                unidadeFilial, 
                "98.765.432-1", 
                "1234500000"
            );
            usuario2.AdicionarPerfil(perfilOperador);

            context.Usuarios.AddRange(usuario1, usuario2);
            await context.SaveChangesAsync();

            // 5. Permissões Especiais
            var permissaoEspecial = new Permissao(
                usuario1.AzureUniqueId, 
                "MODO_AUDITORIA", 
                "Necessário para auditoria externa trimestral", 
                DateTime.UtcNow.AddDays(15), 
                "Diretoria Geral"
            );
            context.Permissoes.Add(permissaoEspecial);

            await context.SaveChangesAsync();
            logger.LogInformation("Seeding robusto finalizado com sucesso.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações ou ao popular o banco de dados.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run("http://localhost:5255");
