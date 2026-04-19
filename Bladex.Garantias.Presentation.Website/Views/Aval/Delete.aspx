<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Eliminacion de Aval
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Eliminacion de Aval</h2>
    <h3>Esta seguro que desea eliminar este Aval?</h3>
    <% using (Html.BeginForm()) { %>
    <fieldset>
        <legend>Aval</legend>
        <%=Html.DisplayFor(m=>m) %>
    </fieldset>
    <%:Html.Hidden("garantiaId", (int?)ViewBag.GarantiaId) %>
    <%:Html.Hidden("operationId", (int?)ViewBag.OperationId) %>
    <p>
		<input type="submit" value="Eliminar" />
        <input type="button" value="Cancelar" onclick="javascript: goBack();" />
    </p>
    <% } %>

</asp:Content>