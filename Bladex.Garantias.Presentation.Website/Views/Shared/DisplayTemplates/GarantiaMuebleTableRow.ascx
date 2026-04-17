<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.DomainModel.DomainBase.Summary.GarantiaMuebleSummary>" %>
<tr>
    <td>
        <%: Model.Key %>
    </td>
    <td>
        <%: Model.IdentificacionDocumentoGarantia %>
    </td>
    <td>
        <%: Model.Cliente %>
    </td>
    <td>
        <%: Model.TipoGarantiaSuper %>
    </td>
    <td>
        <%: Model.ValorInicial.ToString("c")%>
    </td>
    <td>
        <%: Model.ValorPolizaSeguro.ToString("c")%>
    </td>
    <td class="action" style="text-align: center;">
        <% if (!Model.IsReadOnly) {%>
            <%if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Power User") && !Context.User.IsInRole("Checker")){%>
                <%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper, useRepository = true, isReadOnly = true })%>
                <% if (Model.InternalStatus != 3) {%>
                    <%:Html.ActionLink("Editar", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper, useRepository = true, isReadOnly = false })%>
                    <%:Html.ActionLink("Borrar", "Delete", new { id = Model.Key, categoriaSuperId = Model.CategoriaSuper })%>
                    <a id="changeTypeFor_<%:Model.Key.ToString()%>" href="#" class="changeType">Cambiar Tipo</a>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("#changeTypeFor_" + '<%:Model.Key.ToString()%>').click(function () {
                                var garantiaId = '<%:Model.Key%>';
                                var currentType = '<%:Model.CategoriaSuper%>';
                                changeTypeGarantia(garantiaId, currentType);
                                return false;
                            });
                        });
                    </script>
                <% }%>
            <%} else {%>
                <%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper, useRepository = true, isReadOnly = true })%>
            <%}%>
        <%} else {%>
            <%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper, useRepository = true, isReadOnly = true })%>
        <%}%>
    </td>
</tr>
