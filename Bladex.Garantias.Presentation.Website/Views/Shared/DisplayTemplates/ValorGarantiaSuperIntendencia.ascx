<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal>" %>

<%--<%=Html.TextBoxFor(m => m, new { style = "border:0px none; width:auto;color:#7DA5E0;", @readonly = true })%>--%>
<%= Html.Telerik().CurrencyTextBox()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .InputHtmlAttributes(new { style = "width:100%", @class = "money txtMoney" })
        .DecimalDigits(2)
        .EmptyMessage("(empty)")
        .Spinners(false)
        .MinValue(Decimal.MinValue)
        .Value(Model).Enable(false)
%>