<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.FiduciariasViewModel>" %>
<%=

Html.Telerik().ComboBoxFor(m=>m.Key)
                      .AutoFill(true)
                      .BindTo(new SelectList(Model.List.OrderBy(a=>a.Text), "Value", "Text", Model.Key)).Encode(false)
                        //.ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
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



