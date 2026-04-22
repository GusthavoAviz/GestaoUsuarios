-- Backup GestaoUsuarios
-- Gerado em: 04/15/2026 09:41:58 UTC

INSERT INTO "Unidades" ("Id", "RazaoSocial", "NomeFantasia", "Cnpj", "Endereco", "UsuarioResponsavel", "Ativo") VALUES ('ab1b4531-b99b-41e6-9bbf-0abe4989781a', 'Empresa Exemplo LTDA', 'Unidade Matriz', '12345678000190', 'Rua Central, 100', 'Admin', True);
INSERT INTO "Unidades" ("Id", "RazaoSocial", "NomeFantasia", "Cnpj", "Endereco", "UsuarioResponsavel", "Ativo") VALUES ('b49a76d1-dfda-48b3-b735-06ec55bf3027', 'Empresa Filial S.A.', 'Filial Rio', '87654321000100', 'Av. Atlântica, 500', 'Gerente', True);
INSERT INTO "Aplicacoes" ("Id", "Nome", "Descricao") VALUES ('8f09e9a9-dd1b-4fda-a6c7-276384b41300', 'ERP_INTERNO', 'Sistema de Gestão Interna');
INSERT INTO "Aplicacoes" ("Id", "Nome", "Descricao") VALUES ('66c73062-5d2b-419f-9996-671d2f05b777', 'RH_PORTAL', 'Portal do Colaborador');
INSERT INTO "Aplicacoes" ("Id", "Nome", "Descricao") VALUES ('8d62b3bc-d480-4bef-b5c0-5465ee12f4c9', 'ERP_SISTEMA', 'Sistema de Gestão de Recursos');
INSERT INTO "Perfis" ("Id", "Nome", "Descricao", "AplicacaoId", "Permissoes") VALUES ('142e8df3-0d13-4898-a710-4836627cffaf', 'ADMIN_TOTAL', 'Acesso administrativo completo', '8d62b3bc-d480-4bef-b5c0-5465ee12f4c9', ARRAY[]);
INSERT INTO "Perfis" ("Id", "Nome", "Descricao", "AplicacaoId", "Permissoes") VALUES ('91b5c192-57a6-4838-9015-31a5c338628a', 'OPERADOR', 'Acesso operacional básico', '66c73062-5d2b-419f-9996-671d2f05b777', ARRAY[]);
INSERT INTO "Usuarios" ("AzureUniqueId", "Cpf", "Nome", "Rg", "Nit", "UnidadePrincipalId") VALUES ('80ae2993-80f3-4fcf-99c1-f1f0d0a5b0d2', '98765432100', 'Ana Gerente', '98.765.432-1', '1234500000', 'b49a76d1-dfda-48b3-b735-06ec55bf3027');
INSERT INTO "UsuarioPerfis" ("PerfisId", "UsuarioAzureUniqueId") VALUES ('91b5c192-57a6-4838-9015-31a5c338628a', '80ae2993-80f3-4fcf-99c1-f1f0d0a5b0d2');
INSERT INTO "Usuarios" ("AzureUniqueId", "Cpf", "Nome", "Rg", "Nit", "UnidadePrincipalId") VALUES ('d290f1ee-6c54-4b01-90e6-d701748f0851', '12345678901', 'Gusthavo Engenheiro', '12.345.678-9', '9876543210', 'ab1b4531-b99b-41e6-9bbf-0abe4989781a');
INSERT INTO "UsuarioUnidadesSecundarias" ("UnidadesSecundariasId", "UsuarioAzureUniqueId") VALUES ('b49a76d1-dfda-48b3-b735-06ec55bf3027', 'd290f1ee-6c54-4b01-90e6-d701748f0851');
INSERT INTO "UsuarioPerfis" ("PerfisId", "UsuarioAzureUniqueId") VALUES ('142e8df3-0d13-4898-a710-4836627cffaf', 'd290f1ee-6c54-4b01-90e6-d701748f0851');
INSERT INTO "PermissoesEspeciais" ("Id", "UsuarioAzureId", "Nome", "Justificativa", "DataExpiracao", "ConcedidoPor", "DataConcessao") VALUES ('c46ad93f-f84b-4914-9f5e-c2581661d71e', 'd290f1ee-6c54-4b01-90e6-d701748f0851', 'MODO_AUDITORIA', 'Necessário para auditoria externa trimestral', '2026-04-29 19:06:59', 'Diretoria Geral', '2026-04-14 19:06:59');
