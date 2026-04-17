<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Enum>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<%= Html.HiddenFor(m=>m) %>
<%= Model != null ? Model.ToString() : "(empty)" %>
