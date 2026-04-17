<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaBaseModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0">
    <tbody>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.Key) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.Key)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.FCCReference) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.FCCReference)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.IdentificacionDocumentoGarantia) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.IdentificacionDocumentoGarantia)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.NroIncidenteWorkflow) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.NroIncidenteWorkflow)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.Cliente) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.Cliente)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.Garante) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.Garante)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.ValorInicial) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.ValorInicial)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.ValorGarantiaSuperIntendencia) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.ValorGarantiaSuperIntendencia)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.DescripcionDeLaGarantia) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.DescripcionDeLaGarantia)%></div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="display-label">
                    <%:Html.LabelFor(m=>m.Comentarios) %></div>
            </td>
            <td>
                <div class="display-field">
                    <%:Html.DisplayFor(m => m.Comentarios)%></div>
            </td>
        </tr>
        
<%--<div class="display-label">
    <%:Html.LabelFor(m => m.OrigenGarantia)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.OrigenGarantia)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.ValorInicial)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.ValorInicial)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.DescripcionDeLaGarantia)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.DescripcionDeLaGarantia)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.FechaRegistroInicial)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.FechaRegistroInicial)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.FechaFormalizacion)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.FechaFormalizacion)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.FechaVencimientoRiesgo)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.FechaVencimientoRiesgo)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.FechaVencimientoGarantia)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.FechaVencimientoGarantia)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.FechaUltimaRevisionEvaluacion)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.FechaUltimaRevisionEvaluacion)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.FechaProximaRevisionEvaluacion)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.FechaProximaRevisionEvaluacion)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.FechaVencimientoSeguro)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.FechaVencimientoSeguro)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.ReduccionDeRiesgoPorPais)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.ReduccionDeRiesgoPorPais)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.ValorNecesarioDeGarantia)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.ValorNecesarioDeGarantia)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.PorcentajeAplicableMitigacionSuperInt)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.PorcentajeAplicableMitigacionSuperInt)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.Comentarios)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.Comentarios)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.ValorPolizaSeguro)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.ValorPolizaSeguro)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.NumeroPolizaSeguro)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.NumeroPolizaSeguro)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.ValorMercado)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.ValorMercado)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.AttachedToLine)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.AttachedToLine)%></div>
<div class="display-label">
    <%:Html.LabelFor(m => m.ID_Atomo)%></div>
<div class="display-field">
    <%:Html.DisplayFor(m => m.ID_Atomo)%></div>
--%>