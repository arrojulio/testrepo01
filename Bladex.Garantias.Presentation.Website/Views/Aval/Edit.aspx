<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%if ((bool)this.ViewData["EDIT_SUCESSFULL"]) {%>
    <p style="color:Green;">Aval guardado correctamente.</p>
    <fieldset><legend>Aval</legend>
        <%:Html.DisplayFor(m=>m) %>
    </fieldset>
    <%:Html.ActionLink("Volver al listado de Avales", "Index", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId}) %>
<%} else {%>
    <h2>Edicion de Aval</h2>
    <% using (Html.BeginForm()) {%>
        <%:Html.ValidationSummary(true)%>
        <fieldset>
            <legend>Aval</legend>
            <%:Html.EditorFor(m => m) %>
        </fieldset>
        <%:Html.Hidden("garantiaId", (int?)ViewBag.GarantiaId) %>
        <%:Html.Hidden("operationId", (int?)ViewBag.OperationId) %>
        <input type="submit" value="Save" /><input id="btnCancel" type="button" value="Cancelar" onclick="javascript: goBack();"  />
    <%}%>
<%}%>

</asp:Content>
