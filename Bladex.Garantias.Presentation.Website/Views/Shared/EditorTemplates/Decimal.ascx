<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Decimal>" %>
<%=Html.TextBox("", (Model.ToString(ViewData.ModelMetadata.EditFormatString)), new { @class = "money" })%>
<script type="text/javascript">
    $(function () {
//        $(".money").formatCurrency({ roundToDecimalPlace: '2' });
//        $(".money").focus(function () {
//            $(this).val = $(this).toNumber();
//            /* Fix to avoid position of cursor at the begining of the value */
//            var pos = 100;
//            var range = $(this).get(0).createTextRange();
//            range.collapse(true);
//            range.moveEnd('character', pos);
//            range.moveStart('character', pos);
//            range.select();

//        });
//        $(".money").blur(function () {
//            $(this).formatCurrency({ roundToDecimalPlace: '2' });
//        });
        
    });
</script>
