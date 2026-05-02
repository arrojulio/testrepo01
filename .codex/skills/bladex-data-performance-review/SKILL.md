---
name: bladex-data-performance-review
description: Review data-access and data-manipulation performance in the Bladex Garantias project. Use when Codex needs to analyze SQL Server access, ADO.NET repositories, SqlMapper/Factory code, MVC tables/forms for any Garantia type, MakerChecker workflows, pagination, caching, N+1 queries, inefficient DataReader usage, or performance risks in the path from UI to database.
---

# Bladex Data Performance Review

Use this skill to review performance in the Bladex Garantias data path, especially when a screen is slow, a table loads too much data, a form performs repeated lookups, or MakerChecker operations feel heavy.

## Project Context

Bladex Garantias is an ASP.NET MVC 5 and DDD application on .NET Framework 4.6.1, with SQL Server database `BLX_GARANTIAS`.

Key projects:
- `Bladex.Garantias.Presentation.Website`: MVC views, controllers, Telerik and DataTables UI.
- `Bladex.Garantias.Application`: service facades and orchestration.
- `Bladex.Garantias.DomainModel`: entities, repository contracts, domain services, MakerChecker, `Paged<T>`.
- `Bladex.Garantias.Infrastructure.Repositories`: ADO.NET repository implementations.
- `Bladex.Garantias.Database`: SQL Server schema, stored procedures, views, functions, indexes.

Repository implementation pattern:
- `[Entity]SqlRepository`
- `[Entity]Factory`
- `[Entity]SqlMapper`

Respect this pattern. Do not introduce a new data-access abstraction unless the user explicitly asks for it.

## Scope

Focus on access and manipulation of data shown in tables, forms, autocomplete controls, and approval workflows for:
- `GarantiaContrato`
- `GarantiaDeposito`
- `GarantiaDepositoOtroBanco`
- `GarantiaInmueble`
- `GarantiaMueble`
- `GarantiaPrenda`
- `GarantiaOtra`
- `GarantiaBase`
- `Aval`
- `Cliente`
- reference data such as `Pais`, `Monedas`, `Bancos`, `Aseguradoras`
- MakerChecker pending, approve, reject, view, and history flows

## Mandatory Rules

Always read `AGENTS.md` before acting if it has not already been loaded in the current turn.

Follow these rules:
- Keep compatibility with .NET Framework 4.6.1.
- Preserve MVC 5, DDD layering, and repository/factory/mapper conventions.
- Keep Domain independent from Infrastructure and external frameworks.
- Work incrementally.
- Never modify more than 2-3 files without explicit user approval.
- Show a summary and diff preview before editing.
- Prefer explicit, readable changes over clever abstractions.
- Use existing Enterprise Library caching before proposing new cache libraries.
- Propose MSTest coverage for business logic changes.
- Write findings and recommendations in Spanish unless the user asks otherwise.

## Review Workflow

1. Identify the exact workflow under review:
   - table/list screen
   - edit/create/details form
   - autocomplete/dropdown
   - MakerChecker pending/approval/rejection flow
   - export/import/sync flow if it affects Garantias screens

2. Trace the execution path end to end:
   - `.aspx` / `.ascx` view and JavaScript widget
   - controller action
   - application service/facade
   - domain service or repository contract
   - `SqlRepository`
   - `SqlMapper` and `Factory`
   - stored procedure, view, function, table, or inline SQL

3. Determine the data shape:
   - expected row count
   - columns required by the UI
   - sort/filter/pagination behavior
   - reference-data lookups
   - child collections or aggregate loading
   - MakerChecker state transitions and audit/history data

4. Inspect hot-path risks:
   - N+1 repository calls
   - loops that open connections or execute commands
   - unbounded result sets
   - missing `Paged<T>` or pagination
   - oversized projections or `SELECT *`
   - repeated loading of static/reference data
   - `DataSet`/`DataTable` use where `DataReader` is enough
   - `DataReader` or connection lifetime issues
   - AutoMapper/manual mapping inside large loops
   - SQL string concatenation and SQL injection risk
   - stored procedures without `SET NOCOUNT ON`
   - implicit conversions caused by parameter type mismatches
   - missing indexes implied by `WHERE`, `JOIN`, and `ORDER BY`

5. Check UI-specific costs:
   - Telerik grids/widgets loading all rows client-side
   - DataTables 1.9 server-side vs client-side behavior
   - forms that preload too many dropdown options
   - autocomplete controls that do not debounce or limit results
   - repeated AJAX calls triggered by money/date/category changes
   - MakerChecker lists that hydrate full Garantia aggregates when summary data is enough

6. Produce a prioritized review before proposing edits.

## Severity Guide

Use these levels:
- `CRITICAL`: data correctness, security, or production stability risk plus performance impact.
- `HIGH`: likely user-visible slowdown, N+1 query, unbounded list, or heavy MakerChecker workflow.
- `MEDIUM`: inefficient but bounded behavior, avoidable duplicate reads, missing cache on frequent path.
- `LOW`: minor cleanup, small allocation issue, style-only improvement.

## Preferred Fix Patterns

Prefer low-risk changes first:
- Add missing pagination using existing `Paged<T>` patterns.
- Move repeated reference-data reads behind existing Enterprise Library cache wrappers.
- Replace per-row queries with batch queries or set-based joins.
- Reduce selected columns to the fields needed by the screen.
- Add `using` blocks around `SqlConnection`, `SqlCommand`, and `IDataReader` where missing.
- Add `SET NOCOUNT ON` to stored procedures when safe.
- Parameterize SQL instead of concatenating strings.
- Split list summary loading from full form/detail aggregate loading.
- Add targeted SQL indexes only after tying them to concrete query predicates.

Avoid these unless explicitly approved:
- Replacing the repository framework wholesale.
- Migrating to .NET Core/.NET 8.
- Introducing Dapper, new ORM patterns, or new cache libraries.
- Large cross-cutting refactors across many Garantia types in one step.

## Output Format

Use this structure for review results:

```markdown
**Resumen Ejecutivo**
- [3-5 bullets, highest impact first]

**Flujo Analizado**
- UI:
- Controller:
- Application/Domain:
- Repository:
- SQL:

**Inventario de Hallazgos**
| # | Severidad | Componente | Tipo | Evidencia | Impacto | Recomendacion |
|---|-----------|------------|------|-----------|---------|----------------|

**Quick Wins**
- [Fixes under 30 minutes, low risk]

**Plan Incremental**
1. Fase 1 - [low-risk change]
2. Fase 2 - [medium-risk change]
3. Fase 3 - [larger refactor only if justified]

**Diff Preview**
[Show proposed changes before editing]

**Validacion**
- Build/test commands.
- Manual verification path for table/form/MakerChecker screen.
```

When the user asks for implementation, still present the diff preview first unless they have already approved that exact change.
