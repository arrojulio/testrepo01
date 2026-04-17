<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Int32>" %>
<%=Html.Telerik().IntegerTextBoxFor(m=>m).HtmlAttributes(new { @class="txtInf" }).InputHtmlAttributes(new { @class="txtInf"}) %>

