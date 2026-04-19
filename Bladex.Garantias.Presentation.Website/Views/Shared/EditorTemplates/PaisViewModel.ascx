<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.PaisViewModel>" %>
<%= Html.HiddenFor(m=>m.Nombre) %>
<%= Html.HiddenFor(m=>m.CodigoSuper) %>

<%
    if (Model.List == null)
   {
       Model.List = new List<SelectListItem>(new SelectListItem[] { new SelectListItem() { Value = "N/A", Text = "N/A", Selected = true } });

   }
    
    
%>
<%= Html.Telerik().DropDownListFor(m=>m.Key)
    .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
    .Encode(false)
    .DropDownHtmlAttributes(new { placeholder = "Seleccione un país..."})
    .HtmlAttributes(new { placeholder = "Seleccione un país..." })    
    //.BindTo(new SelectList(Model.List , "Value", "Text", string.IsNullOrEmpty(Model.Key) ? "N/A" : Model.Key))
    .BindTo(Model.List)
    
    
    
    
%>

