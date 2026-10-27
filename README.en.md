# DevTrack

[Português](README.md) | **English**

A web application with **ASP.NET Core MVC (.NET 8)** being built to track studies and activities: create activities, split them into small steps and log what was done each day. The project is built in short phases, as a C# and ASP.NET Core learning project and a portfolio case.

> **Status: work in progress.** Phase 1 is done: a home page and a working file module. Activities, steps and daily logs are not implemented yet.

## Features

### Available today (phase 1)

- **Home page** presenting the project and the planned areas (Activities, Small steps, Daily log).
- **Files module** (`/Arquivo`):
  - lists the files saved in the `wwwroot/arquivos` folder;
  - creates a text file from a form with a name and content (POST protected by an antiforgery token);
  - shows a file's content at `/Arquivo/Visualizar?nome=<file>`, returning 400 for an empty name and 404 for a missing file.
- **Validation rules** in the service layer (`ArquivoService`):
  - the name is required, up to 100 characters, with no invalid characters;
  - relative paths are blocked (`..`, `/`, `\`);
  - allowed extensions: `.txt`, `.md`, `.json` and `.log`;
  - content up to 5000 characters.
- **Second path traversal guard** in the repository: the final path is normalized with `Path.GetFullPath` and must stay inside the base folder.
- **Structured logging** with `ILogger<T>` in the service and the repository.

### Planned

Activities, small steps and a daily log, persisted with Entity Framework Core and SQLite. See [Development phases](#development-phases).

## Tech stack

- **C#** / **.NET 8**
- **ASP.NET Core MVC** with Razor views and Tag Helpers
- Built-in ASP.NET Core dependency injection
- Validation with DataAnnotations and jQuery Validation (unobtrusive)
- **HTML, CSS and Bootstrap 5.3** (from a CDN)
- Planned for phase 2: Entity Framework Core + SQLite (not installed yet)

## Structure and architecture

```
DevTrack/
├── DevTrack.sln
└── DevTrack/
    ├── Controllers/          # HomeController, ArquivoController
    ├── Services/             # ArquivoService (business rules) and Interfaces/
    ├── Repositories/         # ArquivoRepository (file system access) and Interfaces/
    ├── Models/ViewModels/    # ProcessarArquivoInput, ResultadoProcessamento
    ├── Views/                # Home, Arquivo and Shared (Razor)
    ├── wwwroot/              # css, js and the arquivos/ folder
    ├── Program.cs            # Dependency registration, pipeline and routes
    └── appsettings.json
```

The code follows simple layers: **Controller → Service → Repository**. The controller handles the HTTP request, the service applies the business rules and the repository reads and writes files with `System.IO`. The service and the repository are used through interfaces (`IArquivoService`, `IArquivoRepository`) registered as `Scoped` in `Program.cs`.

## How to run

Requirement: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

From the repository root:

```bash
dotnet restore DevTrack.sln
dotnet build DevTrack.sln
dotnet run --project DevTrack/DevTrack.csproj
```

Open **https://localhost:7001** or **http://localhost:5001** (profile defined in `Properties/launchSettings.json`). To use HTTPS locally, trust the development certificate once with `dotnet dev-certs https --trust`.

There is no database and there are no migrations in this phase. Files created by the module are stored in `DevTrack/wwwroot/arquivos/`; because that folder is under `wwwroot`, they are also served as static files.

## Development phases

| Phase | Scope | Status |
|-------|-------|--------|
| **1 – Fundamentals** | ASP.NET Core project, MVC structure, home page, file module | ✅ Done |
| **2 – Activities** | Create and list activities, persistence with EF Core + SQLite | Planned |
| **3 – Small steps** | Steps per activity, mark as done | Planned |
| **4 – Daily log** | One entry per day, history | Planned |
| **5 – UX** | Layout, progress indicators, activity summary | Planned |
| **6 – Wrap-up** | Refactoring, documentation, release | Planned |

## Data model (planned)

- **Activity**: Id, Title, Description, Creation date
- **Step**: Id, Description, Status (done or not), ActivityId
- **Daily log**: Id, Date, Note for the day, ActivityId

## License

The repository has no license file. Educational and portfolio project.
