<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaOtraModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<fieldset>
    <legend><%:Html.LabelFor(m=>m.Key) %> <%: Model.Key %></legend>
    <!--------------------- Comienzo Garantia Base ----------------------------------->
    <%Html.RenderPartial("DisplayTemplates/GarantiaBaseModel", Model as GarantiaBaseModel);%>
    <!--------------------- Finalizacion Garantia Base ------------------------------->
    <!--------------------- Comienzo Garantia Otra ------------------------------->
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%=Html.LabelFor(m => m.Emisor)%></div>
        </td>
        <td>
            <div class="display-field">
                <%=Html.DisplayFor(m => m.Emisor.Nombre)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%=Html.LabelFor(m => m.AvalList)%></div>
        </td>
        <td>
            <div class="display-field">
                <%=Model.AvalList.Count%></div>
        </td>
    </tr>
    </tbody> </table>
    <!--------------------- Finalizacion Garantia Otra --------------------------->
</fieldset>
