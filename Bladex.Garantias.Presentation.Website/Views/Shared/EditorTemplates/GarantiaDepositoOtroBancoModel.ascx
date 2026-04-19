<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaDepositoOtroBancoModel>" %>
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
                <%= Html.LabelTooltipFor(model => model.BancoSuper) %>
            </div>
        </td>
        <td>
            <div class="editor-field">
                <%= Html.EditorFor(model => model.BancoSuper)%>
                <%= Html.ValidationMessageFor(model => model.BancoSuper.Key)%>
            </div>
        </td>
    </tr>
    </tbody>
    </table>
</fieldset>
<%:Html.ValidationSummary(false, "Han ocurrido errores") %>