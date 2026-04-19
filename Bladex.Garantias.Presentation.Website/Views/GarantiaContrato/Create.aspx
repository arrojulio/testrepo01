<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.GarantiaContratoViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeaderContent"></asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
<style type="text/css">
    .editor-label
    { width:180px;}
    .editor-field { width:388px; }
</style>
 <%if ((bool)this.ViewData["CREATE_SUCCESSFULL"])
      {%>
        <p style="color:Green;">Asociación efectuada correctamente.</p>
        <fieldset>
        <%if(Model.GarantiaContratoModel.ID==0)  {%>
            <legend>Asociación pendiente de aprobación</legend>
            <%}
        else
        {%>
            <legend><%:Html.LabelFor(m=>m.GarantiaContratoModel.ID) %> <%:Html.DisplayTextFor(m => m.GarantiaContratoModel.ID)%></legend>
        <%}%>        

        <%:Html.DisplayFor(m=>m.GarantiaContratoModel) %>
        </fieldset>
        <%:Html.ActionLink("Volver al listado de Contratos", "Index", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = (bool?)ViewBag.UseRepository })%>
    <%}
      else
      {%>

    <h2>Asociación de contratos a una garantia</h2>
    <div id="deal_references_container">
        <%for (int c = 0; c < Model.DealReferenceList.Count; c++){%>
            <%: Html.HiddenFor(m=>m.DealReferenceList[c].DealReference) %>
            <%: Html.HiddenFor(m=>m.DealReferenceList[c].FechaRegistroInicial) %>
            <%: Html.HiddenFor(m=>m.DealReferenceList[c].FechaVencimientoGarantia) %>
            <%: Html.HiddenFor(m=>m.DealReferenceList[c].FechaVencimientoRiesgo) %>
            <%: Html.HiddenFor(m=>m.DealReferenceList[c].NetBalanceFormatted) %>
            <%: Html.HiddenFor(m=>m.DealReferenceList[c].NetBalance) %>
            <%: Html.HiddenFor(m=>m.DealReferenceList[c].ProductGroup) %>
        <%}%>
    </div>
    <%using (Html.BeginForm()){%>
        <%:Html.ValidationSummary(true)%>
        <%: Html.Hidden("categoriaSuperId",(string)ViewBag.CategoriaSuperId) %>
        <%: Html.Hidden("garantiaId",(int?)ViewBag.GarantiaId) %>
        <%: Html.Hidden("operationId",(int?)ViewBag.OperationId) %>
        <%: Html.Hidden("useRepository",(bool?)ViewBag.UseRepository) %>
        <fieldset>
        <legend>Informacion general de la asociación</legend>
        <table id="tblContracts" class="guaranteeTable" cellpadding="0" cellspacing="0" >
            <tbody>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                           <%if (((int?)ViewBag.OperationId).HasValue) { %> 
                                Nro Operacion
                            <% } else { %>
                                Nro Identificador Garantia
                            <% } %> 
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%if(Model.GarantiaContratoModel.GarantiaId.HasValue) {%>
                                <%:Html.DisplayTextFor(model => model.GarantiaContratoModel.GarantiaId)%>
                            <%} else if (((int?)ViewBag.OperationId).HasValue) {%>
                                <%:((int?)ViewBag.OperationId).Value %>
                            <%} else if  (((int?)ViewBag.GarantiaId).HasValue) {%>
                                <%:((int?)ViewBag.GarantiaId).Value %>
                            <%}%>

                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            Deudor
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.DisplayTextFor(o=>o.CustomerId) %> - <%: Html.DisplayTextFor(o=>o.CustomerName) %>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </fieldset>
    <fieldset>
            <legend>Asociación N° <%: Model.GarantiaContratoModel.ID == 0 ? "(nuevo)" : Model.GarantiaContratoModel.ID.ToString() %></legend>
            <table id="deal_reference_table" class="guaranteeTable" cellpadding="0" cellspacing="0" >
            <tbody>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">Deal Reference</div>
                    </td>
                    <td>
                        <div class="editor-field"><%Html.Telerik().ComboBox().Name("dealreference-list").BindTo(new SelectList(Model.DealReferenceList.Select(o => new SelectListItem() { Value = o.DealReference, Text = string.Format("{0} - {1} - {2}", o.DealReference, o.ProductGroup, o.NetBalanceFormatted) }),"Value", "Text")).ClientEvents(o => o.OnChange("bindDealReferenceInfo").OnDataBound("bindDealReferenceInfo")).Render(); %></div>
                         <%:Html.ActionLink("Refrescar lista de contratos", "Create", "GarantiaContrato", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = false, callRefreshContratos=true }, new { })%>
      
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">Product Group</div>
                    </td>
                    <td>
                        <div class="editor-field"><span id="product_group"></span></div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">Net Balance</div>
                    </td>
                    <td>
                        <div class="editor-field"><span id="net_balance"></span></div>
                    </td>
                </tr>
        </tbody>
        </table>
            <%:Html.EditorFor(m => m.GarantiaContratoModel) %>
        </fieldset>
        

        <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User")){%>
        <input id="btnSubmit" type="submit" value="Save" />
        <%}%>
        <input id="btnCancel" type="button" value="Cancelar" onclick="javascript: goBack();"  />

        <%}%>
    <%}%>

    
    <script type="text/javascript">

        $(document).ready(function () {
            $('#Hidden_CurrentOperation').val($('#operationId').val());
        });

        function bindDealReferenceInfo(e) {
            var combobox = $("#" + e.currentTarget.id).data("tComboBox");
            var label = combobox.text();
            var value = combobox.value();
            var hiddenDealReference = $("#deal_references_container").find("input[value='" + value + "']");
            if (hiddenDealReference) {
                var fields = label.split(" - ");
                if (fields.length == 3) {
                    var product_group = fields[1];
                    var net_balance = fields[2];
                    $("#product_group").text(product_group);
                    $("#net_balance").text(net_balance);
                }

                var hiddenFechaVencimientoRiesgo = $("#" + $(hiddenDealReference).attr('id').replace("_DealReference", "_FechaVencimientoRiesgo")).val();
                var hiddenFechaRegistroInicial = $("#" + $(hiddenDealReference).attr('id').replace("_DealReference", "_FechaRegistroInicial")).val();
                var hiddenFechaVencimientoGarantia = $("#" + $(hiddenDealReference).attr('id').replace("_DealReference", "_FechaVencimientoGarantia")).val();
                var hiddenNetBalancePrincipal = $("#" + $(hiddenDealReference).attr('id').replace("_DealReference", "_NetBalance")).val();
                $("input#GarantiaContratoModel_DealReference").val($(hiddenDealReference).val());
                $("input#GarantiaContratoModel_FechaVencimientoRiesgo").val(hiddenFechaVencimientoRiesgo);
                $("input#GarantiaContratoModel_FechaRegistroInicial").val(hiddenFechaRegistroInicial);
                $("input#GarantiaContratoModel_FechaVencimientoGarantia").val(hiddenFechaVencimientoGarantia);
                $("input#GarantiaContratoModel_NetBalancePrincipal").val(hiddenNetBalancePrincipal);
                $("#btnSubmit").attr('disabled', false);
            } else {
                alert('There is a problem retrieving the necessary information for the deal reference. Please contact support');
                $("#btnSubmit").attr('disabled', true);
            }
            
            
        }
    </script>
</asp:Content>
