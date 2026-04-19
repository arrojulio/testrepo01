<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.Controllers.GarantiaBaseController.VerifyGuaranteeBlock_Result>" %>
<%if( Model.Result) {%>
<script type="text/javascript">
    $(document).ready(function () {
        $("input[type='submit']").attr('disabled', true).hide();
    });
</script>
<div class="over-utilization-alert-container">
    <div class="over-utilization-alert-icon">
        
    </div>
    <div class="over-utilization-alert-text">
        <p>
            <%: Model.Message%>
            
        </p>
    </div>
</div>
<%} %>
