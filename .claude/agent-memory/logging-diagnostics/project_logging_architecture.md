---
name: Logging Architecture — Bladex Garantías
description: Arquitectura completa del sistema de logging Log4Net + ELMAH, incluyendo configuración dual, problemas conocidos y esquema de tabla.
type: project
---

## Configuración Log4Net

**Dos fuentes de configuración activas (conflicto confirmado):**
- `log4net.config` (archivo separado): AdoNetAppender con `bufferSize=100`, `connectionString` con Integrated Security apuntando a `localhost/BLX_GARANTIAS`.
- `Web.config` sección `<log4net>`: AdoNetAppender con `bufferSize=100`, **credenciales en texto plano** (`User ID=linksis;Password=linksis`) + ConsoleAppender adicional.

**La configuración activa en runtime es la de Web.config** porque `XmlConfigurator.Configure()` sin argumentos lee `Web.config` cuando existe la sección `<log4net>` registrada. El archivo `log4net.config` solo se usa si se referencia explícitamente.

**bufferSize=100 en ambas configs**: Los logs NO se persisten hasta que se acumulan 100 eventos en el buffer en memoria. El reciclo del app pool o cualquier cierre abrupto descarta todos los eventos pendientes.

**Sin `Application_End` con flush**: No hay llamada a `log4net.LogManager.Shutdown()` en `Global.asax.cs`, por lo que el buffer se pierde al detener/reciclar el pool.

## Inicialización de Log4Net — Triple inicialización (BUG)

Log4Net se inicializa 3 veces durante el startup:
1. `Global.asax.cs → Application_Start()` línea 140: `log4net.Config.XmlConfigurator.Configure()`
2. `Infrastructure.Bootstrapper.SetupLoggingFramework()` (llamado desde Application_Start línea 162)
3. `Presentation.Website.Bootstrapper.SetupLoggingFramework()` (llamado desde Application_Start línea 171)

Cada llamada reinicializa los appenders, descartando el buffer en memoria acumulado hasta ese punto.

## Esquema tabla ApplicationLog

```sql
CREATE TABLE [dbo].[ApplicationLog](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar](255) NOT NULL,
    [Level] [varchar](50) NOT NULL,
    [Logger] [varchar](255) NOT NULL,
    [Message] [varchar](4000) NOT NULL,
    [Exception] [varchar](2000) NULL,
    [UserId] [varchar](100) NULL   -- columna existente pero NO mapeada en el INSERT
);
```

**El INSERT no incluye `UserId`**: La columna existe pero nunca se popula desde Log4Net. Hay cero correlación usuario/log.

## Patrón de uso del logger

- `ApplicationLogger` es un Singleton con double-check locking correcto.
- Se accede via `HttpApplicationStateBaseExtensions.Logger()` extension method sobre `HttpApplication.Application`.
- `BaseLogger.PrepareMessage()` prepend automáticamente: versión app + método que llama via `StackFrame(3)` + mensaje.
- Logger único global: `LogManager.GetLogger(typeof(ApplicationLogger))` — todos los logs tienen el mismo `[Logger]` en la tabla, perdiendo trazabilidad por componente.

## Configuración ELMAH

- Backend SQL Server, tabla `ELMAH_Error`, connection string `BLX_GARANTIAS` (Integrated Security, correcto).
- `allowRemoteAccess="yes"` — acceso sin restricción a `/elmah.axd` desde cualquier IP.
- El email está comentado.

## Credencial de seguridad en Web.config

`User ID=linksis;Password=linksis` en texto plano en la sección `<log4net>` de `Web.config`. La connection string activa del appender usa estas credenciales, no Integrated Security.

**Why:** El logging fue configurado inicialmente para un entorno de servidor dedicado con usuario SQL específico, pero quedó hardcodeado en config que está en el repositorio.
**How to apply:** Alertar siempre sobre este riesgo cuando se sugieran cambios al Web.config o al pipeline de CI/CD.
