<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.GarantiaContratoViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">Garantia Contrato</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeaderContent"></asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">

<%:Html.Hidden("garantiaId", (int?)ViewBag.GarantiaId) %>
<%:Html.Hidden("operationId", (int?)ViewBag.OperationId) %>

<%-- ================================================================
     ESTADO: guardado con exito
     ================================================================ --%>
<%if ((bool)this.ViewData["EDIT_SUCCESSFULL"])
  {%>

    <div class="alert alert-success d-flex align-items-center gap-2" role="alert">
        <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" viewBox="0 0 16 16" aria-hidden="true">
            <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>
        </svg>
        Asociacion actualizada correctamente.
    </div>

    <div class="blx-card">
        <div class="blx-card-header">
            <h2 class="blx-card-header-title">
                <%if(Model.GarantiaContratoModel.ID == 0) {%>
                    Asociacion pendiente de aprobacion
                <%} else {%>
                    Asociacion N&deg; <%:Html.DisplayTextFor(m => m.GarantiaContratoModel.ID)%>
                <%}%>
            </h2>
        </div>
        <div class="blx-card-body">
            <%:Html.DisplayFor(m => m.GarantiaContratoModel) %>
        </div>
    </div>

    <div class="blx-action-bar">
        <%:Html.ActionLink("Volver al listado de Contratos", "Index",
            new { garantiaId     = (int?)ViewBag.GarantiaId,
                  operationId    = (int?)ViewBag.OperationId,
                  categoriaSuperId = (string)ViewBag.CategoriaSuperId,
                  useRepository  = (bool?)ViewBag.UseRepository },
            new { @class = "btn btn-secondary btn-sm" })%>
    </div>

<%}
  else
  {%>

<%-- ================================================================
     FORMULARIO DE EDICION
     ================================================================ --%>

    <div class="d-flex align-items-center gap-2 mb-3">
        <h2 class="mb-0">Edicion de Asociacion <span class="text-muted fw-normal"><%: ViewBag.LabelGarantiaContrato %></span></h2>
    </div>

    <% using (Html.BeginForm()) { %>
        <%:Html.ValidationSummary(true, "", new { @class = "alert alert-danger" })%>

        <%-- Hidden fields de contexto --%>
        <%:Html.Hidden("categoriaSuperId", (string)ViewBag.CategoriaSuperId) %>
        <%:Html.Hidden("garantiaId",       (int?)ViewBag.GarantiaId) %>
        <%:Html.Hidden("operationId",      (int?)ViewBag.OperationId) %>
        <%:Html.Hidden("useRepository",    (bool?)ViewBag.UseRepository) %>

        <div class="blx-card">
            <div class="blx-card-header">
                <h2 class="blx-card-header-title">
                    <%if(Model.GarantiaContratoModel.ID == 0) {%>
                        Asociacion pendiente de aprobacion
                    <%} else {%>
                        Asociacion N&deg; <%:Html.DisplayTextFor(m => m.GarantiaContratoModel.ID)%>
                    <%}%>
                </h2>
            </div>
            <div class="blx-card-body">
                <%:Html.EditorFor(m => m.GarantiaContratoModel) %>
            </div>
        </div>

        <div class="blx-action-bar">
            <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User")) {%>
            <button id="btnSave" type="submit" class="btn btn-primary btn-sm">
                <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="currentColor" viewBox="0 0 16 16" class="me-1" aria-hidden="true">
                    <path d="M2 1a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4.5L11.5 1H2zM4 7h8v5H4V7zm4 4a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z"/>
                </svg>
                Guardar
            </button>
            <%}%>
            <button id="btnCancel" type="button" class="btn btn-secondary btn-sm" onclick="javascript: goBack();">
                Cancelar
            </button>
        </div>

    <% } %>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#Hidden_CurrentOperation').val($('#operationId').val());

            $("#btnSave").click(function () {
                $("#changeset_viewer_container").css('display', '');
            });
        });
    </script>

<%}%>

</asp:Content>
