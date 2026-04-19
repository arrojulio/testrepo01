<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaPrendaModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<fieldset>
    <legend><%:Html.LabelFor(m=>m.Key) %> <%: Model.Key %></legend>
    <!--------------------- Comienzo Garantia Base ----------------------------------->
    <%Html.RenderPartial("DisplayTemplates/GarantiaBaseModel", Model as GarantiaBaseModel);%>
    <!--------------------- Finalizacion Garantia Base ------------------------------->
    <!--------------------- Comienzo Garantia Prenda ------------------------------->
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.IdentificadorPrenda)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.IdentificadorPrenda)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.Emisor)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.Emisor.Nombre)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.TipoInstrumentoFinanciero)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.TipoInstrumentoFinanciero)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.CalificacionesRiesgoEmision)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.CalificacionesRiesgoEmision)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.CalificacionesRiesgoEmisor)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.CalificacionesRiesgoEmisor)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.PaisEmision)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.PaisEmision)%></div>
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
    </tbody> </table>
    <!--------------------- Finalizacion Garantia Prenda --------------------------->
</fieldset>
