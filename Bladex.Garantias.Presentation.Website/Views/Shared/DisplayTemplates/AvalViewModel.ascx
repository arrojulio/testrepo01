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
                    <%:Model.Nombre%>
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
                    <%: string.Format("{0}", Model.EsCliente ? "Si" : "No")%>
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
                    <%:Model.TipoAval.Nombre%>
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
                    <%: Model.Pais != null ? Model.Pais.Nombre : "(empty)"%>
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
                    <%:Model.PorcentajeCobertura.ToString("F2")%>
                </div>
            </td>
        </tr>
    </tbody>
</table>