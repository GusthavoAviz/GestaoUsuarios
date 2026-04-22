using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoAcesso.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aplicacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissoesEspeciais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAzureId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Justificativa = table.Column<string>(type: "text", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcedidoPor = table.Column<string>(type: "text", nullable: false),
                    DataConcessao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissoesEspeciais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RazaoSocial = table.Column<string>(type: "text", nullable: false),
                    NomeFantasia = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Endereco = table.Column<string>(type: "text", nullable: false),
                    UsuarioResponsavel = table.Column<string>(type: "text", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    AplicacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Permissoes = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perfis_Aplicacoes_AplicacaoId",
                        column: x => x.AplicacaoId,
                        principalTable: "Aplicacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    AzureUniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Rg = table.Column<string>(type: "text", nullable: true),
                    Nit = table.Column<string>(type: "text", nullable: true),
                    UnidadePrincipalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.AzureUniqueId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Unidades_UnidadePrincipalId",
                        column: x => x.UnidadePrincipalId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPerfis",
                columns: table => new
                {
                    PerfisId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAzureUniqueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPerfis", x => new { x.PerfisId, x.UsuarioAzureUniqueId });
                    table.ForeignKey(
                        name: "FK_UsuarioPerfis_Perfis_PerfisId",
                        column: x => x.PerfisId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPerfis_Usuarios_UsuarioAzureUniqueId",
                        column: x => x.UsuarioAzureUniqueId,
                        principalTable: "Usuarios",
                        principalColumn: "AzureUniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioUnidadesSecundarias",
                columns: table => new
                {
                    UnidadesSecundariasId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioAzureUniqueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioUnidadesSecundarias", x => new { x.UnidadesSecundariasId, x.UsuarioAzureUniqueId });
                    table.ForeignKey(
                        name: "FK_UsuarioUnidadesSecundarias_Unidades_UnidadesSecundariasId",
                        column: x => x.UnidadesSecundariasId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioUnidadesSecundarias_Usuarios_UsuarioAzureUniqueId",
                        column: x => x.UsuarioAzureUniqueId,
                        principalTable: "Usuarios",
                        principalColumn: "AzureUniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacoes_Nome",
                table: "Aplicacoes",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_AplicacaoId",
                table: "Perfis",
                column: "AplicacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_Cnpj",
                table: "Unidades",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPerfis_UsuarioAzureUniqueId",
                table: "UsuarioPerfis",
                column: "UsuarioAzureUniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Cpf",
                table: "Usuarios",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UnidadePrincipalId",
                table: "Usuarios",
                column: "UnidadePrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioUnidadesSecundarias_UsuarioAzureUniqueId",
                table: "UsuarioUnidadesSecundarias",
                column: "UsuarioAzureUniqueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissoesEspeciais");

            migrationBuilder.DropTable(
                name: "UsuarioPerfis");

            migrationBuilder.DropTable(
                name: "UsuarioUnidadesSecundarias");

            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Aplicacoes");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
