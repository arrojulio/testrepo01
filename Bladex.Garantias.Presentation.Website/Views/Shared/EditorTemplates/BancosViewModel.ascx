<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BancosViewModel>" %>
<%= Html.HiddenFor(m=>m.Nombre) %>
<%= Html.HiddenFor(m=>m.Categoria) %>
<% //int selectedIndex = Model.List.ToList().IndexOf(Model.List.FirstOrDefault(o => o.Value == Model.Key)); %>
<%= Html.Telerik().DropDownListFor(m=>m.Key)
                        //.SelectedIndex(selectedIndex < 0 ? 0 : selectedIndex)
                            .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange")).Encode(false)
                        .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
%>
