<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal>" %>
<%= Html.Telerik().PercentTextBox()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
    .Spinners(true)
    .MinValue(0)
    .MaxValue(100)
    .Value(Convert.ToDouble(Model))
%>

