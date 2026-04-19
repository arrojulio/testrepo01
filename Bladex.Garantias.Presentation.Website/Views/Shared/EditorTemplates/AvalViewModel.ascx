<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.AvalViewModel>" %>
<%:Html.HiddenFor(m => m.GarantiaId)%>
<%:Html.HiddenFor(model => model.Key)%>
<style type="text/css">
    .editor-label
    { width:180px;}
    .editor-field { width:388px; }
</style>
<table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0" >
    <tbody>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.Nombre)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.EditorFor(model => model.Nombre)%>
                    <%:Html.ValidationMessageFor(model => model.Nombre)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.EsCliente)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.CheckBoxFor(model => model.EsCliente)%>
                    <%:Html.ValidationMessageFor(model => model.EsCliente)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.TipoAval)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.EditorFor(model => model.TipoAval)%>
                    <%:Html.ValidationMessageFor(model => model.TipoAval)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.Pais)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.EditorFor(model => model.Pais)%>
                    <%:Html.ValidationMessageFor(model => model.Pais)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">
            <td>
                <div class="editor-label">
                    <%:Html.LabelFor(model => model.PorcentajeCobertura)%>
                </div>
            </td>
            <td>
                <div class="editor-field">
                    <%:Html.EditorFor(model => model.PorcentajeCobertura)%>
                    <%:Html.ValidationMessageFor(model => model.PorcentajeCobertura)%>
                </div>
            </td>
        </tr>
    </tbody>
</table>
