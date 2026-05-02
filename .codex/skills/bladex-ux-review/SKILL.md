---
name: bladex-ux-review
description: Review and improve UX/UI in the Bladex Garantias project. Use when Codex needs to analyze ASP.NET MVC WebForms views, shared editor/display templates, Telerik MVC controls, jQuery behaviors, CSS styles, read-only form states, MakerChecker screens, tables, validation messages, visual bugs, accessibility, or usability issues in any Garantia workflow.
---

# Bladex UX Review

Use this skill to review and improve the user interface of Bladex Garantias while preserving the existing ASP.NET MVC 5, WebForms view engine, Telerik MVC, jQuery, and legacy visual conventions.

## Project Context

Bladex Garantias is a legacy financial guarantees system with operational forms and tables. The UI is not a marketing site; prioritize clarity, dense but readable information, predictable controls, and safe workflows for repeated back-office use.

Primary UI locations:
- `Bladex.Garantias.Presentation.Website/Views/**/*.aspx`
- `Bladex.Garantias.Presentation.Website/Views/**/*.ascx`
- `Bladex.Garantias.Presentation.Website/Views/Shared/EditorTemplates/*.ascx`
- `Bladex.Garantias.Presentation.Website/Views/Shared/DisplayTemplates/*.ascx`
- `Bladex.Garantias.Presentation.Website/Scripts/common.js`
- `Bladex.Garantias.Presentation.Website/Content/*.css`

Core UI technology:
- ASP.NET MVC 5 with WebForms views (`.aspx`, `.ascx`)
- Telerik MVC legacy widgets: `ComboBox`, `DropDownList`, `DatePicker`, `CurrencyTextBox`, `NumericTextBox`, grids
- jQuery and jQuery UI legacy behavior
- DataTables-like client table enhancement through local helpers such as `makeNiceTable`
- Shared editor/display templates for common visual behavior across Garantia types

## Scope

Review UI for:
- `GarantiaContrato`
- `GarantiaDeposito`
- `GarantiaDepositoOtroBanco`
- `GarantiaInmueble`
- `GarantiaMueble`
- `GarantiaPrenda`
- `GarantiaOtra`
- `GarantiaBase`
- `Aval` and `AvalManager`
- MakerChecker pending, details, approve/reject, current changeset, and change viewer flows

Focus on:
- visual correctness and overlapping controls
- read-only mode and disabled states
- form layout, labels, validation, and required-field clarity
- Telerik control behavior and generated markup
- JavaScript event handlers and accidental re-enabling
- CSS specificity and cross-widget side effects
- table action visibility, paging ergonomics, and scanability
- accessibility basics: keyboard flow, disabled/read-only affordance, labels, focus, contrast
- safe UX for MakerChecker actions and destructive actions

## Mandatory Rules

Always read `AGENTS.md` before acting if it has not already been loaded in the current turn.

Follow these rules:
- Keep compatibility with .NET Framework 4.6.1 and the existing MVC 5/WebForms view stack.
- Preserve Telerik MVC and jQuery patterns unless the user explicitly asks to modernize.
- Work incrementally; never modify more than 2-3 files without explicit approval.
- Show a summary and diff preview before editing.
- Prefer shared templates or common JS/CSS fixes when they safely improve all Garantia types.
- Avoid broad visual redesigns unless requested; keep the back-office financial UI quiet and utilitarian.
- Do not introduce new frontend frameworks, bundlers, icon systems, or CSS methodologies without approval.
- Keep display text in the project's current language and domain terms.
- Write findings and recommendations in Spanish unless the user asks otherwise.

## Review Workflow

1. Identify the exact UI workflow:
   - list/table screen
   - create/edit/delete/details form
   - read-only view
   - GarantiaContrato association
   - Aval manager
   - MakerChecker details/approval flow
   - shared editor/display template

2. Trace the UI path:
   - controller action and ViewBag/ViewModel flags
   - `.aspx` page
   - shared `.ascx` editor/display templates
   - JavaScript handlers in `common.js` or page scripts
   - CSS rules affecting Telerik/generated markup

3. Inspect UI state propagation:
   - `CategoriaSuper.IsReadOnly`
   - hidden `#IsReadOnly`
   - `ViewBag.IsReadOnly`
   - query string flags such as `isReadOnly`, `readOnly`, `useRepository`, `operationId`
   - role checks such as `Power User`, `Checker`, and read-only users

4. Check controls:
   - Telerik widgets should use their own enable/disable API when possible.
   - Native inputs should use `readonly` when values must post and `disabled` when values must not be changed or submitted.
   - Hidden fields needed for navigation or postback must remain intact.
   - Action buttons and links must match the current workflow state.
   - Client-side read-only UX is not a security boundary; note server-side gaps separately.

5. Check visual quality:
   - no overlapping formatted/input layers
   - labels align with controls
   - validation messages do not shift or cover adjacent fields
   - text fits inside buttons and table cells
   - disabled/read-only controls look intentionally inactive
   - tables remain readable with long identifiers, dates, money, percentages, and action links
   - CSS selectors are scoped enough not to break unrelated Telerik widgets

6. Validate when possible:
   - Build the web project after code changes.
   - If a local site is running or can be started safely, inspect with the browser-use browser.
   - For visual fixes, prefer a screenshot or concrete manual verification path.

## Common Bladex UI Risks

- `DisableFields(true)` or similar helpers only disable a subset of fields.
- `HabilitarControlesParaSave()` may re-enable controls temporarily before submit.
- `IsReadOnly` may be passed with inconsistent casing between query string, ViewBag, and hidden inputs.
- Telerik `CurrencyTextBox` can show both formatted text and raw input if CSS forces `.t-input` colors/backgrounds globally.
- Editor templates can make one fix affect every Garantia type, which is useful but high impact.
- Action links may be hidden visually but still reachable by URL; call this out as server-side risk.
- DataTables/Telerik tables can be client-side only; UX review may need performance skill for large datasets.
- MakerChecker screens must make approve/reject permissions and current operation state obvious.

## Preferred Fix Patterns

Prefer:
- A small shared helper in `common.js` for behavior that truly applies to all Garantia forms.
- Scoped CSS selectors under specific Telerik widgets or form containers.
- Template-level fixes when the same control appears across many forms.
- `readonly` for textual values that must still post.
- Telerik `.disable()` / `.enable()` APIs for widgets.
- Hiding or disabling primary actions in read-only mode, while preserving navigation actions.
- Clear validation placement near the failing control.

Avoid:
- Inline page-by-page duplication when a shared template or script handles the issue.
- Global CSS changes to `.t-input`, `input`, `select`, or `table` without checking generated Telerik markup.
- Disabling hidden fields required by postback, routing, MakerChecker, or contract navigation.
- Cosmetic rewrites that change workflow semantics.
- Frontend rewrites or library upgrades as part of a small UX fix.

## Output Format

Use this structure for reviews:

```markdown
**Resumen Ejecutivo**
- [3-5 bullets, highest UX impact first]

**Flujo Analizado**
- Controller:
- View:
- Templates:
- JavaScript:
- CSS:

**Hallazgos**
| # | Severidad | Componente | Evidencia | Impacto | Recomendacion |
|---|-----------|------------|-----------|---------|----------------|

**Quick Wins**
- [Low-risk fixes in 1-2 files]

**Diff Preview**
[Show proposed changes before editing]

**Validacion**
- Build/test commands.
- Manual browser path or screenshot target when applicable.
```

When implementing, still present a diff preview first unless the user has already approved that exact change.
