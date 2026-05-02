<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MakerCheckerChangesetViewerViewModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Controllers" %>
<script type="text/javascript">
    $(document).ready(function () {

        var changesetId = '<%=Model.Changeset.ChangesetId %>';

        /* Enviar el changeset al Checker */
        $("#submit_changeset").click(function (e) {
            var confirmation = confirm('\xBFDesea enviar el conjunto de operaciones al Checker para su revisi\xF3n?');
            if (confirmation) {
                var button = $(this);
                button.prop('disabled', true)
                      .html('<span class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span>Enviando...');

                var changesetComment = $("#Changeset_ChangesetComment").val();

                $.ajax({
                    type:        "POST",
                    url:         "/MakerChecker/Commit",
                    data:        JSON.stringify({ changesetId: changesetId, changesetComment: changesetComment }),
                    contentType: "application/json; charset=utf-8",
                    cache:       false,
                    success: function (data) {
                        window.location.href = data.redirectToUrl;
                    },
                    error: function () {
                        button.prop('disabled', false)
                              .html('Enviar al Checker');
                        alert('Error al intentar enviar el changeset.\nPor favor intente nuevamente o contacte al administrador.');
                    }
                });
            }
            e.preventDefault();
            return false;
        });

        /* Cancelar una operacion individual */
        $(".maker-checker-cancel-operation").click(function (e) {
            var confirmation = confirm('\xBFDesea eliminar la operaci\xF3n?\nLa misma no ser\xE1 enviada al Checker y tampoco podr\xE1 restaurarse.');
            if (confirmation) {
                var button = $(this);
                var operationId = parseInt(button.attr('id').replace('btnReject_Operation_', ''));
                var currentOperation = $('#Hidden_CurrentOperation').val();

                if (operationId === 0) return false;

                button.hide();
                $("#btnRejected_Operation_" + operationId).show();

                $.ajax({
                    type:        "POST",
                    url:         baseUrl + "/MakerChecker/CancelOperation",
                    data:        JSON.stringify({ operationId: operationId, currentOperation: currentOperation }),
                    contentType: "application/json; charset=utf-8",
                    cache:       false,
                    success: function (data) {
                        if (data === "success") {
                            button.closest('tr').fadeOut(200, function () { $(this).remove(); });
                        } else if (data === "reload") {
                            window.location.href = "Garantia/Index";
                        } else {
                            button.show();
                            $("#btnRejected_Operation_" + operationId).hide();
                            alert('Error al cancelar la operacion:\n' + data);
                        }
                    },
                    error: function () {
                        button.show();
                        $("#btnRejected_Operation_" + operationId).hide();
                        alert('Error al cancelar la operacion. Por favor intente nuevamente.');
                    }
                });
            }
            e.preventDefault();
            return false;
        });

        /* Toggle del panel de detalles del changeset */
        $("#changeset-viewer-action").click(function (e) {
            $("#changeset_viewer_container").slideToggle(180);
            e.preventDefault();
            return false;
        });
    });
</script>

<%if (Model.Operations.Count > 0) { %>
<div class="changeset-viewer">
    <%-- Barra de accion visible --%>
    <div class="changeset-viewer-action-container">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#856404" viewBox="0 0 16 16" aria-hidden="true" style="flex-shrink:0">
            <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"/>
        </svg>
        <span>
            Tiene <strong><%=Model.Operations.Count %></strong> operacion<%=Model.Operations.Count != 1 ? "es" : "" %> pendiente<%=Model.Operations.Count != 1 ? "s" : "" %> de revision.
            <a href="#" id="changeset-viewer-action" class="ms-2">Revisar</a>
        </span>
    </div>

    <%-- Contenedor del detalle (oculto por defecto) --%>
    <div id="changeset_viewer_container" class="changeset-viewer-container" style="display:none; padding: var(--blx-gap-sm) var(--blx-gap);">

        <%-- Hidden fields --%>
        <%= Html.HiddenFor(m => m.Changeset.ChangesetId) %>
        <%= Html.Hidden("Hidden_CurrentOperation") %>

        <%-- Tabla de operaciones pendientes --%>
        <div style="overflow-x: auto;">
        <table class="styled-table" style="margin-bottom: var(--blx-gap-sm);">
            <thead>
                <tr>
                    <th colspan="<%=Model.GetColumnsToDisplay().Count + 3 %>">
                        Operaciones pendientes del changeset <strong><%=Model.Changeset.ChangesetId %></strong>
                    </th>
                </tr>
                <tr>
                    <th>Accion</th>
                    <th>Fecha</th>
                    <th>Tipo</th>
                    <%foreach (var column in Model.GetColumnsToDisplay()) {%>
                        <th><%=column%></th>
                    <%}%>
                </tr>
            </thead>
            <tbody>
                <%
                string source = string.Empty;
                foreach (var row in Model.Operations) {
                    switch (row.Model.CategoriaSuper.Key) {
                        case "01": source = Url.Content("~/") + "GarantiaMueble";            break;
                        case "02": source = Url.Content("~/") + "GarantiaInmueble";          break;
                        case "04": source = Url.Content("~/") + "GarantiaDepositoOtroBanco"; break;
                        case "05": source = Url.Content("~/") + "GarantiaPrenda";            break;
                        case "06": source = Url.Content("~/") + "GarantiaOtra";              break;
                    }
                %>
                <tr>
                    <td style="white-space:nowrap;">
                        <%-- Boton cancelar (activo) --%>
                        <button id="btnReject_Operation_<%=row.Operation.OperationId %>"
                                class="maker-checker-cancel-operation btn btn-danger btn-sm"
                                title="Cancelar esta operacion"
                                style="padding:2px 7px; font-size:0.72rem;">
                            <svg xmlns="http://www.w3.org/2000/svg" width="10" height="10" fill="currentColor" viewBox="0 0 16 16" aria-hidden="true">
                                <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
                            </svg>
                        </button>
                        <%-- Boton cancelar (deshabilitado — se muestra mientras procesa) --%>
                        <button id="btnRejected_Operation_<%=row.Operation.OperationId %>"
                                class="btn btn-secondary btn-sm"
                                disabled style="display:none; padding:2px 7px; font-size:0.72rem;">
                            <span class="spinner-border spinner-border-sm" style="width:10px;height:10px;" role="status" aria-hidden="true"></span>
                        </button>
                        <%-- Enlace editar --%>
                        <a href="<%=string.Format("{0}/Edit?operationId={1}&garantiaId={2}&categoriaSuperId={3}&useRepository={4}&isReadOnly={5}",
                                    source, row.Operation.OperationId, string.Empty, row.Model.CategoriaSuper.Key, false, false) %>"
                           class="btn btn-secondary btn-sm ms-1"
                           title="Editar esta operacion"
                           style="padding:2px 7px; font-size:0.72rem;">
                            <svg xmlns="http://www.w3.org/2000/svg" width="10" height="10" fill="currentColor" viewBox="0 0 16 16" aria-hidden="true">
                                <path d="M12.854.146a.5.5 0 0 0-.707 0L10.5 1.793 14.207 5.5l1.647-1.647a.5.5 0 0 0 0-.708zm.646 6.061L9.793 2.5 3.293 9H3.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.207zm-7.468 7.468A.5.5 0 0 1 6 13.5V13h-.5a.5.5 0 0 1-.5-.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.5-.5V10h-.5a.499.499 0 0 1-.175-.032l-.179.178a.5.5 0 0 0-.11.168l-2 5a.5.5 0 0 0 .65.65l5-2a.5.5 0 0 0 .168-.11z"/>
                            </svg>
                        </a>
                    </td>
                    <td style="white-space:nowrap; font-size:0.78rem; color:var(--blx-text-muted);">
                        <%=row.Operation.MakerDate %>
                    </td>
                    <td style="font-size:0.78rem;">
                        <%=row.Model.GetDisplayName() %>
                    </td>
                    <%foreach (var column in Model.GetColumnsToDisplay()) {%>
                        <td style="font-size:0.78rem;"><%=row.Proposed.ContainsKey(column) ? row.Proposed[column] : string.Empty%></td>
                    <%}%>
                </tr>
                <%}%>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="<%=Model.GetColumnsToDisplay().Count + 3 %>">
                        <label for="Changeset_ChangesetComment" class="form-label small fw-semibold mb-1">
                            Comentarios para el Checker:
                        </label>
                        <%=Html.TextAreaFor(m => m.Changeset.ChangesetComment,
                            new { placeholder = "Agregar comentarios para el Checker...",
                                  @class = "changeset-comment form-control",
                                  rows = "3" })%>
                    </td>
                </tr>
                <tr>
                    <td colspan="<%=Model.GetColumnsToDisplay().Count + 3 %>" class="pt-2">
                        <div class="d-flex align-items-center gap-2 flex-wrap">
                            <button id="submit_changeset" type="button" class="btn btn-primary btn-sm">
                                <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" fill="currentColor" viewBox="0 0 16 16" class="me-1" aria-hidden="true">
                                    <path d="M15.854.146a.5.5 0 0 1 .11.54l-5.819 14.547a.75.75 0 0 1-1.329.124l-3.178-4.995L.643 7.184a.75.75 0 0 1 .124-1.33L15.314.037a.5.5 0 0 1 .54.11zM6.636 10.07l2.761 4.338L14.13 2.576 6.636 10.07zm6.787-8.201L1.591 6.602l4.339 2.76 7.494-7.493z"/>
                                </svg>
                                Enviar al Checker
                            </button>
                            <span class="text-muted small">
                                <%=Model.Operations.Count %> operacion<%=Model.Operations.Count != 1 ? "es" : "" %> pendiente<%=Model.Operations.Count != 1 ? "s" : "" %>
                            </span>
                        </div>
                    </td>
                </tr>
            </tfoot>
        </table>
        </div>
    </div>
</div>
<%}%>
