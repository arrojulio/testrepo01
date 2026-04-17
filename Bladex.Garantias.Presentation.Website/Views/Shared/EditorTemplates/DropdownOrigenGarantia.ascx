<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>

<% int selectedIndex = string.IsNullOrEmpty(Model) ? 1 : Model.ToUpper() == "L" ? 0 : 1; %>
<%= Html.Telerik().DropDownListFor(m=>m)
        .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))   
        .BindTo(new SelectList(new[] { new { Value = "L", Text = "Local" }, new { Value = "E", Text = "Extranjero" } }, "Value", "Text"))
        .SelectedIndex(selectedIndex)
        
    %>
