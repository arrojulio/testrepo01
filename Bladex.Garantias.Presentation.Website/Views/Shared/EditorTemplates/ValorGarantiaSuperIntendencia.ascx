<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal>" %>
<%= Html.Telerik().CurrencyTextBox()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .InputHtmlAttributes(new {style="width:100%", @class = "money txtMoney"})
        .MinValue(System.Decimal.MinValue)
        .Value(Model)
%>
