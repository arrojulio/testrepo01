<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.ActorViewModel>" %>
<% //int selectedIndex = Model.List.ToList().IndexOf(Model.List.FirstOrDefault(o => o.Value == Model.Key)); %>
<%= Html.HiddenFor(m=>m.Nombre) %>
<%: Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true).Encode(false)
                      .BindTo(new SelectList(Model.List.OrderBy(a=>a.Text), "Value", "Text", Model.Key))
                          .ClientEvents(c => c.OnChange("getActorInformation").OnLoad("onDropdownChange"))
                      .Filterable(filtering =>
                      {
                          if (true) 
                          {
                              filtering.FilterMode(AutoCompleteFilterMode.Contains);
                          }
                      })
                      .HighlightFirstMatch(true)
    %>
<div class="hSeparator"></div>
<div id="paisContainer">
<%: Html.EditorFor(m=>m.Pais) %>
</div>

<script type="text/javascript">
    logInBrowser('Actor Key: ' + '<%=Model.Key %>');
    logInBrowser('Actor Nombre: ' + '<%=Model.Nombre %>');
    logInBrowser('Actor Pais: ' + '<%=Model.Pais.Nombre%>');
</script>







