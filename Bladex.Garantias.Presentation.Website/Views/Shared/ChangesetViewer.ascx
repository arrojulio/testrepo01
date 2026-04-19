<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MakerCheckerChangesetViewerViewModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Controllers" %>
<script type="text/javascript">
    $(document).ready(function () {

        var changesetId = '<%=Model.Changeset.ChangesetId %>';

        //****** IMPORTANTE *******//////////////////
        //Se incorporan los cambios realizados por Michael Sanchez al repositorio de garantias
        $("#submit_changeset").click(function (e) {

            var confirmation = confirm('¿Desea enviar el conjunto de operaciones al Checker para su revision?');
            if (confirmation) {
                var button = $(this);
                $(button).attr('disabled', true);
                var changesetComment = $("#Changeset_ChangesetComment").val();
                //debugger
                var operationId = [] //BLX
                $('[id^="btnReject_Operation_"]').each(function (index, value) {

                    operationId[index] = parseInt($(this).attr('id').replace('btnReject_Operation_', ''));
                    //do something based on isFound...
                    //alert("operationId: "+operationId[index] + ": " +baseUrl)
                });

                $.ajax({
                    type: "POST",
                    //url: baseUrl + "/MakerChecker/Commit",
                    url: "/MakerChecker/Commit",
                    data: "{'changesetId': " + JSON.stringify(changesetId) + ", 'changesetComment': " + JSON.stringify(changesetComment) + "}",
                    format: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        window.location.href = data.redirectToUrl;
                    },
                    error: function (error) {
                        $(button).attr('disabled', false);
                        alert('Error trying to commit the changeset.\nPlease try again in a few seconds or contact system administrator.');
                    }
                });


                /*
                $.ajax({
                    type: "POST",
                    url: baseUrl + "/MakerChecker/Commit",
                    data: "{'changesetId': " + JSON.stringify(changesetId) + ", 'changesetComment': " + JSON.stringify(changesetComment) + "}",
                    format: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {

                        
                        $.each(operationId, function (key, value) {                        
                            var comment = "Aprobado Autománticamente."//$("#Operation_Comment").val();
                            $.ajax({
                                type: "POST",
                                url: baseUrl + "/MakerChecker/Approve",
                                data: "{'operationId': " + value + ", 'comment': " + JSON.stringify(comment) + "}",
                                format: "json",
                                cache: false,
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {                                    
                                    if (data == "success") {                                        
                                        if (key == operationId.length - 1) window.location.reload(true);
                                    } else {
                                        logInBrowser(data);
                                        alert('Ha ocurrido un error tratando de aprobar la operacion.\nLe pedimos que vuelva a intentar nuevamente. OperationId: ' + value);
                                    }
                                },
                                error: function (error) {
                                    logInBrowser(error);
                                    alert('Ha ocurrido un error tratando de aprobar la operacion.\nLe pedimos que vuelva a intentar nuevamente. OperationId: ' + value);
                                }
                            });

                        });
                        window.location.href = baseUrl;

                    },
                    error: function (error) {
                        $(button).attr('disabled', false);
                        alert('Error trying to commit the changeset.\nPlease try again in a few seconds or contact system administrator. OperationId: ' + value);
                    }
                });
                 */
            }
            e.preventDefault();
            return false;
        });

        $(".maker-checker-cancel-operation").click(function (e) {
            var confirmation = confirm('¿Desea eliminar la operacion?\nLa misma no será enviada al Checker y tampoco podrá restaurarse.');
            if (confirmation) {
                var button = $(this);
                // get the operation id
                var operationId = parseInt($(this).attr('id').replace('btnReject_Operation_', ''));

                var currentOperation = $('#Hidden_CurrentOperation').val();
                if (operationId == 0) return false;
                // hide the button
                $(button).hide();
                // show the disabled button
                $("#btnRejected_Operation_" + operationId).show();
                $.ajax({
                    type: "POST",
                    url: baseUrl + "/MakerChecker/CancelOperation",
                    //url: "/MakerChecker/CancelOperation",
                    data: "{'operationId': " + JSON.stringify(operationId) + ",'currentOperation':" + JSON.stringify(currentOperation) + "}",
                    format: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data == "success") {
                            // remove the row of the operation
                            $(button).parent().parent().remove();
                        }
                        else {
                            if (data == "reload") {
                                //alert("cancel reload");
                                window.location.href = "Garantia/Index";
                            }
                            else {
                                // show the enabled button
                                $(button).show();
                                // hide the disabled button
                                $("#btnRejected_Operation_" + operationId).hide();
                                alert('An error has ocurred trying to cancel the operation.\nDetails:\n' + data);
                            }
                        }
                    },
                    error: function (error) {
                        // show the enabled button
                        $(button).show();
                        // hide the disabled button
                        $("#btnRejected_Operation_" + operationId).hide();
                        alert('An error has ocurred trying to cancel the operation.\nPlease try again in a few seconds or contact system administrator.');
                    }
                });
            }
            e.preventDefault();
            return false;
        });

        $("#changeset-viewer-action").click(function (e) {
            $("#changeset_viewer_container").toggle();
            e.preventDefault();
            return false;
        });
    });
</script>
<%if (Model.Operations.Count > 0){ %>
    <div class="changeset-viewer-action-container">
    You have pending operations to review. <a href="#" id="changeset-viewer-action">Click here to review them.</a>
    </div>
<%} %>
<!-- changeset viewer -->
<div class="changeset-viewer">
<div id="changeset_viewer_container" class="changeset-viewer-container" style="display:none;">
<!-- the changeset identifier -->
<%= Html.HiddenFor(m=>m.Changeset.ChangesetId) %>
<%= Html.Hidden("Hidden_CurrentOperation")%>

<table border="0" class="styled-table">
    <thead>
        <tr>
            <%=string.Format("<th colspan='{0}'>Pending operations for changeset {1}</th>", Model.GetColumnsToDisplay().Count + 3, Model.Changeset.ChangesetId) %>
        </tr>
        <tr>
            <th>Action</th>
            <th>Creation Date</th>
            <th>Type</th>
            <%foreach (var column in Model.GetColumnsToDisplay()){%>
                <th><%= column%></th>
            <%}%>
        </tr>
    </thead>
    <tbody>
        
        <%
            string source = string.Empty;
                        
            foreach (var row in Model.Operations){

                switch (row.Model.CategoriaSuper.Key) 
                {
                    case "01":
                        source = Url.Content("~/")+ "GarantiaMueble";
                        break;

                    case "02":
                        source = Url.Content("~/") + "GarantiaInmueble";
                        break;

                    case "04":
                        source = Url.Content("~/") + "GarantiaDepositoOtroBanco";
                        break;

                    case "05":
                        source = Url.Content("~/") + "GarantiaPrenda";
                        break;
                        
                    case "06":
                        source = Url.Content("~/") + "GarantiaOtra";
                        break;                        
                }
        %>
        <tr>
            <td style="text-align:center;">
                <%= string.Format("<img id='btnReject_Operation_{0}' class='maker-checker-cancel-operation' style='cursor:pointer; outline-style: none; width: 15px; height: 15px; vertical-align: middle;' alt='Cancel' title='Cancel this operation' src='{1}'>", row.Operation.OperationId, Url.Content("~/Content/Images/buttons-30x30/02-no.png"))%>
                <%= string.Format("<img id='btnRejected_Operation_{0}' class='maker-checker-cancel-operation' style='display:none; outline-style: none; width: 15px; height: 15px; vertical-align: middle;' alt='Cancel' title='Cancel this operation' src='{1}'>", row.Operation.OperationId, Url.Content("~/Content/Images/buttons-30x30/02-no2.png"))%>                 
                 <%= string.Format("<a href='{0}/Edit?operationId={1}&garantiaId={2}&categoriaSuperId={3}&useRepository={4}&isReadOnly={5}' title='Edit'>", source, row.Operation.OperationId,null, row.Model.CategoriaSuper.Key, false,false)%>
                <%--<%= string.Format("<img id='btnEdit_Operation_{0}' class='maker-checker-edit-operation' style='cursor:pointer; outline-style: none; width: 15px; height: 15px; vertical-align: middle;' alt='Edit' title='Edit this operation' src='/Content/Images/buttons-30x30/edit.png'>", row.Operation.OperationId)%>--%>
                 <%= string.Format("<img style='cursor:pointer; outline-style: none; width: 15px; height: 15px; vertical-align: middle; border-style: none;' alt='Edit' title='Edit this operation' src='{0}'>", Url.Content("~/Content/Images/buttons-30x30/edit.png"))%>
                 
            </td>
            <td>
                <%=row.Operation.MakerDate %>            
            </td>
            <td>
                <%=row.Model.GetDisplayName() %>
            </td>
            <%foreach (var column in Model.GetColumnsToDisplay()){%>
                <%if (row.Proposed.ContainsKey(column)) {%>
                <td><%=row.Proposed[column]%></td>
                <% } else {%>
                    <td></td>
                <%}%>
            <%}%>
        </tr>
        <%}%>
    </tbody>
    <tfoot>
        <tr>
            <%=string.Format("<td colspan='{0}'>", Model.GetColumnsToDisplay().Count + 3) %>
            <p>Add your notes to the checker:</p>
            <%=Html.TextAreaFor(m => m.Changeset.ChangesetComment, new { placeholder="Add your notes to the checker here", @class="changeset-comment" })%>
            
            <%= "</td>" %>
        </tr>
        <tr>
            <%=string.Format("<td colspan='{0}'>", Model.GetColumnsToDisplay().Count + 3) %>
            
            <input id="submit_changeset" name="submit-changeset" type="button" value="Commit Changeset" />
            
            <%= "</td>" %>
        </tr>
        <tr>
            <%=string.Format("<td colspan='{0}'>Showing {1} operations pending to commit.</td>", Model.GetColumnsToDisplay().Count + 3, Model.Operations.Count) %>
        </tr>
    </tfoot>
</table>
</div>
</div>

