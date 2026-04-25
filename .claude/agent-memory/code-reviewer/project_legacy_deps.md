---
name: Legacy frontend dependencies — Telerik, DataTables, FancyBox
description: Telerik MVC 2011.2.712 injects its own scripts via ScriptRegistrar with jQuery(false); DataTables 1.9.4 and FancyBox 1.3.4 are active and jQuery 3.x incompatible
type: project
---

- Telerik MVC 2011.2.712 is loaded via Html.Telerik().ScriptRegistrar().jQuery(false) in both master pages. jQuery(false) tells Telerik NOT to embed its own jQuery copy, relying on the host page's jQuery. This must remain after the CDN upgrade.
- DataTables 1.9.4 is loaded in both master pages (Site.Master loads jquery.dataTables-1.9.4.min.js; EmptyMasterPage loads jquery.dataTables.min.js which may be a different version).
- FancyBox 1.3.4 CSS and JS are loaded only in EmptyMasterPage.Master; no views appear to call .fancybox() directly in markup — usage may be in common.js or inline scripts in child views.
- Both DataTables 1.9.4 and FancyBox 1.3.4 are known jQuery 3.x incompatible; upgrade is deferred to a separate task.

**Why:** These are active production dependencies; removing or upgrading them is a separate workstream.

**How to apply:** When reviewing changes that touch script loading order, remember that Telerik scripts land at the bottom of body via ScriptRegistrar — jQuery must be fully available before that point.
