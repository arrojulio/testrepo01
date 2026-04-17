<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<Bladex.Garantias.DomainModel.DomainBase.Summary.GarantiaMuebleSummary>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Listado de Garantías
</asp:Content>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/jquery.dataTables.min.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/common.js") %>" type="text/javascript"></script>
	<script src="<%= Url.Content("~/Scripts/index.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Listado de <%:ViewData["CategoriaSuperTitle"]%></h2>

    <table id="tblGarantiasList" cellspacing="0" cellpadding="5">
        <thead>
            <tr>
                <th>[Key]</th>
                <th>Identificador Garantía</th>
                <th>Cliente (Bladex)</th>
                <th>Tipo Garantía (Super)</th>
                <th>Valor Inicial</th>
                <th>Valor de Cobertura de la Póliza</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
        <%foreach(var garantia in Model){ %>
            <%Html.RenderPartial("DisplayTemplates/GarantiaMuebleTableRow", garantia);%>
        <%} %>
        </tbody>
    </table>
    <div>
        <%if (!ViewBag.CategoriaSuper.IsReadOnly){%>
        <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User") && !User.IsInRole("Checker")) {%>
            <%:Html.ActionLink(string.Format("Crear {0}", ViewData["CategoriaSuperTitle"].ToString()), "Create", new {CategoriaSuperId = ViewData["CategoriaSuperId"].ToString()})%> | 
        <%}%>
        <%}%>
        <%: Html.ActionLink("Volver al Inicio", "Index", "Garantia") %>
    </div>
  

</asp:Content>
