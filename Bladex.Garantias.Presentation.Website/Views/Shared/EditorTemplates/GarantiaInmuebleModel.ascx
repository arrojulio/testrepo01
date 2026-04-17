<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaInmuebleModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<%this.ViewData["GarantiaBase.Key"] = Model.Key; %>
<%:Html.ValidationSummary(false, "Han ocurrido errores") %>
<fieldset>
    <legend>Garantia</legend>
    <%Html.RenderPartial("EditorTemplates/GarantiaBaseModel", Model as GarantiaBaseModel);%>
    <!--------------------- Finalizacion Garantia Base ------------------------------->
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%: Html.LabelTooltipFor(model => model.InscripcionRegistroPublico) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.InscripcionRegistroPublico)%>
                <%: Html.ValidationMessageFor(model => model.InscripcionRegistroPublico) %>
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%: Html.LabelTooltipFor(model => model.NumeroDeFinca) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.NumeroDeFinca)%>
                <%: Html.ValidationMessageFor(model => model.NumeroDeFinca) %>
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%: Html.LabelTooltipFor(model => model.ValorAvaluo) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.ValorAvaluo)%>
                <%: Html.ValidationMessageFor(model => model.ValorAvaluo) %>
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%: Html.LabelTooltipFor(model => model.ValorEvaluacionVentaRapida) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.ValorEvaluacionVentaRapida)%>
                <%: Html.ValidationMessageFor(model => model.ValorEvaluacionVentaRapida) %>
            </div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="editor-label">
                <%= Html.LabelTooltipFor(model => model.AseguradorSuper) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%= Html.EditorFor(model => model.AseguradorSuper)%>
                <%= Html.ValidationMessageFor(model => model.AseguradorSuper.Key)%>
            </div>
        </td>
    </tr>
    </tbody> </table>
</fieldset>
 <%:Html.ValidationSummary(false, "Han ocurrido errores") %>