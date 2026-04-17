<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>
<%--<%=Html.TextBox("", ((Model != null && Model.HasValue && Model.Value.ToShortDateString() != "1/1/0001") ? Model.Value.ToString(ViewData.ModelMetadata.EditFormatString) : ViewData.ModelMetadata.NullDisplayText), new { @class = "datePicker" })%>--%>
<%--<script type="text/javascript">
    $(function () {
        $(".datePicker").datepicker({ buttonImage: "/content/images/calendar.png", showOn: "both" });
    });
</script>--%>
<%= Html.Telerik().DatePicker()
            .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
            .ShowButton(true)
            .Value(Model)
    %>

<%--<%string name = ViewData.TemplateInfo.HtmlFieldPrefix;%>
<%string id = name.Replace(".", "_");%>
<%string imagePath = Url.Content("~/Content/Images/calendar.png"); %>

<%= Html.TextBoxFor(model=> model)%>
    


<script type="text/javascript">
    $(document).ready(function () {

        $("#<%=id%>").datepicker({
            showOn: 'both',
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            buttonImageOnly: true,
            buttonImage: '<%=imagePath%>'
        });
    });
</script>--%>