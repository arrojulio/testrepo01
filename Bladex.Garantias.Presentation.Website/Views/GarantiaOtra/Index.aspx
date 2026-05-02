<%--<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<Bladex.Garantias.DomainModel.DomainBase.GarantiaOtra>>" MasterPageFile="~/Views/Shared/Site.Master" %>--%>
<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<Bladex.Garantias.DomainModel.DomainBase.Summary.GarantiaOtraSummary>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Listado de Otras Garantías x
</asp:Content>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeaderContent" runat="server">
	<script src="<%= Url.Content("~/Scripts/index.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Listado de <%:ViewData["CategoriaSuperTitle"]%> x</h2>

    <table id="tblGarantiasList" cellspacing="0" cellpadding="5">
        <thead>
            <tr>
                <th>[Key]</th>
                <th>Identificador Garantía</th>
                <th>Cliente (Bladex)</th>
                <th>Tipo de Garantía (Bladex)</th>
                <th>Valor Inicial</th>
                <th>Valor Super Intendencia</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
        <%foreach(var garantia in Model){ %>
            <%--<%Html.RenderPartial("DisplayTemplates/GarantiaOtraTableRow", garantia as Bladex.Garantias.DomainModel.DomainBase.GarantiaBase);%>--%>
            <%Html.RenderPartial("DisplayTemplates/GarantiaOtraTableRow", garantia as Bladex.Garantias.DomainModel.DomainBase.Summary.GarantiaOtraSummary);%>
        <%} %>
        </tbody>
    </table>
    <div>
        <%if (ViewBag.CategoriaSuper != null && !ViewBag.CategoriaSuper.IsReadOnly){%>
        <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User") && !User.IsInRole("Checker")) {%>
            <%:Html.ActionLink(string.Format("Crear {0}", ViewData["CategoriaSuperTitle"].ToString()), "Create", new {CategoriaSuperId = ViewData["CategoriaSuperId"].ToString()})%> | 
        <%}%>
        <%}%>
        <%: Html.ActionLink("Volver al Inicio", "Index", "Garantia") %>
    </div>
   
  
</asp:Content>
