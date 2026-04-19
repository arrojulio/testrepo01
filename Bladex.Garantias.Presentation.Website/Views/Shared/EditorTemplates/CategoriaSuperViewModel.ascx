<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.CategoriaSuperViewModel>" %>
<%= Html.HiddenFor(m=>m.Key) %>
<%= Html.HiddenFor(m => m.Nombre)%>
<%= Html.HiddenFor(m => m.IsReadOnly)%>
