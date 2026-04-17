<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<String>" %>
<%=Html.LabelFor(m=>m) %>: <%=Html.TextBoxFor(m => m, new { style = "border:0px none; width:auto;color:#7DA5E0;", @readonly = true })%>
