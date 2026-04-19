<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaOtraModel>" %>
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
                <%= Html.LabelTooltipFor(m=>m.AvalComponent) %>
            </div>
        </td>
        <td>
            <div class="editor-field">                            
                <%:Html.HiddenFor(model => model.AvalComponent.hiddenAvales)%>                
                <%= Html.EditorFor(m => m.AvalComponent)%>                               
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
            </div>
        </td>
    </tr>

    </tbody>
    </table>
</fieldset>
<%:Html.ValidationSummary(false, "Han ocurrido errores") %>

<script type="text/jscript">
    $("#hidden_AvalList").val("");
 </script>