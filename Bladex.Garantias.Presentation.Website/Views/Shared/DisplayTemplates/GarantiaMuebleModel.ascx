<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaMuebleModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<fieldset>
    <legend><%:Html.LabelFor(m=>m.Key) %> <%: Model.Key %></legend>
    <!--------------------- Comienzo Garantia Base ----------------------------------->
    <%Html.RenderPartial("DisplayTemplates/GarantiaBaseModel", Model as GarantiaBaseModel);%>
    <!--------------------- Finalizacion Garantia Base ------------------------------->
    <!--------------------- Comienzo Garantia Mueble ------------------------------->
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.AseguradorSuper)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.AseguradorSuper)%></div>
        </td>
    </tr>
    <!-- Fecha Inicio Avaluo -->
<tr class="guaranteeRow">
    <td>
        <div class="display-label">
            <%:Html.LabelFor(m => m.FechaInicialAvaluo)%></div>
    </td>
    <td>
        <div class="display-field">
            <%:Html.DisplayFor(m => m.FechaInicialAvaluo)%></div>
    </td>
</tr>
<!-- Fecha Vencimiento Avaluo -->
<tr class="guaranteeRow">
    <td>
        <div class="display-label">
            <%:Html.LabelFor(m => m.FechaVencimientoAvaluo)%></div>
    </td>
    <td>
        <div class="display-field">
            <%:Html.DisplayFor(m => m.FechaVencimientoAvaluo)%></div>
    </td>
</tr>
<!-- Valor Total Avaluo -->
<tr class="guaranteeRow">
    <td>
        <div class="display-label">
            <%:Html.LabelFor(m => m.ValorTotalAvaluo)%></div>
    </td>
    <td>
        <div class="display-field">
            <%:Html.DisplayFor(m => m.ValorTotalAvaluo)%></div>
    </td>
</tr>
    </tbody> 
    </table>
    <!--------------------- Finalizacion Garantia Mueble --------------------------->
</fieldset>
