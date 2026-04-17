<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.GarantiaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Creacion de garantias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Crear <%:Model.CategoriaSuperSelected.Nombre %></h2>

    <%Html.BeginForm(Model.CreateAction, "Garantia"); %>
    <%:Html.EditorFor(m => m.Garantia,this.Model.Garantia)%>
    <% if (!Model.Garantia.CategoriaSuperSelected.IsReadOnly){%>
    <input type="submit" value="Salvar"  />
    <%}%>
    <input type="button" value="Cancelar" onclick="javascript: goBack();" />
    <%Html.EndForm(); %>

</asp:Content>
