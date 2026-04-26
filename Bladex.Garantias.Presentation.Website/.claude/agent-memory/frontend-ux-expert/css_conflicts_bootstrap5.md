---
name: Bootstrap 5 vs existing styles — conflict zones
description: CSS conflicts between Bootstrap 5.3.3, app-styles.css, and jQuery UI 1.13.3 base theme identified during April 2026 analysis
type: project
---

Load order in master pages: app-styles.css → jquery-ui-1.13.3.min.css → bootstrap-5.3.3.min.css → Telerik CSS

## Confirmed high-risk conflicts

1. **box-sizing**: Bootstrap resets all elements to box-sizing:border-box. app-styles.css input widths (e.g. .editor-field input width:100%) were written without box-sizing in mind. May cause form field overflow or misalignment.

2. **button/input[type=submit] styles**: app-styles.css defines explicit background-image (bg-bluetitle2.jpg), border, padding, width:100px for input[type=button|submit|reset]. Bootstrap 5 normalises these via its own button reset. Bootstrap wins (loaded later) unless app-styles.css selectors have higher specificity — they are equal, so Bootstrap last-wins and STRIPS the custom background images.

3. **h2 background image**: app-styles.css defines h2 { background: url(images/bg-green.jpg) repeat-x } — Bootstrap resets heading margins/padding. The heading visual (green bar) likely survives since Bootstrap doesn't target background-image, but line-height and margin may shift.

4. **table styles**: Bootstrap 5 adds its own table reset (border-collapse, border-color, vertical-align). The .guaranteeTable and .styled-table classes may absorb Bootstrap's base table styles before their own rules apply. Specifically, Bootstrap sets table>:not(caption)>*>* { padding: 0.5rem } which conflicts with cellpadding="0" and app-styles.css row padding.

5. **a (anchor) color**: Bootstrap defines a { color: #0d6efd }. app-styles.css defines a { color: #718ABE }. Bootstrap wins (loaded later) — all unclassed links will turn Bootstrap blue, not the existing brand blue.

6. **body font-size**: Bootstrap sets body { font-size: 1rem } (= 16px browser default). app-styles.css sets body { font-size: 12px }. Bootstrap wins → all text scales up to 16px unless overridden. This is a significant visual regression.

7. **jQuery UI / Bootstrap modal z-index clash**: jQuery UI dialogs use z-index ~1000; Bootstrap modals use z-index 1055+. If both are ever open simultaneously there is overlap risk. Currently no Bootstrap modals are used so this is latent.

8. **input[type=text] text-transform**: app-styles.css forces uppercase on all text inputs. This conflicts with Bootstrap's form-control styling (no transform). Since app-styles loads first, the uppercase rule survives — BUT Bootstrap's .form-control class does not inherit it. If Bootstrap form classes are ever adopted, inputs will lose uppercase.

9. **fieldset/legend**: Bootstrap 5 resets fieldset { min-width: 0; padding: 0; border: 0 } and legend with specific float/sizing. app-styles.css defines fieldset { border-color:#b8cce4; padding:10px }. Bootstrap's reset (loaded later) will strip the custom fieldset borders — all <fieldset> containers in GarantiaContrato/Create will lose their visual border.

10. **Pager styles (.pager-number color:#fff)**: Bootstrap 5 defines .pagination styles independently but does not target .pager-number. The custom pager should survive, BUT Bootstrap defines a global color variable on :root that may affect inherited color where not explicitly set.

## Lower-risk observations
- .field-validation-error (color:red) — Bootstrap defines .invalid-feedback similarly; no direct conflict since class names differ.
- .validation-summary-errors — Bootstrap has its own validation classes; no conflict on class names.
- Telerik CSS (windows7 theme) loads after Bootstrap via ScriptRegistrar — Telerik's component styles should override Bootstrap's base styles on Telerik elements.

**How to apply:** When implementing any Bootstrap-class-based improvement, add a defensive override section to app-styles.css (or a new bootstrap-overrides.css) to restore: body font-size, anchor color, fieldset border, button background-image, and table cell padding.
