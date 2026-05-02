---
name: view_patterns
description: Patrones de vistas ASP.NET Web Forms (.aspx/.ascx) — estructura, tamaño y convenciones (actualizado con modernizacion 2026-05)
type: project
---

**Formato de vistas:** .aspx (Web Forms con MasterPage), .ascx (controles de usuario)
- NO hay vistas Razor (.cshtml) — todo es Web Forms con MVC helpers
- MasterPage: Views/Shared/Site.Master
- Layout de contenido: TitleContent, HeaderContent, MainContent

**Formularios de Garantia (Edit.aspx) — tamanos:**
- GarantiaInmueble/Edit.aspx: 973 lineas
- GarantiaPrenda/Edit.aspx: 1003 lineas
- GarantiaDeposito/Edit.aspx: 867 lineas
- GarantiaDepositoOtroBanco/Edit.aspx: 871 lineas
- GarantiaMueble/Edit.aspx: 919 lineas
- GarantiaOtra/Edit.aspx: 914 lineas
- GarantiaContrato/Edit.aspx: ya modernizado con blx-card y btn Bootstrap

**Layout de campos en formularios:**
- Tabla HTML (`<table class="guaranteeTable">`) con filas `.guaranteeRow`
- Dos columnas: `<td class="editor-label">` + `<td class="editor-field">` (son TD, no DIV)
- Tabs jQuery UI (#tabs-1 Required Fields, #tabs-2 Optional Fields)
- El `GarantiaBaseViewModel.ascx` template usa tabla — modernizado con semantica aria y `.blx-required`

**DisplayTemplates/EditorTemplates (Views/Shared/):**
- DisplayTemplates: AvalViewModel, Boolean, DateTime, GarantiaBaseModel, GarantiaBaseTableRow, etc.
- EditorTemplates: Autocomplete, AvalManager, GarantiaBaseViewModel, Date, DateTime, Money, Currency, etc.
- Convencion: un .ascx por tipo de ViewModel

**Listas de garantias (Index.aspx):**
- Tabla HTML con `id="tblGarantiasList"` inicializada con `makeNiceTable()`
- DataTables 1.9.4 con configuracion: 10 registros/pagina, orden desc columna 1, sin filtro visible
- Cada row renderizada con `Html.RenderPartial("DisplayTemplates/GarantiaXxxTableRow", item)`

**MakerChecker:**
- Index.aspx: usa Telerik Grid (unico uso de Grid en todo el proyecto)
- Details.aspx: modernizado con blx-card, botones Bootstrap btn-success/btn-danger, tabs jQuery UI, paginador extraido a _Pager.ascx
- ChangesetViewer.ascx: partial view insertada en Site.Master via Html.RenderAction("Current","MakerChecker"); modernizado con botones Bootstrap y SVG inline
- _Pager.ascx: NUEVO partial reutilizable de paginacion MakerChecker (registrado en .csproj)

**Paleta CSS actual (desde 2026-05 modernizacion):**
- Variables CSS en :root via app-styles.css — prefijo `--blx-*`
- Navy: #1B2B4B | Acento: #4A7FC1 | Fondo: #F4F6FA | Borde: #D0D9E8
- Tipografia: "Segoe UI", system-ui (en lugar de Tahoma/Verdana)
- Componente `.blx-card` / `.blx-card-header` / `.blx-card-body` para secciones de formulario
- Clase `.blx-action-bar` para grupos de botones
- Clase `.blx-status-badge` para estados MakerChecker

**Site.Master modernizado:**
- Navbar Bootstrap 5 sticky (`.blx-navbar`) con menu de navegacion y badge de usuario
- Scripts Microsoft AJAX eliminados (Ajax.BeginForm no se usa en ninguna vista — verificado con grep)
- Comentarios condicionales IE eliminados (ie6.css, ie8.css)
- Scripts movidos al final del body donde es posible
- `#blx-changeset-bar` envuelve el ChangesetViewer

**Encoding:**
- `<%: %>` (HTML encoded) — usado correctamente en la mayoria de campos
- `<%= %>` (sin encode) — usado para HTML helpers y string.Format con datos del servidor
- `<%= Html.Raw(...) %>` — usado para serializar Model a JSON en variables JS (13 vistas)

**How to apply:** Al crear nuevas vistas, seguir el patron .aspx existente. No introducir Razor (.cshtml) sin aprobacion del usuario. El layout de dos columnas (editor-label / editor-field) es el estandar. Usar `.blx-card` para envolver secciones y `.blx-action-bar` para grupos de botones.
