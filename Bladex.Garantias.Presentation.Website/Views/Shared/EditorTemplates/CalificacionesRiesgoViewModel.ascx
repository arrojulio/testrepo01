<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.CalificacionesRiesgoViewModel>" %>
<%= Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true)
                      .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key)).Encode(false)
                                  .ClientEvents(c => c.OnChange("onDropdownChangeReadOnly").OnLoad("onDropdownChange"))
                      .Filterable(filtering =>
                      {
                          if (true) 
                          {
                              filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                          }
                      })
                      .HighlightFirstMatch(true)
    %>

