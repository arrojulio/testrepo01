---
name: js_architecture
description: Arquitectura del código JavaScript en el frontend — common.js y patrones de AJAX
type: project
---

**common.js — 1895 líneas, archivo monolítico global**
- `var chromeLog = true` en línea 2 — flag de debug en producción (console.log comentado por Ticket #1888)
- `var baseUrl = "http://localhost:47730"` en línea 4 — URL hardcodeada, NO anulada para producción
- 24 llamadas a `alert()` — UX pobre para manejo de errores
- AJAX calls usan baseUrl prefijado en algunas funciones pero URL relativa en otras (inconsistente)

**Patrones AJAX observados:**
- JSON manual: `"{'key': " + JSON.stringify(value) + "}"` — propenso a errores
- contentType: "application/json;" (sin charset) en algunas llamadas
- Sin manejo de CSRF token en ninguna llamada AJAX POST
- `changeStatusGarantia` y `changeTypeGarantia` usan `location.reload(true)` como respuesta de éxito

**Funciones de validación JS:**
- `SaveGarantia_OnClick()` — 400+ líneas, valida por CategoriaSuper.Key ("01"-"06")
- `CleanLabelValidation()` — limpia manualmente 20+ labels de validación con IDs hardcodeados
- `ValidateComboBox()` — reitera la lista completa de items para validar selección
- Doble `var today = new Date()` en calcFechaVencimiento (variable re-declarada en mismo scope)

**Gestión de estado de controles:**
- Patrón: `.attr("disabled", "disabled")` y `.removeAttr("disabled")` — jQuery 3.x compatible pero verbose
- Patrón: Telerik `.enable()` / `.disable()` para componentes Telerik
- `HabilitarControlesParaSave()` y `DeshabilitarControlesParaSave()` duplican lógica del botón Save

**Estructura de carga en Site.Master:**
- Scripts en <head>: jQuery, shims, jQuery UI, Bootstrap, DataTables, tooltips, common.js
- Scripts en <body>: validate, unobtrusive-ajax, MicrosoftAjax*, inline user label script
- Telerik ScriptRegistrar al final del body (correcto)
- Scripts de página en HeaderContent placeholder (se ejecutan antes de body scripts — orden correcto)

**How to apply:** Al refactorizar JS, priorizar: (1) fix baseUrl, (2) eliminar alerts con manejo de errores apropiado, (3) centralizar validaciones, (4) agregar CSRF a AJAX POSTs.
