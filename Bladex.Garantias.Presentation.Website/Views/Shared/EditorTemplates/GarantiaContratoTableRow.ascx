<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaContratoModel>" %>
<tr>
    <td>
        <%: Html.DisplayFor(m => m.DealReference) %>
    </td>
    <td>
        <%: Html.DisplayTextFor(m => m.FechaRegistroInicial) %>
    </td>
    <td>
        <%: Html.DisplayTextFor(m => m.FechaVencimientoGarantia) %>
    </td>
    <td>
        <%: Html.DisplayTextFor(m => m.FechaVencimientoRiesgo)%>
    </td>
    <td>
        <%: Html.EditorFor(m => m.PorcUtilization) %>
    </td>
    <td>
        <%: Html.DisplayTextFor(m => m.GarantiaId) %>
    </td>
    <td>
        <%: Html.DisplayTextFor(m => m.NetBalancePrincipal)%>
    </td>
    <td>
       <%if (!ViewBag.CategoriaSuper.IsReadOnly){%>
            <%if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Power User")){%>
                <%:Html.ActionLink("Editar", "Edit", "GarantiaContrato", new { id = Model.ID }, null)%>
                <%:Html.HiddenFor(m => m.ID)%>
            <%} else {%>
                <span>Not available</span>    
            <%}%>
        <%} else {%>
            <span>Not available</span>
        <%}%>
    </td>
</tr>
