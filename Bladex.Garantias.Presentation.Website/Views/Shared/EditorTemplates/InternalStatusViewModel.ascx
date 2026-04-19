<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.InternalStatusViewModel>" %>
<%= Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true)
                      .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
                      .Encode(false)
                      .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
                      .Filterable(filtering =>
                      {
                          if (true) 
                          {
                              filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                          }
                      })
                      .HighlightFirstMatch(true)
    %>

