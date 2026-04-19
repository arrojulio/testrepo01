<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.CategoriaSuperChangeViewModel>" %>
<div id="changeTypeModal" title="Cambio de Tipo de Garantia">

<div>
    <%:Html.HiddenFor(m=>m.Garantia) %>
    <%:Html.HiddenFor(m=>m.CurrentCategoriaSuperId) %>
    
    <table>
        <tr>
            <td>
                ¿Desea cambiar el tipo de garantia para la garantía con identificador <b><%:Model.Garantia.Key%></b>?        
            </td>
        </tr>
        <tr>
            <td>
                Seleccione el nuevo tipo de garantía:            
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <%:Html.DropDownListFor(m => m.NewCategoriaSuperId, Model.CategoriaSuperList)%>
            </td>
        </tr>
        <tr>
            <td><b>Advertencias</b></td>
        </tr>
        <tr><td><i style="color:Red">Los campos especificos de la garantia original se perderán.</i></td></tr>
        <tr><td><i style="color:Red">El Tipo de Garantia Super será modificado al valor N/A.</i></td></tr>
        <tr>
            <td style="text-align: center">
                <input id="btnChangeTypeSubmit" type="button" value="Cambiar" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnChangeTypeSubmit").click(function () {
            var garantiaId = '<%:Model.Garantia.Key %>';
            var currentType = '<%:Model.Garantia.CategoriaSuper.Key.ToString() %>';
            var newType = $("#NewCategoriaSuperId").val();
            submitChangeTypeGarantia(garantiaId, currentType, newType);
            return false;
        });
    });
</script>
</div>

