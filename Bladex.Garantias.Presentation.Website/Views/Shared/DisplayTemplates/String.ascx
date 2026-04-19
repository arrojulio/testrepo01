<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<String>" %>
<%= Html.Encode(string.IsNullOrEmpty(Model) ? string.Empty : Model) %>
