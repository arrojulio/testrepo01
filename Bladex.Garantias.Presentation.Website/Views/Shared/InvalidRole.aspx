<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>
<%@ Import Namespace="System.Text" %>
<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Acceso denegado
</asp:Content>
<asp:Content ID="errorHeader" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#blx-changeset-bar").hide();
            $("#btnShowDetails").click(function (e) {
                $("#collapsePanel").slideToggle(200);
                e.preventDefault();
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <%
    string errorName    = (Model != null && Model.Exception != null) ? Model.Exception.GetType().Name : "Error de acceso";
    string errorMessage = (Model != null && Model.Exception != null) ? Model.Exception.Message : "Sin informacion disponible.";
    string actionName   = (Model != null) ? Model.ActionName : "undefined";
    string controllerName = (Model != null) ? Model.ControllerName : "undefined";
    string stackTrace   = (Model != null && Model.Exception != null) ? Model.Exception.StackTrace : string.Empty;
    %>

    <div class="blx-card" style="max-width: 600px; margin: var(--blx-gap-xl) auto;">
        <div class="blx-card-header" style="background: #fff3cd; border-bottom-color: #ffc107;">
            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="#856404" viewBox="0 0 16 16" aria-hidden="true" style="flex-shrink:0">
                <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z"/>
            </svg>
            <h2 class="blx-card-header-title" style="color: #856404;">Acceso denegado</h2>
        </div>
        <div class="blx-card-body">

            <div class="alert alert-warning" role="alert">
                Usted no posee los privilegios necesarios para acceder a esta seccion.
            </div>

            <p class="blx-operation-meta">
                Si considera que esto es un error, contacte al administrador del sistema.
            </p>

            <%if (!string.IsNullOrEmpty(errorMessage)) {%>
            <p>
                <a id="btnShowDetails" href="#" class="small">Ver detalle tecnico</a>
            </p>
            <div id="collapsePanel" style="display:none;">
                <p class="small"><strong>Tipo:</strong> <%: errorName %></p>
                <p class="small"><strong>Mensaje:</strong> <%: errorMessage %></p>
                <p class="small"><strong>Controlador:</strong> <%: controllerName %> / <strong>Accion:</strong> <%: actionName %></p>
                <%if (!string.IsNullOrEmpty(stackTrace)) {%>
                <pre style="font-size:0.72rem; background:#f5f5f5; border:1px solid var(--blx-border); border-radius:var(--blx-radius-sm); padding:var(--blx-gap-sm); overflow:auto; max-height:200px; white-space:pre-wrap;"><%: stackTrace %></pre>
                <%}%>
            </div>
            <%}%>
        </div>
    </div>
</asp:Content>
