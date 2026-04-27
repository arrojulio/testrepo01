/* set to false for production */
var chromeLog = true;
var garantiaId;
var baseUrl = "http://localhost:47730";
/*var baseUrl = "http://liberia/GarantiasWeb";*/
/* comment for production or uat. */
//baseUrl = "";

/* Variables used to dynamic calcs on controls */
/* Controls that report on change */
var controlsThatReportChange = ["Garantia_ValorInicial", "Garantia_ValorNecesarioDeGarantia", "Garantia_ValorPolizaSeguro", "Garantia_ValorMercado", "Garantia_ValorEvaluacionVentaRapida", "ValorGarantiaSuperIntendencia", "Garantia_ValorAvaluo"];
//"Garantia_ValorAvaluo"];
/* Targe Control, where we should store the calc */
var targetControl = "Garantia_ValorGarantiaSuperIntendencia";
var fechaUltimaRevisionEvaluacionControl = "Garantia_FechaUltimaRevisionEvaluacion";
var frecuenciaRevisionControl = "Garantia_FrecuenciaRevision_Key";
var fechaProximaRevisionEvaluacionControl = "Garantia_FechaProximaRevisionEvaluacion";
var indicadorAtomoControlName = "Garantia_IndAtomo";
var notAvailableTiposGarantiaSuper = ["01NA", "02NA", "03NA", "04NA", "05NA", "06NA"];
var indicadorNotInAtomo = 3;
var tooltips_MillisecondsToHide = 5000;

var clientFieldId = "Garantia_Cliente_Key";
var garanteFieldId = "Garantia_Garante_Key";
var fiduciariaSuperFieldId = "Garantia_FiduciariaSuper_Key";
var fiduciariaSuperFieldName = "Garantia_FiduciariaSuper_Nombre";
var tipoGarantiaSuperFieldId = "Garantia_TipoGarantiaSuper_Key";
var tipoGarantiaBladexFieldId = "Garantia_TipoGarantiaBladex_Key";
var categoriaRiesgoFieldId = "Garantia_CategoriaRiesgoGarantia_Key";
var frecuenciaRevisionFieldId = "Garantia_FrecuenciaRevision_Key";
var ratingGaranteFieldId = "Garantia_RatingGarante_Key";
var statusFieldId = "Garantia_Status_Key";
var emisorFieldId = "Garantia_Emisor_Key";
var tipoInstrumentoFinancieroFieldId = "Garantia_TipoInstrumentoFinanciero_Key";
var calificacionEmisionFieldId = "Garantia_CalificacionesRiesgoEmision_Key";
var calificacionEmisorFieldId = "Garantia_CalificacionesRiesgoEmisor_Key";
var identificadorFideicomisoId = "Garantia_IdentificacionFideicomiso";
var paisFieldId = "Garantia_PaisGarantia_Key";
var regionFieldId = "Garantia_Region_Key";
var tipoPolizaFieldId = "Garantia_TipoPoliza_Key";
var fiduciariaBladexFieldId = "Garantia_FiduciariaBladex";
var aseguradorSuperFieldId = "Garantia_AseguradorSuper_Key";

var nombreAseguradorFieldId = "Garantia_Asegurador_Key";
var nombreAvaluadorFieldId = "Garantia_Evaluador_Key";
var aseguradoraPaisFieldId = "Garantia_Asegurador_Pais_Key";
var avaluadoraPaisFieldId = "Garantia_Evaluador_Pais_Key";
var bancoSuperFieldId = "Garantia_BancoSuper_Key";
var depositanteFieldId = "Garantia_Depositante_Key";
var fiduciariaEnElExteriorID = "599";
var aseguradoraEnElExteriorID = "699";
var avaluadoraEnElExteriorID = "799";
var garantiaNumeroPolizaSeguro = "Garantia_NumeroPolizaSeguro";

var _AUTO_COMPLETE_LENGHT = 50;

$(document).ready(function () {

    setupTooltips();
    CleanLabelValidation();
    $("#logged-user").val($.query.get('UserId'));
    $("#user_label").click(function (e) {
        $.get('Account/SignOff?UserId=' + $.query.get('UserId'), function () {
            window.location.reload(true);
        });


        e.preventDefault();
        return false;
    });

    //Ticket #1550 : Nueva logica si el id fideicomiso es NA se debe deshabilitar    
    $('#Garantia_IdentificacionFideicomiso').change(function (e) {
        validateIdentificacionFideicomiso(e);
    }).triggerHandler('change');

    //Ticket #1619 : Logica para validar tipo poliza
    $("#Garantia_NumeroPolizaSeguro").change(function (e) {
        validateTipoPoliza(e);
    }).triggerHandler('change');

    if ($("#Garantia_CategoriaSuper_Key").val() == "02") {
        $("#Garantia_ValorMercado").attr("disabled", "disabled");
    }


    $("#btnSaveGarantia").click(function () {
        SaveGarantia_OnClick();
    });

    // Deshabilitar campos
    DisableFields(true);
});

$(document).keydown(function (e) {
    if ($("#Garantia_CategoriaSuper_Key").val() != undefined
        && e.keyCode === 8
        && document.activeElement.type !== 'textarea'
        && (document.activeElement.type !== 'text' || (document.activeElement.type === 'text' && document.activeElement.readOnly))) {
        e.preventDefault();
        alert('Por favor utilice el boton de retroceder del browser para salir de la pantalla');
    }
});

function DisableFields(disable) {
    var categoriaSupe = $("#Garantia_CategoriaSuper_Key").val();
    if (categoriaSupe != "06") {
        if (disable) {
            $("#Garantia_NombreOrganismo").attr("disabled", "disabled");
        }
        else {
            $("#Garantia_NombreOrganismo").removeAttr("disabled");
        }
    }
}

function onAutocompleteChange(e) {
    var htmlControlId = e.currentTarget.name;
    var value = e.value;
    if (value && htmlControlId) {
        if (value.length > _AUTO_COMPLETE_LENGHT) {
            alert("Ingreso inválido. No puede exceder " + _AUTO_COMPLETE_LENGHT + ' caracteres');erw
            return;
        }
        //convert value to UpperCase
        value = value.toUpperCase();
        var parameters = "{'htmlControlId':" + JSON.stringify(htmlControlId) + ", 'value': " + JSON.stringify(value) + "}";
        var objUrl = baseUrl + "/GarantiaBase/SaveAutocomplete";
        $.ajax({
            type: "POST",
            url: objUrl,
            data: parameters,
            contentType: "application/json;",
            cache: false,
            success: function (htmlResult) {
            },
            error: function (result) { }
        });
    }
}

function saveSelection(source) {
    logInBrowser("saving on hidden input..");
    logInBrowser("previous value: " + $("hidden_" + source).val());
    $("hidden_" + source).val($("#txtName_" + source).val());
    logInBrowser("new value: " + $("hidden_" + source).val());

}

function calcProximaFechaRevisionEvaluacion(e) {
    var fechaUltimaRevisionEvaluacion = null;
    var frecuenciaRevision = null;
    if (e.currentTarget.id == fechaUltimaRevisionEvaluacionControl) {
        fechaUltimaRevisionEvaluacion = e.value;
        frecuenciaRevision = getSelectedFromComboBox(frecuenciaRevisionControl);
    }
    else if (e.currentTarget.id == frecuenciaRevisionControl) {
        if (e.type == 'load')
            onDropdownChange(e);
        else
            onDropdownChangeReadOnly(e);

        frecuenciaRevision = e.value;
        fechaUltimaRevisionEvaluacion = $("#" + fechaUltimaRevisionEvaluacionControl).val();
    }
    else {

    }
    if ((fechaUltimaRevisionEvaluacion != null && fechaUltimaRevisionEvaluacion != "") && (frecuenciaRevision != null && frecuenciaRevision != "")) {
        var parameters = "{'frecuenciaRevisionId': " + JSON.stringify(frecuenciaRevision) + ", 'fechaUltimaRevisionEvaluacion': " + JSON.stringify(fechaUltimaRevisionEvaluacion) + "}";
        var objUrl = baseUrl + "/GarantiaBase/CalcProximaFechaRevisionEvaluacion";
        $.ajax({
            type: "POST",
            url: objUrl,
            data: parameters,
            contentType: "application/json;",
            cache: false,
            success: function (result) {
                if (result && result.ProximaFechaRevisionEvaluacion != undefined) {
                    $("#" + fechaProximaRevisionEvaluacionControl).val(result.ProximaFechaRevisionEvaluacion);
                } else {
                    $("#" + fechaProximaRevisionEvaluacionControl).val("");
                }
            },
            error: function (result) {
                $("#" + fechaProximaRevisionEvaluacionControl).val("");
            }
        });
        //return false;        
    }

}


//Ticket #1550 : Si la garantia no es de tipo deposito, solo cuando se modifica el closure date se le agregan 15 dias
function calcFechaVencimiento(e) {
    var controlId = e.currentTarget.id;
    var originalDate = null;
    if (controlId == "Garantia_FechaVencimientoGarantia") {
        if (e.type == "valueChange") {
            if ($("#Garantia_CategoriaSuper_Key").val() != "03") {

                // Fecha vencimiento garantia debe ser mayor o igual ahoy
                var dateFechVencimientoGarantia = new Date(e.value);

                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();
                var today = new Date(mm + "/" + dd + "/" + yyyy);

                if ((dateFechVencimientoGarantia - today) < 0) {
                    alert("Error: La fecha de vencimiento de la garantía no puede ser anterior a la fecha actual");
                }
                else {

                    // Ticket 1150, misma logica para todas las garantias excepto depositos bladex
                    var date = null;
                    var originalDate = null;
                    if (e != undefined && e.value != undefined) {
                        date = new Date(e.value);
                        originalDate = new Date(e.value);
                    }
                    var controlVencimientoRiesgo = $('#Garantia_FechaVencimientoRiesgo').data('tDatePicker');
                    var controlVencimientoGarantia = $('#Garantia_FechaVencimientoGarantia').data('tDatePicker');


                    if (date != null) {
                        if (!isNaN(date.getTime())) {
                            date.setDate(date.getDate() + 15);

                            controlVencimientoRiesgo.value((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
                            controlVencimientoGarantia.value("");
                            controlVencimientoGarantia.value((originalDate.getMonth() + 1) + '/' + originalDate.getDate() + '/' + originalDate.getFullYear());

                        }
                    }
                } // Fecha vencimiento garantia

            }
        }

    }

}

function changeStatusGarantia(garantiaId) {
    var parameters = "{'id':" + JSON.stringify(garantiaId) + "}";
    var objUrl = baseUrl + "/GarantiaBase/ChangeStatusGarantia/?id=" + garantiaId;
    $.ajax({
        type: "POST",
        url: objUrl,
        data: parameters,
        contentType: "application/json;",
        cache: false,
        success: function (htmlResult) {
            var result = htmlResult;
            location.reload(true);
        },
        error: function (result) { alert("La garantia no ha podido activarse."); }
    });
    return false;
}
function changeTypeGarantia(garantiaId, currentType) {
    var parameters = "{'id':" + JSON.stringify(garantiaId) + ", 'currentType':" + JSON.stringify(currentType) + "}";
    parameters = "{}";
    var objUrl = baseUrl + "/GarantiaBase/ChangeTypeGarantia/?id=" + garantiaId + "&currentType=" + currentType;
    $.ajax({
        type: "GET",
        url: objUrl,
        data: parameters,
        contentType: "text/html;",
        dataType: "html",
        cache: false,
        success: function (htmlResult) {
            $("#changeTypeModal").remove();
            $("#changeTypeContainer").append(htmlResult);
            $("#changeTypeModal").dialog();
        },
        error: function (result) {
            alert(result);
        }
    });
    return false;
}

function submitChangeTypeGarantia(garantiaId, currentType, newType) {
    var param = new Object();
    param.id = garantiaId;
    param.currentType = currentType;
    param.newType = newType;
    var parameters = JSON.stringify(param);
    var objUrl = baseUrl + "/GarantiaBase/ChangeTypeGarantia";
    $.ajax({
        type: "POST",
        url: objUrl,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (result) {
            if (result) {
                $("#changeTypeModal").append("<b style='color:green;'>La operacion se ha realizado con exito.</b><br/>Aguarde mientras se actualizan las garantías.");
                window.location = document.URL;
            }
            else {
                $("#changeTypeModal").append("<b style='color:red;'>La operacion no ha podido concretarse.</b><br/>");
            }
        },
        error: function (result) { $("#changeTypeModal").append("<b style='color:red;'>La operacion no ha podido concretarse.</b><br/>"); }
    });
    return false;
}

function setupTooltips() {
    $(".tooltip").bt({
        ajaxPath: baseUrl + '/GarantiaBase/GetTooltip/',
        ajaxError: "<STRONG>An error has ocurred</STRONG> There was a problem getting the help for this item. Error:<EM>%error</EM>.",
        showTip: function (box) {
            $(box).fadeIn(100);
        },
        shrinkToFit: true,
        hideTip: function (box, callback) {
            //setTimeout(3000);
            $(box).animate({ opacity: 0 }, tooltips_MillisecondsToHide, callback);
        },
        hoverIntentOpts: { interval: 100, timeout: tooltips_MillisecondsToHide },
        fill: '#F7F7F7',
        strokeStyle: '#B7B7B7',
        spikeLength: 10,
        spikeGirth: 10,
        padding: 8,
        cornerRadius: 5,
        cssClass: 'tooltipBox',
        cssStyles: {
            fontFamily: '"lucida grande",tahoma,verdana,arial,sans-serif',
            fontSize: '11px',
            backgroundColor: '#F7F7F7'
        },
        closeWhenOthersOpen: true

    });

}

function setEqualSizes2() {
    var max = 0;
    $("fieldset").each(function () {
        $(this).find(".editor-label label").each(function () {
            if ($(this).width() > max)
                max = $(this).width();
        });
        $(this).find(".editor-label").attr('style', 'width:' + max + 'px');
        max = 0;
    });

    $("fieldset").each(function () {
        $(this).find(".editor-label").each(function () {
            $(this).height($(this).next().height());
        });
    });
}
function goBack() {
    document.location = document.referrer;
}
function makeNiceTable(tableId) {

    var newTable = $("#" + tableId).dataTable({
        "bJQueryUI": false,
        "sDom": 'lfrt<"dt-footer clearfix"ip>',
        "oLanguage": {
            "sLengthMenu": "Display _MENU_ records per page",
            "sZeroRecords": "No records found",
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
            "sInfoEmpty": "Showing 0 to 0 of 0 records",
            "sInfoFiltered": "(filtered from _MAX_ total records)"
        },
        "aLengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "aoColumnDefs": [
            { "bSearchable": false, "bSortable": false, "aTargets": ["action"] }
        ],
        "iDisplayLength": 10,
        "aaSorting": [[1, "desc"]],
        "bAutoWidth": true,
        "bProcessing": true
    });
    return newTable;

}

function makeAvalNiceTable(tableId) {

    var newTable = $("#" + tableId).dataTable({
        "bJQueryUI": true,
        "oLanguage": {
            "sLengthMenu": "Display _MENU_ records per page",
            "sZeroRecords": "No records found",
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
            "sInfoEmpty": "Showing 0 to 0 of 0 records",
            "sInfoFiltered": "(filtered from _MAX_ total records)"
        },
        "sScrollY": "250px",
        "bScrollCollapse": false,
        "bPaginate": false,
        "aLengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "aoColumnDefs": [
            { "bSearchable": false, "bSortable": false, "aTargets": ["action"] }

        ],
        "oTableTools": {
            "sRowSelect": "single"
        },
        "iDisplayLength": 200,
        "aaSorting": [[1, "desc"]],
        "bLengthChange": false,
        "bAutoWidth": true,
        "bProcessing": true,
        "bStateSave": true,
        "iCookieDuration": 300

        //"sPaginationType": "full_numbers"
    });

    return newTable;


}

function getAvalesPage(id) {

    var parameters = "{'garantiaId':" + JSON.stringify(id) + "}";
    parameters = "{}";
    var objUrl = baseUrl + "/Aval/Index/?garantiaId=" + id;
    $.ajax({
        type: "GET",
        url: objUrl,
        data: parameters,
        contentType: "text/html;", // charset=utf-8",
        dataType: "html",
        cache: false,
        success: function (msg) {
            $("#avalContainerContent").empty();
            $("#avalContainerContent").html(msg.toString());
        },
        error: function (result) {
            $("#avalContainerContent").empty();
            $("#avalContainerContent").html("<p>Ha ocurrido un error.</p>");

            //alert(result.responseText);
            var message = "An error has ocurred.";
            var arr = JSON.parse(result.responseText);
            var detail = arr.Message;
            var title = "Retrieval error";
            //alert(message + " - " + detail + " - " + title);
            //showError(message, detail, title);
        }
    });
}

function refreshAvalCounter(id) {
    var parameters = "{ }";

    $.ajax({
        type: "POST",
        url: baseUrl + "/Aval/Count/?garantiaId=" + id,
        data: parameters,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var obj = parseInt(msg);
            $("#avalCounter").html(obj);

        },
        error: function (result) {
            //alert('error');
            alert('Ha ocurrido un error actualizando los avales.');
            //alert(result.responseText);
            //                    var message = "An error has ocurred trying to change the status of your order #" + orderId + ".";
            //                    var arr = JSON.parse(result.responseText);
            //                    var detail = arr.Message;
            //                    var title = "Retrieval error";
            //                    showError(message, detail, title);
        }
    });
}

function setupGarantiaBaseModel(garantiaKey) {
    garantiaId = garantiaKey;

}

function setupGarantiasIndex() {
    $(document).ready(function () {
        makeNiceTable("tblGarantiasList");
    });
}

function bindLimitInformation(resultMessage) {

    if (resultMessage) {
        $("#limitContainer").html("<div></div><div><div style='float:left;clear:left;'>Customer Name:</div><div style='float:right;'>" + resultMessage.MatrixName + "</div></div><div></div><div><div style='float:left;clear:left;'>" + "Limit Value:</div><div style='float:right;'>" + resultMessage.LimitValue + "</div></div><div></div><div><div style='float:left;clear:left;'>" + "Utilization:</div><div style='float:right;'>" + resultMessage.LastLimit + "</div></div><div></div><div><div style='float:left;clear:left;'>" + "Line Exp. Date:</div><div style='float:right;'>" + resultMessage.ExpirationDate + "</div></div><div></div><div><div style='float:left;clear:left;'>" + "Internal Remark:</div><div style='float:right;'>" + resultMessage.Comments + "</div></div><div></div><div><div style='float:left;clear:left;'>");
    }
    else {
        $("#limitContainer").html("<div>No existe limite para el cliente.</div>");
    }
}

function isMandatoryField(FieldId) {
    var result = false;

    switch (FieldId) {
        //case "Garantia_TipoGarantiaSuper_Key":
        //  result = true;
        //  break;
        case "Garantia_Emisor_Key":
            result = true;
            break;
        case "Garantia_TipoInstrumentoFinanciero_Key":
            result = true;
            break;
        case "Garantia_CalificacionesRiesgoEmision_Key":
            result = true;
            break;
        case "Garantia_CalificacionesRiesgoEmisor_Key":
            result = true;
            break;
        case "Garantia_FiduciariaSuper_Key":
            result = true;
            break;
        default:
            result = false;
            break;
    }

    return result;

}

function onDropdownChangeReadOnly(a) {
    var isValid = false;

    var controlId = a.currentTarget.id;
    logInBrowser("Selected Value: " + a.value + " on control " + controlId);
    var comboBox = $("#" + controlId).data("tComboBox");
    if (!comboBox) {
        comboBox = $("#" + controlId).data("tDropDownList");
    }

    validateFiduciariaExterior(a);
    validateAseguradorSuper(a);

    // Este fix es para hacer trigger en combobox de tipo degarantia para garantia 06, para cambiar el
    // nombre del organismo de acuerdo a su logica para este tipo degarantia.
    if (controlId == tipoGarantiaSuperFieldId) {
        validateNombreOrganismo(a);
    }

    var text = comboBox.text().trim();
    var list = comboBox.data;

    if (controlId == aseguradorSuperFieldId) {
        $("#Garantia_NombreOrganismo").val(text);
    }
    if (text == "") {
        if (isMandatoryField(controlId))
            isValid = false;
        else
            isValid = true;
    }
    else {

        $(list).each(function (index, item) {
            if (item.Text.trim() === text) {
                isValid = true;
            }
        });
    }

    if (isValid) {
        var controlTextId = controlId.toString().replace('Key', 'Nombre');
        if (controlTextId != controlId) {
            if (document.getElementById(controlTextId) == undefined) {
                var nameOfControl = controlId.toString().replace('Key', 'Nombre');
                nameOfControl = nameOfControl.replace('_', '.').replace('_', '.');
                $("#" + controlId).parent().append("<input type='hidden' name='" + nameOfControl + "' id='" + controlTextId + "' />");
            }

            $("#" + controlTextId).val(text);
            comboBox.text(text);
        }
    }
    else {

        alert("El valor ingresado es invalido");
        $("#" + controlId).data("tComboBox").text("");
        $("#" + controlTextId).val("");
        comboBox.text('');
        comboBox.value('');
        comboBox.reload();
    }
    validateTipoInstrumento(a);
}

function onDropdownChange(a) {
    var controlId = a.currentTarget.id;
    //Ticket #1550 : si no hay un pais pre seleccionado se agrega un vacio
    if (controlId == paisFieldId) {
        if ($("#PaisGarantiaSelected").val() != undefined && $("#PaisGarantiaSelected").val() == "(empty)") {
            if (a.type == 'load') {
                if (a.value == null || a.value == "") {
                    var ddlPais = $("#" + paisFieldId).data("tDropDownList");
                    var data = ddlPais.data;
                    data.unshift({ Text: " ", Value: "" });
                    ddlPais.dataBind(data)
                }
            }
        }
    }

    if (controlId == bancoSuperFieldId) {
        if ($("#BancoSuperSelected").val() != undefined && $("#BancoSuperSelected").val() == "(empty)") {
            if (a.type == 'load') {
                if (a.value == null || a.value == "") {
                    var ddlBancoSuper = $("#" + bancoSuperFieldId).data("tDropDownList");
                    var data = ddlBancoSuper.data;
                    data.unshift({ Text: " ", Value: "" });
                    ddlBancoSuper.dataBind(data);
                }
            }
        }
    }

    validateFiduciariaExterior(a);
    validateAseguradorSuper(a);
    validateNombreAseguradora(a);
    validateAseguradoraPais(a);
    validateNombreOrganismo(a);
    validateTipoInstrumento(a);

    // Ticket #1619
    validateOrigenGarantia(a);
    //validateTipoPoliza(a);

    logInBrowser("Selected Value: " + a.value + " on control " + controlId);
    var comboBox = $("#" + controlId).data("tComboBox");
    if (!comboBox) {
        comboBox = $("#" + controlId).data("tDropDownList");
    }

    var text = comboBox.text();

    if (controlId == aseguradorSuperFieldId) {

        $("#Garantia_NombreOrganismo").val(text);
    }
    var controlTextId = controlId.toString().replace('Key', 'Nombre');
    if (controlTextId != controlId) {
        if (document.getElementById(controlTextId) == undefined) {
            var nameOfControl = controlId.toString().replace('Key', 'Nombre');
            nameOfControl = nameOfControl.replace('_', '.').replace('_', '.');
            $("#" + controlId).parent().append("<input type='hidden' name='" + nameOfControl + "' id='" + controlTextId + "' />");
        }
        $("#" + controlTextId).val(text);
    }

    //Ticket #1550
    if (controlId == fiduciariaSuperFieldId && a.type == "load") {
        /*     if ($("#" + identificadorFideicomisoId).val() == "") {
        $("#" + identificadorFideicomisoId).val("NA");
        }
        */
        validateIdentificacionFideicomiso(a);
    }
    else {
        if (controlId == fiduciariaSuperFieldId && a.type == "change") {
            //alert($("#" + fiduciariaSuperFieldId).val());
        }
    }

    if (controlId == tipoPolizaFieldId && a.type == "load") {
        validateTipoPoliza(a);
    }

    EnableRiskRelatedFields(false);
}

function validateOrigenGarantia(a) {
    // Ticket #1619
    var varValue = $('#Garantia_OrigenGarantia').val().toUpperCase();
    var ddlRegion = $("#" + regionFieldId).data("tDropDownList");

    if (ddlRegion != null) {
        if (varValue != null && varValue == "L") {
            ddlRegion.enable();
        }
        else {
            ddlRegion.value("NA");
            $("#Garantia_Region_Nombre").val("NA");
            ddlRegion.disable();
        }
    }
}

function validateTipoPoliza(a) {
    // Ticket #1619
    var varValue = $('#' + garantiaNumeroPolizaSeguro).val().toUpperCase();
    var ddlTipoPoliza = $("#" + tipoPolizaFieldId).data("tDropDownList");

    if ($("#Garantia_CategoriaSuper_Key").val() != "04") {
        if (ddlTipoPoliza != null) {
            if (varValue != null && varValue != "NA" && varValue != "") {
                ddlTipoPoliza.enable();
            }
            else {
                ddlTipoPoliza.value("NA");
                $("#Garantia_TipoPoliza_Nombre").val("NA");
                ddlTipoPoliza.disable();
            }
        }
    } else {
        if (ddlTipoPoliza != null) {
            ddlTipoPoliza.value("NA");
            $("#Garantia_TipoPoliza_Nombre").val("NA");
            ddlTipoPoliza.disable();
        }
    }
}

function validateIdentificacionFideicomiso(a) {

    var varValue = $('#Garantia_IdentificacionFideicomiso').val().toUpperCase();

    var comboBoxSuper = $("#" + fiduciariaSuperFieldId).data("tComboBox");
    var inputFiduciariaExterior = $("#" + fiduciariaBladexFieldId);

    if (comboBoxSuper != null) {
        if (varValue != null && varValue == "NA") {
            comboBoxSuper.value("NA");

            comboBoxSuper.disable();


            if (inputFiduciariaExterior != undefined) {
                $("#" + fiduciariaBladexFieldId).val("NA");
                $("#" + fiduciariaBladexFieldId).attr("disabled", "disabled");
            }
        }
        else {
            comboBoxSuper.enable();
        }
    }

}

function onClientChange(a) {
    onDropdownChange(a);
    getRucCode(a);
    refreshAttachedToLine(a);
}
function onGaranteChange(a) {
    onDropdownChange(a);
    getRucCode(a);

    //ticket #1550 : dejo on hold hasta ver que pasa con el match de los ids
    /*
    if ($("#Garantia_CategoriaSuper_Key").val() == "06") {
        var combo = $("#Garantia_Emisor_Key").data("tComboBox");

        if (combo != undefined && a.value!=undefined && a.value !=null) {
            combo.text(a.value);
        }
    }
    */
}
function refreshAttachedToLine(a) {
    logInBrowser("refreshAttachedToLine function receives value: " + a.value);
    var customerId = a.value;
    getLimitInformation(customerId);
    return;
}

function getRucCode(a) {
    logInBrowser("getRucCode function receives value: " + a.value);
    getCustomerInformation(a);
    return;
}

function getLimitInformation(customerId) {
    var parameters = "{'CustomerId':" + JSON.stringify(customerId) + "}";
    var svcUrl = "";
    //alert(parameters);
    //    svcUrl = "ClientLimitInformation";
    svcUrl = baseUrl + "/GarantiaBase/ClientLimitInformation";

    $.ajax({
        type: "POST",
        url: svcUrl,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //alert("success");
            //alert(svcUrl);
            bindLimitInformation(msg);
        },
        error: function (result) {
            //alert("error");
            //alert(result.responseText);
            var message = "An error has ocurred trying to retrieve customer limit information from CPER.";
            var detail = result.Message;
            //alert(arr);
            //alert(result.statusText);
            //showError(message, detail, result.statusText);
        }
    });

}

function logInBrowser(text) {
    if (chromeLog) {
        //Ticket #1888 - problema con chromeLog
        //console.log(text);
    }
}

function getCustomerInformation(a) {

    var controlId = a.currentTarget.id;
    logInBrowser("Selected Value: " + a.value + " on control " + controlId);
    var comboBox = $("#" + controlId).data("tComboBox");
    var svcUrl = "";

    svcUrl = "GetCustomer";

    var parameters = "{'CustomerId':" + JSON.stringify(a.value) + "}";
    $.ajax({
        type: "POST",
        url: svcUrl,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            bindCustomerInformation(controlId, msg);
        },
        error: function (result) {
            var arr = JSON.parse(result.responseText);
            var message = "An error has ocurred trying to retrieve the customer information.";
            var detail = arr.Message;
            //alert(arr);
            //alert(result.statusText);
        }
    });

}

// Bind the customer properties to the dropdown of actor country. Receives the id of the Actor Dropdown control and the Actor Key selected.
function bindCustomerInformation(controlId, customer) {

    logInBrowser("Request from Control: " + controlId);
    var newControlId = controlId.toString().replace('_Key', '_NationalId');
    logInBrowser("Request to Control: " + newControlId);
    logInBrowser("Setting value " + customer.NationalId + " on control " + newControlId);
    if (customer == undefined || customer.NationalId == undefined) {
        $("#" + newControlId).val("N/A");
    } else {
        $("#" + newControlId).val(customer.NationalId.toString());
    }


}

function getActorInformation(a) {
    onDropdownChange(a);
    var controlId = a.currentTarget.id;
    logInBrowser("Selected Value: " + a.value + " on control " + controlId);
    var comboBox = $("#" + controlId).data("tComboBox");
    var svcUrl = "";
    svcUrl = "GetActor";

    var parameters = "{'ActorId':" + JSON.stringify(a.value) + "}";
    $.ajax({
        type: "POST",
        url: svcUrl,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            bindActorInformation(controlId, msg);
        },
        error: function (result) {
            var arr = JSON.parse(result.responseText);
            var message = "An error has ocurred trying to retrieve the actor information.";
            var detail = arr.Message;
            alert(message);
            alert(result.statusText);
        }
    });

}

// Bind the actor properties to the dropdown of actor country. Receives the id of the Actor Dropdown control and the Actor Key selected.
function bindActorInformation(controlId, actor) {

    logInBrowser("Request from Control: " + controlId);
    var newControlId = controlId.toString().replace('_Key', '_Pais_Key');
    logInBrowser("Request to Control: " + newControlId);
    logInBrowser("Looking for index of " + actor.Pais.Key);
    var countryIndex = getIndexOfDropDownValue(newControlId, actor.Pais.Key);
    logInBrowser("Country Index is: " + countryIndex);
    logInBrowser("Setting value " + actor.Pais.Nombre + " with index " + countryIndex + " on control " + newControlId);
    selectItemInDropDownList(newControlId, countryIndex);

}

// Returns the index of the item with the specified value in parameter value for a DropDownList control
function getIndexOfDropDownValue(controlId, value) {

    var comboBox = $("#" + controlId).data("tDropDownList");
    var indexes = $.map(comboBox.data, function (item, index) {
        if (item.Value == value) {
            return index;
        }
    });
    if (indexes.length > 0) return indexes[0];
}

// Returns the index of the item with the specified value in parameter value for a ComboBox control
function getIndexOfComboBoxValue(controlId, value) {

    var comboBox = $("#" + controlId).data("tComboBox");
    var indexes = $.map(comboBox.data, function (item, index) {
        if (item.Value == value) {
            return index;
        }
    });
    if (indexes.length > 0) return indexes[0];
}

// Set as selected the item on index paremeter for a ComboBox control.
function selectItemInComboBox(controlId, index) {
    var comboBox = $("#" + controlId).data("tComboBox");
    if (comboBox) {
        comboBox.select(index);
    }
}

// Set as selected the item on index paremeter for a DropDownList control.
function selectItemInDropDownList(controlId, index) {
    var dropDownList = $("#" + controlId).data("tDropDownList");
    if (dropDownList) {
        dropDownList.select(index);
    }
}

// Returns the selected value for the DropDownList control specified in controlId parameter.
function getSelectedFromDropDownList(controlId) {
    var dropDownList = $("#" + controlId).data("tDropDownList");
    if (dropDownList) {
        logInBrowser("The selected item for control " + controlId + " is Text: " + dropDownList.text() + " Value: " + dropDownList.value());
        return dropDownList.value();
    }
    else
        return undefined;
}
// Returns the selected value for the ComboBox control specified in controlId parameter.
function getSelectedFromComboBox(controlId) {
    var comboBox = $("#" + controlId).data("tComboBox");
    if (comboBox) {
        logInBrowser("The selected item for control " + controlId + " is Text: " + comboBox.text() + " Value: " + comboBox.value());
        return comboBox.value();
    }
    else
        return undefined;
}

function indicadorAtomoDropDownChange(a) {
    onDropdownChange(a);
    var selectedValue = a.value;
    var comboBox = $("#" + tipoGarantiaSuperControlId).data("tComboBox");

    logInBrowser("Selected Value: " + selectedValue);

    if (selectedValue == indicadorNotInAtomo || selectedValue == "No Esta En Atomo") {
        var tipoGarantia = checkTipoGarantiaSuperType(comboBox);
        setItemSelected(comboBox, tipoGarantia);
        comboBox.disable();
    }
    else {
        comboBox.enable();
    }
}

function checkTipoGarantiaSuperType(comboBox) {
    var tipoGarantia = comboBox.value();

    switch (tipoGarantia.substr(0, 2)) {

        case "01":
            return notAvailableTiposGarantiaSuper[0];
            break;
        case "02":
            return notAvailableTiposGarantiaSuper[1];
            break;
        case "03":
            return notAvailableTiposGarantiaSuper[2];
            break;
        case "04":
            return notAvailableTiposGarantiaSuper[3];
            break;
        case "05":
            return notAvailableTiposGarantiaSuper[4];
            break;
        case "06":
            return notAvailableTiposGarantiaSuper[5];
            break;
    }
}

function setItemSelected(comboBox, tipoGarantia) {

    for (var i = 0; i < comboBox.dropDown.$items.length; i++) {
        comboBox.select(i);
        if (comboBox.value() == tipoGarantia) {
            return;
        }
    }
}

function validateIndicadorAtomoSelected(a) {
    /*
        var selectedValue = $("#" + indicadorAtomoControlName).data("tDropDownList").value();
        var comboBox = $("#" + tipoGarantiaSuperControlId).data("tComboBox");
        var ddl = $("#" + indicadorAtomoControlName).data("tDropDownList");
            
        
        comboBox.open();
        comboBox.close();
    
        if (selectedValue == indicadorNotInAtomo) {
            //select non nullable item from comboBox    
            comboBox.select(comboBox.dropDown.$items.length - 1);    
    
            var tipoGarantia = checkTipoGarantiaSuperType(comboBox);
            setItemSelected(comboBox, tipoGarantia);
            comboBox.disable();
        }
    */
}

function setTipoGarantiaSuperControlName(controlId) {
    tipoGarantiaSuperControlId = controlId + "_Key";
}

function ValidateComboBox(controlId, value) {

    var isValid = false;
    var comboBox = $("#" + controlId).data("tComboBox");
    var text = value;
    var list = comboBox.data;
    if (controlId == emisorFieldId) {
        if (value != null && value != "")
            isValid = true;
        else
            isValid = false;
    }
    else {
        if (value == "") {
            if (!isMandatoryField(controlId) && text == "") {
                isValid = true;
            }
            else {
                isValid = false;
            }
        }
        else {

            $(list).each(function (index, item) {
                if (item.Value === text) {
                    isValid = true;
                }
            });
        }
    }

    if (controlId == garanteFieldId) {
        if (isValid == false) {
            if ($("#" + garanteFieldId).val() == "-1") {
                isValid = true;
            }
        }

    }

    //Ticket #1550 : Si el garante no es igual al depositante no debe dejar salvar
    /*if ($("#Garantia_CategoriaSuper_Key").val() == "04") {
        if ($("#" + depositanteFieldId).val() != $("#" + garanteFieldId).val()) {            
            isvalid = false;
        }
    }*/


    return isValid;

}

function CleanLabelValidation() {

    $("#lblValidation_Garantia_Cliente_Key").hide();
    $("#lblValidation_Garantia_Cliente_Key").text("");

    $("#lblValidation_Garantia_Garante_Key").hide();
    $("#lblValidation_Garantia_Garante_Key").text("");

    $("#lblValidation_Garantia_FiduciariaSuper_Key").hide();
    $("#lblValidation_Garantia_FiduciariaSuper_Key").text("");

    $("#lblValidation_Garantia_TipoGarantiaSuper_Key").hide();
    $("#lblValidation_Garantia_TipoGarantiaSuper_Key").text("");

    $("#lblValidation_Garantia_TipoGarantiaBladex_Key").hide();
    $("#lblValidation_Garantia_TipoGarantiaBladex_Key").text("");

    $("#lblValidation_Garantia_CategoriaRiesgoGarantia_Key").hide();
    $("#lblValidation_Garantia_CategoriaRiesgoGarantia_Key").text("");

    $("#lblValidation_Garantia_FrecuenciaRevision_Key").hide();
    $("#lblValidation_Garantia_FrecuenciaRevision_Key").text("");

    $("#lblValidation_Garantia_RatingGarante_Key").hide();
    $("#lblValidation_Garantia_RatingGarante_Key").text("");

    $("#lblValidation_Garantia_Status_Key").hide();
    $("#lblValidation_Garantia_Status_Key").text("");

    if ($("#Garantia_CategoriaSuper_Key").val() == "05") {
        $("#lblValidation_Garantia_Emisor_Key").hide();
        $("#lblValidation_Garantia_Emisor_Key").text("");

        $("#lblValidation_Garantia_TipoInstrumentoFinanciero_Key").hide();
        $("#lblValidation_Garantia_TipoInstrumentoFinanciero_Key").text("");

        $("#lblValidation_Garantia_CalificacionesRiesgoEmision_Key").hide();
        $("#lblValidation_Garantia_CalificacionesRiesgoEmision_Key").text("");

        $("#lblValidation_Garantia_CalificacionesRiesgoEmisor_Key").hide();
        $("#lblValidation_Garantia_CalificacionesRiesgoEmisor_Key").text("");

        $("#lblValidation_Garantia_Depositante_Key").hide();
        $("#lblValidation_Garantia_Depositante_Key").text("");
    }

    if ($("#Garantia_CategoriaSuper_Key").val() == "01") {
        $("#lblValidation_Garantia_NumeroPoliza").hide();
        $("#lblValidation_Garantia_NumeroPoliza").text("");
        $("#lblGarantia_FechaUltimaRevisionEvaluacion_Validation").hide();
        $("#lblGarantia_FechaUltimaRevisionEvaluacion_Validation").text("");
    }
    if ($("#Garantia_CategoriaSuper_Key").val() == "02") {
        $("#lblGarantia_ValorAvaluo_Validation").hide();
        $("#lblGarantia_ValorAvaluo_Validation").text("");

        $("#lblGarantia_ValorVentaRapida_Validation").hide();
        $("#lblGarantia_ValorVentaRapida_Validation").text("");

        $("#lblValidation_Garantia_NumeroPoliza").hide();
        $("#lblValidation_Garantia_NumeroPoliza").text("");

        $("#lblValidation_Garantia_AseguradorSuper").hide();
        $("#lblValidation_Garantia_AseguradorSuper").text("");
    }

    if ($("#Garantia_CategoriaSuper_Key").val() != "03") {
        $("#lblGarantia_FechaVencimientoRiesgo_Validation").hide();
        $("#lblGarantia_FechaVencimientoRiesgo_Validation").text("");
    }

    if ($("#Garantia_CategoriaSuper_Key").val() == "04") {
        $("#lblGarantia_FechaUltimaRevision_Validation").hide();
        $("#lblGarantia_FechaUltimaRevision_Validation").text("");

        $("#lblValidation_Garantia_Depositante_Key").hide();
        $("#lblValidation_Garantia_Depositante_Key").text("");
    }

    $("#lblGarantia_FechaVencimientoGarantia_Validation").hide();
    $("#lblGarantia_FechaVencimientoGarantia_Validation").text("");

    $("#lblValidation_Garantia_Region_Key").hide();
    $("#lblValidation_Garantia_Region_Key").text("");

    $("#lblValidation_Garantia_PaisGarantia_Key").hide();
    $("#lblValidation_Garantia_PaisGarantia_Key").text("");

    $("#lblValidation_Garantia_TipoPoliza").hide();
    $("#lblValidation_Garantia_TipoPoliza").text("");
}

function SaveGarantia_OnClick() {

    // Para comienzo de esta funcion tener presenta esta otra: HabilitarControlesParaSave()
    $("#btnSaveGarantia").attr("disabled", "disabled");
    $("#" + fiduciariaBladexFieldId).removeAttr("disabled");
    var comboBoxAseguradora = $("#" + nombreAseguradorFieldId).data("tComboBox");
    comboBoxAseguradora.enable();
    DisableFields(false);
    EnableRiskRelatedFields(true);

    if ($("#Garantia_CategoriaSuper_Key").val() == "02") {
        $("#Garantia_ValorMercado").removeAttr("disabled");
    }
    CleanLabelValidation();

    var clientId = $("#" + clientFieldId).val();
    var garanteId = $("#" + garanteFieldId).val();
    var fiduciriaSuperId = $("#" + fiduciariaSuperFieldId).val();
    var tipoGarantiaSuperId = $("#" + tipoGarantiaSuperFieldId).val();
    var tipoGarantiaBladex = $("#" + tipoGarantiaBladexFieldId).val();
    var categoriaRiesgo = $("#" + categoriaRiesgoFieldId).val();
    var frecuenciaRevision = $("#" + frecuenciaRevisionFieldId).val();
    var ratingGarante = $("#" + ratingGaranteFieldId).val();
    var status = $("#" + statusFieldId).val();
    var identificadorFideicomiso = $("#" + identificadorFideicomisoId).val();
    var valorAvaluo = 0;
    var numeroPoliza = $("#" + garantiaNumeroPolizaSeguro).val();


    var resClient = ValidateComboBox(clientFieldId, clientId);
    if (!resClient) {
        $("#lblValidation_Garantia_Cliente_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_Cliente_Key").show();
    }

    var resGarante = ValidateComboBox(garanteFieldId, garanteId);
    if (!resGarante) {
        if ($("#Garantia_CategoriaSuper_Key").val() == "04") {
            $("#lblValidation_Garantia_Garante_Key").text("Error: El valor seleccionado no coincide con el Nombre Depositante/Custodio/Fiel");
            $("#lblValidation_Garantia_Garante_Key").show();
        }
        else {
            $("#lblValidation_Garantia_Garante_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo. Por favor para ingresar un nuevo garante hacer click en el boton 'Ingresar un nuevo garante'");
            $("#lblValidation_Garantia_Garante_Key").show();
        }
    }

    var resFiduciariaSuper = ValidateComboBox(fiduciariaSuperFieldId, fiduciriaSuperId);
    if (!resFiduciariaSuper) {
        $("#lblValidation_Garantia_FiduciariaSuper_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_FiduciariaSuper_Key").show();
    }

    var resTipoGarantiaSuper = ValidateComboBox(tipoGarantiaSuperFieldId, tipoGarantiaSuperId);
    if (!resTipoGarantiaSuper) {
        $("#lblValidation_Garantia_TipoGarantiaSuper_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_TipoGarantiaSuper_Key").show();
    }
    else {
        if (tipoGarantiaSuperId == "-1") {
            $("#lblValidation_Garantia_TipoGarantiaSuper_Key").text("Error: Debe seleccionar un tipo de garantía válido");
            $("#lblValidation_Garantia_TipoGarantiaSuper_Key").show();
            resTipoGarantiaSuper = false;
        }
    }

    var resTipoGarantiaBladex = ValidateComboBox(tipoGarantiaBladexFieldId, tipoGarantiaBladex);
    if (!resTipoGarantiaBladex) {
        $("#lblValidation_Garantia_TipoGarantiaBladex_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_TipoGarantiaBladex_Key").show();
    }

    var resCategoriaRiesgo = ValidateComboBox(categoriaRiesgoFieldId, categoriaRiesgo);
    if (!resCategoriaRiesgo) {
        $("#lblValidation_Garantia_CategoriaRiesgoGarantia_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_CategoriaRiesgoGarantia_Key").show();
    }

    var resFrecuenciaRevision = ValidateComboBox(frecuenciaRevisionFieldId, frecuenciaRevision);
    if (!resFrecuenciaRevision) {
        $("#lblValidation_Garantia_FrecuenciaRevision_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_FrecuenciaRevision_Key").show();
    }

    var resRatingGarante = ValidateComboBox(ratingGaranteFieldId, ratingGarante);
    if (!resRatingGarante) {
        $("#lblValidation_Garantia_RatingGarante_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_RatingGarante_Key").show();
    }

    var resStatus = ValidateComboBox(statusFieldId, status);
    if (!resStatus) {
        $("#lblValidation_Garantia_Status_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_Status_Key").show();
    }

    var resIdentificadorFideicomiso = false;
    resIdentificadorFideicomiso = true;
    /*if (identificadorFideicomiso != null && identificadorFideicomiso != "") {
        resIdentificadorFideicomiso = true;
    }
    else {
        $("#lblValidation_Garantia_IdentificacionDocumentoGarantia_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
        $("#lblValidation_Garantia_IdentificacionDocumentoGarantia_Key").show();         
    }*/


    var resEmisor;
    var resTipoInstrumentoFinanciero;
    var resCalificacionEmision;
    var resCalificacionEmisor;
    var resDepositante = false;

    //Si la garantia es prendaria valido sus campos
    if ($("#Garantia_CategoriaSuper_Key").val() == "05") {

        var emisor = $("#Garantia_Emisor_Key").data("tComboBox").text();
        var tipoInstrumentoFinanciero = $("#" + tipoInstrumentoFinancieroFieldId).val();
        var calificacionEmision = $("#" + calificacionEmisionFieldId).val();
        var calificacionEmisor = $("#" + calificacionEmisorFieldId).val();
        var comboBoxDepositante = $("#" + depositanteFieldId).data("tComboBox");

        resEmisor = ValidateComboBox(emisorFieldId, emisor);
        if (!resEmisor) {
            $("#lblValidation_Garantia_Emisor_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
            $("#lblValidation_Garantia_Emisor_Key").show();
        }

        resTipoInstrumentoFinanciero = ValidateComboBox(tipoInstrumentoFinancieroFieldId, tipoInstrumentoFinanciero);
        if (!resTipoInstrumentoFinanciero) {
            $("#lblValidation_Garantia_TipoInstrumentoFinanciero_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
            $("#lblValidation_Garantia_TipoInstrumentoFinanciero_Key").show();
        }

        resCalificacionEmision = ValidateComboBox(calificacionEmisionFieldId, calificacionEmision);
        if (!resCalificacionEmision) {
            $("#lblValidation_Garantia_CalificacionesRiesgoEmision_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
            $("#lblValidation_Garantia_CalificacionesRiesgoEmision_Key").show();
        }

        resCalificacionEmisor = ValidateComboBox(calificacionEmisorFieldId, calificacionEmisor);
        if (!resCalificacionEmisor) {
            $("#lblValidation_Garantia_CalificacionesRiesgoEmisor_Key").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
            $("#lblValidation_Garantia_CalificacionesRiesgoEmisor_Key").show();
        }

        if (comboBoxDepositante != undefined && comboBoxDepositante != null) {
            if (comboBoxDepositante.value() == null || comboBoxDepositante.value() == "") {
                $("#lblValidation_Garantia_Depositante_Key").text("Error: El campo es mandatorio, debe ingresar un valor");
                $("#lblValidation_Garantia_Depositante_Key").show();
                resDepositante = false;

            }
            else {
                resDepositante = true;
            }
        }
        else {
            resDepositante = true;
        }
    }
    else {
        //Si no es prendaria coloco los campos en true
        resEmisor = true;
        resTipoInstrumentoFinanciero = true;
        resCalificacionEmision = true;
        resCalificacionEmisor = true;
        resDepositante = true;
    }

    var resValorAvaluo = false;
    var resValorVentaRapida = false;
    var resNumeroPoliza = true;
    var resAseguradorSuper = true;

	/*
     if ($("#Garantia_CategoriaSuper_Key").val() == "01") {

         if (numeroPoliza == undefined || numeroPoliza == null || numeroPoliza == "") {
             resNumeroPoliza = false;
             $("#lblValidation_Garantia_NumeroPoliza").text("Error: El campo es mandatorio, debe ingresar un valor");
             $("#lblValidation_Garantia_NumeroPoliza").show();
         }
     }
	*/

    //Si es inmueble chequeo valor avaluo > 0 
    if ($("#Garantia_CategoriaSuper_Key").val() == "02") {

        var valorAvaluo = $("#Garantia_ValorAvaluo").val();
        var valorVentaRapida = $('#Garantia_ValorEvaluacionVentaRapida').val();
        var aseguradorSuper = $("#" + aseguradorSuperFieldId).val();

        if (numeroPoliza == undefined || numeroPoliza == null || numeroPoliza == "") {
            resNumeroPoliza = false;
            $("#lblValidation_Garantia_NumeroPoliza").text("Error: El campo es mandatorio, debe ingresar un valor");
            $("#lblValidation_Garantia_NumeroPoliza").show();
        }

        if (valorVentaRapida != undefined && valorVentaRapida > 0) {
            resValorVentaRapida = true;
        }
        else {
            $("#lblGarantia_ValorVentaRapida_Validation").text("Error: El valor ingresado debe ser mayor a 0");
            $("#lblGarantia_ValorVentaRapida_Validation").show();
            resValorVentaRapida = false;
        }

        if (valorAvaluo != undefined && valorAvaluo > 0) {
            resValorAvaluo = true;
        }
        else {
            $("#lblGarantia_ValorAvaluo_Validation").text("Error: El valor ingresado debe ser mayor a 0");
            $("#lblGarantia_ValorAvaluo_Validation").show();
            resValorAvaluo = false;
        }

        if (aseguradorSuper != undefined && aseguradorSuper != null) {
            resAseguradorSuper = ValidateComboBox(aseguradorSuperFieldId, aseguradorSuper);
            if (resAseguradorSuper == false) {

                var comboBoxAsegurador = $("#" + aseguradorSuperFieldId).data("tComboBox");
                comboBoxAsegurador.text('');
                comboBoxAsegurador.value('');
                comboBoxAsegurador.reload();

                $("#lblValidation_Garantia_AseguradorSuper").text("Error: El valor seleccionado no se encuentra dentro del catalogo");
                $("#lblValidation_Garantia_AseguradorSuper").show();
            }
        }

    }
    else {
        resValorAvaluo = true;
        resValorVentaRapida = true;
    }

    var resClosuredate = true;
    var resFechaRevision = true;

	/*var controlVencimientoRiesgo = $('#Garantia_FechaVencimientoRiesgo').data('tDatePicker');
    if (controlVencimientoRiesgo != undefined && controlVencimientoRiesgo != null) {
        if (controlVencimientoRiesgo.value() == null || controlVencimientoRiesgo.value() == "") {
             $("#lblGarantia_FechaVencimientoRiesgo_Validation").text("Error: El campo es mandatorio, debe ingresar un valor");
             $("#lblGarantia_FechaVencimientoRiesgo_Validation").show();
             resClosuredate = false;
         }
     }*/

    if ($("#Garantia_CategoriaSuper_Key").val() == "04") {
        /// var controlFechaUltimaRevision = $('#Garantia_FechaUltimaRevisionEvaluacion').data('tDatePicker');
        var comboBoxDepositante = $("#" + depositanteFieldId).data("tComboBox");

        /*if (controlFechaUltimaRevision != undefined && controlFechaUltimaRevision != null) {
            if (controlFechaUltimaRevision.value() == null || controlFechaUltimaRevision.value() == "") {
                $("#lblGarantia_FechaUltimaRevision_Validation").text("Error: El campo es mandatorio, debe ingresar un valor");
                $("#lblGarantia_FechaUltimaRevision_Validation").show();
                resFechaRevision = false;
            }
        }*/
        if (numeroPoliza == undefined || numeroPoliza == null || numeroPoliza == "") {
            resNumeroPoliza = false;
            $("#lblValidation_Garantia_NumeroPoliza").text("Error: El campo es mandatorio, debe ingresar un valor");
            $("#lblValidation_Garantia_NumeroPoliza").show();
        }

        if (comboBoxDepositante != undefined && comboBoxDepositante != null) {
            if (comboBoxDepositante.value() == null || comboBoxDepositante.value() == "") {
                $("#lblValidation_Garantia_Depositante_Key").text("Error: El campo es mandatorio, debe ingresar un valor");
                $("#lblValidation_Garantia_Depositante_Key").show();
                resDepositante = false;

            }
            else {
                resDepositante = true;
            }
        }
        else {
            resDepositante = true;
        }

    }

    if ($("#Garantia_CategoriaSuper_Key").val() == "06") {
        if (numeroPoliza == undefined || numeroPoliza == null || numeroPoliza == "") {
            resNumeroPoliza = false;
            $("#lblValidation_Garantia_NumeroPoliza").text("Error: El campo es mandatorio, debe ingresar un valor");
            $("#lblValidation_Garantia_NumeroPoliza").show();
        }
    }


    var controlFechaUltimaRevision = $('#Garantia_FechaUltimaRevisionEvaluacion').data('tDatePicker');

    if ($("#Garantia_CategoriaSuper_Key").val() != "01") {

        if (controlFechaUltimaRevision != undefined && controlFechaUltimaRevision != null) {
            if (controlFechaUltimaRevision.value() == null || controlFechaUltimaRevision.value() == "") {
                $("#lblGarantia_FechaUltimaRevisionEvaluacion_Validation").text("Error: El campo es mandatorio, debe ingresar un valor");
                $("#lblGarantia_FechaUltimaRevisionEvaluacion_Validation").show();
                resFechaRevision = false;
            }
        }
    }
    else {
        if (controlFechaUltimaRevision != undefined && controlFechaUltimaRevision != null) {
            if ((controlFechaUltimaRevision.value() == null || controlFechaUltimaRevision.value() == "") && numeroPoliza.toUpperCase() != "NA") {
                $("#lblGarantia_FechaUltimaRevisionEvaluacion_Validation").text("Error: El campo es mandatorio, debe ingresar un valor");
                $("#lblGarantia_FechaUltimaRevisionEvaluacion_Validation").show();
                resFechaRevision = false;
            }
        }
    }

    var resFechaVencimientoGarantia = true;
    var controlFechaVencimientoGarantia = $("#Garantia_FechaVencimientoGarantia").data("tDatePicker");
    if (controlFechaVencimientoGarantia == undefined || controlFechaVencimientoGarantia == null) {
        /*$("#lblGarantia_FechaVencimientoGarantia_Validation").text("Error: La fecha de Vencimiento de la Garantia es un campo mandatorio.");
        $("#lblGarantia_FechaVencimientoGarantia_Validation").show();
        resFechaVencimientoGarantia = false;*/
    }
    else {
        if (controlFechaVencimientoGarantia.value() != null) {
            var date = new Date(controlFechaVencimientoGarantia.value());
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            var today = new Date(mm + "/" + dd + "/" + yyyy);
            if ((date - today) < 0) {
                $("#lblGarantia_FechaVencimientoGarantia_Validation").text("Error: La fecha de Vencimiento de la Garantia no puede ser anterior a la fecha actual");
                $("#lblGarantia_FechaVencimientoGarantia_Validation").show();
                resFechaVencimientoGarantia = false;
            }
        }
    }

    // Ticket #1619
    var resRegion = true;
    var varValueOrigen = $('#Garantia_OrigenGarantia').val().toUpperCase();
    var ddlRegion = $("#" + regionFieldId).data("tDropDownList");
    var ddlPaisGarantiaReg = $("#Garantia_PaisGarantia_Key").data("tDropDownList");
    if (ddlRegion != undefined && ddlRegion != null && ddlPaisGarantiaReg != undefined && ddlPaisGarantiaReg != null) {
        // Si es local (o sea Panamá) y no se selecciono region, entonces error.
        if ((ddlRegion.value() == null || ddlRegion.value() == "NA") && varValueOrigen == 'L') {
            $("#lblValidation_Garantia_Region_Key").text("Error: El campo es mandatorio, debe ingresar un valor válido");
            $("#lblValidation_Garantia_Region_Key").show();
            resRegion = false;
        }

        // Si es local y el pais seleccionado es distinto de Panamá, es error.
        if ((ddlPaisGarantiaReg.value() == null || ddlPaisGarantiaReg.value() != "PA") && varValueOrigen == 'L') {
            $("#lblValidation_Garantia_PaisGarantia_Key").text("Error: El Pais de la garantía no corresponde al Origen 'local' (Panamá)");
            $("#lblValidation_Garantia_PaisGarantia_Key").show();
            resRegion = false;
        }
    }
    else {
        resRegion = false;
    }

    var resTipoPoliza = true;
    var varValueNumeroPoliza = $('#' + garantiaNumeroPolizaSeguro).val().toUpperCase();
    var ddlTipoPoliza = $("#" + tipoPolizaFieldId).data("tDropDownList");

    if ($("#Garantia_CategoriaSuper_Key").val() != "04") {
        // Si es create/edit de garantia pignorado en otro banco, no hace falta validar este campo, siempre va 'NA' en tipo poliza.
        if (ddlTipoPoliza != undefined && ddlTipoPoliza != null) {
            //Si el numero de poliza tiene un valor distinto a NA es obligatorio seleccionar un valor distinto a NA en el campo tipo poliza
            if ((ddlTipoPoliza.value() == null || ddlTipoPoliza.value() == "NA") && varValueNumeroPoliza != "NA" && varValueNumeroPoliza != "") {
                $("#lblValidation_Garantia_TipoPoliza").text("Error: El campo es mandatorio, debe ingresar un valor válido");
                $("#lblValidation_Garantia_TipoPoliza").show();
                resTipoPoliza = false;
            }
        }
    }

    if (resClient && resGarante && resFiduciariaSuper && resTipoGarantiaSuper && resTipoGarantiaBladex && resCategoriaRiesgo
        && resFrecuenciaRevision && resRatingGarante && resStatus && resEmisor && resTipoInstrumentoFinanciero && resCalificacionEmision && resCalificacionEmisor
        && resValorAvaluo && resValorVentaRapida & resClosuredate && resFechaRevision && resDepositante && resNumeroPoliza && resFechaVencimientoGarantia && resAseguradorSuper && resRegion && resTipoPoliza) {
        $("form").submit();
        //& resIdentificadorFideicomiso
    }
    else {
        // Compatibilidad de validaciones js con las de .net
        $("form").valid();
        alert("Error la garantia no sera salvada");

        $("#btnSaveGarantia").removeAttr("disabled");

    }

    // Para lo de abajo, tener presente esta funcion: DeshabilitarControlesParaSave
    // Ticket #1654: si no es 599, exterior entonces disabled 
    if ($("#Garantia_FiduciariaSuper_Key").val() != fiduciariaEnElExteriorID) {
        $("#" + fiduciariaBladexFieldId).attr("disabled", "disabled");
    }
    comboBoxAseguradora.disable();

    DisableFields(true);
    EnableRiskRelatedFields(false);

    if ($("#Garantia_CategoriaSuper_Key").val() == "02") {
        $("#Garantia_ValorMercado").attr("disabled", "disabled");
    }

    $("#btnSaveGarantia").removeAttr("disabled");
}

function HabilitarControlesParaSave() {
    /* IMPORTANTE: esta funcion equivale a lo mismo que tiene el boton save al COMIENZO,
    por lo tanto lo que se agregue ahi, tambien tiene que agregarse aca, esto habilita a guardar los datos!*/
    $("#btnSaveGarantia").attr("disabled", "disabled");
    $("#" + fiduciariaBladexFieldId).removeAttr("disabled");

    DisableFields(false);
    EnableRiskRelatedFields(true);

    if ($("#Garantia_CategoriaSuper_Key").val() == "02") {
        $("#Garantia_ValorMercado").removeAttr("disabled");
    }
}

function DeshabilitarControlesParaSave() {
    /* IMPORTANTE: esta funcion equivale a lo mismo que tiene el boton save al FINAL,
    por lo tanto lo que se agregue ahi, tambien tiene que agregarse aca, esto habilita a guardar los datos!*/
    // Ticket #1654: si no es 599, exterior entonces disabled 
    if ($("#Garantia_FiduciariaSuper_Key").val() != fiduciariaEnElExteriorID) {
        $("#" + fiduciariaBladexFieldId).attr("disabled", "disabled");
    }

    DisableFields(true);
    EnableRiskRelatedFields(false);

    if ($("#Garantia_CategoriaSuper_Key").val() == "02") {
        $("#Garantia_ValorMercado").attr("disabled", "disabled");
    }

    $("#btnSaveGarantia").removeAttr("disabled");
}

function validateFiduciariaExterior(a) {
    //Ticket #1550 : Si la fiduciaria super es Fiduciaria en el exterior, habilito el dropdown fiduciaria en el exterior
    var controlId = a.currentTarget.id
    var fiduciariaExteriorControl = $("#" + fiduciariaBladexFieldId);


    if (controlId == fiduciariaSuperFieldId) {
        var varValue = a.value;

        // Ticket #1654: ("formulario esta colocando por default NA en el campo a pesar que el mismo tiene valor en Base de datos")
        if (a.type == "load") {
            varValue = $("#Garantia_FiduciariaSuper_Key").data("tComboBox").selectedValue;
        }

        // Deshabilito el control y le pongo 'NA'
        if (varValue != null && varValue != undefined && varValue == fiduciariaEnElExteriorID) {
            // Ticket #1654: si el control Fic en el Ext ya tiene un valor le coloca el valor sino, vacio.
            var fidExterior = $("#Garantia_FiduciariaBladex").val();
            if (fidExterior == undefined || fidExterior == 'NA')
                fidExterior = '';

            $("#" + fiduciariaBladexFieldId).val(fidExterior);
            $("#" + fiduciariaBladexFieldId).removeAttr("disabled");
        }
        else {
            // Deshabilito el control y le pongo 'NA'
            $("#" + fiduciariaBladexFieldId).val("NA");
            $("#" + fiduciariaBladexFieldId).attr("disabled", "disabled");
        }
    }

}

function validateAseguradorSuper(a) {
    //Ticket #1550: Si el asegurador Super es en el exterior, habilito el nombre asegurador

    var controlId = a.currentTarget.id;
    var ExteriorKey = null;

    if (controlId == aseguradorSuperFieldId) {
        var varValue = a.value;

        //Si es mueble miro el asegurador
        if ($("#Garantia_CategoriaSuper_Key").val() == "01") {
            var comboBoxAseguradora = $("#" + nombreAseguradorFieldId).data("tComboBox");
            var ddlAseguradoraPais = $("#" + aseguradoraPaisFieldId).data("tDropDownList");
            ExteriorKey = aseguradoraEnElExteriorID;
        }
        else {
            //si es inmueble veo el avaluador
            if ($("#Garantia_CategoriaSuper_Key").val() == "02") {
                //Si el asegurador es vacio lo seteo en NA
                var comboBoxAsegurador = $("#" + nombreAseguradorFieldId).data("tComboBox");
                if (comboBoxAsegurador != undefined && comboBoxAsegurador.text() == "") {
                    comboBoxAsegurador.text("NA");
                    $("#" + nombreAseguradorFieldId).val("NA");
                }

                var comboBoxAseguradora = $("#" + nombreAvaluadorFieldId).data("tComboBox");
                var ddlAseguradoraPais = $("#" + avaluadoraPaisFieldId).data("tDropDownList");
                ExteriorKey = avaluadoraEnElExteriorID;
            }
        }

        if (comboBoxAseguradora != undefined) {

            if (varValue != null && varValue != undefined && varValue == ExteriorKey) {

                comboBoxAseguradora.enable();
                comboBoxAseguradora.text("");
                //$("#"+nombreAseguradorFieldId).val("NA");

                if (ddlAseguradoraPais != undefined) {
                    ddlAseguradoraPais.text("NA");
                    ddlAseguradoraPais.enable();
                }
            }
            else {
                $("#" + nombreAseguradorFieldId).val("NA");
                comboBoxAseguradora.text("NA");
                comboBoxAseguradora.disable();

                if (ddlAseguradoraPais != undefined) {
                    ddlAseguradoraPais.text("NA");
                    ddlAseguradoraPais.disable();
                }
            }
        }

    }



}

function validateNombreAseguradora(a) {
    if (a.type == "load") {
        var nombreAseguradoraControl = a.currentTarget.id;

        if (nombreAseguradoraControl == nombreAseguradorFieldId) {
            var comboBoxAseguradora = $("#" + nombreAseguradorFieldId).data("tComboBox");
            var ddlAseguradoraPais = $("#" + aseguradoraPaisFieldId).data("tDropDownList");
            if (comboBoxAseguradora != undefined) {
                if (comboBoxAseguradora.text() == "") {
                    comboBoxAseguradora.text("NA");
                }
                var aseguradoraSuperValue = $("#" + aseguradorSuperFieldId).val();

                if (aseguradoraSuperValue != undefined && aseguradoraSuperValue != null && aseguradoraSuperValue.toUpperCase() == "NA") {
                    comboBoxAseguradora.text("NA");
                    comboBoxAseguradora.disable();
                }
            }
        }
    }
}

function validateAseguradoraPais(a) {
    if (a.type == "load") {
        var nombreControl = a.currentTarget.id;
        if (nombreControl == aseguradoraPaisFieldId) {

            var ddlAseguradoraPais = $("#" + aseguradoraPaisFieldId).data("tDropDownList");
            var nombreAseguradorValue = $("#Garantia_Asegurador_Nombre").val();

            if (ddlAseguradoraPais != undefined && nombreAseguradorValue.toUpperCase() == "NA") {
                ddlAseguradoraPais.text("NA");
                ddlAseguradoraPais.disable();
            }
        }
    }
}

function validateNombreOrganismo(a) {

    var nombreControl = a.currentTarget.id;
    var varCategoriaSuper = $("#Garantia_CategoriaSuper_Key").val();
    switch (varCategoriaSuper) {
        case "04":
            if (nombreControl == bancoSuperFieldId) {
                var comboBoxBancoSuper = $("#Garantia_BancoSuper_Key").data("tDropDownList");

                if (comboBoxBancoSuper != undefined && comboBoxBancoSuper != null) {
                    var varValue = comboBoxBancoSuper.text();

                    if (varValue == "&nbsp;")
                        varValue = "";

                    if (varValue != undefined && varValue != null) {

                        $("#Garantia_NombreOrganismo").val(varValue);
                    }
                }
            }
            break;
        case "05":

            if (nombreControl == depositanteFieldId) {
                var comboBoxDepositante = $("#" + depositanteFieldId).data("tComboBox");
                var comboBoxEmisor = $("#" + emisorFieldId).data("tComboBox");

                var varText = comboBoxDepositante.text();
                var varValue = comboBoxDepositante.value();

                if (varText != undefined && varText != null) {

                    $("#Garantia_NombreOrganismo").val(varText);

                    if (comboBoxEmisor != undefined && comboBoxEmisor != null) {
                        comboBoxEmisor.text(varText);
                        comboBoxEmisor.value(varValue);
                    }
                }
            }
            break;
        case "06":
            if (nombreControl == emisorFieldId || nombreControl == tipoGarantiaSuperFieldId || nombreControl == garanteFieldId) {
                var comboboxTipoGarantiaSuper = $("#" + tipoGarantiaSuperFieldId).data("tComboBox");
                var comboboxGarante = $("#" + garanteFieldId).data("tComboBox");
                var comboBoxEmisor = $("#" + emisorFieldId).data("tComboBox");

                var varText = "";
                if (comboboxTipoGarantiaSuper != undefined && comboboxGarante != undefined && comboBoxEmisor != undefined) {
                    if (comboboxTipoGarantiaSuper.value() == "0604" || comboboxTipoGarantiaSuper.value() == "0605") {
                        varText = comboboxGarante.text();
                    }
                    else {
                        varText = comboBoxEmisor.text();
                    }

                    if (varText != undefined && varText != null) {
                        $("#Garantia_NombreOrganismo").val(varText);
                    }
                }
            }
            break;

        default:

            break;
    }

}

function validateTipoInstrumento(a) {

    var nombreControl = a.currentTarget.id;
    if ($("#Garantia_CategoriaSuper_Key").val() == "05") {

        if (nombreControl == tipoGarantiaSuperFieldId) {
            var comboBox = $("#" + tipoInstrumentoFinancieroFieldId).data("tComboBox");
            if (comboBox != undefined && (a.value == "0503" || a.value == "0504" || a.value == "0505")) {
                comboBox.text("NA");
            }

            EnableRiskRelatedFields(false);
        }
    }
    //tipoGarantiaSuperFieldId
}

function EnableRiskRelatedFields(habilitar) {
    // Fix:1550
    if ($("#Garantia_CategoriaSuper_Key").val() == "05") {
        if (habilitar) {
            $("#Garantia_TipoInstrumentoFinanciero_Key").data("tComboBox").enable();
            $("#Garantia_CalificacionesRiesgoEmision_Key").data("tComboBox").enable();
            $("#Garantia_CalificacionesRiesgoEmisor_Key").data("tComboBox").enable();
            $("#Garantia_PaisEmision_Key").data("tDropDownList").enable();
        }
        else {
            if ($("#Garantia_CategoriaSuper_Key").val() == "05" && $("#Garantia_PaisEmision_Key").data("tDropDownList") != undefined) {
                var comboBox = $("#" + tipoGarantiaSuperFieldId).data("tComboBox");
                var varValue = comboBox.value();
                if (comboBox == "0502" || varValue == "0506" || varValue == "0507" || varValue == "0508" || varValue == "0509") {
                    $("#Garantia_TipoInstrumentoFinanciero_Key").data("tComboBox").enable();
                    $("#Garantia_CalificacionesRiesgoEmision_Key").data("tComboBox").enable();
                    $("#Garantia_CalificacionesRiesgoEmisor_Key").data("tComboBox").enable();
                    $("#Garantia_PaisEmision_Key").data("tDropDownList").enable();
                }
                else {
                    $("#Garantia_TipoInstrumentoFinanciero_Key").data("tComboBox").disable();
                    $("#Garantia_CalificacionesRiesgoEmision_Key").data("tComboBox").disable();
                    $("#Garantia_CalificacionesRiesgoEmisor_Key").data("tComboBox").disable();
                    $("#Garantia_PaisEmision_Key").data("tDropDownList").disable();

                    $("#Garantia_TipoInstrumentoFinanciero_Key").data("tComboBox").value("NA");
                    $("#Garantia_CalificacionesRiesgoEmision_Key").data("tComboBox").value("NA");
                    $("#Garantia_CalificacionesRiesgoEmisor_Key").data("tComboBox").value("NA");
                    $("#Garantia_PaisEmision_Key").data("tDropDownList").value("N/A");
                }
            }
        }
    }
}
