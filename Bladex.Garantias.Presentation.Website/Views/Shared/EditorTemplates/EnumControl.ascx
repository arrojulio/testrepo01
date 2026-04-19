<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Enum>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<%= Html.EnumDropDownListFor(model => model)%>
