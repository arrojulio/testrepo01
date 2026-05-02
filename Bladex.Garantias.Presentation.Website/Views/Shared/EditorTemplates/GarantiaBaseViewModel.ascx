<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.GarantiaBaseViewModel>" %>
<% if (ViewData.TemplateInfo.TemplateDepth > 1) { %>
    <%= ViewData.ModelMetadata.SimpleDisplayText %>
<% } else { %>
    <%-- Layout de campos: tabla de dos columnas (label | field).
         Preservado para compatibilidad con los formularios de Garantia existentes.
         El markup se modernizo para eliminar atributos obsoletos y agregar semantica. --%>
    <table class="guaranteeTable w-100">
    <% foreach (var prop in ViewData.ModelMetadata.Properties.Where(pm => pm.ShowForEdit && !ViewData.TemplateInfo.Visited(pm))) { %>
        <% if (prop.HideSurroundingHtml) { %>
            <%= Html.Editor(prop.PropertyName) %>
        <% } else { %>
            <tr class="guaranteeRow">
                <td class="editor-label">
                    <% if (prop.IsRequired) { %>
                        <span class="blx-required" aria-label="Campo requerido" title="Campo requerido">*</span>
                    <% } %>
                    <%= Html.Label(prop.GetDisplayName()) %>
                </td>
                <td class="editor-field">
                    <%= Html.Editor(prop.PropertyName) %>
                    <%= Html.ValidationMessage(prop.PropertyName, "*") %>
                </td>
            </tr>
        <% } %>
    <% } %>
    </table>
<% } %>
