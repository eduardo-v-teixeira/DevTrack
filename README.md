# DevTrack

Sistema web de **acompanhamento de estudos e atividades**: criar atividades, dividir em mini passos e registrar o que foi feito a cada dia.

## Objetivo

Projeto de aprendizado em **C#** e **ASP.NET Core MVC**, desenvolvido em pequenos passos diários. Ao final: site funcional, repositório organizado e case real para portfólio e LinkedIn.

## Tecnologias (escopo inicial)

- **C#** / .NET 8  
- **ASP.NET Core MVC**  
- **Entity Framework Core** + SQLite (a partir da Fase 2)  
- **HTML, CSS e Bootstrap 5**  
- **Git e GitHub**

## Estrutura do projeto

```
DevTrack/
├── Controllers/     # Lógica de requisições (ex.: HomeController)
├── Models/          # Entidades e DTOs (Atividade, Passo, RegistroDiário)
├── Views/           # Páginas Razor (Home, Shared)
│   ├── Home/
│   └── Shared/
├── wwwroot/         # Arquivos estáticos (CSS, JS)
├── Program.cs       # Configuração da aplicação
└── appsettings.json # Configurações e connection string
```

## Como executar

Requisitos: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
cd DevTrack
dotnet restore
dotnet run
```

Acesse: **https://localhost:7001** ou **http://localhost:5001**.

## Fases de desenvolvimento

| Fase | Conteúdo |
|------|----------|
| **1 – Fundamentos** | Projeto ASP.NET Core, estrutura MVC, página inicial ✅ |
| **2 – Atividades** | Cadastro e listagem de atividades, persistência com EF + SQLite |
| **3 – Mini passos** | Passos por atividade, marcar como concluído |
| **4 – Registro diário** | Registro por dia, histórico |
| **5 – UX** | Layout, indicadores de progresso, resumo da atividade |
| **6 – Finalização** | Refatoração, documentação, publicação no GitHub |

## Dados (conceitual)

- **Atividade**: Id, Título, Descrição, Data de criação  
- **Passo**: Id, Descrição, Status (concluído ou não), AtividadeId  
- **Registro diário**: Id, Data, Observação do dia, AtividadeId  

## Licença

Uso educacional e portfólio.
