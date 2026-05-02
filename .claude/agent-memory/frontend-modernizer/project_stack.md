---
name: stack_frontend_actual
description: Stack frontend real observado en el proyecto — difiere del CLAUDE.md que menciona jQuery 1.7.2
type: project
---

El proyecto ya fue parcialmente modernizado en commits recientes (38a30bb, 55e7c1b, 23e38e5):

**Stack actual en producción (Site.Master):**
- jQuery 3.7.1.min.js (migrado desde 1.7.2)
- jquery-migrate-3.4.1.min.js (shim de compatibilidad)
- jquery.browser-shim.js (shim customizado para Telerik legacy)
- jQuery UI 1.13.3 (migrado desde 1.8.19)
- Bootstrap 5.3.3 (nuevo — cargado junto con bootstrap-overrides.css)
- DataTables 1.9.4 (sin cambios, legacy)
- jquery.bt.min.js (tooltips, legacy)

**Telerik versión:** 2011.2.712 — sin cambio (scripts/CSS siguen siendo la versión antigua)

**Archivos viejos presentes pero no referenciados:**
- jquery-1.4.1.js, jquery-1.4.4.js, jquery-1.7.2.js (Scripts/)
- jquery-ui-1.8.19.js, jquery-ui-1.8.5.custom.min.js (Scripts/)
- Múltiples temas Telerik sin uso activo en Content/2010.3.1318/, Content/2011.1.315/, etc.

**Why:** La migración jQuery 3.7.1 se realizó para corregir bugs de seguridad y compatibilidad con browsers modernos. El shim en jquery.browser-shim.js fue necesario porque Telerik 2011 usa APIs eliminadas en jQuery 1.9+ ($.browser, $.fn.live, $.telerik.trigger).

**How to apply:** Al sugerir cambios JS, verificar primero que sean compatibles con jQuery 3.7.1 (no asumir 1.7.2 como indica CLAUDE.md). Los componentes Telerik siguen siendo la versión 2011.2.712.
