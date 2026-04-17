<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.AvalManagerViewModel>" %>

<%@ Import Namespace="Bladex.Garantias.Presentation.Website.Models" %>
<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>
<%--<%this.ViewData["GarantiaBase.Key"] = Model.Key; %>--%>

<%@ Import Namespace="System.Globalization" %>
<%--<%:Html.ValidationSummary(false, "Han ocurrido errores")%>--%>
<style type="text/css">
       .dataTables_length {       
       float: left;       
       width: 100%;
        }
</style>
<div class="window">
    <% Html.Telerik().Window()
    .Name("Window_Admin_Avales")
    .Title("Formulario de administracion de avales")    
    .Visible(false)
    .Draggable(true)    
    .Resizable(resizing => resizing
        .Enabled(false)
        .MinHeight(700)
        .MinWidth(700)
        .MaxHeight(800)
        .MaxWidth(800)        
    )
    .Scrollable(false)
    .Modal(true)
    .Buttons(b => b.Close())    
    .Content(() =>
    {%>

<input type="hidden" id="hidden_Input_isNew" value="true" />         
<table id="tblGarantia" class="guaranteeTable" cellpadding="0" cellspacing="0" width="350px" >
    <tbody>    
        <tr class="guaranteeRow">   
            <td>
                <input type="hidden" id="hidden_Input_AvalId" value="" />
                <div class="editor-label">
                    Nombre
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
                    Es Cliente?
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
                    Tipo Aval
                </div>
            </td>
            <td>
                <div class="editor-field" id="div_ddlTipoAval">                    
                     <%:Html.EditorFor(m=>m.TipoAvalCatalog)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">   
            <td>
                <div class="editor-label">
                    Pais
                </div>
            </td>
            <td>
                <div class="editor-field" id="div_ddlPais">                    
                    <%:Html.EditorFor(m=>m.PaisCatalog)%>
                </div>
            </td>
        </tr>
        <tr class="guaranteeRow">   
            <td>
                <div class="editor-label">
                    % Cobertura
                </div>
            </td>
            <td>
                <div class="editor-field" id="div_PorcentajeCobertura">                    
                    <%:Html.EditorFor(model => model.PorcentajeCobertura,"","InputPorcentajeCobertura")%>
                    <%:Html.ValidationMessageFor(model => model.PorcentajeCobertura)%>
                </div>
                <input type="hidden" id="hidden_Input_isDirty" value="" />                              
            </td>            
        </tr>
    </tbody>
</table>
 <br/>

<input type='button' id='btnAddAval' name='btnAddAval' class='t-group' value='Salvar' />
<input type='button' id='btnNewAval' name='btnNewAval' class='t-group' value='Nuevo' />

<br/>
<table id="avalTable" cellspacing="0" cellpadding="5">
  <thead id="headerAvalTable">
		<tr>
			<th id="AvalId" style="visibility:hidden"/>            
			<th>
				Nombre
			</th>
            <th>
				Es Cliente?
			</th>
			<th>
				Pais
			</th>
			<th>
				Tipo de Aval
			</th>
			<th>
				% de Cobertura
			</th>
			<th id="action" title="action">Action</th>
            <th id="isDirty" style="visibility:hidden"/>                        
		</tr>
	</thead>
	<tbody>        

	<%         
        foreach (var item in Model.AvalList)        
        {
    %>
	
		<tr>
			<td>
                <input type="hidden" id="hidden_AvalId" value="<%: item.Key %>" />                				
			</td>            			
			<td class="dataTables_empty">
				<%: item.Nombre.Trim() %>
			</td>
            <td>
				<%: item.EsCliente==true?"Si":"No" %>
			</td>
			<td class="dataTables_empty">
				<%: item.Pais.Nombre %>
                <%: Html.Hidden("hidden_Row_Pais_Key",item.Pais.Key) %>
			</td>
			<td class="dataTables_empty">
				<%: item.TipoAval.Nombre %>                
                <%: Html.Hidden("hidden_Row_TipoAval_Key",item.TipoAval.Key) %>
                
			</td>
            <td class="dataTables_empty">
				<%: String.Format("{0:F2}", item.PorcentajeCobertura) %>
			</td>			
			<td>    				    
                <input type="button" class="DeleteRow" id="btnRemoveAval" name="btnRemoveAval" value="Borrar" />                                        
			</td>
            <td class="dataTables_empty">
				<input type="hidden" id="hidden_isDirty" value="false" />                           
			</td>                        
		</tr>
	
	<%        
     }     
      %>
	</tbody>
	</table>
    
    <input type="hidden" id="hidden_rowId" value="0" />
    <input type="hidden" id="hidden_AvalList" value="" />
    
    <br />
    
    <%})
    .Width(700)
    .Height(700)        
    .Render();        
%>

<% Html.Telerik().ScriptRegistrar().OnDocumentReady(() =>
    { %>
<script type="text/jscript">

    var oTable = makeAvalNiceTable('avalTable');

    setAvalList();
    function init_Admin_Avales() {

        $("#headerAvalTable").parent().css("width", "700px");                
        
        oTable.fnDraw();

        var windowElement = $('#Window_Admin_Avales');
        var undoButton = $('#btn_undo_Admin_Avales');
        var submitButton = $('#btnSubmit_Admin_Avales');
        var hiddenElement = $('#hidden_Admin_Avales');
        var divContainer = $('#div_Admin_Avales');

        submitButton
                    .bind('click', function (e) {
                        windowElement.data('tWindow').close();
                        divContainer.show();
                    });

        undoButton
                    .bind('click', function (e) {
                        e.preventDefault();
                        logInBrowser('opening window ' + $(windowElement).attr('id'));
                        //windowElement.data('tWindow').center().open();                        
                        $("#Window_Admin_Avales").css("position", "absolute");
                        $("#Window_Admin_Avales").css("left", "100px");
                        $("#Window_Admin_Avales").css("top", "100px");       
                        windowElement.data('tWindow').open();
                                
                        undoButton.hide();
                    });

        windowElement.bind('close', function () {
            logInBrowser('closing window ' + $(windowElement).attr('id'));
            setAvalList();
            undoButton.show();
            divContainer.hide();

        });

    }

    init_Admin_Avales();
    

    $("#btnAddAval").click(function () {

        var isDirty = $("#hidden_Input_isDirty").val();
        var isNew = $("#hidden_Input_isNew").val();
        var rowId = $("#hidden_rowId").val();
        var avalId = $("#hidden_Input_AvalId").val();
        var values = setAvalId(isNew, getValues());
        
        if (isNew == "true" || isNew == "" || isNew.length == 0) {
        
            oTable.fnAddData([values]);
        }
        else {
        
            $("#hidden_Input_isNew").val(true);
            var row = oTable.fnGetData(rowId);
            var newRow = updateRow(row, values);
            oTable.fnAddData([newRow]);
            oTable.fnDeleteRow(rowId);
        }
        oTable.fnDraw();
        clearInput();
        
    });

    $("#btnNewAval").click(function () {
        $("#hidden_Input_isNew").val(true);
        clearInput();    
    });

    function setAvalId(isNew, values) {

        if (isNew == "false")
            values[0] = "<input type='hidden' id='hidden_AvalId' value='" + $("#hidden_Input_AvalId").val() + "' />";
        else
            values[0] = "<input type='hidden' id='hidden_AvalId' value='-1' />";

        return values;
    }


    function updateRow(originalRow, values) {
        //hidden aval id
        originalRow[0] = values[0];
        //Nombre del aval
        originalRow[1] = values[1];
        //es cliente?
        originalRow[2] = values[2];
        //Pais
        originalRow[3] = values[3];
        //Tipo Aval
        originalRow[4] = values[4];
        //% cobertura
        originalRow[5] = values[5];
        //btn delete
        originalRow[6] = values[6];

        originalRow[7] = "<input type='hidden' id='hidden_isDirty' value='true' />";        

        return originalRow;
    }

    function getValues() {
        var esCliente;

        if ($("#Garantia_AvalComponent_EsCliente").is(':checked') == true)
            esCliente = 'Si';
        else
            esCliente = 'No';

        var obj = new Array(
                    "<input type='hidden' id='hidden_AvalId' value='" + $("#hidden_Input_AvalId").val() + "' />",
                    
                     $("#Garantia_AvalComponent_Nombre").val(),
                    //$("#Garantia_AvalComponent_EsCliente").is(':checked'),                                       
                     esCliente,
                    $("#Garantia_AvalComponent_PaisCatalog_Nombre").val() + " " + "<input type='hidden' id='Garantia_AvalComponent_hidden_Row_Pais_Key' name='Garantia.AvalComponent.hidden_Row_Pais_Key' value='" + $("#Garantia_AvalComponent_PaisCatalog_Key").val() + "' />",                      
                     $("#Garantia_AvalComponent_TipoAvalCatalog_Nombre").val() + " " + "<input type='hidden' id='Garantia_AvalComponent_hidden_Row_TipoAval_Key' name='Garantia.AvalComponent.hidden_Row_TipoAval_Key' value='" + $("#Garantia_AvalComponent_TipoAvalCatalog_Key").val() + "' />",
                     $("div#div_PorcentajeCobertura div").children(":first").text(),
                     "<input type='button' class='DeleteRow' id='btnRemoveAval' name='btnRemoveAval' value='Borrar'/>",                     
                     "<input type='hidden' id='hidden_isDirty' value='false' />"                                          
                  );
        return obj;
    }

    $('#avalTable tbody tr').live('click', function () {

        $("#hidden_rowId").val(oTable.fnGetPosition(this));
        $("#hidden_Input_isNew").val(false);

        var avalId = $(this).children().get(0);
        var nombre = $(this).children().get(1);
        var EsCliente = $(this).children().get(2);
        var tipoAval = $(this).children().get(4);
        var pais = $(this).children().get(3);
        var cobertura = $(this).children().get(5);
        var isDirty = $(this).children().get(7);
        var isNew = $(this).children().get(8);

        var flag = false;

        if ($(EsCliente).text().trim().toUpperCase() == "SI")
            flag = true;

        $("#hidden_Input_AvalId").val($(avalId).children().val());
        $("#Garantia_AvalComponent_Nombre").val($.trim($(nombre).text()));
        $("#Garantia_AvalComponent_EsCliente").attr('checked', flag);

        $("#div_ddlPais span.t-input").text($(pais).text());
        $("#Garantia_AvalComponent_PaisCatalog_Nombre").val($(pais).text());
        $("#Garantia_AvalComponent_PaisCatalog_Key").val($(pais).children().val());
            
        $("#div_ddlTipoAval span.t-input").text($(tipoAval).text());
        
        $("#Garantia_AvalComponent_TipoAvalCatalog_Nombre").val($(tipoAval).text());
        $("#Garantia_AvalComponent_TipoAvalCatalog_Key").val($(tipoAval).children().val());
                
        $('#Garantia_AvalComponent_InputPorcentajeCobertura').data("tTextBox").value($(cobertura).text());
        
        $("#hidden_Input_isDirty").val($(isDirty).children().val());

    });

    /* Add a click handler for the delete row */
    $('.DeleteRow').live('click', function (e) {
        var objRow = $(this).parents().get(1);
        oTable.fnDeleteRow(oTable.fnGetPosition(objRow));
        clearInput();
        $("#hidden_Input_isNew").val(true);
        e.stopPropagation();
    });

    /* Get the rows which are currently selected */
    function fnGetSelected(oTableLocal) {
        return oTableLocal.$('tr.row_selected');
    }

    $('#avalTable tr').click(function () {
        $(this).toggleClass('row_selected');
    });


    function clearInput() {

        //Nombre
        $("#Garantia_AvalComponent_Nombre").val("");

        //Pais
        var dropdownPais = $("#Garantia_AvalComponent_PaisCatalog_Key");
        dropdownPais.select(0);        
        $("#div_ddlPais span.t-input").text("NA");        
        $("#Garantia_AvalComponent_PaisCatalog_Nombre").val("N/A");
        $("#Garantia_AvalComponent_PaisCatalog_Key").val("N/A");

        //Es cliente?
        $("#Garantia_AvalComponent_EsCliente").attr('checked', false);

        //Tipo aval
        var dropdownCatalog = $("#Garantia_AvalComponent_TipoAvalCatalog_Key");
        dropdownCatalog.select(0);
        $("#Garantia_AvalComponent_TipoAvalCatalog_Nombre").val("Solidario");
        $("#Garantia_AvalComponent_TipoAvalCatalog_Key").val("1");
        $("#div_ddlTipoAval span.t-input").text("Solidario");
        
        $('#Garantia_AvalComponent_InputPorcentajeCobertura').data("tTextBox").value("0");

        $("#hidden_Input_isDirty").val("");        
    }

    function setAvalList() {
        var result = "";
        var id, nombre, esCliente, pais, tipoAval, cobertura, isDirty;
        
        $("#hidden_AvalList").val("");
        result = result + "{";
        $("#avalTable tbody tr").each(function (index) {
            $(this).children("td").each(function (index2) {

                switch (index2) {
                    case 0:
                        id = "";
                        id = $(this).children().val();
                        break;
                    case 1:
                        nombre = "";
                        nombre = $(this).text();
                        break;
                    case 2:
                        esCliente = "";
                        esCliente = $(this).text();
                        break;
                    case 3:
                        pais = "";                        
                        pais = $(this).children().val();
                        break;
                    case 4:
                        tipoAval = "";                        
                        tipoAval = $(this).children().val();
                        break;
                    case 5:
                        cobertura = "";
                        cobertura = $(this).text();
                        break;
                    case 7:
                        isDirty = "";
                        isDirty = $(this).children().val();
                        break;
                }

            })

            
            if(id!=undefined && nombre!= undefined && pais!=undefined && tipoAval!=undefined)
                result = result + "['" + id + "','" + nombre + "','" + esCliente + "','" + pais + "','" + tipoAval + "','" + cobertura + "','" + isDirty + "'];";
        })

        result = result + "}";
        $("#hidden_AvalList").val(result);        
        $("#Garantia_AvalComponent_hiddenAvales").val(result);         

    }
        
    
</script>
  <%}); %>
  </div>  
  
  <input type='button' id='btn_undo_Admin_Avales' name='btn_undo_Admin_Avales' class='t-group' value='Administracion de Avales' />
  
  
  