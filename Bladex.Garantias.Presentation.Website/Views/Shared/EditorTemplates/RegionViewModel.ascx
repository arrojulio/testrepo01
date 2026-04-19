<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.RegionViewModel>" %>
<%= Html.HiddenFor(m=>m.Nombre) %>
<%= Html.Telerik().DropDownListFor(m=>m.Key)
    .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
    .Encode(false)
    .DropDownHtmlAttributes(new { placeholder = "Seleccione una Región..."})
    .HtmlAttributes(new { placeholder = "Seleccione una Región..." })    
     //.BindTo(Model.List)
    .BindTo(new SelectList(Model.List, "Value", "Text", string.IsNullOrEmpty(Model.Key) ? "NA" : Model.Key))
    
%>