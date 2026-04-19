<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Listado de Garantías Eliminadas
</asp:Content>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeaderContent" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            makeNiceTable("tblGarantiasList");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Listado de Garantías Eliminadas</h2>

    <table id="tblGarantiasList" cellspacing="0" cellpadding="5">
        <thead>
            <tr>
                <th>[Key]</th>
                <th>Identificador Garantía</th>
                <th>Cliente (Bladex)</th>
                <th>Tipo de Garantía (Super)</th>
                <th>Valor Inicial</th>
                <th>Valor Mercado</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
        <%foreach(var garantia in Model){ %>
            <%Html.RenderPartial("DisplayTemplates/GarantiaBaseTableRowDeleted", garantia as Bladex.Garantias.DomainModel.DomainBase.GarantiaBase);%>
        <%} %>
        </tbody>
    </table>
    <div>
        <%: Html.ActionLink("Volver al Inicio", "Index") %>
    </div>

</asp:Content>

