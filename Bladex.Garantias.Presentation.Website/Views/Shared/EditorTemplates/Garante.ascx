<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ClienteViewModel>" %>
<%@ Import Namespace="System.Globalization" %>
<%= Html.HiddenFor(o=>o.Nombre) %>
<% Html.Telerik().ComboBoxFor(m => m.Key)
    .AutoFill(true)    
    .BindTo(new SelectList(Model.List, "Value", "Text",Model.Key))        
    .ClientEvents(c => c.OnChange("onGaranteChange").OnLoad("onDropdownChange"))
    .Value(Model.Key)    
    .Filterable(filtering =>
    {
    if (true)
    {
        filtering.FilterMode(AutoCompleteFilterMode.Contains);
    }
    })
    .HighlightFirstMatch(true).Encode(false)    
    .Render();
    %>
<i><%= Html.EditorFor(m => m.NationalId, "Span")%></i>
<div class="window">
    <% Html.Telerik().Window()
    .Name("Window_" + ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
    .Title("Formulario de ingreso de Garantes")
    .Visible(false)
    .Draggable(false)
    .Resizable(resizing => resizing
        .Enabled(false)
        .MinHeight(250)
        .MinWidth(250)
        .MaxHeight(500)
        .MaxWidth(500)
    )
    .Scrollable(false)
    .Modal(true)
    .Buttons(b => b.Close())
    .Content(() =>
    {%>
            <table border="0" cellpadding="2" cellspacing="2" style="width:100%;">
                <tr>
                    <%=string.Format("<td><p style='text-align: left'><label for='txtName_{1}'>Nombre:</label></p></td><td><p style='text-align: left'><input id='txtName_{0}' name='txtName_{1}' type='text' /></p></td>", ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty), ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))%>        
                </tr>
                <tr>
                    <td colspan="2">
                        <%=string.Format("<input id='btnSubmit_{0}' type='button' name='btnSubmit_{1}' class='t-group' style='width:100%;' value='Guardar Garante Dummy'/>", ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty), ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))%>            
                    </td>
                </tr>
            </table>
        <br />
        
    <%})
    .Width(300)
    .Height(115)
    .Render();
    
%>

<% Html.Telerik().ScriptRegistrar().OnDocumentReady(() =>
    { %>
        <script type="text/javascript">
            function clearSelection_<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>(a,b,c,d,e) {
                var hiddenElement = $('#hidden_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                var divContainer = $('#div_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                hiddenElement.val("");
                divContainer.hide();
                onClientChange(a,b,c,d,e);
            }
            function init_<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>() {
                var windowElement = $('#Window_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                var undoButton = $('#undo_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                var submitButton = $('#btnSubmit_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                var hiddenElement = $('#hidden_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                var txtElement = $('#txtName_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                var divContainer = $('#div_' + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>');
                var dropdown = $("#" + '<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>' + "_Key");

                submitButton
                    .bind('click', function (e) {
                        var gName = $(txtElement).val().toString().toUpperCase();
                        $(hiddenElement).val(gName);
                        $(txtElement).val("");
                        windowElement.data('tWindow').close();
                        var combobox = $(dropdown).data('tComboBox');
                        var result = combobox.data.push({ NationalId: "N/A", IsInternal: true, Text: gName, Value: "-1" });
                        combobox.select(result - 1);
                        $(divContainer).html("<i>El Garante Dummy " + $(hiddenElement).val() + " será generado.</i>");
                        divContainer.show();
                    });

                undoButton
                    .bind('click', function (e) {
                        e.preventDefault();
                        logInBrowser('opening window ' + $(windowElement).attr('id'));
                        windowElement.data('tWindow').center().open();                        
                        undoButton.hide();
                    });

                    windowElement.bind('close', function () {
                        logInBrowser('closing window ' + $(windowElement).attr('id'));
                        undoButton.show();
                        divContainer.hide();
                });
            }
            init_<%=ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty)%>();

        </script>
    <%}); %>

</div>   

<%=string.Format("<input type='button' id='undo_{0}' name='undo_{1}' class='t-group' style='width:100%;' value='Ingresar un nuevo garante' />", ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty), ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))%>
<%=string.Format("<input id='hidden_{0}' type='hidden' name='hidden_{1}' />", ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty), ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))%>
<%=string.Format("<div id='div_{0}' visible='false'>", ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty))%>
<%=string.Format("</div>")%>