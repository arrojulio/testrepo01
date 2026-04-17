<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GarantiaIndexViewModel>" %>

<%@ Import Namespace="Bladex.Garantias.DomainModel.DomainBase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Seleccion de tipo de garantia
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Seleccion de tipo de garantia</h2>
    <div id="garantiasMenu">
    <%:Html.Telerik().PanelBar()
                             .Name("PanelBar").ExpandMode(PanelBarExpandMode.Multiple).ExpandAll(true)
                             /*.HtmlAttributes(new { style = "width: 300px;" })*/
                                                                                     .BindTo(Model.Menu, mappings =>
                                                                                     {
                                                                                         mappings.For<GarantiasMenu>(binding => binding
                                                                                                 .ItemDataBound((item, category) =>
                                                                                                 {
                                                                                                     item.Text = category.Label;
                                                                                                     item.ControllerName = category.IsParent ? category.ChildrenItems[0].ControllerName : "Garantia";
                                                                                                     item.ActionName = category.IsParent ? category.ChildrenItems[0].ActionName : "Index";
                                                                                                 })
                                                                                                 .Children(category => category.ChildrenItems));
                                                                                         mappings.For<GarantiasMenuItem>(binding => binding
                                                                                                 .ItemDataBound((item, categoryItem) =>
                                                                                                 {
                                                                                                     item.Text = categoryItem.Label;
                                                                                                     item.ControllerName = categoryItem.ControllerName;
                                                                                                     item.ActionName = categoryItem.ActionName;
                                                                                                     item.RouteValues.Add("categoriaSuperId", categoryItem.Value);
                                                                                                 }));
                                                                                     })
%>
</div>
</asp:Content>
