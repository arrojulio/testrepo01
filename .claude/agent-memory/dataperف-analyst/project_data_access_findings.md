---
name: Data Access Performance Findings
description: Hallazgos de performance del análisis completo de la capa de acceso a datos de Bladex Garantías (actualizado 2026-04-23)
type: project
---

# Hallazgos de Performance de Acceso a Datos — Bladex Garantías

**Fecha de análisis:** 2026-04-19 (actualizado 2026-04-23)
**Why:** Análisis para plan de refactoring de performance de toda la capa de datos.
**How to apply:** Usar como punto de partida para refactoring; evitar re-sugerir problemas ya documentados y corregidos.

## Estado de Fases Completadas
- **Fase 2 (COMPLETA):** Cache de 1h agregado a StatusService, InternalStatusService y ~10 servicios de referencia adicionales.
- **Fase 3 Nivel A (COMPLETA):** 11 callbacks en Application layer redirigidos a servicios con cache.

## Patrón Base Confirmado
- Framework: `SqlRepositoryBase<T>` + `IEntityFactory<T>` + `ChildCallbacks` pattern
- ADO.NET puro via Enterprise Library `Database` abstraction
- `DataHelper.GetSqlValue()` genera SQL inline (NO usa SqlParameter en INSERTs/UPDATEs)
- `SELECT *` en TODOS los 45+ repositorios (método `GetBaseQuery()` universal)
- `BuildEntitiesFromSql` es el punto central de construcción de entidades

## GetSchemaTable() — Problema de Framework Base
`SqlRepositoryBase.BuildEntitiesFromSql()` obtiene `reader.GetSchemaTable()` UNA VEZ (fuera del loop, línea 343 de SqlRepositoryBase.cs).
`BuildEntityFromReader(reader, schemaTable)` recibe el schemaTable ya calculado. CORRECTO — no es N+1.
Sin embargo, `BuildEntityFromReader(reader)` (sin schemaTable) sí llama `reader.GetSchemaTable()` por cada entidad — solo usado por `BuildEntityFromSql()` (entity singular). ACEPTABLE.

## N+1 CRÍTICO — GarantiaBaseSqlRepository (BuildChildCallbacks)

`BuildChildCallbacks()` registra 21 callbacks que cada uno dispara un `FindBy()` por DB:
- AppendCliente → ClienteSqlRepository.FindBy() — 1 query por garantía
- AppendDepositante, AppendEvaluador, AppendAdministrador, AppendAsegurador, AppendRevisor → ActorSqlRepository.FindBy() — 5 queries por garantía (a través de AppendActorHelper)
- AppendPais → PaisService.GetById() — ya cacheado post-Fase 2
- AppendTipoGarantiaSuper → TipoGarantiaSuperService.GetById() — ya cacheado post-Fase 3A
- AppendTipoGarantiaBladex → TipoGarantiaBladexService.GetById() — ya cacheado post-Fase 3A
- AppendFrequencias → FrecuenciasService.GetById() — ya cacheado post-Fase 3A
- AppendCategoriaRiesgoGarantia → CategoriaRiesgoGarantiaService.GetById() — ya cacheado
- AppendMonedas → MonedasService.GetById() — ya cacheado post-Fase 2
- AppendCalificacionesRiesgo → CalificacionesRiesgoService.GetById() — ya cacheado post-Fase 2
- AppendInternalStatus → InternalStatusService.GetById() — ya cacheado post-Fase 2
- AppendStatus → StatusService.GetById() — ya cacheado post-Fase 2
- AppendFiduciarias → FiduciariasSqlRepository.FindBy() — SIN cache (directo a DB)
- AppendGarante → ClienteSqlRepository.FindBy() — SIN cache (directo a DB)
- AppendRegion → RegionService.GetById() — ya cacheado post-Fase 3A
- AppendTipoPoliza → TipoPolizaService.GetById() — ya cacheado post-Fase 3A

**Callbacks SIN cache restantes para Fase 3B:**
1. AppendCliente → ClienteSqlRepository.FindBy() (1 query/garantía)
2. AppendGarante → ClienteSqlRepository.FindBy() (1 query/garantía)
3. AppendFiduciarias → FiduciariasSqlRepository.FindBy() (1 query/garantía)
4. AppendDepositante/Evaluador/Administrador/Asegurador/Revisor → ActorSqlRepository.FindBy() (5 queries/garantía)

**Costo residual post-Fase 3A:** ~8 queries de DB por garantía al cargar lista.
Con 100 garantías = 800 queries de DB solo en callbacks.

## N+1 CRÍTICO — MakerCheckerOperationSqlRepository

### GetSqlOperationsNotApproved() — N+1 dentro del DataReader
Líneas 271-306: Dentro del `while(reader.Read())` llama:
- `GetOperationStatusByID()` → 1 query por operación (FindBy en APP_MakerChecker_OperationStatus)
- `GetChangesetByGuid()` → 1 query por operación (FindBy en APP_MAKERCHECKER_Changeset)
- `GetCheckerUserById()` → 1 query por operación (FindBy en APP_MAKERCHECKER_User)

**Con N operaciones pendientes = 1 + 3N queries.**

### GetSQLOperationsByFilter() — Mismo patrón N+1
Líneas 348-384: Mismo problema que GetSqlOperationsNotApproved.

### GetSqlOperationNotApprovedById() — Mismo patrón para 1 registro
Líneas 309-346: Para un solo registro hace 3 queries adicionales.

### GetOperationsByMaker() — GetAll() completo sin filtro
Línea 253: `this.GetAll().Where(o => o.Changeset.MakerUserId == makerUserId).ToList()`
Carga TODAS las operaciones de la tabla con todos sus callbacks N+1, luego filtra en memoria.

## N+1 CRÍTICO — MakerCheckerChangesetSqlRepository

### GetChangesetSummary() — N+1 explícito
Líneas 183-193: Carga todos los changesets con `GetAll()`, luego por cada changeset llama `operationRepository.GetOperationsByChangeset()` → 1 query por changeset. Además cada operación tiene sus propios N+1 internos.

### GetSQLAvailableChangesetList() — Query dentro del DataReader
Línea 255: Dentro del loop `while(reader.Read())` llama `GetMakerCheckerUser()` → 1 query por changeset.

## WriteRecord — SELECT innecesario antes de INSERT/UPDATE
En 6 repositorios de subtipos de garantía, `WriteRecord()` llama `FindBy(garantia.Key)` antes de decidir INSERT o UPDATE:
- GarantiaDepositoSqlRepository.WriteRecord() — línea 259
- GarantiaInmuebleSqlRepository.WriteRecord() — línea 236
- GarantiaPrendaSqlRepository.WriteRecord() — línea 267
- GarantiaDepositoOtroBancoSqlRepository.WriteRecord() — línea 192
- GarantiaOtraSqlRepository.WriteRecord() — línea 209
- GarantiaMuebleSqlRepository.WriteRecord() — línea 272

Costo: 1 SELECT adicional con sus ~8 callbacks activos = ~9 queries innecesarios por operación de escritura.

## GarantiaContrato — Double-Roundtrip en Insert
`PersistNewItem()` llama `CheckAlreadyInserted()` (COUNT query) antes de cada INSERT = 2 roundtrips por insert.

## SQL Injection (ya documentado, sin cambios)
- `GarantiaOtraSqlRepository.UpdateIdentificacionDocumentoGarantia()` línea 371: interpolación directa de `valor` y `garantiaId` en UPDATE.
- `GarantiaOtraSqlRepository.GetAllByFianzasAvalesNoBancariosSQL()` línea 270: interpolación de `fianzaAvalID`.
- `GarantiaOtraSqlRepository.GetAllAndNotFianzasAvalesNoBancariosSQL()` línea 312: ídem.
- `MakerCheckerOperationSqlRepository.GetSQLOperationsByFilter()` línea 353: `fieldName` y `value` interpolados directamente — riesgo ALTO porque `fieldName` viene de código C# pero `value` puede venir de entrada de usuario.

## DataHelper.GetSqlValue(bool) — Bug confirmado
```csharp
return true ? "1" : "0";  // SIEMPRE devuelve "1" — bug de precedencia de ternario
```

## Caching: Estado Actual (post-Fase 2 y 3A)
**YA CACHEADO (1 hora):**
- PaisService, MonedasService, CalificacionesRiesgoService, InternalStatusService, StatusService
- TipoGarantiaBladexService, TipoGarantiaSuperService, FrecuenciasService
- CategoriaRiesgoGarantiaService, RegionService, TipoPolizaService, CategoriaSuperService
- TooltipService, LimitInformationService, BancosService, AseguradorasService, AvaluadorasService

**SIN CACHE — Candidatos para Fase 3B:**
- ClienteSqlRepository.FindBy() — llamado como callback en GarantiaBase (Cliente + Garante)
- FiduciariasSqlRepository.FindBy() — llamado como callback en GarantiaBase
- ActorSqlRepository.FindBy() — 5 callbacks (Depositante, Evaluador, Administrador, Asegurador, Revisor)
- MakerCheckerUser, MakerCheckerChangeset, MakerCheckerOperationStatus — en callbacks MakerChecker
