# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Bladex Garantías** — a financial guarantees management system built with ASP.NET MVC 5 + Domain-Driven Design. Developed by Intelledata LLC. Target database: SQL Server (`BLX_GARANTIAS`).
**Bladex Garantías** es un sistema de gestión de garantías financieras desarrollado con ASP.NET MVC 5 + Domain-Driven Design (DDD).  
Creado por Intelledata LLC. Base de datos objetivo: SQL Server (`BLX_GARANTIAS`).

**Objetivos actuales del proyecto:**
- Modernizar y actualizar el código legacy
- Mejorar performance
- Actualizar librerías desactualizadas
- Corregir vulnerabilidades de seguridad
- Mejorar mantenibilidad y testabilidad
- Agregar/fortalecer tests unitarios

## Build Commands

```powershell
# Build entire solution (Debug)
msbuild Bladex.Garantias.sln /p:Configuration=Debug /p:Platform="Any CPU"

# Build entire solution (Release)
msbuild Bladex.Garantias.sln /p:Configuration=Release /p:Platform="Any CPU"

# Clean
msbuild Bladex.Garantias.sln /t:Clean

# Run tests (MSTest)
mstest /testcontainer:Bladex.Garantias.UnitTests\bin\Debug\Bladex.Garantias.UnitTests.dll
```

Supported configurations: `Debug|Any CPU`, `Release|Any CPU`, `Debug|x86`, `Release|x86`.

## Architecture

The solution is organized into strict DDD layers:

| Project | Layer | Purpose |
|---|---|---|
| `Bladex.Garantias.Presentation.Website` | Presentation | ASP.NET MVC 5 web app (.NET 4.6.1) |
| `Bladex.Garantias.Infrastructure.UI` | Presentation | AutoMapper profiles, UI helpers |
| `Bladex.Garantias.Application` | Application | Service facades / orchestration |
| `Bladex.Garantias.DomainModel` | Domain | Entities, repository interfaces, domain services (.NET 4.0) |
| `Bladex.Garantias.Infrastructure` | Infrastructure | Caching (Enterprise Library), logging (Log4Net), serialization |
| `Bladex.Garantias.Infrastructure.Repositories` | Infrastructure | SQL repository implementations |
| `Bladex.Garantias.Database` | Data | SQL Server database project (.sqlproj) |
| `Bladex.Garantias.ImportSync` | Integration | Data import/sync module |
| `Bladex.Garantias.UnitTests` | Test | MSTest unit tests |

Layer diagrams enforcing dependency rules live in `ModelingProject1/`.

**Regla importante de dependencias:** La capa Domain no debe depender de Infrastructure ni de frameworks externos.

## Domain Model

All guarantee types inherit from `GarantiaBase`:
- `GarantiaContrato` — Contract guarantees
- `GarantiaDeposito` — Deposit guarantees
- `GarantiaDepositoOtroBanco` — Other-bank deposits
- `GarantiaInmueble` — Real estate
- `GarantiaMueble` — Movable assets
- `GarantiaPrenda` — Pledged assets
- `GarantiaOtra` — Other types

Supporting entities: `Cliente`, `Bancos`, `Aseguradoras`, `Aval`, `Pais`, `Monedas`.

Key domain subsystems under `DomainModel/DomainBase/Components/`:
- **MakerChecker** — Two-step approval workflow for sensitive operations
- **Pagination** — `Paged<T>` generic paging
- **AvalManager** — Endorsement management
- **Autocomplete** — `AutocompleteValue` for search/dropdown support

## Infrastructure Patterns

**Custom Repository Framework** (`Infrastructure/RepositoryFramework/`): Each domain entity has a matching triple in `Infrastructure.Repositories/`:
- `[Entity]SqlRepository` — ADO.NET data access
- `[Entity]Factory` — entity construction
- `[Entity]SqlMapper` — maps `DataReader` → entity

Repository and entity factory bindings are declared in `App.config` (not code), allowing implementation swaps without recompilation.

**Caching:** Enterprise Library Caching 3.0 wrapper in `Infrastructure/Caching/`.

**Logging:** Log4Net writing to the `ApplicationLog` SQL table; config at `Bladex.Garantias.Presentation.Website/log4net.config`.

**Error Logging:** ELMAH with SQL Server backend (configured in web.config).

**Mapping:** AutoMapper 1.1 — profiles registered in `Infrastructure.UI`; bootstrapped in `Bootstrapper.cs`.

## Web Application Entry Points

- `Global.asax.cs` — `Application_AuthenticateRequest` uses a custom cookie-based auth system; `UserId` may be passed as a query string parameter.
- `Bootstrapper.cs` — AutoMapper config, Log4Net init, culture set to `en-US`.
- Controllers inherit from `BaseController` → `GarantiaBaseController` for guarantee-specific controllers.

## Database

Default connection string (local dev):
```
Data Source=localhost;Initial Catalog=BLX_GARANTIAS;Integrated Security=SSPI;MultipleActiveResultSets=true;
```

The `Bladex.Garantias.Database` project holds schema objects (tables, stored procedures, views, functions, indexes). SQL Server 2008+ compatible.

## Key Libraries

- .NET Framework 4.0 (domain/infra) and 4.6.1 (web)
- Entity Framework 6.4.4
- AutoMapper 1.1
- Log4Net 1.2.10
- Enterprise Library Caching 3.0
- Telerik MVC UI components (legacy)
- jQuery 1.7.2 / jQuery UI 1.8.19

## Important Rules & Guidelines (SIEMPRE respetar)

1. **Trabajo incremental y seguro**
   - Siempre trabajar en módulos pequeños.
   - Nunca modificar más de 2-3 archivos sin mi aprobación explícita.
   - Primero mostrar resumen de cambios propuestos + diff antes de editar.

2. **Modernización**
   - Priorizar corrección de problemas de performance y seguridad.
   - Sugerir actualizaciones de librerías solo cuando sea seguro y con alternativa clara.
   - Mantener compatibilidad con .NET Framework 4.6.1 por ahora (no migrar a .NET 8 todavía a menos que yo lo indique).

3. **Coding Style & Patterns**
   - Respetar el patrón Repository existente (EntitySqlRepository + Factory + SqlMapper).
   - Usar el lenguaje ubicuo del dominio (términos como Garantía, MakerChecker, Aval, etc.).
   - Preferir código explícito y legible sobre código "elegante" pero oscuro.
   - No introducir nuevos patrones sin consultarme primero.

4. **Testing**
   - Siempre que se modifique lógica de negocio, proponer o mejorar tests unitarios.
   - Priorizar tests en la capa Domain.

5. **Seguridad y Performance**
   - Prestar especial atención a posibles SQL Injection, manejo de cookies/auth, y queries ineficientes.
   - Evitar N+1 queries y loops innecesarios.
   
## Cómo trabajar conmigo

Primero analiza y entiende el código existente antes de proponer cambios.
Usa el lenguaje del dominio.
Sé conservador con cambios grandes.
Siempre explica el "por qué" de cada cambio importante.

