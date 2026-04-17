<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.GarantiaMuebleViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Eliminacion
</asp:Content>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeaderContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Eliminacion de <%:ViewData["CategoriaSuperTitle"]%></h2>

    <h3>Desea eliminar esta garantia?</h3>
    <% using (Html.BeginForm()) { %>
        <p>
            <%:Html.HiddenFor(model => model.Garantia.Key)%>
		    <%if (!Model.Garantia.CategoriaSuper.IsReadOnly){%>
            <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User")) {%>
                <input type="submit" value="Eliminar" /> | 
            <%}%>
        <%}%>
		    <%: Html.ActionLink("Regresar al listado", "Index", new { categoriaSuperId = ViewData["categoriaSuperId"] })%>
        </p>
    <% } %>
    <%: Html.DisplayFor(model => model.Garantia)%>

    <% using (Html.BeginForm()) { %>
        <p>
            <%:Html.HiddenFor(model => model.Garantia.Key)%>
		    <%if (!Model.Garantia.CategoriaSuper.IsReadOnly){%>
            <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User")) {%>
                <input type="submit" value="Eliminar" /> | 
            <%}%>
        <%}%>
		    <%: Html.ActionLink("Regresar al listado", "Index", new { categoriaSuperId = ViewData["categoriaSuperId"] })%>
        </p>
    <% } %>
</asp:Content>