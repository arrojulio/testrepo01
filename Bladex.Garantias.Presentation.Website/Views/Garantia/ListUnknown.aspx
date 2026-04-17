<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow>>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Listado de Garantías
</asp:Content>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeaderContent" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            makeNiceTable("tblGarantiasList");
        });
    </script>

    <style type="text/css">
        .dataTables_filter {
            display: none;
        }
    </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm()){ %>
    <h2>Listado de Garantías</h2>
    <%: Html.TextBox("SearchText")%>        
        <input type="button" id="btnSearch" value="Search"></input>
    <table id="tblGarantiasList" cellspacing="0" cellpadding="5">
        <thead>
            <tr>
                <th>[Key]</th>
                <th>Identificador Garantía</th>
                <th>Cliente (Bladex)</th>
                <th>Categoría de Garantía</th>
                <th>Tipo de Garantía</th>
                <th>Valor Inicial</th>
                <th>Valor Mercado</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
        

        <%foreach(var garantia in Model){ %>
            <%Html.RenderPartial("DisplayTemplates/GarantiaBaseTableRow", garantia as Bladex.Garantias.DomainModel.DomainBase.GarantiaBaseRow);%>
        <%} %>
        </tbody>
    </table>
    <div>
        <%: Html.ActionLink("Volver al Inicio", "Index") %>
    </div>
    <%} %>

        <script type="text/javascript">
            $(document).ready(function () {
                $("#btnSearch").click(function () {
                    $('form').submit();
                });
            });
    </script>
</asp:Content>
