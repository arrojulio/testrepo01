<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaDepositoOtroBancoModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<fieldset>
    <legend><%:Html.LabelFor(m=>m.Key) %> <%: Model.Key %></legend>
    <!--------------------- Comienzo Garantia Base ----------------------------------->
    <%Html.RenderPartial("DisplayTemplates/GarantiaBaseModel", Model as GarantiaBaseModel);%>
    <!--------------------- Finalizacion Garantia Base ------------------------------->
    <!--------------------- Comienzo Garantia DepositoOtroBanco ------------------------------->
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%=Html.LabelFor(m => m.BancoSuper)%></div>
        </td>
        <td>
            <div class="display-field">
                <%=Html.DisplayFor(m => m.BancoSuper.Nombre)%></div>
        </td>
    </tr>
    </tbody> 
    </table>
    <!--------------------- Finalizacion Garantia DepositoOtroBanco --------------------------->
</fieldset>
