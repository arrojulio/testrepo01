<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Bladex.Garantias.Presentation.Website.ViewModels.CategoriaSuperViewModel>" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Desvincular Garantías
</asp:Content>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeaderContent" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSubmit").click(function () {
                if (confirm("¿Desea desvincular el tipo de categoría?")) {
                    $("#lblResult_Dis").text("");
                    var idTipoCategoriaSuper = $("#disTipoCategoriaSuper").val();
                    var parameters = "{ }";
                    var objUrl = baseUrl + "/Garantia/DisableGuaranteeType?id=" + idTipoCategoriaSuper;
                    $.ajax({
                        type: "GET",
                        url: objUrl,
                        data: parameters,
                        contentType: "application/json; charset=utf-8", // charset=utf-8",
                        dataType: "text",
                        cache: false,
                        success: function (data) {
                            var res = data.split(";");
                            if (res[0] == 1) {
                                $("#lblResult_Dis").addClass("resValid");
                                $("#lblResult_Dis").removeClass("resError");
                                $("#lblResult_Dis").text(res[1]);
                            }
                            else {
                                $("#lblResult_Dis").addClass("resError");
                                $("#lblResult_Dis").removeClass("resValid");
                                $("#lblResult_Dis").text(res[1]);
                            }

                            $("#disCategoriaSuper").data("tDropDownList").select(0)
                        },
                        error: function (request, status, error) {
                            alert('Se ha producido un error');
                        }
                    });
                }
            });
        });

        function onDisCategoriaSuperChange() {
            var idCategoriaSuper = $("#disCategoriaSuper").val();
            var parameters = "{ }";
            var objUrl = baseUrl + "/Garantia/GetTipoGarantiaSuperById?id=" + idCategoriaSuper;
            $.ajax({
                type: "GET",
                url: objUrl,
                data: parameters,
                contentType: "application/json; charset=utf-8", // charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (data) {
                    var ddlTipoCat = $("#disTipoCategoriaSuper").data("tDropDownList");
                    ddlTipoCat.dataBind(data);
                },
                error: function (request, status, error) {
                    alert('Se ha producido un error');
                }
            });
        }
    </script>

    <style type="text/css">
        .resError
        {
            color:Red;
        }
        
        .resValid
        {
            color:Green;
        }
    </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Desvincular tipos de Garantías</h2>
    <fieldset> 
    <table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0">            
        <tbody>   
            <tr class="guaranteeRow">
                <td>
                    <div class="editor-label">
                        Categoría Super
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                        <%= Html.Telerik().DropDownListFor(m => m.Key).Encode(false)
                            .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
                            .Name("disCategoriaSuper")
                            .ClientEvents(c => c.OnChange("onDisCategoriaSuperChange"))
                            .HtmlAttributes(new { style="min-width:300px;" })%>
                    </div>
                </td>
            </tr>
            <tr class="guaranteeRow">
                <td>
                    <div class="editor-label">
                        Tipo Categoría Super
                    </div>
                </td>
                <td>
                    <div class="editor-field">
                    <%= Html.Telerik().DropDownList()
                        .Name("disTipoCategoriaSuper")
                        .BindTo(new SelectList(new[] { new { Value = "", Text = "" } }, "Value", "Text"))
                        .HtmlAttributes(new { style="min-width:300px;" })%>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    </fieldset>

    <br /><br />
    <%if (User.Identity.IsAuthenticated && User.IsInRole("Power User") && !User.IsInRole("Checker"))
        {%>
        <input id="btnSubmit" type="submit" value="Desvincular" />
    <%} %>
    <br />
    <label id="lblResult_Dis"></label>
    <br /><br />
    <div>
        <%: Html.ActionLink("Volver al Inicio", "Index") %>
    </div>
</asp:Content>