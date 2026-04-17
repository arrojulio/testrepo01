<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Boolean>" %>
<label><%= Html.Encode(Model ? "Yes" : "No") %></label>

