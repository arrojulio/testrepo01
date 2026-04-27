---
name: Legacy frontend dependencies — Telerik, DataTables, FancyBox
description: Telerik MVC 2011.2.712 injects its own scripts via ScriptRegistrar with jQuery(false); DataTables 1.9.4 and FancyBox 1.3.4 are active and jQuery 3.x incompatible
type: project
---

- Telerik MVC 2011.2.712 is loaded via Html.Telerik().ScriptRegistrar().jQuery(false) in both master pages. jQuery(false) tells Telerik NOT to embed its own jQuery copy, relying on the host page's jQuery. This must remain after the CDN upgrade.
- Site.Master loads jquery.dataTables-1.9.4.min.js globally in `<head>`. The per-type Index.aspx views (GarantiaDeposito, GarantiaInmueble, etc.) use EmptyMasterPage and additionally load jquery.dataTables.min.js (version 1.7.5 — the old unversioned file) in their HeaderContent placeholder, causing a double-load.
- All makeNiceTable() calls pass `"bJQueryUI": true`, which switches DataTables to the jQuery UI ThemeRoller CSS class set: buttons get classes `fg-button ui-button ui-state-default ui-corner-left/right`. No Bootstrap integration plugin (dataTables.bootstrap.js) is present. No DataTables-specific CSS file (jquery.dataTables.css) is loaded anywhere.
- FancyBox 1.3.4 CSS and JS are loaded only in EmptyMasterPage.Master.
- Both DataTables 1.9.4 and FancyBox 1.3.4 are known jQuery 3.x incompatible; upgrade is deferred to a separate task.
- ROOT CAUSE of pagination regression: DataTables 1.9.4 with bJQueryUI:true emits `<a class="fg-button ui-button ui-state-default ui-corner-left">` anchors as pagination controls. These anchors were styled by the old jQuery UI CSS. Bootstrap 5 Reboot adds `a { text-decoration: underline; }` and normalises box model — but critically, the pagination buttons were always relying entirely on jQuery UI `ui-state-default` border/background CSS. Bootstrap 5's more aggressive reboot overrides the anchor styles. Combined with the fact that NO DataTables CSS file is loaded (so `dataTables_paginate` wrapper has no layout CSS), the buttons collapse to zero-height or invisible inline anchors with no background/border visible against the page background.

**Why:** These are active production dependencies; removing or upgrading them is a separate workstream.

**How to apply:** When reviewing changes that touch script loading order, remember that Telerik scripts land at the bottom of body via ScriptRegistrar — jQuery must be fully available before that point.
