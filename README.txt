================================================================================
                       PROJETO: GESTAO DE USUARIOS E ACESSOS
================================================================================

DESCRIÇÃO
Este sistema é uma solução completa para a gestão de identidades e permissões, 
integrando dados locais em PostgreSQL com identidades do Azure AD via Graph API.
O foco principal é o controle de acesso granular por perfis e unidades organizacionais.

FUNCIONALIDADES
- Gestão de Usuários: Cadastro enriquecido com dados locais (NIT, CPF, RG, Vínculos).
- Controle de Unidades: Gerenciamento de Unidade Principal (obrigatória) e Secundárias.
- Gestão de Perfis e Permissões: Atribuição de acessos por aplicação e perfil.
- Permissões Especiais: Controle com justificativa, expiração e trilha de auditoria.
- Integração Azure AD: Sincronização via AzureUniqueId.
- Validação de Acessos: Endpoint para verificação em tempo real de permissões ativas.

TECNOLOGIAS E FRAMEWORKS
Back-end:
  - Linguagem: C# 12+ (.NET 8)
  - Arquitetura: Clean Architecture (Domain, Application, Infrastructure, API)
  - Banco de Dados: PostgreSQL
  - Bibliotecas Principais: Entity Framework Core, xUnit, Moq, FluentValidation
  - Padrões: Repository Pattern, Dependency Injection, Result Pattern

Front-end:
  - Framework: React 19+
  - Linguagem: TypeScript
  - Ferramenta de Build: Vite
  - Estilização: CSS Vanilla (Foco em performance e customização)
  - Ícones: Lucide React
  - Comunicação API: Axios

COMO FAZER BUILD E TESTES
1. Pré-requisitos:
   - .NET 8 SDK
   - Node.js (v18+)
   - PostgreSQL ativo

2. Executar Testes (Back-end):
   $ cd tests/UnitTests
   $ dotnet test

3. Executar Back-end (API):
   $ cd src/API
   $ dotnet run

4. Executar Front-end:
   $ cd frontend
   $ npm install
   $ npm run dev

ARQUITETURA
O projeto segue os princípios da Clean Architecture, dividida em:
- Domain: Entidades, Interfaces de Repositório e Regras de Negócio (Sem dependências).
- Application: Serviços de aplicação, Interfaces, DTOs (ViewModels) e Mapeamentos.
- Infrastructure: Implementação de Repositórios, Contexto do Banco (EF Core) e Migrations.
- API: Controllers, Configurações de DI e Middleware.

PONTOS DE MELHORIA
- Implementação de Cache: Adicionar Redis para consultas frequentes de permissões.
- Dockerização: Criar Dockerfile e docker-compose para padronizar o ambiente de dev.
- CI/CD: Configurar GitHub Actions para execução automática de testes e lint.
- Logs e Observabilidade: Integrar Serilog com ElasticStack ou Application Insights.
- UI/UX: Adicionar uma biblioteca de componentes (ex: Shadcn/UI) para maior agilidade, 
  mantendo a base de CSS customizada.
- Cobertura de Testes: Expandir para testes de integração no Front-end com Vitest.

================================================================================
Desenvolvido por: Gusthavo Aviz
================================================================================
