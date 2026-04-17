<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.GarantiaMuebleViewModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edicion de Garantias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var backUrl = '<%= Url.Action("Index","GarantiaMueble", new { categoriaSuperId = "01" }) %>';
    logInBrowser(backUrl);

    </script>
    <h2>Editar <%:ViewData["CategoriaSuperTitle"]%></h2>
    
         <script type="text/javascript">
             setupGarantiaBaseModel(parseInt('<%=Model.Key %>'));
    </script>

    <%if (Model.Garantia.Key.HasValue && Model.Garantia.Key.Value != 0)
      { %>
        <% Html.RenderAction("CheckOverUtilization", "GarantiaBase", new { garantiaId = Model.Garantia.Key.Value }); %>
        <% Html.RenderAction("VerifyGuaranteeBlock", "GarantiaBase", new { garantiaId = Model.Garantia.Key.Value }); %>
        <%:Html.HiddenFor(m => m.Garantia.Key)%>
    <%} %>

    <%: Html.HiddenFor(m => m.Garantia.CategoriaSuper.Key)%>
    <%: Html.HiddenFor(m => m.Garantia.CategoriaSuper.Nombre)%>
    <%: Html.HiddenFor(m => m.Garantia.selectedOperationId)%>
    <%: Html.HiddenFor(m => m.Garantia.CategoriaSuper.IsReadOnly, new { Id = "IsReadOnly", name = "IsReadOnly" })%>

    <%using (Html.BeginForm()) {%>
        
      <div id="tabs">
      <ul>
        <li><a href="#tabs-1">Required Fields</a></li>
        <li><a href="#tabs-2">Optional Fields</a></li>        
      </ul>      
      <div id="tabs-1">
        <fieldset>                     
            <table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0">            
                <tbody>    
                         <% if (!string.IsNullOrEmpty(Model.Garantia.BusinessError))
                         { %>
                        <tr class="guaranteeRow">
                            <td>
                                <div class="editor-label">
                                    <%: Html.LabelTooltipFor(m => m.Garantia.BusinessError)%>
                                </div>
                            </td>
                            <td>
                            <div class="editor-field">
                                <div class="field-validation-error">
                                    <%:Html.DisplayFor(m => m.Garantia.BusinessError)%>
                                    <%: Html.ValidationMessageFor(m => m.Garantia.BusinessError)%>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <%} %>      
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.Key)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <label>
                                    <%: Html.Encode(Model.Garantia.Key.HasValue ? Model.Garantia.Key.ToString() : "(not generated)")%></label>
                                    <%: Html.HiddenFor(model => model.Garantia.Key)%>
                            </div>
                        </td>
                    </tr>                        
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.IdentificacionDocumentoGarantia)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.DisplayFor(m => m.Garantia.IdentificacionDocumentoGarantia)%>
                                <%: Html.HiddenFor(m => m.Garantia.IdentificacionDocumentoGarantia)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.IdentificacionDocumentoGarantia)%>                                
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.CodigoBanco)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%if (string.IsNullOrEmpty(Model.Garantia.CodigoBanco)) { Model.Garantia.CodigoBanco = "027"; }%>
                                <%: Html.DisplayTextFor(m => m.Garantia.CodigoBanco)%>
                                <%: Html.HiddenFor(model => model.Garantia.CodigoBanco)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="display-label">
                                <%= Html.LabelTooltipFor(m => m.Garantia.Cliente)%>
                        </td>
                        <td>
                            <div class="display-field">
                                <%:Html.EditorFor(m => m.Garantia.Cliente)%> 
                                <%= Html.ValidationMessageFor(model => model.Garantia.Cliente.Key) %>
                                <label id="lblValidation_Garantia_Cliente_Key" class="field-validation-error"></label>                                         
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="display-label">
                                 <%: Html.LabelTooltipFor(m => m.Garantia.Garante)%>
                                 </div>
                        </td>
                        <td>
                            <div class="display-field">
                                <%:Html.EditorFor(m => m.Garantia.Garante)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.Garante.Key)%>
                                <label id="lblValidation_Garantia_Garante_Key" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.IdentificacionFideicomiso)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.IdentificacionFideicomiso)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.IdentificacionFideicomiso)%>
                            </div>
                        </td>
                    </tr>
                     <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.FiduciariaSuper)%>                                
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <span id="FiduciariaSuperDiv">
                                    <%: Html.EditorFor(m => m.Garantia.FiduciariaSuper)%>
                                </span>
                                <%: Html.ValidationMessageFor(model => model.Garantia.FiduciariaSuper.Key)%>
                                <label id="lblValidation_Garantia_FiduciariaSuper_Key" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>                    
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.FiduciariaBladex)%>                                
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">                                
                                <%: Html.EditorFor(m => m.Garantia.FiduciariaBladex)%>                                
                                <%: Html.ValidationMessageFor(model => model.Garantia.FiduciariaBladex)%>
                                <label id="lblValidation_Garantia_FiduciariaBladex_Key" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                     <tr class="guaranteeRow">
                        <td>
                            <div class="display-label">
                                <%= Html.LabelTooltipFor(m => m.Garantia.AseguradorSuper)%>
                            </div>
                        </td>
                        <td>
                            <div class="display-field">
                                <%:Html.EditorFor(m => m.Garantia.AseguradorSuper)%>
                                <%:Html.ValidationMessageFor(model => model.Garantia.AseguradorSuper.Key)%>
                            </div>                           
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.Asegurador)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.Asegurador)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.Asegurador)%>

                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.OrigenGarantia) %>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.OrigenGarantia)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.OrigenGarantia)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.PaisGarantia)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.PaisGarantia)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.PaisGarantia.Key)%>
                                <label id="lblValidation_Garantia_PaisGarantia_Key" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.Region)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.Region)%>                                
                                <%: Html.ValidationMessageFor(model => model.Garantia.Region.Key)%>
                                <label id="lblValidation_Garantia_Region_Key" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.TipoGarantiaSuper)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.TipoGarantiaSuper)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.TipoGarantiaSuper.Key)%>
                                <label id="lblValidation_Garantia_TipoGarantiaSuper_Key" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.Moneda)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.Moneda)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.Moneda.Key)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.ValorInicial)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.ValorInicial)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.ValorInicial)%>
                            </div>
                        </td>
                    </tr>
                      <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.ValorGarantiaSuperIntendencia)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <!-- ValorGarantiaSuperIntendencia dynamic field -->
                                <%= Html.DisplayFor(model => model.Garantia.ValorGarantiaSuperIntendencia)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.ValorMercado)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.DisplayFor(model => model.Garantia.ValorMercado)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.ValorMercado)%>
                            </div>
                        </td>
                    </tr>
                                        <!-- Fecha Inicial Avaluo -->
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.FechaInicialAvaluo)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.FechaInicialAvaluo)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.FechaInicialAvaluo)%>
                                <label id="lblGarantia_FechaInicialAvaluo_Validation" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                    <!-- Fecha Vencimineto Avaluo -->
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.FechaVencimientoAvaluo)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.FechaVencimientoAvaluo)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.FechaVencimientoAvaluo)%>
                                <label id="lblGarantia_FechaVencimientoAvaluo_Validation" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                    <!-- Valor Total Avaluo -->
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.ValorTotalAvaluo)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.ValorTotalAvaluo)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.ValorTotalAvaluo)%>
                                <label id="lblGarantia_ValorTotalAvaluo_Validation" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>

                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.NumeroPolizaSeguro)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.NumeroPolizaSeguro)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.NumeroPolizaSeguro)%>
                            </div>
                        </td>
                    </tr>                
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.TipoPoliza)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.TipoPoliza)%>                                
                                <%: Html.ValidationMessageFor(model => model.Garantia.TipoPoliza.Key)%>
                                <label id="lblValidation_Garantia_TipoPoliza" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>  
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.ValorPolizaSeguro)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.ValorPolizaSeguro)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.ValorPolizaSeguro)%>
                            </div>
                        </td>
                    </tr>                  
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.FechaRegistroInicial)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.FechaRegistroInicial)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.FechaRegistroInicial)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.FechaFormalizacion)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.FechaFormalizacion)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.FechaFormalizacion)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.FechaVencimientoGarantia)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.FechaVencimientoGarantia)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.FechaVencimientoGarantia)%>
                                <label id="lblGarantia_FechaVencimientoGarantia_Validation" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>    
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.FechaUltimaRevisionEvaluacion)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.FechaUltimaRevisionEvaluacion)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.FechaUltimaRevisionEvaluacion)%>
                                <label id="lblGarantia_FechaUltimaRevisionEvaluacion_Validation" class="field-validation-error"></label>
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
                                <% if (Model.Garantia.CategoriaSuper.IsReadOnly == false)
                                   {%>
                                    <input type='button' id='btnVerContratos' name='btnVerContratos' class='t-group' value='Ver Contratos' />
                                    <% }
                                   else
                                   { %>
                                    <%: Html.ActionLink("Ver Contratos", "Index", "GarantiaContrato", new { garantiaId = Model.Garantia.Key, operationId = Model.Garantia.selectedOperationId, categoriaSuperId = Model.Garantia.CategoriaSuper.Key, useRepository = ViewBag.UseRepository, IsReadOnly = Model.Garantia.CategoriaSuper.IsReadOnly }, null)%>
                                    <%}%>                    
                            </div>   
                            </div>
                        </td>
                    </tr>     
                    <!--- Specialization Fields -->                                   
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.DescripcionDeLaGarantia)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.DescripcionDeLaGarantia)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.DescripcionDeLaGarantia)%>
                            </div>
                        </td>
                    </tr>
                    <!--- End Specialization Fields -->                                                    
                </tbody> 
            </table>
        </fieldset>
      </div>
      <div id="tabs-2">
      <fieldset>
            <table id="Table1" class="guaranteeTable" cellpadding="0" cellspacing="0">            
                <tbody>             
                     <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">                    
                                <%: Html.LabelTooltipFor(m => m.Garantia.ID_Atomo)%>
                            </div>                
                        </td>
                        <td>
                            <div class="editor-field">
                                <label><%: Html.Encode(Model.Garantia.ID_Atomo == 0 ? "(empty)" : Model.Garantia.ID_Atomo.ToString())%></label>
                            </div>
                        </td>
                    </tr>
                     <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.FCCReference)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <label>
                                    <%: Html.Encode(!string.IsNullOrEmpty(Model.Garantia.FCCReference) ? Model.Garantia.FCCReference : "(empty)")%></label>
                                <%: Html.HiddenFor(model => model.Garantia.FCCReference)%>
                            </div>
                        </td>
                    </tr>            
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.AttachedToLine)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <table border="0" cellpadding="0" cellspacing="0" class="tblAttachedToLine">

                                    <tr class="attachedToLineRow">
                                        <td style="width:50px" valign="middle"><%: Html.EditorFor(m=>m.Garantia.AttachedToLine) %></td>
                                        <td valign="middle">
                                            <%Html.RenderPartial("EditorTemplates/AttachedToLineViewModel", new AttachedToLineViewModel() { AttachedToLine = Model.Garantia.AttachedToLine, CustomerId = Model.Garantia.Cliente.Key, CustomerControlId = "empty" }); %>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%= Html.LabelTooltipFor(m => m.Garantia.Beneficiario)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%= Html.DisplayFor(m => m.Garantia.Beneficiario)%>
                                <%= Html.ValidationMessageFor(model => model.Garantia.Beneficiario)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.Depositante)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.Depositante)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.Depositante.Key)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.Evaluador)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.Evaluador)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.Evaluador.Key)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.Administrador)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(model => model.Garantia.Administrador)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.Administrador)%>
                            </div>
                        </td>
                    </tr>                    
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.Revisor)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.Revisor)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.Revisor.Key)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%:Html.LabelTooltipFor(model => model.Garantia.IndAtomo)%>
                            </div>
                            </td>
                            <td>
                            <div class="editor-field">
                                <%: Html.DisplayFor(model => model.Garantia.IndAtomo)%>
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.TipoGarantiaBladex)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.TipoGarantiaBladex)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.TipoGarantiaBladex.Key)%>
                                <label id="lblValidation_Garantia_TipoGarantiaBladex_Key" class="field-validation-error"></label>
                            </div>
                        </td>
                    </tr>
                     <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(m => m.Garantia.CategoriaRiesgoGarantia)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.EditorFor(m => m.Garantia.CategoriaRiesgoGarantia)%>
                                <%: Html.ValidationMessageFor(model => model.Garantia.CategoriaRiesgoGarantia.Key)%>
                                <label id="lblValidation_Garantia_CategoriaRiesgoGarantia_Key" class="field-validation-error"></label>                    
                            </div>
                        </td>
                    </tr>
                    <tr class="guaranteeRow">
                        <td>
                            <div class="editor-label">
                                <%: Html.LabelTooltipFor(model => model.Garantia.ValorNecesarioDeGarantia)%>
                            </div>
                        </td>
                        <td>
                            <div class="editor-field">
                                <%: Html.DisplayFor(model => model.Garantia.ValorNecesarioDeGarantia)%>
                            </div>
                        </td>
                    </tr>                                          
                    <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.FechaVencimientoRiesgo)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%--<%: Html.EditorFor(model => model.Garantia.FechaVencimientoRiesgo)%>--%>
                            <%: Html.DisplayFor(model => model.Garantia.FechaVencimientoRiesgo)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.FechaVencimientoRiesgo)%>
                            <label id="lblGarantia_FechaVencimientoRiesgo_Validation" class="field-validation-error"></label>
                        </div>
                    </td>
                </tr>                
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(m => m.Garantia.FrecuenciaRevision)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(m => m.Garantia.FrecuenciaRevision)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.FrecuenciaRevision.Key)%>
                            <label id="lblValidation_Garantia_FrecuenciaRevision_Key" class="field-validation-error"></label>                    
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.FechaProximaRevisionEvaluacion)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                    
                            <%: Html.EditorFor(m => m.Garantia.FechaProximaRevisionEvaluacion)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.FechaVencimientoSeguro)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.Garantia.FechaVencimientoSeguro)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.FechaVencimientoSeguro)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.FechaComienzoEjecucion)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.Garantia.FechaComienzoEjecucion)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.FechaCierreEjecucion)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.Garantia.FechaCierreEjecucion)%>
                        </div>
                    </td>
                </tr>
                 <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.RatioCoberturaGarantia)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%= Html.Encode(Model.Garantia.RatioCoberturaGarantia.ToString("p"))%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.Comentarios)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.Garantia.Comentarios)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.Comentarios)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(m => m.Garantia.RatingGarante)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(m => m.Garantia.RatingGarante)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.RatingGarante.Key)%>
                            <label id="lblValidation_Garantia_RatingGarante_Key" class="field-validation-error"></label>                    
                        </div>
                    </td>
                </tr>
                 <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.Status)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.Garantia.Status)%>
                            <label id="lblValidation_Garantia_Status_Key" class="field-validation-error"></label>                    
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.InternalStatus)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.DisplayFor(model => model.Garantia.InternalStatus)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.Source)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.DisplayFor(model => model.Garantia.Source)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.ReduccionDeRiesgoPorPais)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.Garantia.ReduccionDeRiesgoPorPais)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.ReduccionDeRiesgoPorPais)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.NombreOrganismo)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(m => m.Garantia.NombreOrganismo)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.NombreOrganismo)%>
                        </div>
                    </td>
                </tr>
                <tr class="guaranteeRow">
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelTooltipFor(model => model.Garantia.PorcentajeAplicableMitigacionSuperInt)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.Garantia.PorcentajeAplicableMitigacionSuperInt)%>
                            <%: Html.ValidationMessageFor(model => model.Garantia.PorcentajeAplicableMitigacionSuperInt)%>
                        </div>
                    </td>
                </tr>
                <!--- Specialization Fields -->                
        
                <!--- End Specialization Fields -->                
                </tbody> 
            </table>
        </fieldset>
     </div>      
    </div>

        <%if (!Model.Garantia.CategoriaSuper.IsReadOnly){%>
            <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User")) {%>
                <input type="button" value="Salvar" id="btnSaveGarantia" />
            <%}%>
        <%}%>
        <%:Html.Hidden("useRepository", (bool?)ViewBag.UseRepository)%>
        <input type="button" value="Cancelar" onclick="javascript: document.location = backUrl;" />
    <%}%>

    <%: Html.Hidden("Hidden_OperationId",(int?)ViewBag.OperationId) %>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Hidden_CurrentOperation').val($('#Hidden_OperationId').val());

            $("#tabs").tabs();

            $("#btnVerContratos").click(function () {

                var model = '<%= Html.Raw(Json.Encode(Model))%>';
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
                }
            });
        });
    </script>
 
</asp:Content>
