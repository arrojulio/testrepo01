<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Creación de Avales
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% if ((bool)this.ViewData["CREATE_SUCESSFULL"]) {%>
    <p style="color:Green">Aval generado correctamente.</p>
    <fieldset>
    <legend>Aval</legend>
    <%:Html.DisplayFor(m => m) %>
    </fieldset>
    <%:Html.ActionLink("Volver al listado de Avales", "Index", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = ViewBag.CategoriaSuperId }) %>
<%} else {%>
    <h2>Creación de Aval</h2>

    <% using (Html.BeginForm()) {%>
        <%:Html.ValidationSummary(true)%>
        <fieldset>
            <legend>Aval</legend>
            <%:Html.EditorFor(m => m) %>
        </fieldset>
        <%:Html.Hidden("garantiaId", (int?)ViewBag.GarantiaId) %>
        <%:Html.Hidden("operationId", (int?)ViewBag.OperationId) %>
        <input type="submit" value="Salvar" /><input id="btnCancel" type="button" value="Cancelar" onclick="javascript: goBack();"  />
    <%}%>
<%}%>
</asp:Content>



