<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<bool>" %>
<%if(Model) {%>
<div class="over-utilization-alert-container">
    <div class="over-utilization-alert-icon">
        
    </div>
    <div class="over-utilization-alert-text">
        <p>
            La garantía se encuentra sobre utilizada según los contratos seleccionados.
        </p>
    </div>
</div>
<%} %>
