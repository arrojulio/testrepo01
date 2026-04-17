<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Models.GarantiaContratoModel>" %>
<tr>
    <td>
        <%: Html.DisplayFor(m => m.DealReference) %>
    </td>
    <td>
        <%: Model.FechaRegistroInicial.HasValue ? Model.FechaRegistroInicial.Value.ToShortDateString() : string.Empty %>
    </td>
    <td>
        <%: Model.FechaVencimientoGarantia.HasValue ? Model.FechaVencimientoGarantia.Value.ToShortDateString() : string.Empty%>
    </td>
    <td>
        <%: Model.FechaVencimientoRiesgo.HasValue ? Model.FechaVencimientoRiesgo.Value.ToShortDateString() : string.Empty%>
    </td>
    <td>
        <%: (Model.PorcUtilization).ToString("0.0000000000") + "%" %>
    </td>
    <td>
        <%: Html.DisplayTextFor(m => m.GarantiaId) %>
    </td>
    <td>
        <%: Model.NetBalancePrincipal.HasValue ? Model.NetBalancePrincipal.Value.ToString("c") : new Decimal(0).ToString("c") %>
    </td>
    <td>
        <%--<%if (!ViewBag.CategoriaSuper.IsReadOnly){%>--%>
        <%if (!ViewBag.IsReadOnly){%>
            <%if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Power User") && !Context.User.IsInRole("Checker") && (Request.QueryString["readOnly"] == null || !bool.Parse(Request.QueryString["readOnly"]))) {%>                
                
                <%:Html.ActionLink("Editar", "Edit", "GarantiaContrato", new { id = Model.ID, garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, useRepository = (bool?)ViewBag.UseRepository, dealReference = Model.DealReference, categoriaSuperId = Request.QueryString["categoriaSuperId"] }, null)%>                                                                                    
                 
                <%:Html.ActionLink("Desvincular", "Delete", "GarantiaContrato", new { id = Model.ID, garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = Request.QueryString["categoriaSuperId"], useRepository = (bool?)ViewBag.UseRepository,dealReference = Model.DealReference }, new { onclick = string.Format("{0}", Model.ID != 0 || (int?)ViewBag.OperationId !=0 ? "javascript:return confirm('Desea eliminar esta asociacion?');" : "alert('The delete feature is not implemented yet.'); return false;") })%>
                <%:Html.HiddenFor(m => m.ID)%>
            <%} else {%>
                <%--<span>No disponible</span>    --%>
            <%}%>
        <%} else {%>
            <%--<span>No disponible</span>--%>
        <%}%>
    </td>
</tr>
    