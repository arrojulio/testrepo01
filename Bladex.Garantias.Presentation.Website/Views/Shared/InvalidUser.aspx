<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Invalid User
</asp:Content>
<asp:Content ID="errorHeader" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#changeset-viewer-action-container").hide();
            $("#changeset_viewer_container").hide();
            $("#btnShowDetails").click(function (e) {
                var options = {};
                $(".collapsePanel").toggle('slide', options, 500);
                e.preventDefault();
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Acceso denegado. Usted no posee los privilegios necesarios para acceder al sistema.
    </h2>
    <% StringBuilder messageBuilder = new StringBuilder(); %>
    <% string errorName = (Model != null && Model.Exception != null) ? Model.Exception.GetType().Name : "Unhandled error";  %>
    <% string errorMessage = (Model != null && Model.Exception != null) ? Model.Exception.Message : "No information available.";  %>
    <% string actionName = (Model != null) ? Model.ActionName : "undefined"; %>
    <% string controllerName = (Model != null) ? Model.ControllerName : "undefined"; %>
    
    <% messageBuilder.Append("<br/><a id='btnShowDetails' href='#'>Detailed information:</a><br/>"); %>
    <% messageBuilder.AppendFormat("<div id='collapsePanel' class='collapsePanel' style='display:none;'><p>Message: {0}.</p>", errorMessage); %>
	<% messageBuilder.AppendFormat("<p>An error of type {0} has occurred into the {1} controller while performing the {2} action.</p>", errorName, controllerName, actionName); %>
    <%if (Model != null && Model.Exception != null)
      {%>
    <%
          string exceptionStackTrace = Model.Exception.StackTrace;%>
            <% messageBuilder.AppendFormat("<p>Stack Trace: {0}<p/>", exceptionStackTrace ); %>       
    <%
      }%>
      <% messageBuilder.Append("</div>"); %>
      <div id="errorContainer">
        <p>
            <%= messageBuilder.ToString() %>           
        </p>
      </div>
      <h3>
        Please, contact support using the information displayed on this page.
      </h3>
      
</asp:Content>
