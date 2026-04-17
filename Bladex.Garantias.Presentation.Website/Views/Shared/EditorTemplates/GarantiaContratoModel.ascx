<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaContratoModel>" %>
<%: Html.HiddenFor(model => model.ID) %>

<table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0" >
    <tbody>
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
                    <%:Html.LabelFor(model => model.PorcUtilization)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    
                    <%:Html.HiddenFor(model => model.DealReference)%>
                    <%:Html.HiddenFor(model => model.FechaRegistroInicial)%>
                    <%:Html.HiddenFor(model => model.FechaVencimientoGarantia)%>
                    <%:Html.HiddenFor(model => model.FechaVencimientoRiesgo)%>
                    <%:Html.HiddenFor(model => model.GarantiaId) %>
                    <%:Html.HiddenFor(model => model.NetBalancePrincipal) %>                    
                    <%if (ViewBag!=null && ViewBag.IsReadOnly != null && ViewBag.IsReadOnly)
                        {%>
                        <%:Html.DisplayFor(model => model.PorcUtilization)%>                        
                        <%:Html.HiddenFor(model => model.PorcUtilization) %>         
                    <% }
                    else
                    { %>
                        <%:Html.EditorFor(model => model.PorcUtilization)%> 
                    <%} %>
                    <%:Html.ValidationMessageFor(model => model.PorcUtilization)%>
                </div>
            </td>
        </tr>
    </tbody>
</table>
