<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.InstrumentoFinancieroViewModel>" %>
<% //int selectedIndex = Model.List.ToList().IndexOf(Model.List.FirstOrDefault(o => o.Value == Model.Key)); %>
<%= Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true)
                                .ClientEvents(c => c.OnChange("onDropdownChangeReadOnly").OnLoad("onDropdownChange"))                      
                      .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
                      .Encode(false)
                      .Filterable(filtering =>
                      {
                          if (true) 
                          {
                              filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                          }
                      })
                      .HighlightFirstMatch(true)
    %>

