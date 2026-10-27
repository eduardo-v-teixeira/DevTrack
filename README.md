# DevTrack

**Português** | [English](README.en.md)

Aplicação web em **ASP.NET Core MVC (.NET 8)** que está sendo construída para acompanhar estudos e atividades: criar atividades, dividi-las em mini passos e registrar o que foi feito a cada dia. O projeto é construído em fases curtas, como estudo de C# e ASP.NET Core e como case de portfólio.

> **Status: em desenvolvimento.** A fase 1 está concluída: página inicial e um módulo de arquivos funcional. O cadastro de atividades, passos e registros diários ainda não foi implementado.

## Funcionalidades

### Disponível hoje (fase 1)

- **Página inicial** com a apresentação do projeto e das áreas planejadas (Atividades, Mini passos, Registro diário).
- **Módulo Arquivos** (`/Arquivo`):
  - lista os arquivos salvos na pasta `wwwroot/arquivos`;
  - cria um arquivo de texto a partir de um formulário com nome e conteúdo (POST protegido com token antifalsificação);
  - exibe o conteúdo de um arquivo em `/Arquivo/Visualizar?nome=<arquivo>`, com resposta 400 para nome vazio e 404 para arquivo inexistente.
- **Regras de validação** na camada de serviço (`ArquivoService`):
  - nome obrigatório, com até 100 caracteres e sem caracteres inválidos;
  - bloqueio de caminhos relativos (`..`, `/`, `\`);
  - extensões permitidas: `.txt`, `.md`, `.json` e `.log`;
  - conteúdo com até 5000 caracteres.
- **Segunda proteção contra path traversal** no repositório: o caminho final é normalizado com `Path.GetFullPath` e precisa ficar dentro da pasta base.
- **Logs estruturados** com `ILogger<T>` no serviço e no repositório.

### Planejado

Cadastro de atividades, mini passos e registro diário, com persistência em Entity Framework Core e SQLite. Veja [Fases de desenvolvimento](#fases-de-desenvolvimento).

## Tecnologias

- **C#** / **.NET 8**
- **ASP.NET Core MVC** com Razor Views e Tag Helpers
- Injeção de dependência nativa do ASP.NET Core
- Validação com DataAnnotations e jQuery Validation (unobtrusive)
- **HTML, CSS e Bootstrap 5.3** (via CDN)
- Planejado para a fase 2: Entity Framework Core + SQLite (ainda não instalado)

## Estrutura e arquitetura

```
DevTrack/
├── DevTrack.sln
└── DevTrack/
    ├── Controllers/          # HomeController, ArquivoController
    ├── Services/             # ArquivoService (regras de negócio) e Interfaces/
    ├── Repositories/         # ArquivoRepository (acesso ao sistema de arquivos) e Interfaces/
    ├── Models/ViewModels/    # ProcessarArquivoInput, ResultadoProcessamento
    ├── Views/                # Home, Arquivo e Shared (Razor)
    ├── wwwroot/              # css, js e a pasta arquivos/
    ├── Program.cs            # Registro de dependências, pipeline e rotas
    └── appsettings.json
```

O fluxo segue camadas simples: **Controller → Service → Repository**. O controller trata a requisição HTTP, o serviço aplica as regras de negócio e o repositório lê e grava os arquivos com `System.IO`. Serviço e repositório são acessados por interfaces (`IArquivoService`, `IArquivoRepository`) registradas como `Scoped` em `Program.cs`.

## Como executar

Requisito: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

Na raiz do repositório:

```bash
dotnet restore DevTrack.sln
dotnet build DevTrack.sln
dotnet run --project DevTrack/DevTrack.csproj
```

Acesse **https://localhost:7001** ou **http://localhost:5001** (perfil definido em `Properties/launchSettings.json`). Para usar HTTPS localmente, confie no certificado de desenvolvimento uma vez com `dotnet dev-certs https --trust`.

Não há banco de dados nem migrations nesta fase. Os arquivos criados pelo módulo ficam em `DevTrack/wwwroot/arquivos/` e, por estarem em `wwwroot`, também são servidos como arquivos estáticos.

## Fases de desenvolvimento

| Fase | Conteúdo | Status |
|------|----------|--------|
| **1 – Fundamentos** | Projeto ASP.NET Core, estrutura MVC, página inicial, módulo de arquivos | ✅ Concluída |
| **2 – Atividades** | Cadastro e listagem de atividades, persistência com EF Core + SQLite | Planejada |
| **3 – Mini passos** | Passos por atividade, marcar como concluído | Planejada |
| **4 – Registro diário** | Registro por dia, histórico | Planejada |
| **5 – UX** | Layout, indicadores de progresso, resumo da atividade | Planejada |
| **6 – Finalização** | Refatoração, documentação, publicação | Planejada |

## Modelo de dados (planejado)

- **Atividade**: Id, Título, Descrição, Data de criação
- **Passo**: Id, Descrição, Status (concluído ou não), AtividadeId
- **Registro diário**: Id, Data, Observação do dia, AtividadeId

## Licença

O repositório não tem arquivo de licença. Projeto de uso educacional e de portfólio.
