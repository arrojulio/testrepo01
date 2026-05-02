<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.MakerCheckerDetailViewModel>" %>
<%-- Paginacion reutilizable para MakerChecker/Details --%>
<nav class="pager" aria-label="Paginacion de operaciones">
    <%:Html.ActionLink("Primera", "Details", "MakerChecker",
        new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = 1 },
        new { @class = "pager-number pager-number-first", title = "Primera pagina" })%>

    <%for (int index = 0; index < Model.TotalPages; index++) {%>
        <% if ((index + 1) == Model.Page) {%>
            <span class="pager-number pager-number-selected" aria-current="page"><%=Model.Page %></span>
        <%} else {%>
            <%:Html.ActionLink((index + 1).ToString(), "Details", "MakerChecker",
                new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = (index + 1) },
                new { @class = "pager-number" })%>
        <%}%>
    <%}%>

    <%:Html.ActionLink("Ultima", "Details", "MakerChecker",
        new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = Model.TotalPages },
        new { @class = "pager-number pager-number-last", title = "Ultima pagina" })%>
</nav>
