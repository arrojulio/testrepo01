---
name: Established UI/HTML patterns
description: Table-based form layout, CSS class naming, editor templates, and list/grid patterns used across the codebase
type: project
---

## Form layout pattern
All guarantee forms use a two-column table layout (not Bootstrap grid):
- Table: id="tblGarantia" class="guaranteeTable" — border/collapse defined in app-styles.css
- Each row: <tr class="guaranteeRow">
- Left cell: <div class="editor-label"> — fixed-width label (180px in GarantiaContrato views)
- Right cell: <div class="editor-field"> — 388px in GarantiaContrato; 100%-width inputs

The primary EditorTemplate driving all guarantee forms is:
  Views/Shared/EditorTemplates/GarantiaBaseModel.ascx (~875 lines)
  Renders ~35 fields via Html.EditorFor per property.

EditorTemplates follow a large flat list pattern; no grouping/sectioning by field type.

## CSS classes to know
- .guaranteeTable / .guaranteeRow / tr.guaranteeRow td — main form table
- .editor-label / .editor-field — form cell divs
- .styled-table / .styled-table th / .styled-table td — used for MakerChecker and list views
- .styled-table.property-bag — diff table in MakerChecker/Details
- .changeset-viewer / .changeset-viewer-action-container — MakerChecker banner (rendered by RenderAction)
- .over-utilization-alert-container / .over-utilization-alert-icon — alert banner for over-utilization
- .itemContainer — MakerChecker widget cards
- .pager / .pager-number / .pager-number-selected — custom pager (NOT Bootstrap)
- .divRow — generic spacing wrapper (min-width:600px)
- .commentBox / .commentBoxInput — textarea for checker comments
- h2 — has custom background (bg-green.jpg image), white text, 15px — heavily branded

## List/grid pattern
- Garantia/List.aspx: plain HTML table with DataTables 1.9.4 applied via makeNiceTable("tblGarantiasList")
- MakerChecker/Index.aspx: Telerik Grid component (Html.Telerik().Grid())
- Both approaches coexist in the codebase

## JavaScript patterns
- Inline <script> tags appear both in HeaderContent (early load) and at the bottom of MainContent (late load)
- common.js provides: makeNiceTable(), goBack(), baseUrl, logInBrowser(), setupGarantiaBaseModel(), HabilitarControlesParaSave(), DeshabilitarControlesParaSave()
- MakerChecker actions (Approve/Reject) use $.ajax POST with JSON body; success check is string comparison against "success"
- Telerik ComboBox: client event OnChange fires bindDealReferenceInfo(e), accesses combobox via $("#id").data("tComboBox")
