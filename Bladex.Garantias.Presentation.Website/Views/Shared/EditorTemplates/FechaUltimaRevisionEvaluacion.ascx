<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>
<%= Html.Telerik().DatePicker()
            .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
                    .ClientEvents(e => e.OnLoad("calcProximaFechaRevisionEvaluacion").OnChange("calcProximaFechaRevisionEvaluacion"))
            .ShowButton(true)
            .Value(Model)
%>

<script type="text/javascript">
    
</script>