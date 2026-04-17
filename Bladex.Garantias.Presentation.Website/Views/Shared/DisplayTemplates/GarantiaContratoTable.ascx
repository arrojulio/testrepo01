<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Bladex.Garantias.Presentation.Website.Models.GarantiaContratoModel>>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<table id="tblGarantiasList" cellspacing="0" cellpadding="5">
 <thead>
    <tr>
        <th>
            Deal Reference
        </th>
        <th>
            Fecha Registro Inicial
        </th>
        <th>
            Fecha Vencimiento Garantia
        </th>
        <th>
            Fecha Vencimiento Riesgo
        </th>
        <th>
            % Cobertura de la Garantía
        </th>
        <th>
            Id Garantia / Operacion 
        </th>
        <th>
            Net Balance Principal
        </th>
        <th>[Action]</th>
    </tr>
    </thead>
    <tbody>
<% foreach (var item in Model) { %>
    <%Html.RenderPartial("DisplayTemplates/GarantiaContratoTableRow", item as GarantiaContratoModel);%>
<% } %>
    </tbody>
    <tfoot>
        <tr>
        <td colspan="8">
            <div>
                <p><i>Sólo será posible modificar el porcentaje de cobertura de las garantías.</i></p>
            </div>
        </td>
        </tr>
    </tfoot>
</table>
<script type="text/javascript">
    $(document).ready(function () {
        makeNiceTable('tblGarantiasList');
    });
	</script>
