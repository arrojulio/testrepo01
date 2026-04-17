<%--<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>" %>--%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.DomainModel.DomainBase.Summary.GarantiaOtraSummary>" %>
<tr>
    <td>
        <%: Model.Key %>
    </td>
    <td>
        <%--<%: string.IsNullOrEmpty(Model.GetIdentificacionDocumentoGarantia) ? string.IsNullOrEmpty(Model.FCCReference) ? "(vacio)" : Model.FCCReference : Model.GetIdentificacionDocumentoGarantia()%>        --%>
        <%: Model.IdentificacionDocumentoGarantia %>
    </td>
    
    <td>
        <%--<%: Model.Cliente != null ? Model.Cliente.Nombre : string.Empty%>--%>
        <%: Model.Cliente != null ? Model.Cliente : string.Empty%>
    </td>
    <td>
        <%--<%: Model.TipoGarantiaSuper != null ? Model.TipoGarantiaSuper.Nombre : string.Empty%>--%>
        <%: Model.TipoGarantiaSuper != null ? Model.TipoGarantiaSuper : string.Empty%>
    </td>
    <td>
        <%: Model.ValorInicial.ToString("c")%>
    </td>
    <td>
        <%// TODO Agregar "Valor Libros" %>
        <%--<%: Model.GetValorGarantiaSuperIntendencia().ToString("c")%>--%>
        <%: Model.ValorGarantiaSuperIntendencia.ToString("c")%>
        
    </td>
    <td class="action" style="text-align: center;">
        <% if (!Model.IsReadOnly) {%>
            <%if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Power User") && !Context.User.IsInRole("Checker")){%>
                <%--<%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString(), useRepository = true,isReadOnly = true })%>--%>
                <%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper.ToString(), useRepository = true,isReadOnly = true })%>
                <%--<% if (Convert.ToInt32(Model.InternalStatus.Key) != 3)--%>
                <% if (Convert.ToInt32(Model.InternalStatus) != 3)
                   {%>
                    <%--<%:Html.ActionLink("Editar", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString(), useRepository = true, isReadOnly = false })%>--%>
                    <%:Html.ActionLink("Editar", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper.ToString(), useRepository = true, isReadOnly = false })%>

                    <%--<%:Html.ActionLink("Borrar", "Delete", new {id = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString()})%>--%>
                    <%:Html.ActionLink("Borrar", "Delete", new {id = Model.Key, categoriaSuperId = Model.CategoriaSuper.ToString()})%>
                    <a id="changeTypeFor_<%:Model.Key.ToString()%>" href="#" class="changeType">Cambiar Tipo</a>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("#changeTypeFor_" + '<%:Model.Key%>').click(function () {
                                var garantiaId = '<%:Model.Key%>';              
                                var currentType = '<%:Model.CategoriaSuper%>'
                                changeTypeGarantia(garantiaId, currentType);

                                return false;
                            });
                        });
                    </script>
                <% }%>
                
            <%} else {%>
                <%--<%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString() })%>--%>
                <%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper , useRepository = true, isReadOnly = true})%>
            <%}%>
        <%} else {%>
        <%--<%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper.Key.ToString() })%>--%>
        <%:Html.ActionLink("Ver", "Edit", new { garantiaId = Model.Key, categoriaSuperId = Model.CategoriaSuper, useRepository = true, isReadOnly = true })%>
        <%}%>
    </td>
</tr>