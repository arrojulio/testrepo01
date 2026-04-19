<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.GarantiaContratoViewModel>" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeaderContent"></asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">


<%:Html.Hidden("garantiaId", (int?)ViewBag.GarantiaId) %>

        <%:Html.Hidden("operationId", (int?)ViewBag.OperationId) %>
 <%if ((bool)this.ViewData["EDIT_SUCCESSFULL"])
      {%>
                               
        <p style="color:Green;">Asociación actualizada correctamente.</p>        
        <fieldset>
        <%if(Model.GarantiaContratoModel.ID==0)  {%>
            <legend>Asociación pendiente de aprobación</legend>                
        <%}
        else
        {%>
            <legend>Asociación N°<%:Html.DisplayTextFor(m => m.GarantiaContratoModel.ID)%></legend>
        <%}%>        
                    
        <%:Html.DisplayFor(m=>m.GarantiaContratoModel) %>        
        </fieldset>
        <%:Html.ActionLink("Volver al listado de Contratos", "Index", new { garantiaId = (int?)ViewBag.GarantiaId, operationId = (int?)ViewBag.OperationId, categoriaSuperId = (string)ViewBag.CategoriaSuperId, useRepository = (bool?)ViewBag.UseRepository })%>
    <%}
      else
      {%>

    <%--<h2>Edicion de <%: Html.LabelFor(m=>m.GarantiaContratoModel.ID) %> <%:Html.DisplayTextFor(m => m.GarantiaContratoModel.ID)%></h2>--%>
    <h2>Edicion de Asociación <%: ViewBag.LabelGarantiaContrato %></h2>
    
    <%
          using (Html.BeginForm())
          {%>
        <%:Html.ValidationSummary(true)%>
        <%:Html.Hidden("categoriaSuperId", (string)ViewBag.CategoriaSuperId) %>
        <%:Html.Hidden("garantiaId", (int?)ViewBag.GarantiaId) %>
        <%:Html.Hidden("operationId", (int?)ViewBag.OperationId) %>
        <%:Html.Hidden("useRepository", (bool?)ViewBag.UseRepository) %>
        <fieldset>
        <%if(Model.GarantiaContratoModel.ID==0)  {%>
            <legend>Asociación pendiente de aprobación</legend>
        <%}
        else
        {%>
            <legend>Asociación N° <%:Html.DisplayTextFor(m=>m.GarantiaContratoModel.ID)%></legend>
        <%}%>  
            <%:Html.EditorFor(m => m.GarantiaContratoModel) %>
        </fieldset>
        <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User")){%>
        <input id="btnSave" type="submit" value="Save" />
        <%}%>
        <input id="btnCancel" type="button" value="Cancelar" onclick="javascript: goBack();"  />

    <%
          }%>
          <%
      }%>
      
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Hidden_CurrentOperation').val($('#operationId').val());

            var flagChangesetViewer = $('#changeset-viewer-action').length;
            //alert(flagChangesetViewer);

            $("#btnSave").click(function () {
                //alert('click btnSave');
                $("#changeset_viewer_container").css('display', '');                
            });
        });
    </script>
      
</asp:Content>

