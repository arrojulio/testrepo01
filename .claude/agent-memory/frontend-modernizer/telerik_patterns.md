---
name: telerik_patterns_2011
description: Patrones de uso de componentes Telerik 2011.2.712 observados en el proyecto
type: project
---

**Versión Telerik activa:** 2011.2.712 (Scripts/2011.2.712/, Content/2011.2.712/)
**Registro en Site.Master:**
- StyleSheetRegistrar: telerik.common.css + telerik.windows7.css
- ScriptRegistrar: jQuery(false) — indica que Telerik NO carga su propio jQuery

**Componentes en uso activo:**

1. **DatePicker** — EditorTemplates/Date.ascx y FechaVencimientoRiesgo.ascx
   - API de datos: `$('#id').data('tDatePicker').value()`
   - Evento: `.ClientEvents(e => e.OnChange("calcFechaVencimiento"))`

2. **CurrencyTextBox** — EditorTemplates/Money.ascx y ValorGarantiaSuperIntendencia.ascx
   - API: `$('#id').data("tTextBox").value()`
   - Evento: `.ClientEvents(events => events.OnChange("onMoneyChange_ID"))`
   - Genera función JS por instancia: `onMoneyChange_[fieldId]`

3. **ComboBox** — ClienteViewModel, ActorViewModel, etc.
   - API: `$('#id').data("tComboBox").text()`, `.value()`, `.enable()`, `.disable()`, `.reload()`
   - Evento: `.ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))`

4. **DropDownList** — BancosViewModel, PaisViewModel, etc.
   - API: `$('#id').data("tDropDownList").value()`, `.dataBind(data)`, `.select(index)`
   - Diferencia con ComboBox: no permite texto libre

5. **AutoComplete** — EditorTemplates/Autocomplete.ascx
   - Binding AJAX: Select("GetAutocomplete", "GarantiaBase")
   - Filtro: MinimumChars(2), FilterMode Contains
   - Evento: OnChange("onAutocompleteChange")
   - .Encode(false) — importante para compatibilidad

6. **Grid** — MakerChecker/Index.aspx
   - `.Name("changeset_summary_table")`, Sortable, Pageable(10), Filterable
   - columns.Template() con HTML inline

7. **PanelBar** — Garantia/Index.aspx
   - ExpandMode.Multiple, ExpandAll(true), BindTo(Model.Menu, mappings)

**Shims necesarios (jquery.browser-shim.js):**
- $.browser (eliminado en jQuery 1.9)
- $.fn.live / $.fn.die (eliminados en jQuery 1.9)
- $.parseJSON con null-safety
- $.telerik.trigger reemplazado con triggerHandler
- $.fn.tCalendar con stopAnimation=true

**How to apply:** Al modificar templates de edición o forms de garantía, mantener exactamente estos patrones API de Telerik. No mezclar con jQuery UI nativo para los mismos controles.
