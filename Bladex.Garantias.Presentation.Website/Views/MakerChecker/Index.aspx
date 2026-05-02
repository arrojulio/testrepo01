<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker.MakerCheckerOperationSummary>>" MasterPageFile="~/Views/Shared/Site.Master" %>
<%@ Import Namespace="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    MakerChecker — Changesets
</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeaderContent">
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">

    <div class="d-flex align-items-center gap-2 mb-3">
        <h2 class="mb-0">MakerChecker</h2>
        <span class="blx-status-badge blx-status-badge--pending">Changesets pendientes</span>
    </div>

    <%-- Grid Telerik: conservado tal cual (unico uso de Telerik Grid en el proyecto).
         Los estilos del Grid se actualizan en bootstrap-overrides.css (.t-grid). --%>
    <%Html.Telerik().Grid(Model)
        .Name("changeset_summary_table")
        .Columns(columns =>
        {
            columns.Bound(c => c.CustomerName).Title("Cliente").Sortable(true).Filterable(true);
            columns.Bound(c => c.Garante).Title("Garante").Sortable(true).Filterable(true);
            columns.Bound(c => c.GarantiaId).Title("Nro Garantia").Format("{0}").Sortable(true).Filterable(true);
            columns.Bound(c => c.TipoGarantia).Title("Tipo Garantia SBP");
            columns.Bound(c => c.ValorGarantia).Title("Valor Garantia").Format("{0:c}").Sortable(true).Filterable(true);
            columns.Bound(c => c.MakerUserId).Title("Maker User").Sortable(true).Filterable(true);
            columns.Bound(c => c.ChangesetCommitDate).Title("Fecha Envio").Filterable(true).Sortable(true).Format("{0:d}");
            columns.Template(c =>
            { %> <%=string.Format("{0}", c.RelatedDeals)%> <% }).Title("# Deals");
            columns.Bound(c => c.ChangesetComment).Title("Comentarios");
            columns.Template(c =>
            { %> <%=Html.ActionLink("Ver detalle", "Details", "MakerChecker", new { ChangesetId = c.ChangesetId.ToString(), Page = 1 }, new { @class = "btn btn-primary btn-sm" })%> <% }).Title("Acciones");
        })
        .Sortable()
        .Pageable(paging => paging.PageSize(10))
        .Filterable()
        .Render();
    %>

</asp:Content>
