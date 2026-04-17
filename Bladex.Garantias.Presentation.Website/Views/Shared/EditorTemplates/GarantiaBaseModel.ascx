<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<script type="text/javascript">
    setupGarantiaBaseModel(parseInt('<%=Model.Key %>'));
</script>

<%if (Model.Key.HasValue && Model.Key.Value != 0) { %>
    <% Html.RenderAction("CheckOverUtilization", "GarantiaBase", new { garantiaId = Model.Key.Value }); %>
    <% Html.RenderAction("VerifyGuaranteeBlock", "GarantiaBase", new { garantiaId = Model.Key.Value }); %>
    <%:Html.HiddenFor(m => m.Key)%>
<%} %>

<%: Html.HiddenFor(m => m.CategoriaSuper.Key)%>
<%: Html.HiddenFor(m => m.CategoriaSuper.Nombre)%>
<%: Html.HiddenFor(m => m.selectedOperationId) %>
<%: Html.HiddenFor(m => m.CategoriaSuper.IsReadOnly, new { Id = "IsReadOnly", name = "IsReadOnly" })%>



<table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0">
    <tbody>
        <% if (!string.IsNullOrEmpty(Model.BusinessError))
           { %>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m=>m.BusinessError) %>
                </div>
            </td>
            <td>
            <div class="editor-field">
                <div class="field-validation-error">
                    <%:Html.DisplayFor(m=>m.BusinessError) %>
                    <%: Html.ValidationMessageFor(m=>m.BusinessError) %>
                </div>
                </div>
            </td>
        </tr>
        <%} %>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.Key)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <label>
                        <%: Html.Encode(Model.Key.HasValue ? Model.Key.ToString() : "(not generated)")%></label>
                    <%: Html.HiddenFor(model => model.Key) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%--<%: Html.LabelFor(model => model.ID_Atomo) %>--%>
                    <%: Html.LabelTooltipFor( m => m.ID_Atomo) %>
                </div>
                
            </td>
            <td>
                <div class="editor-field">
                    <label>
                        <%: Html.Encode(Model.ID_Atomo == 0 ? "(empty)" : Model.ID_Atomo.ToString())%></label>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FCCReference) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <label>
                        <%: Html.Encode(!string.IsNullOrEmpty(Model.FCCReference) ? Model.FCCReference: "(empty)")%></label>
                    <%: Html.HiddenFor(model => model.FCCReference)%>
                </div>
            </td>
        </tr>
         <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.IdentificacionDocumentoGarantia)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.DisplayFor(m => m.IdentificacionDocumentoGarantia)%>
                    <%: Html.HiddenFor(m => m.IdentificacionDocumentoGarantia)%>
                    <%: Html.ValidationMessageFor(model => model.IdentificacionDocumentoGarantia)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.NroIncidenteWorkflow) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.NroIncidenteWorkflow)%>
                    <%: Html.ValidationMessageFor(m => m.NroIncidenteWorkflow)%>
                </div>
            </td>
        </tr>
        
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.CodigoBanco) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%if (string.IsNullOrEmpty(Model.CodigoBanco)) { Model.CodigoBanco = "027"; }%>
                    <%: Html.DisplayTextFor(m=>m.CodigoBanco) %>
                    <%: Html.HiddenFor(model => model.CodigoBanco)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%= Html.LabelTooltipFor(m => m.Cliente)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%= Html.EditorFor(m => m.Cliente)%>
                    <%= Html.ValidationMessageFor(model => model.Cliente.Key) %>
                    <label id="lblValidation_Garantia_Cliente_Key" class="field-validation-error"></label>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.AttachedToLine) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <table border="0" cellpadding="0" cellspacing="0" class="tblAttachedToLine">

                        <tr class="attachedToLineRow">
                            <td style="width:50px" valign="middle"><%: Html.EditorFor(m=>m.AttachedToLine) %></td>
                            <td valign="middle">
                                <%Html.RenderPartial("EditorTemplates/AttachedToLineViewModel", new AttachedToLineViewModel() { AttachedToLine = Model.AttachedToLine, CustomerId = Model.Cliente.Key, CustomerControlId = "empty" }); %>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.Garante)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.Garante)%>
                    <%: Html.ValidationMessageFor(model => model.Garante.Key) %>
                    <label id="lblValidation_Garantia_Garante_Key" class="field-validation-error"></label>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%= Html.LabelTooltipFor(m => m.Beneficiario)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%= Html.EditorFor(m => m.Beneficiario)%>
                    <%= Html.ValidationMessageFor(model => model.Beneficiario) %>
                </div>
            </td>
        </tr>
<%--        <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <label>Contratos de la garantía </label>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%
              if (Model.Key.HasValue)
              {%>
                  <div><i>Presione en el link para visualizar los contratos asociados a esta garantía.</i></div>
                <div><%=Html.ActionLink("Ver contratos", "Index", "GarantiaContrato", new { garantiaId = Model.Key.Value, categoriaSuperId = Model.CategoriaSuper.Key, readOnly = false}, new {title = "Ver Contratos asociados"})%>
                </div>
                <%
              }
              else
              {%>
                 <div><i>Esta garantía no proviene del Atomo, por lo que aún no posee contratos asociados.</i></div>
                <%
              }%>
            </div>
        </td>
    </tr>--%>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.IdentificacionFideicomiso) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.IdentificacionFideicomiso)%>
                    <%: Html.ValidationMessageFor(model => model.IdentificacionFideicomiso) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.FiduciariaSuper)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.FiduciariaSuper)%>
                    <%: Html.ValidationMessageFor(model => model.FiduciariaSuper.Key)%>
                    <label id="lblValidation_Garantia_FiduciariaSuper_Key" class="field-validation-error"></label>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FiduciariaBladex) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FiduciariaBladex)%>
                    <%: Html.ValidationMessageFor(model => model.FiduciariaBladex) %>                    
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.Depositante)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.Depositante)%>
                    <%: Html.ValidationMessageFor(model => model.Depositante.Key)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.Evaluador)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.Evaluador)%>
                    <%: Html.ValidationMessageFor(model => model.Evaluador.Key)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.Administrador) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.Administrador)%>
                    <%: Html.ValidationMessageFor(model => model.Administrador)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.Asegurador) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.Asegurador)%>
                    <%: Html.ValidationMessageFor(model => model.Asegurador)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.Revisor)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.Revisor)%>
                    <%: Html.ValidationMessageFor(model => model.Revisor.Key)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.OrigenGarantia) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.OrigenGarantia)%>
                    <%: Html.ValidationMessageFor(model => model.OrigenGarantia) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.PaisGarantia)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.PaisGarantia)%>
                    <%: Html.ValidationMessageFor(model => model.PaisGarantia.Key)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelTooltipFor(model=>model.IndAtomo) %>
                </div>
                </td>
                <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.IndAtomo)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.TipoGarantiaSuper)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.TipoGarantiaSuper)%>
                    <%: Html.ValidationMessageFor(model => model.TipoGarantiaSuper.Key)%>
                    <label id="lblValidation_Garantia_TipoGarantiaSuper_Key" class="field-validation-error"></label>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.TipoGarantiaBladex)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.TipoGarantiaBladex)%>
                    <%: Html.ValidationMessageFor(model => model.TipoGarantiaBladex.Key)%>
                    <label id="lblValidation_Garantia_TipoGarantiaBladex_Key" class="field-validation-error"></label>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.CategoriaRiesgoGarantia)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.CategoriaRiesgoGarantia)%>
                    <%: Html.ValidationMessageFor(model => model.CategoriaRiesgoGarantia.Key)%>
                    <label id="lblValidation_Garantia_CategoriaRiesgoGarantia_Key" class="field-validation-error"></label>                    
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.NombreOrganismo)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.NombreOrganismo)%>
                    <%: Html.ValidationMessageFor(model => model.NombreOrganismo)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.Moneda)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.Moneda)%>
                    <%: Html.ValidationMessageFor(model => model.Moneda.Key)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.ValorInicial) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.ValorInicial)%>
                    <%: Html.ValidationMessageFor(model => model.ValorInicial) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.ValorGarantiaSuperIntendencia)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <!-- ValorGarantiaSuperIntendencia dynamic field -->
                    <%= Html.DisplayFor(model => model.ValorGarantiaSuperIntendencia)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.ValorNecesarioDeGarantia) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.DisplayFor(model => model.ValorNecesarioDeGarantia)%>
                </div>
            </td>
        </tr>
        
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.ValorMercado) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.ValorMercado)%>
                    <%: Html.ValidationMessageFor(model => model.ValorMercado) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.ValorPolizaSeguro) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.ValorPolizaSeguro)%>
                    <%: Html.ValidationMessageFor(model => model.ValorPolizaSeguro) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.NumeroPolizaSeguro) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.NumeroPolizaSeguro)%>
                    <%: Html.ValidationMessageFor(model => model.NumeroPolizaSeguro) %>
                </div>
            </td>
        </tr>
        
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.DescripcionDeLaGarantia) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.DescripcionDeLaGarantia) %>
                    <%: Html.ValidationMessageFor(model => model.DescripcionDeLaGarantia) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaRegistroInicial) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaRegistroInicial) %>
                    <%: Html.ValidationMessageFor(model => model.FechaRegistroInicial) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaFormalizacion) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaFormalizacion)%>
                    <%: Html.ValidationMessageFor(model => model.FechaFormalizacion) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaVencimientoRiesgo) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaVencimientoRiesgo)%>
                    <%: Html.ValidationMessageFor(model => model.FechaVencimientoRiesgo) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaVencimientoGarantia) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaVencimientoGarantia)%>
                    <%: Html.ValidationMessageFor(model => model.FechaVencimientoGarantia) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaUltimaRevisionEvaluacion) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaUltimaRevisionEvaluacion)%>
                    <%: Html.ValidationMessageFor(model => model.FechaUltimaRevisionEvaluacion) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.FrecuenciaRevision)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.FrecuenciaRevision)%>
                    <%: Html.ValidationMessageFor(model => model.FrecuenciaRevision.Key)%>
                    <label id="lblValidation_Garantia_FrecuenciaRevision_Key" class="field-validation-error"></label>                    
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaProximaRevisionEvaluacion) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    
                    <%: Html.DisplayFor(m => m.FechaProximaRevisionEvaluacion) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaVencimientoSeguro) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaVencimientoSeguro)%>
                    <%: Html.ValidationMessageFor(model => model.FechaVencimientoSeguro) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaComienzoEjecucion) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaComienzoEjecucion)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.FechaCierreEjecucion) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FechaCierreEjecucion)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.ReduccionDeRiesgoPorPais) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.ReduccionDeRiesgoPorPais) %>
                    <%: Html.ValidationMessageFor(model => model.ReduccionDeRiesgoPorPais) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.PorcentajeAplicableMitigacionSuperInt) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.PorcentajeAplicableMitigacionSuperInt) %>
                    <%: Html.ValidationMessageFor(model => model.PorcentajeAplicableMitigacionSuperInt) %>
                </div>
            </td>
        </tr>
        
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.RatioCoberturaGarantia)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%= Html.Encode(Model.RatioCoberturaGarantia.ToString("p"))%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.Comentarios) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.Comentarios)%>
                    <%: Html.ValidationMessageFor(model => model.Comentarios) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(m => m.RatingGarante)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(m => m.RatingGarante)%>
                    <%: Html.ValidationMessageFor(model => model.RatingGarante.Key)%>
                    <label id="lblValidation_Garantia_RatingGarante_Key" class="field-validation-error"></label>                    
                </div>
            </td>
        </tr>
         <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.Status) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.Status)%>
                    <label id="lblValidation_Garantia_Status_Key" class="field-validation-error"></label>                    
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model => model.InternalStatus) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.DisplayFor(model => model.InternalStatus)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%: Html.LabelTooltipFor(model=>model.Source) %>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%: Html.DisplayFor(model=>model.Source) %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <label>Contratos de la garantía </label>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <div id="divVerContrato">
                <% if (Model.CategoriaSuper.IsReadOnly == false)
                   {%>
                    <input type='button' id='btnVerContratos' name='btnVerContratos' class='t-group' value='Ver Contratos' />
                    <% }
                   else
                   { %>
                    <%: Html.ActionLink("Ver Contratos", "Index", "GarantiaContrato", new { garantiaId = Model.Key, operationId = Model.selectedOperationId, categoriaSuperId = Model.CategoriaSuper.Key, useRepository = ViewBag.UseRepository, IsReadOnly = Model.CategoriaSuper.IsReadOnly }, null)%>
                    <%}%>
                    
                </div>
             <%--  <%
              if (Model.Key.HasValue)                  
              {                                    
                  %>
                  <div><i>Presione en el boton para visualizar los contratos asociados a esta garantía.</i></div>
                
                <div id="divVerContrato">
                    <input type='button' id='btnVerContratos' name='btnVerContratos' class='t-group' value='Ver Contratos' />
                </div>
                <%
              }
              else
              {%>
                 <div><i>Esta garantía no proviene del Atomo, por lo que aún no posee contratos asociados.</i></div>
                <%            
                    
                    
              }%>--%>
            </div>
        </td>
    </tr>

<script type="text/javascript">

    $("#btnVerContratos").click(function () {
        //var form = $("form");
        var model = '<%= Html.Raw(Json.Encode(Model))%>';
        //var json = $.toJSON(form);
        var messageConfirmation = "";

        if ($('#IsReadOnly').val() == "True")
            messageConfirmation = "¿Desea visualizar el listado de contratos?";
        else
            messageConfirmation = "¿Los valores modificados de la garantia seran guardados, desea continuar?";


        var confirmation = confirm(messageConfirmation);
        var garantiaId = $("#Garantia_Key_Value").val();
        var categoriaSuperId = $("#Garantia_CategoriaSuper_Key").val();
        var readOnly = $('#IsReadOnly').val();
        var source = "garantiaContrato";

        switch (categoriaSuperId) {
            case "01":
                source = "GarantiaMueble";
                break;

            case "02":
                source = "GarantiaInmueble";
                break;

            case "04":
                source = "GarantiaDepositoOtroBanco";
                break;

            case "05":
                source = "GarantiaPrenda";
                break;

            case "06":
                source = "GarantiaOtra";
                break;
        }
        if (confirmation) {
            HabilitarControlesParaSave();
            $("form").submit();
            DeshabilitarControlesParaSave();
            /*  $.ajax({
            type: "POST",
            //url: baseUrl + "/" + source + "/Edit",
            //url: baseUrl + "/" + source + "/Index",
            url: "/GarantiaContrato/Index",
            data: "{'garantiaId': " + JSON.stringify($("#Garantia_Key_Value").val()) + ", 'categoriaSuperId': " + JSON.stringify($("#Garantia_CategoriaSuper_Key").val()) + ", 'readOnly': " + JSON.stringify($('#IsReadOnly').val()) + "}",
            //data: "{'viewModel': " + JSON.stringify(model) + "}",
            format: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            if (data == "success") {
            window.location.reload(true);
            }
            else {
            alert('Error displaying related contracts.\nPlease try again in a few seconds or contact system administrator.');
            }
            },
            error: function (error) {
            //$(button).attr('disabled', false);
            alert('Error displaying related contracts.\nPlease try again in a few seconds or contact system administrator.');
            }
            });*/

            //var parameters = "{'garantiaId':" + JSON.stringify($("#Garantia_Key_Value").val()) + ", 'categoriaSuperId':" + JSON.stringify($("#Garantia_CategoriaSuper_Key").val()) + ", 'IsReadOnly':" + JSON.stringify($('#IsReadOnly').val()) + "}";
            /*
            $.ajax({
                url: baseUrl + '/GarantiaContrato/Index',
                type: 'POST',
                data: "{'viewModel': " + JSON.stringify(model) + "}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (data) {
                    alert('success');
                },
                error: function () {
                    alert("error");
                }
            });*/
        }
    });
</script>        


        