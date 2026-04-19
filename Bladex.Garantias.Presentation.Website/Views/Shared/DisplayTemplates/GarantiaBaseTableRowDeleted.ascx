<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>" %>
<tr>
    <td>
        <%: Model.Key %>
    </td>
    <td>
        <%: string.IsNullOrEmpty(Model.GetIdentificacionDocumentoGarantia()) ? "(vacio)" : Model.GetIdentificacionDocumentoGarantia()%>
    </td>
    <td>
        <%: Model.Cliente != null ? Model.Cliente.Nombre : string.Empty%>
    </td>
    <td>
        <%: Model.CategoriaSuper != null ? Model.CategoriaSuper.Nombre : string.Empty%>
    </td>
    
    <td>
        <%: Model.ValorInicial.ToString("c")%>
    </td>
    <td>
        <%: Model.ValorMercado.ToString("c")%>
    </td>
    <td class="action" style="text-align: center;">
        <a id="changeStatusFor_<%:Model.Key.ToString()%>" href="#" class="changeStatus">Activar</a>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#changeStatusFor_" + '<%:Model.Key.ToString() %>').click(function () {
                    var garantiaId = '<%:Model.Key %>';
                    changeStatusGarantia(garantiaId);
                    return false;
                });
            });
        </script>
    </td>
</tr>
