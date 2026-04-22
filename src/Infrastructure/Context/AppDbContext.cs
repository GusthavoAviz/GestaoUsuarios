using GestaoAcesso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoAcesso.Infrastructure.Context;

/// <summary>
/// Contexto principal do Entity Framework Core para o sistema de Gestão de Acesso.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Construtor do contexto do banco de dados.
    /// </summary>
    /// <param name="options">Opções de configuração do contexto.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>
    /// Coleção de usuários no banco de dados.
    /// </summary>
    public DbSet<Usuario> Usuarios { get; set; }

    /// <summary>
    /// Coleção de unidades organizacionais no banco de dados.
    /// </summary>
    public DbSet<Unidade> Unidades { get; set; }

    /// <summary>
    /// Coleção de perfis de acesso no banco de dados.
    /// </summary>
    public DbSet<Perfil> Perfis { get; set; }

    /// <summary>
    /// Coleção de aplicações cadastradas no banco de dados.
    /// </summary>
    public DbSet<Aplicacao> Aplicacoes { get; set; }

    /// <summary>
    /// Coleção de permissões especiais no banco de dados.
    /// </summary>
    public DbSet<Permissao> Permissoes { get; set; }

    /// <summary>
    /// Configuração do modelo de dados durante a criação do banco.
    /// </summary>
    /// <param name="modelBuilder">Construtor do modelo.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new UnidadeConfiguration());
        modelBuilder.ApplyConfiguration(new PerfilConfiguration());
        modelBuilder.ApplyConfiguration(new AplicacaoConfiguration());
        modelBuilder.ApplyConfiguration(new PermissaoConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

/// <summary>
/// Configuração de mapeamento para a entidade Usuario.
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    /// <summary>
    /// Define as regras de mapeamento para a tabela de Usuários.
    /// </summary>
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.AzureUniqueId);
        
        builder.Property(u => u.Cpf).IsRequired().HasMaxLength(11);
        builder.HasIndex(u => u.Cpf).IsUnique();

        builder.HasOne(u => u.UnidadePrincipal)
            .WithMany()
            .HasForeignKey(u => u.UnidadePrincipalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.UnidadesSecundarias)
            .WithMany()
            .UsingEntity(j => j.ToTable("UsuarioUnidadesSecundarias"));

        builder.HasMany(u => u.Perfis)
            .WithMany()
            .UsingEntity(j => j.ToTable("UsuarioPerfis"));
    }
}

/// <summary>
/// Configuração de mapeamento para a entidade Unidade.
/// </summary>
public class UnidadeConfiguration : IEntityTypeConfiguration<Unidade>
{
    /// <summary>
    /// Define as regras de mapeamento para a tabela de Unidades.
    /// </summary>
    public void Configure(EntityTypeBuilder<Unidade> builder)
    {
        builder.ToTable("Unidades");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Cnpj).IsRequired().HasMaxLength(14);
        builder.HasIndex(u => u.Cnpj).IsUnique();
    }
}

/// <summary>
/// Configuração de mapeamento para a entidade Perfil.
/// </summary>
public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
{
    /// <summary>
    /// Define as regras de mapeamento para a tabela de Perfis.
    /// </summary>
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.ToTable("Perfis");
        builder.HasKey(p => p.Id);
        
        builder.HasOne(p => p.Aplicacao)
            .WithMany(a => a.Perfis)
            .HasForeignKey(p => p.AplicacaoId);

        builder.Property(p => p.Permissoes).HasColumnType("text[]");
    }
}

/// <summary>
/// Configuração de mapeamento para a entidade Aplicacao.
/// </summary>
public class AplicacaoConfiguration : IEntityTypeConfiguration<Aplicacao>
{
    /// <summary>
    /// Define as regras de mapeamento para a tabela de Aplicações.
    /// </summary>
    public void Configure(EntityTypeBuilder<Aplicacao> builder)
    {
        builder.ToTable("Aplicacoes");
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.Nome).IsUnique();
    }
}

/// <summary>
/// Configuração de mapeamento para a entidade Permissao.
/// </summary>
public class PermissaoConfiguration : IEntityTypeConfiguration<Permissao>
{
    /// <summary>
    /// Define as regras de mapeamento para a tabela de Permissões Especiais.
    /// </summary>
    public void Configure(EntityTypeBuilder<Permissao> builder)
    {
        builder.ToTable("PermissoesEspeciais");
        builder.HasKey(p => p.Id);
    }
}
