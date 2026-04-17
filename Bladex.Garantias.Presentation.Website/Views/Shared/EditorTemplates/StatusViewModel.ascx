<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.StatusViewModel>" %>
<%= Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true)
                      .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
                      .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
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
