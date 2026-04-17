<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal>" %>
<%= Html.Encode(Model.ToString("c")) %>
