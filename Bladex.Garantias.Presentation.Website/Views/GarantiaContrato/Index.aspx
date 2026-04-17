<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<Bladex.Garantias.Presentation.Website.Models.GarantiaContratoModel>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeaderContent">
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">

    
    <%: Html.DisplayFor(m => m, "GarantiaContratoTable", new { ReadOnly = ((int?)ViewBag.GarantiaId).HasValue, useRepository = (bool?)ViewBag.UseRepository })%>
    <p>
        <%if (!this.ViewBag.IsReadOnly) {%>
            <%if (this.ViewBag.AvailableContracts && ((int?)ViewBag.OperationId).HasValue) {%>
            <%--<%:Html.ActionLink("Seleccionar nuevo contrato", "Create", "GarantiaContrato", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = (bool?)ViewBag.UseRepository }, new { })%>--%>
            <%:Html.ActionLink("Seleccionar nuevo contrato", "Create", "GarantiaContrato", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = false }, new { })%>
            |
            <% } else if (!this.ViewBag.AvailableContracts)
              {%>
                <i><%:this.ViewBag.AvailableContractsMessage %></i><br /><br />
            <%} else { %>
                <i>Para realizar modificaciones sobre los contratos asociados a una garantía primero debe salvar la misma.</i><br /><br />
            <% } %>
                <% if (((int?)ViewBag.GarantiaId).HasValue) { %>
                    <%--<%:Html.ActionLink("Regresar a la garantia", "Edit", "Garantia", new { garantiaId = ((int?)ViewBag.GarantiaId).Value, operationId = (int?)ViewBag.OperationId, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = (bool?)ViewBag.UseRepository }, new { })%>--%>
                    <%:Html.ActionLink("Regresar a la garantia", "Edit", "Garantia", new { garantiaId = ((int?)ViewBag.GarantiaId).Value, operationId = (int?)ViewBag.OperationId, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = false}, new { })%>
                 <% } %>           
        <% }  else {%>
                <%:Html.ActionLink("Regresar a la garantia ", "Edit", "Garantia", new { garantiaId = ((int?)ViewBag.GarantiaId).Value, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = true, isReadOnly = true }, new { })%>
        <% } %>          

        <%: Html.ActionLink("Regresar al Inicio", "Index", "Garantia")%>
    </p>
    <%: Html.Hidden("Hidden_OperationId",(int?)ViewBag.OperationId) %>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Hidden_CurrentOperation').val($('#Hidden_OperationId').val());
        });
    </script>
</asp:Content>
