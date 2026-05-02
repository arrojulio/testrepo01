---
name: security_issues_frontend
description: Problemas de seguridad frontend identificados en el análisis inicial
type: project
---

**1. Ausencia total de AntiForgeryToken — CRÍTICO**
- Ninguna vista usa @Html.AntiForgeryToken()
- Ningún controller POST usa [ValidateAntiForgeryToken]
- Afecta: todos los formularios de Garantía (Create/Edit), Aval, GarantiaContrato, MakerChecker (Approve/Reject/Commit)
- Archivos: todos los controllers en Controllers/

**2. XSS en ChangesetViewer.ascx — ALTO**
- Línea 175: `<%= column%>` sin encode (nombres de columnas del modelo)
- Líneas 211-215: `string.Format()` con valores de `row.Model.CategoriaSuper.Key` sin encode
- Líneas 226-228: `row.Proposed[column]` renderizado sin encode
- Archivo: Views/Shared/ChangesetViewer.ascx

**3. XSS en MakerChecker/Details.aspx — ALTO**
- Líneas 165-168: `<%= Model.Operation.MakerDate %>`, `<%= Model.Operation.CheckerDate %>`, `<%= Model.Operation.CheckerUserId %>` sin encode
- Líneas 246-254: `Model.Original[kv.Key].GetValueFormatted()` y `kv.GetValueFormatted()` sin encode
- Archivo: Views/MakerChecker/Details.aspx

**4. baseUrl hardcodeado en common.js — ALTO**
- Línea 4: `var baseUrl = "http://localhost:47730";` — URL de desarrollo hardcodeada
- Líneas 5-7: hay comentarios de producción pero baseUrl no se anula correctamente
- Afecta AJAX calls a SaveAutocomplete, CalcProximaFechaRevision, GetTooltip, etc.
- Archivo: Scripts/common.js

**5. UserId como query string — MEDIO**
- BaseController.cs línea 132: acepta UserId por QueryString para cambiar usuario
- Usado en Site.Master línea 63: `$.query.get('UserId')` para logout
- Permite potencialmente forzar logout de otro usuario conociendo su ID

**6. Html.Raw con datos del modelo en JS — MEDIO**
- Patrón: `var model = '<%= Html.Raw(Json.Encode(Model))%>';` en 13 vistas
- Si el modelo contiene comillas simples en campos de texto, puede romper el JS
- Archivos: GarantiaDeposito/Create.aspx, Edit.aspx, GarantiaDepositoOtroBanco/*, GarantiaInmueble/*, GarantiaMueble/*, GarantiaOtra/*, GarantiaPrenda/*, Shared/EditorTemplates/GarantiaBaseModel.ascx

**Why:** Estos problemas fueron identificados en el análisis inicial de mayo 2026.
**How to apply:** Al revisar o modificar cualquier vista de garantía o MakerChecker, verificar estos puntos primero.
