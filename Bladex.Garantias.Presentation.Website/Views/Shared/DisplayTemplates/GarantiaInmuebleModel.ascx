<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaInmuebleModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<fieldset>
    <legend><%:Html.LabelFor(m=>m.Key) %> <%: Model.Key %></legend>
    <!--------------------- Comienzo Garantia Base ----------------------------------->
    <%Html.RenderPartial("DisplayTemplates/GarantiaBaseModel", Model as GarantiaBaseModel);%>
    <!--------------------- Finalizacion Garantia Base ------------------------------->
    <!--------------------- Comienzo Garantia Inmueble ------------------------------->
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%=Html.LabelFor(m => m.InscripcionRegistroPublico)%></div>
        </td>
        <td>
            <div class="display-field">
                <%=Html.DisplayFor(m => m.InscripcionRegistroPublico)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%=Html.LabelFor(m => m.NumeroDeFinca)%></div>
        </td>
        <td>
            <div class="display-field">
                <%=Html.DisplayFor(m => m.NumeroDeFinca)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%=Html.LabelFor(m => m.ValorAvaluo)%></div>
        </td>
        <td>
            <div class="display-field">
                <%=Html.DisplayFor(m => m.ValorAvaluo)%></div>
        </td>
    </tr>
    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%:Html.LabelFor(m => m.ValorEvaluacionVentaRapida)%></div>
        </td>
        <td>
            <div class="display-field">
                <%:Html.DisplayFor(m => m.ValorEvaluacionVentaRapida)%></div>
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



    <tr class="guaranteeRow">
        <td>
            <div class="display-label">
                <%=Html.LabelFor(m => m.AseguradorSuper)%></div>
        </td>
        <td>
            <div class="display-field">
                <%=Html.DisplayFor(m => m.AseguradorSuper.Nombre)%></div>
        </td>
    </tr>
    </tbody> 
    </table>
    <!--------------------- Finalizacion Garantia Inmueble --------------------------->
</fieldset>
