<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AttachedToLineViewModel>" %>
<div id="attachedToLineContainer">
<%= Html.HiddenFor(m=>m.AttachedToLine) %>
<%= Html.HiddenFor(m=>m.CustomerId) %>
<%if (!string.IsNullOrEmpty(Model.CustomerId))
  {%>
    <script type="text/javascript">
        getLimitInformation("<%:Model.CustomerId %>");
    </script>
<%}%>

<div id="limitContainer" style="display:table-cell">
</div>

   
</div>


