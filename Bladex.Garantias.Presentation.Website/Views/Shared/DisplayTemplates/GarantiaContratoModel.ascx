<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaContratoModel>" %>
<%: Html.HiddenFor(model => model.ID) %>
<style type="text/css">
    .editor-label
    { width:180px;}
    .editor-field { width:388px; }
</style>
<table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0" >
    <tbody>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.DealReference)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.DisplayTextFor(model => model.DealReference)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.FechaRegistroInicial)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.DisplayFor(model => model.FechaRegistroInicial)%>
                    
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.FechaVencimientoGarantia)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.DisplayFor(model => model.FechaVencimientoGarantia)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.FechaVencimientoRiesgo)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.DisplayFor(model => model.FechaVencimientoRiesgo)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">                                                
                        <%:Html.LabelFor(model => model.GarantiaId)%> / Operacion                     
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Model.GarantiaId.HasValue ? Model.GarantiaId.Value.ToString() : "(empty)" %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.NetBalancePrincipal)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:    //                    :Html.DisplayFor(model => model.NetBalancePrincipal)
    Model.NetBalancePrincipal != null && Model.NetBalancePrincipal.HasValue ? Model.NetBalancePrincipal.Value.ToString() : "(empty)"
    %>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.PorcUtilization)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.DisplayFor(model => model.PorcUtilization)%>
                </div>
            </td>
        </tr>
    </tbody>
</table>
