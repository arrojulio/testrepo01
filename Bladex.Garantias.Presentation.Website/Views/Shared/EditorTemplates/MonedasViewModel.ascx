<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.MonedasViewModel>" %>
<%--<%= Html.Telerik().ComboBoxFor(m=>m.Key)
    .AutoFill(true)
    .BindTo(new SelectList(Model.List, "Value", "Text", string.IsNullOrEmpty(Model.Key) ? MonedasViewModel._DEFAULT_VALUE : Model.Key))
    .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
    .Filterable(filtering => filtering.Enabled(true).FilterMode(AutoCompleteFilterMode.Contains).MinimumChars(2))
    .HighlightFirstMatch(true)
%>--%>
<%= Html.Telerik().DropDownListFor(m=>m.Key)
.BindTo(new SelectList(Model.List, "Value", "Text", string.IsNullOrEmpty(Model.Key) ? MonedasViewModel._DEFAULT_VALUE : Model.Key))
.ClientEvents(c=>c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
%>

