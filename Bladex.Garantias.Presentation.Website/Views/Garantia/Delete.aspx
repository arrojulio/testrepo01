<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.DomainModel.DomainBase.GarantiaBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Eliminacion de Garantía
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Garantía <%:Html.Label(Model.GetIdentificacionDocumentoGarantia()) %> (Key: <%:Html.Label(Model.Key.ToString()) %>)</h2>

    <%Html.BeginForm(new { garantiaId = Model.Key.ToString() }); %>
    <%if (!Model.CategoriaSuper.IsReadOnly)
      {%>
    <%
          if (User.Identity.IsAuthenticated && User.IsInRole("Power User"))
          {%>
    <div>
        <p>
            Desea eliminar la garantia?
        </p>
        <input type="submit" value="Si" />
        <input type="reset" value="No" />
    </div>
    <%}%>
    <%}%>
    <%Html.EndForm(); %>
</asp:Content>
