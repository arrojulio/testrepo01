---
name: Frontend stack versions
description: JS/CSS library versions loaded in both master pages after the April 2026 upgrade
type: project
---

After the upgrade committed around 2026-04-25, both master pages load:

- jQuery 3.7.1 (local, ~/Scripts/jquery-3.7.1.min.js)
- jquery-migrate 3.4.1 (compatibility shim for Telerik 2011 components)
- jquery.browser-shim.js (custom shim for $.browser used by Telerik)
- jQuery UI 1.13.3 (local), theme: base (jquery-ui-1.13.3.min.css)
- Bootstrap 5.3.3 (CSS + bundle JS, local)
- DataTables 1.9.4 (jquery.dataTables-1.9.4.min.js) — standardised on both pages
- Telerik MVC 2011.2.712 — StyleSheetRegistrar + ScriptRegistrar (telerik.common.css + telerik.windows7.css)
- json2.js, hoverIntent.js, jquery.bt.min.js, jquery.query.js, excanvas.compiled.js
- common.js — project-wide JS utilities
- jquery.validate + jquery.validate.unobtrusive + MicrosoftAjax/MvcAjax/MvcValidation

Site.Master uses these scripts in <head>; script tags for validate/Microsoft are placed inside a <div class="scripts"> at the top of <body>.
EmptyMasterPage.Master additionally loads jquery.formatCurrency-1.3.0.min.js and fancybox 1.3.4.

Doctype: XHTML 1.0 Transitional (legacy, not HTML5).

**Why:** Context for any future library version work or conflict investigation.
**How to apply:** Do not suggest removing jquery-migrate or jquery.browser-shim without validating that all Telerik 2011 components still work.
