<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>" %>
<tr>
    <td>
        <%: Model.Key %>
    </td>
    <td>
        <%: string.IsNullOrEmpty(Model.GetIdentificacionDocumentoGarantia()) ? string.IsNullOrEmpty(Model.FCCReference) ? "(vacio)" : Model.FCCReference : Model.GetIdentificacionDocumentoGarantia()%>        
    </td>
    <td>
        <%: Model.Cliente != null ? Model.Cliente.Nombre : string.Empty%>
    </td>
    <td>
        <%: Model.TipoGarantiaSuper != null ? Model.TipoGarantiaSuper.Nombre : string.Empty%>
    </td>
    <td>
        <%: Model.ValorInicial.ToString("c")%>
    </td>
    <td>
        <%//TODO: Colocar Valor Pignorado %>
        <%: Model.ValorMercado.ToString("c")%>
    </td>
    <td class="action" style="text-align: center;">
        <% if (!Model.CategoriaSuper.IsReadOnly) {%>
            <%if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Power User")  && !Context.User.IsInRole("Checker")){%>
                <%:Html.ActionLink("Ver", "Edit", new { id = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString(), useRepository = true,isReadOnly=true })%>
                <% if ((int)Model.InternalStatus.Key!=3) {%>
                    <%:Html.ActionLink("Editar", "Edit", new { id = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString(), useRepository = true, isReadOnly = false})%>
                <%}%>
                <%:Html.ActionLink("Borrar", "Delete", new {id = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString()})%>
                <a id="changeTypeFor_<%:Model.Key.ToString()%>" href="#" class="changeType">Cambiar Tipo</a>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#changeTypeFor_" + '<%:Model.Key.ToString()%>').click(function () {
                            var garantiaId = '<%:Model.Key%>';
                            var currentType = '<%:Model.CategoriaSuper.Key.ToString()%>';
                            changeTypeGarantia(garantiaId, currentType);

                            return false;
                        });
                    });
                </script>
            <%} else {%>
                <%:Html.ActionLink("Ver", "Edit", new { id = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString(), isReadOnly = true })%>
            <%}%>
        <%} else {%>
        <%:Html.ActionLink("Ver", "Edit", new { id = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString(), isReadOnly = true })%>
        <%}%>
    </td>
</tr>