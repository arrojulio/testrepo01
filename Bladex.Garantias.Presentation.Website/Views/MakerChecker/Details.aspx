<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MakerCheckerDetailViewModel>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<%@ Import Namespace="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    MakerChecker — Operacion</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeaderContent">
    <script type="text/javascript">
        var backUrl = baseUrl + "/MakerChecker/Index";

        $(document).ready(function () {
            SetupPage();
            $("#tabs").tabs();
        });

        function SetupPage() {
            /* Resaltar el textarea al recibir foco */
            $('.commentBoxInput')
                .focus(function () { $(this).css('background-color', '#f0f6ff'); })
                .blur(function ()  { $(this).css('background-color', ''); });

            /* Nivelar el alto de los itemContainer */
            var maxH = 0, maxW = 0;
            $('.itemContainer').each(function () {
                if ($(this).width()  > maxW) maxW = $(this).width();
            });
            $('.itemContainer').width(maxW);
            $('.itemContainer').each(function () {
                if ($(this).height() > maxH) maxH = $(this).height();
            });
            $('.itemContainer').height(maxH);

            var changesetId = '<%: Model.Changeset.ChangesetId %>';
            var operationId = '<%= Model.Operation.OperationId %>';

            /* Toggle del ID del changeset en el enlace */
            $("#changeset-link").click(function (e) {
                $(this).text($(this).text() === "changeset"
                    ? "changeset " + changesetId
                    : "changeset");
                e.preventDefault();
                return false;
            });

            /* Aprobar operacion */
            $("#btnApprove").click(function (e) {
                var comment = $("#Operation_Comment").val();
                $("#btnApprove").prop("disabled", true)
                                .html('<span class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span>Aprobando...');
                $.ajax({
                    type:        "POST",
                    url:         "Approve",
                    data:        '{"operationId": ' + operationId + ', "comment": ' + JSON.stringify(comment) + '}',
                    contentType: "application/json; charset=utf-8",
                    cache:       false,
                    success: function (data) {
                        if (data === "success") {
                            window.location.reload(true);
                        } else {
                            logInBrowser(data);
                            showAlert('danger', 'Ha ocurrido un error al aprobar la operacion. Por favor intente nuevamente.');
                            $("#btnApprove").prop("disabled", false).html(approveBtnHtml);
                        }
                    },
                    error: function (error) {
                        logInBrowser(error);
                        showAlert('danger', 'Ha ocurrido un error al aprobar la operacion. Por favor intente nuevamente.');
                        $("#btnApprove").prop("disabled", false).html(approveBtnHtml);
                    }
                });
                e.preventDefault();
                return false;
            });

            /* Rechazar operacion */
            $("#btnReject").click(function (e) {
                var comment = $("#Operation_Comment").val();
                $("#btnReject").prop("disabled", true)
                               .html('<span class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span>Rechazando...');
                $.ajax({
                    type:        "POST",
                    url:         "Reject",
                    data:        '{"operationId": ' + operationId + ', "comment": ' + JSON.stringify(comment) + '}',
                    contentType: "application/json; charset=utf-8",
                    cache:       false,
                    success: function (data) {
                        if (data === "success") {
                            window.location.reload(true);
                        } else {
                            logInBrowser(data);
                            showAlert('danger', 'Ha ocurrido un error al rechazar la operacion. Por favor intente nuevamente.');
                            $("#btnReject").prop("disabled", false).html(rejectBtnHtml);
                        }
                    },
                    error: function (error) {
                        logInBrowser(error);
                        showAlert('danger', 'Ha ocurrido un error al rechazar la operacion. Por favor intente nuevamente.');
                        $("#btnReject").prop("disabled", false).html(rejectBtnHtml);
                    }
                });
                e.preventDefault();
                return false;
            });
        }

        /* Guarda el HTML original de los botones para restaurarlos si la llamada falla */
        var approveBtnHtml = '';
        var rejectBtnHtml  = '';
        $(document).ready(function () {
            approveBtnHtml = $("#btnApprove").html();
            rejectBtnHtml  = $("#btnReject").html();
        });

        /* Muestra un alert de Bootstrap sobre la tabla */
        function showAlert(type, message) {
            var $a = $('<div class="alert alert-' + type + ' alert-dismissible fade show mt-2" role="alert">' +
                       message +
                       '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Cerrar"></button>' +
                       '</div>');
            $("#blx-alert-area").html($a);
        }
    </script>
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">

<%-- Titulo de pagina --%>
<div class="d-flex align-items-center gap-2 mb-3 flex-wrap">
    <h2 class="mb-0">MakerChecker &mdash; Operaciones</h2>
    <%-- Badge de estado --%>
    <%
    int statusId = Model.Operation.OperationStatusId;
    string badgeClass = "blx-status-badge--pending";
    if (statusId == (int)MakerCheckerOperationStatus.OperationStatus.New)      badgeClass = "blx-status-badge--new";
    if (statusId == (int)MakerCheckerOperationStatus.OperationStatus.Approved) badgeClass = "blx-status-badge--approved";
    if (statusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected) badgeClass = "blx-status-badge--rejected";
    %>
    <span class="blx-status-badge <%=badgeClass %>"><%: Model.Operation.OperationStatus.OperationStatusDescription %></span>
</div>

<p class="blx-operation-meta">
    Operaciones del <a id="changeset-link" href="#">changeset</a>
    enviadas por el usuario <strong><%:Model.Changeset.MakerUserId %></strong>
</p>

<%-- Area de alertas dinamicas --%>
<div id="blx-alert-area"></div>

<%-- Paginacion superior --%>
<%: Html.Partial("~/Views/MakerChecker/_Pager.ascx", Model) %>

<%-- Campos hidden de la operacion --%>
<%:Html.HiddenFor(o => o.Operation.OperationId) %>
<%:Html.HiddenFor(o => o.Operation.OperationStatusId) %>
<%:Html.HiddenFor(o => o.Operation.ItemId) %>

<%-- Over-utilization alert (si aplica) --%>
<%if (Model.Operation.ItemId.HasValue && Model.Operation.ItemId.Value != 0) {%>
    <% Html.RenderAction("CheckOverUtilization", "GarantiaBase", new { garantiaId = Model.Operation.ItemId.Value }); %>
<%}%>

<%-- ================================================================
     CARD: Cabecera de la operacion + botones de accion
     ================================================================ --%>
<div class="blx-card mb-3">
    <div class="blx-card-header">
        <h2 class="blx-card-header-title">Detalle de la Operacion</h2>
    </div>
    <div class="blx-card-body">

        <%-- Metadatos de la operacion --%>
        <div class="blx-operation-meta mb-3">
            <p class="mb-1">Esta operacion fue enviada el <strong><%=Model.Operation.MakerDate %></strong>.</p>
            <%if (Model.Operation.OperationStatusId != (int)MakerCheckerOperationStatus.OperationStatus.New) {%>
                <p class="mb-0">
                    Fue <strong><%=Model.Operation.OperationStatus.OperationStatusDescription.ToLower() %></strong>
                    el <strong><%=Model.Operation.CheckerDate %></strong>
                    por el usuario <strong><%=Model.Operation.CheckerUserId %></strong>.
                </p>
            <%}%>
        </div>

        <%-- Botones de accion (Aprobar / Rechazar / Revisar) --%>
        <div class="blx-action-bar">
            <%if (!Model.ReadOnly) {%>

                <%-- Modo activo: el checker puede aprobar o rechazar --%>
                <button id="btnApprove" type="button" class="btn btn-success btn-sm">
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="currentColor" viewBox="0 0 16 16" class="me-1" aria-hidden="true">
                        <path d="M13.485 1.431a1.473 1.473 0 0 1 2.104 2.062l-7.84 9.801a1.473 1.473 0 0 1-2.12.04L.431 9.477a1.473 1.473 0 0 1 2.084-2.084l4.111 4.112 6.86-8.574z"/>
                    </svg>
                    Aprobar
                </button>
                <button id="btnReject" type="button" class="btn btn-danger btn-sm">
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="currentColor" viewBox="0 0 16 16" class="me-1" aria-hidden="true">
                        <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
                    </svg>
                    Rechazar
                </button>

            <%} else {%>

                <%-- Modo solo lectura: botones deshabilitados --%>
                <%if (Model.Operation.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected
                      && Model.Changeset.MakerUser.Key.ToString() == this.User.Identity.Name
                      && !Model.Changeset.MakerUser.IsChecker) {%>
                    <a href="Review?operationId=<%=Model.Operation.OperationId %>"
                       class="btn btn-warning btn-sm" title="Revisar esta operacion rechazada">
                        <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="currentColor" viewBox="0 0 16 16" class="me-1" aria-hidden="true">
                            <path d="M12.854.146a.5.5 0 0 0-.707 0L10.5 1.793 14.207 5.5l1.647-1.647a.5.5 0 0 0 0-.708zm.646 6.061L9.793 2.5 3.293 9H3.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.207zm-7.468 7.468A.5.5 0 0 1 6 13.5V13h-.5a.5.5 0 0 1-.5-.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.5-.5V10h-.5a.499.499 0 0 1-.175-.032l-.179.178a.5.5 0 0 0-.11.168l-2 5a.5.5 0 0 0 .65.65l5-2a.5.5 0 0 0 .168-.11z"/>
                        </svg>
                        Revisar
                    </a>
                <%}%>

                <button type="button" class="btn btn-success btn-sm" disabled title="No tiene permisos para aprobar">
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="currentColor" viewBox="0 0 16 16" class="me-1" aria-hidden="true">
                        <path d="M13.485 1.431a1.473 1.473 0 0 1 2.104 2.062l-7.84 9.801a1.473 1.473 0 0 1-2.12.04L.431 9.477a1.473 1.473 0 0 1 2.084-2.084l4.111 4.112 6.86-8.574z"/>
                    </svg>
                    Aprobar
                </button>
                <button type="button" class="btn btn-danger btn-sm" disabled title="No tiene permisos para rechazar">
                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="currentColor" viewBox="0 0 16 16" class="me-1" aria-hidden="true">
                        <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
                    </svg>
                    Rechazar
                </button>

            <%}%>

            <%-- Boton regresar siempre visible --%>
            <button type="button" class="btn btn-secondary btn-sm ms-auto"
                    onclick="javascript: document.location = backUrl;">
                Regresar
            </button>
        </div>

    </div>
</div>

<%-- ================================================================
     CARD: Comentarios
     ================================================================ --%>
<div class="blx-card mb-3">
    <div class="blx-card-header">
        <h2 class="blx-card-header-title">Comentarios</h2>
    </div>
    <div class="blx-card-body">
        <%if (!Model.ReadOnly) {%>
            <%:Html.TextAreaFor(o => o.Operation.Comment,
                new { @class = "commentBox commentBoxInput w-100",
                      placeholder = "Ingrese un comentario para esta operacion...",
                      rows = "4" })%>
        <%} else {%>
            <div class="commentBox">
                <%: string.IsNullOrEmpty(Model.Operation.Comment)
                        ? "No hay comentarios para esta operacion."
                        : Model.Operation.Comment %>
            </div>
        <%}%>
    </div>
</div>

<%-- ================================================================
     CARD: Campos (Required / Optional) — tabs jQuery UI
     ================================================================ --%>
<div class="blx-card mb-3">
    <div class="blx-card-header">
        <h2 class="blx-card-header-title">Campos de la Garantia</h2>
    </div>
    <div class="blx-card-body p-0">
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">Campos Requeridos</a></li>
                <li><a href="#tabs-2">Campos Opcionales</a></li>
            </ul>

            <%-- Tab 1: Campos requeridos --%>
            <div id="tabs-1">
                <table class="styled-table property-bag w-100">
                    <thead>
                        <tr>
                            <th style="width:22%">Campo</th>
                            <th style="width:39%">Valor Actual</th>
                            <th style="width:39%">Valor Propuesto</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%if (Model.Proposed.Count != 0) {%>
                            <%foreach (var kv in Model.Proposed) {%>
                                <%if (!Model.Original.ContainsKey(kv.Key) || (Model.Original.ContainsKey(kv.Key) && Model.Original[kv.Key] != kv.Value)) {%>
                                    <%if (kv.GetKeyFormatted().Contains("(*)")) {%>
                                    <tr>
                                        <td><%:kv.GetKeyFormatted()%></td>
                                        <td><span><%=Model.Original.ContainsKey(kv.Key) ? Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() : "(vacio)"%></span></td>
                                        <%if (Model.Original.ContainsKey(kv.Key) && Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() != kv.GetValueFormatted()) {%>
                                        <td class="property-bag-new"><%=kv.GetValueFormatted()%></td>
                                        <%} else {%>
                                        <td><%=kv.GetValueFormatted()%></td>
                                        <%}%>
                                    </tr>
                                    <%}%>
                                <%}%>
                            <%}%>
                        <%} else if (Model.Original.Count != 0) {%>
                            <%foreach (var kv in Model.Original) {%>
                                <%if (!Model.Proposed.ContainsKey(kv.Key) || (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed[kv.Key] != kv.Value)) {%>
                                    <%if (kv.GetKeyFormatted().Contains("(*)")) {%>
                                    <tr>
                                        <td><%:kv.GetKeyFormatted()%></td>
                                        <td><%=Model.Proposed.ContainsKey(kv.Key) ? Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() : "(vacio)"%></td>
                                        <%if (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() == kv.GetValueFormatted()) {%>
                                        <td class="property-bag-new"><%=kv.GetValueFormatted()%></td>
                                        <%} else {%>
                                        <td><%=kv.GetValueFormatted()%></td>
                                        <%}%>
                                    </tr>
                                    <%}%>
                                <%}%>
                            <%}%>
                        <%} else {%>
                            <tr><td colspan="3" class="text-center text-muted py-3">No hay informacion para mostrar.</td></tr>
                        <%}%>
                    </tbody>
                </table>
            </div>

            <%-- Tab 2: Campos opcionales --%>
            <div id="tabs-2">
                <table class="styled-table property-bag w-100">
                    <thead>
                        <tr>
                            <th style="width:22%">Campo</th>
                            <th style="width:39%">Valor Actual</th>
                            <th style="width:39%">Valor Propuesto</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%if (Model.Proposed.Count != 0) {%>
                            <%foreach (var kv in Model.Proposed) {%>
                                <%if (!Model.Original.ContainsKey(kv.Key) || (Model.Original.ContainsKey(kv.Key) && Model.Original[kv.Key] != kv.Value)) {%>
                                    <%if (!kv.GetKeyFormatted().Contains("(*)")) {%>
                                    <tr>
                                        <td><%:kv.GetKeyFormatted()%></td>
                                        <td><span><%=Model.Original.ContainsKey(kv.Key) ? Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() : "(vacio)"%></span></td>
                                        <%if (Model.Original.ContainsKey(kv.Key) && Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() != kv.GetValueFormatted()) {%>
                                        <td class="property-bag-new"><%=kv.GetValueFormatted()%></td>
                                        <%} else {%>
                                        <td><%=kv.GetValueFormatted()%></td>
                                        <%}%>
                                    </tr>
                                    <%}%>
                                <%}%>
                            <%}%>
                        <%} else if (Model.Original.Count != 0) {%>
                            <%foreach (var kv in Model.Original) {%>
                                <%if (!Model.Proposed.ContainsKey(kv.Key) || (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed[kv.Key] != kv.Value)) {%>
                                    <%if (!kv.GetKeyFormatted().Contains("(*)")) {%>
                                    <tr>
                                        <td><%:kv.GetKeyFormatted()%></td>
                                        <td><%=Model.Proposed.ContainsKey(kv.Key) ? Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() : "(vacio)"%></td>
                                        <%if (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() == kv.GetValueFormatted()) {%>
                                        <td class="property-bag-new"><%=kv.GetValueFormatted()%></td>
                                        <%} else {%>
                                        <td><%=kv.GetValueFormatted()%></td>
                                        <%}%>
                                    </tr>
                                    <%}%>
                                <%}%>
                            <%}%>
                        <%} else {%>
                            <tr><td colspan="3" class="text-center text-muted py-3">No hay informacion para mostrar.</td></tr>
                        <%}%>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

<%-- Paginacion inferior --%>
<%: Html.Partial("~/Views/MakerChecker/_Pager.ascx", Model) %>

<%-- Segundo boton regresar (pie de pagina) --%>
<div class="mt-2">
    <button type="button" class="btn btn-secondary btn-sm"
            onclick="javascript: document.location = backUrl;">
        Regresar
    </button>
</div>

</asp:Content>
