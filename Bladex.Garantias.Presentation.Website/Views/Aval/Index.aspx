<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Mantenimiento de Avales
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>
	Listado de Avales disponibles para la Garantia
</h2>
<% var readOnlyPage = !(Request.QueryString["readOnly"] == null || !bool.Parse(Request.QueryString["readOnly"])); %>
  <table id="avalTable" cellspacing="0" cellpadding="5">
  <thead>
		<tr>
			<th>
				Id
			</th>
			<th>
				Es Cliente?
			</th>
			<th>
				Nombre
			</th>
			<th>
				Pais
			</th>
			<th>
				% de Cobertura
			</th>
			<th>
				Tipo de Aval
			</th>
			<th id="action" title="action">Action</th>
		</tr>
	</thead>
	<tbody>
	<% foreach (var item in Model) { %>
	
		<tr>
			<td>
				<%: item.Key %>
			</td>
			<td>
				<%: item.EsCliente %>
			</td>
			<td>
				<%: item.Nombre %>
			</td>
			<td>
				<%: item.Pais.Nombre %>
			</td>
			<td>
				<%: String.Format("{0:F2}", item.PorcentajeCobertura) %>
			</td>
			<td>
				<%: item.TipoAval.Nombre %>
			</td>
			<td>
                <%if (!readOnlyPage)
                  { %>
				<%:Html.ActionLink("Editar", "Edit", new { id = item.Key, garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId })%> |
				<%: Html.ActionLink("Borrar", "Delete", new { id = item.Key, garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId }, new { onclick = string.Format("{0}", item.Key == null || !item.Key.HasValue || item.Key.Value == 0 ? "alert('The delete feature is not implemented yet.'); return false;" : "javascript:return confirm('Desea eliminar este aval?');") })%>
                <% } else { %> No disponible <%} %>
			</td>
		</tr>
	
	<% } %>
	</tbody>
	</table>

	<p>
        
        <%if (!readOnlyPage) {%>
		<%:Html.ActionLink("Ingresar un nuevo Aval", "Create", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper.OTRAS_ID })%> | 
        <%} else {%>
            <i>Para realizar modificaciones sobre los avales asociados a una garantía primero debe salvar la misma.</i><br /><br />
        <%}%>
        <% if (ViewBag.CategoriaSuperId != null) { %>
        <%:Html.ActionLink("Regresar a la garantia", "Edit", "Garantia", new { id = (int?) ViewBag.GarantiaId, categoriaSuperId = Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper.OTRAS_ID }, new {})%> | 
        <% } %>
        <%if (!readOnlyPage) {%>
        <%:Html.ActionLink("Asociar Contratos a la Garantia", "Index", "GarantiaContrato", new {garantiaId = (int?) ViewBag.GarantiaId, operationId = (int?) ViewBag.OperationId, categoriaSuperId = Bladex.Garantias.DomainModel.DomainBase.CategoriaSuper.OTRAS_ID, useOperation = true}, new {})%> | 
        <%}%>
		<%: Html.ActionLink("Regresar al Inicio", "Index", "Garantia")%>
        
	</p>

	<script type="text/javascript">
		$(document).ready(function () {
			makeNiceTable('avalTable');
		});
	</script>
	
	
</asp:Content>



   