<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>
<%@ Import Namespace="System.Text" %>
<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Error
</asp:Content>
<asp:Content ID="errorHeader" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            /* Ocultar el changeset viewer en paginas de error */
            $("#blx-changeset-bar").hide();
            /* Toggle del detalle tecnico */
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
    string errorName    = (Model != null && Model.Exception != null) ? Model.Exception.GetType().Name : "Error desconocido";
    string errorMessage = (Model != null && Model.Exception != null) ? Model.Exception.Message : "Sin informacion disponible.";
    string actionName   = (Model != null) ? Model.ActionName : "undefined";
    string controllerName = (Model != null) ? Model.ControllerName : "undefined";
    string stackTrace   = (Model != null && Model.Exception != null) ? Model.Exception.StackTrace : string.Empty;
    %>

    <div class="blx-card" style="max-width: 760px; margin: var(--blx-gap-xl) auto;">
        <div class="blx-card-header" style="background: #fce4ec; border-bottom-color: #f48fb1;">
            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="#880e4f" viewBox="0 0 16 16" aria-hidden="true" style="flex-shrink:0">
                <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z"/>
            </svg>
            <h2 class="blx-card-header-title" style="color: #880e4f;">Ha ocurrido un error</h2>
        </div>
        <div class="blx-card-body">

            <div class="alert alert-danger" role="alert">
                Se ha producido un error al procesar su solicitud en el controlador
                <strong><%: controllerName %></strong>, accion <strong><%: actionName %></strong>.
            </div>

            <p class="blx-operation-meta">
                <strong>Tipo:</strong> <%: errorName %><br />
                <strong>Mensaje:</strong> <%: errorMessage %>
            </p>

            <%if (!string.IsNullOrEmpty(stackTrace)) {%>
            <p>
                <a id="btnShowDetails" href="#" class="small">Ver detalle tecnico</a>
            </p>
            <div id="collapsePanel" style="display:none;">
                <pre style="font-size:0.72rem; background:#f5f5f5; border:1px solid var(--blx-border); border-radius:var(--blx-radius-sm); padding:var(--blx-gap-sm); overflow:auto; max-height:260px; white-space:pre-wrap;"><%: stackTrace %></pre>
            </div>
            <%}%>

            <p class="text-muted small mt-3">
                Por favor contacte al soporte tecnico indicando la informacion que aparece en esta pagina.
            </p>

            <div class="blx-action-bar">
                <button type="button" class="btn btn-secondary btn-sm" onclick="javascript: goBack();">
                    Volver
                </button>
            </div>
        </div>
    </div>
</asp:Content>
