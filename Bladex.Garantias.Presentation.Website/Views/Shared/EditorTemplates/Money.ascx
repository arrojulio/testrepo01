<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal>" %>
<%--<%=Html.TextBox("", (Model.ToString(ViewData.ModelMetadata.EditFormatString)), new { @class = "money" })%>--%>
<%= Html.Telerik().CurrencyTextBox()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .InputHtmlAttributes(new { style = "width:100%", @class = "money txtMoney" })
        .ButtonTitleDown("Decrease amount")
        .ButtonTitleUp("Increase amount")
        .DecimalDigits(2)
        .EmptyMessage("(empty)")
        .Spinners(true)
        .MinValue(Decimal.MinValue)
        .Value(Model)
            .ClientEvents(events => events.OnChange(string.Format("onMoneyChange_{0}", ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty))))
%>

<script type="text/javascript">
    
    function onMoneyChange_<%= ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty) %>(e) {
    
        var reportChange = '<%= ViewData.TemplateInfo.GetFullHtmlFieldId(string.Empty) %>';
        logInBrowser("ID of Control raising the onChange event: " + reportChange);
        logInBrowser("Old Value of Control: " + e.oldValue);
        logInBrowser("New Value of Control: " + e.newValue);

//          if($("#Garantia_CategoriaSuper_Key").val() == "02"){ 
//                if(reportChange=='Garantia_ValorAvaluo')
//                {
//                    var htmlControlId = 'Garantia_ValorGarantiaSuperIntendencia';
//                        var value = e.newValue;            
//                        if (value && htmlControlId) {
//                            var inputMoney = $("#" + htmlControlId).data("tTextBox");
//                            inputMoney.value(value);
//                        }
//                }                 
//            }

        if(jQuery.inArray( reportChange, controlsThatReportChange) > -1)
        {
//          if($("#Garantia_CategoriaSuper_Key").val() == "02"){ 
//                if(reportChange=='Garantia_ValorAvaluo')
//                {
//                    var htmlControlId = 'Garantia_ValorGarantiaSuperIntendencia';
//                        var value = e.newValue;            
//                        if (value && htmlControlId) {
//                            var inputMoney = $("#" + htmlControlId).data("tTextBox");
//                            inputMoney.value(value);
//                        }
//                }                 
//            }

            //Ticket #1550 : 
            if($("#Garantia_CategoriaSuper_Key").val() == "02"){
            //if garantia super = inmueble
                    if(reportChange=='Garantia_ValorEvaluacionVentaRapida')
                    {
                        var htmlControlId = 'Garantia_ValorMercado';
                            var value = e.newValue;            
                            if (value && htmlControlId) {
                                var inputMoney = $("#" + htmlControlId).data("tTextBox");
                                inputMoney.value(value);
                            }
                    }
                                                           
            }            

            logInBrowser(reportChange + " Control report changes.");
            
            var obj = new Object();          
          
            $(controlsThatReportChange).each(function() {
                var value = "";
                if(this == reportChange)
                {
                    value = e.newValue;
                }
                else
                {
                    // If control exists in form, take his value.
                    if(document.getElementById(this)) {
                        value = $("#" + this).data("tTextBox").value();
                    }
                    else 
                        value = "0";
                }
                var idOfControl = this.replace('Garantia_','');
                logInBrowser("Value obtained: " + value + " from control " + idOfControl);
                logInBrowser("Assigning value: " + value + " to array[" + idOfControl + "]");
                obj[idOfControl] = value.toString();
            });

            var idTargetControl = targetControl.replace('Garantia_','');
            logInBrowser("Value obtained: " + $("#" + targetControl).data("tTextBox").value() + " from control " + idTargetControl);
            logInBrowser("Assigning value: " + $("#" + targetControl).data("tTextBox").value() + " to array[" + idTargetControl + "]");
            obj[idTargetControl] = $("#" + targetControl).data("tTextBox").value();
            
            var parameters = JSON.stringify(obj);
            var svcUrl = "";

            svcUrl = "CalcValorGarantiaSuperIntendencia";

            logInBrowser("Performing call to service " + svcUrl);            
                     
                $.ajax({
                    type: "POST",
                    url: svcUrl,
                    data: parameters,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        logInBrowser("Service respond with " + JSON.stringify(data));
                        var targetControlValue = data.ValorGarantiaSuperIntendencia;
                        logInBrowser("New Target Control Value is " + targetControlValue);
                        $("#" + targetControl).data("tTextBox").value(targetControlValue);
                    }
                });
           
            }
                                        
        }    
    
</script>


