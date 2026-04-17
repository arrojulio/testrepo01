<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.AseguradorasViewModel>" %>
<%= Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true)
                      .Encode(false)
                      .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
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


