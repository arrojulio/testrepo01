<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.TipoGarantiaSuperViewModel>" %>
<script type="text/javascript">
    setTipoGarantiaSuperControlName('<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
</script>
<%= Html.EditorFor(m=>m.Categoria)%>
<%= Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true)
                              .ClientEvents(c => c.OnChange("onDropdownChangeReadOnly").OnLoad("onDropdownChange"))
                      .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
                      .Filterable(filtering =>
                      {
                          if (true) 
                          {
                              filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                          }
                      })
                      .HighlightFirstMatch(true).Encode(false)
    %>
<% Html.Telerik().ScriptRegistrar().OnDocumentReady("validateIndicadorAtomoSelected();"); %>
