<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaPrendaModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<%this.ViewData["GarantiaBase.Key"] = Model.Key; %>
<%:Html.ValidationSummary(false, "Han ocurrido errores") %>
<fieldset>
    <legend>Garantia</legend>
    <%Html.RenderPartial("EditorTemplates/GarantiaBaseModel", Model as GarantiaBaseModel); %>
    <!--------------------- Finalizacion Garantia Base ------------------------------->
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%= Html.LabelTooltipFor(model => model.IdentificadorPrenda)%>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%= Html.EditorFor(model => model.IdentificadorPrenda)%>
                <%= Html.ValidationMessageFor(model => model.IdentificadorPrenda)%>
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%= Html.LabelTooltipFor(model => model.Emisor) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%= Html.EditorFor(model => model.Emisor)%>
                <%= Html.ValidationMessageFor(model => model.Emisor.Key)%>
                <label id="lblValidation_Garantia_Emisor_Key" class="field-validation-error"></label>                    
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%= Html.LabelTooltipFor(model => model.TipoInstrumentoFinanciero) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%= Html.EditorFor(model => model.TipoInstrumentoFinanciero)%>
                <%= Html.ValidationMessageFor(model => model.TipoInstrumentoFinanciero.Key)%>
                <label id="lblValidation_Garantia_TipoInstrumentoFinanciero_Key" class="field-validation-error"></label>                    
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%= Html.LabelTooltipFor(model => model.CalificacionesRiesgoEmision) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%= Html.EditorFor(model => model.CalificacionesRiesgoEmision)%>
                <%= Html.ValidationMessageFor(model => model.CalificacionesRiesgoEmision.Key)%>
                <label id="lblValidation_Garantia_CalificacionesRiesgoEmision_Key" class="field-validation-error"></label>                    
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%= Html.LabelTooltipFor(model => model.CalificacionesRiesgoEmisor) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%= Html.EditorFor(model => model.CalificacionesRiesgoEmisor)%>
                <%= Html.ValidationMessageFor(model => model.CalificacionesRiesgoEmisor.Key)%>
                <label id="lblValidation_Garantia_CalificacionesRiesgoEmisor_Key" class="field-validation-error"></label>                    
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%=Html.LabelTooltipFor(model=>model.PaisEmision) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%=Html.EditorFor(model=>model.PaisEmision) %>
                <%=Html.ValidationMessageFor(model=>model.PaisEmision.Key) %>
            </div>
        </td>
    </tr>
    </tbody> </table>
</fieldset>
<%:Html.ValidationSummary(false, "Han ocurrido errores") %>