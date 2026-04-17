<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>
<%= Html.Telerik().DatePicker()
    .Enable(false)
    .Format("MM/dd/yyyy")
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
    .ShowButton(false)
        .Value((Model != null && (Model > DateTime.MinValue && Model.Value.Year < 2099)) ? Model : default(DateTime?))
%>