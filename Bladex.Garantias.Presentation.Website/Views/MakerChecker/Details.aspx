<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<MakerCheckerDetailViewModel>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="Bladex.Garantias.Presentation.Website" %>

<%@ Import Namespace="Bladex.Garantias.DomainModel.DomainBase.Components.MakerChecker" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    Maker and Checker Operations</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeaderContent">
    <script type="text/javascript">
        var backUrl = baseUrl + "/MakerChecker/Index";

        $(document).ready(function () {
            SetupPage();
        });
        function SetupPage() {
            $('.commentBoxInput').focus(function () { $(this).css('background-color', 'InfoBackground'); });
            $('.commentBoxInput').blur(function () { $(this).css('background-color', '#ffffff'); });
            // Make same size 
            var maxH = 0;
            var maxW = 0;
            // set same width for controls especified by the .itemContainer css class.
            $('.itemContainer').each(function () {
                if ($(this).width() > maxW)
                    maxW = $(this).width();
            });
            $('.itemContainer').width(maxW);

            // set same height for all controls
            $('.itemContainer').each(function () {
                if ($(this).height() > maxH)
                    maxH = $(this).height();
            });
            $('.itemContainer').height(maxH);
            var changesetId = '<%: Model.Changeset.ChangesetId %>';
            var operationId = '<%= Model.Operation.OperationId %>';
            $("#changeset-link").click(function (e) {
                if ($(this).text() == "changeset") {
                    $(this).text("changeset " + changesetId);
                } else {
                    $(this).text("changeset");
                }
                e.preventDefault();
                return false;
            });
            $("#btnApprove").click(function (e) {
                var comment = $("#Operation_Comment").val();
                $("#btnApprove").attr("disabled", "disabled");
                $.ajax({
                    type: "POST",
                    url: "Approve",
                    data: "{'operationId': " + operationId + ", 'comment': " + JSON.stringify(comment) + "}",
                    format: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data == "success") {
                            
                            window.location.reload(true);
                        } else {
                            logInBrowser(data);
                            alert('Ha ocurrido un error tratando de aprobar la operacion.\nLe pedimos que vuelva a intentar nuevamente.');
                        }
                    },
                    error: function (error) {
                        logInBrowser(error);
                        alert('Ha ocurrido un error tratando de aprobar la operacion.\nLe pedimos que vuelva a intentar nuevamente.');
                    }
                });
                e.preventDefault();
                return false;
            });
            $("#btnReject").click(function (e) {
                var comment = $("#Operation_Comment").val();
                $.ajax({
                    type: "POST",
                    url: "Reject",
                    data: "{'operationId': " + operationId + ", 'comment': " + JSON.stringify(comment) + "}",
                    format: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data == "success") {
                            window.location.reload(true);
                        } else {
                            logInBrowser(data);
                            alert('Ha ocurrido un error tratando de rechazar la operacion.\nLe pedimos que vuelva a intentar nuevamente.');
                        }
                    },
                    error: function (error) {
                        logInBrowser(error);
                        alert('Ha ocurrido un error tratando de rechazar la operacion.\nLe pedimos que vuelva a intentar nuevamente.');
                        
                    }
                });
                e.preventDefault();
                return false;
            });
        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
<div id="operation_container">
    <p>
        Operations of the <a id="changeset-link" href="#">changeset</a> submitted by the user <%:Model.Changeset.MakerUserId %></p>
    <p></p>
    <p>
        <input type="button" value="Regresar" onclick="javascript: document.location = backUrl;" />
    </p>

    <table id="operation_table" border="0" class="styled-table fixed">
        <thead>
            <tr>
                <th>
                    <div class="pager">
                    <%:Html.ActionLink("First", "Details", "MakerChecker", new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = 1 }, new { @class = "pager-number pager-number-first" })%>
                    <%for (int index = 0; index < Model.TotalPages; index++)
                      {%>
                    <% if ((index + 1) == Model.Page)
                       {%>
                    <span class="pager-number pager-number-selected">
                        <%=Model.Page %></span>
                    <% }
                       else
                       {%>
                    <%:Html.ActionLink((index + 1).ToString(), "Details", "MakerChecker", new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = (index + 1) }, new { @class="pager-number" })%>
                    <%}%>
                    <%}%>
                    <%:Html.ActionLink("Last", "Details", "MakerChecker", new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = Model.TotalPages }, new { @class = "pager-number pager-number-last" })%>
                    </div>
                </th>
            </tr>
            <tr>
                <th>
                    <span><%: Model.Operation.OperationStatus.OperationStatusDescription %> operation</span>
                </th>
            </tr>
        </thead>
        <tbody>
            <!-- Check if the item is new or already exists -->
            <%if (Model.Operation.ItemId.HasValue && Model.Operation.ItemId.Value != 0){%>
                <tr>
                    <td>
                        <% Html.RenderAction("CheckOverUtilization", "GarantiaBase", new { garantiaId = Model.Operation.ItemId.Value }); %>
                    </td>
                </tr>
            <%} %>
            <%--<%if(Model.Operation.OperationStatus.OperationStatusId == (int)MakerCheckerOperationStatus.OperationStatus.Rejected && Model.Changeset.MakerUserId == User.Identity.Name) {%>
                <tr>
                    <td>
                        <div id="enable_rejected_container" class="enable-rejected-container">
                        <%: Html.ActionLink("Click here to review your rejected operation. You can submit it again.", "Review", "MakerChecker", new { changesetId = Model.Changeset.ChangesetId.ToString(), operationId = Model.Operation.OperationId }, new { onclick="javascript:alert('This feature is not implemented yet.'); return false;" }) %>
                        </div>
                    </td>
                </tr>
            <%  }%>--%>
            <tr>
                <td>
                    <%:Html.HiddenFor(o=>o.Operation.OperationId) %>
                    <%:Html.HiddenFor(o=>o.Operation.OperationStatusId) %>
                    <%:Html.HiddenFor(o=>o.Operation.ItemId) %>
                    <div class="divRow">
                        <div>
                            <div style="float: left; vertical-align: middle">
                                <p>This operation was submitted in <%=Model.Operation.MakerDate %>.</p>
                                <%if (Model.Operation.OperationStatusId != (int)MakerCheckerOperationStatus.OperationStatus.New) {%>
                                    <p>This operation was <%=Model.Operation.OperationStatus.OperationStatusDescription.ToLower() %> in <%=Model.Operation.CheckerDate %> by the user <%=Model.Operation.CheckerUserId %>.</p>
                                <%}%>
                            </div>
                            <div style="float: right">
                                <%if (!Model.ReadOnly)
                                  {%>
                                <img id="btnApprove" style="margin: 5px 10px 5px 10px; outline-style: none; width: 30px;
                                    height: 30px; vertical-align: middle;" alt="Approve" title="Approve this operation"
                                    src="<%=Url.Content("~/Content/Images/buttons-30x30/02-yes.png") %>" />
                                    
                                <img id="btnReject" style="margin: 5px 10px 5px 10px; outline-style: none; width: 30px;
                                    height: 30px; vertical-align: middle;" alt="Reject" title="Return this operation"
                                    src="<%=Url.Content("~/Content/Images/buttons-30x30/02-no.png") %>" />
                                <% }
                                  else
                                  {%>
                                      <%if (Model.Operation.OperationStatusId==(int)MakerCheckerOperationStatus.OperationStatus.Rejected && Model.Changeset.MakerUser.Key.ToString()==this.User.Identity.Name && !Model.Changeset.MakerUser.IsChecker)
                                      {%>
                                      <%= string.Format("<a href='Review?operationId={0}' title='Review'>", Model.Operation.OperationId)%>
                                      <img id="btnReview" style="margin: 5px 10px 5px 10px; outline-style: none;
                                        width: 30px; height: 30px; vertical-align: middle;border-style: none;" alt="Review" title="Review this operation"
                                        src="<%=Url.Content("~/Content/Images/buttons-30x30/edit.png") %>" />
                                        </a>
                                    <%}%>
                                <img id="btnApproveDisabled" style="margin: 5px 10px 5px 10px; outline-style: none;
                                    width: 30px; height: 30px; vertical-align: middle;" alt="Approve" title="Approve this operation"
                                    src="<%=Url.Content("~/Content/Images/buttons-30x30/02-yes2.png") %>" />
                                <img id="btnRejectDisabled" style="margin: 5px 10px 5px 10px; outline-style: none;
                                    width: 30px; height: 30px; vertical-align: middle;" alt="Reject" title="Return this operation"
                                    src="<%=Url.Content("~/Content/Images/buttons-30x30/02-no2.png") %>" />
                                <%}%>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="divRow">
                        <p>Comments</p>
                        <div style="padding-left: 2px; padding-top: 2px;">
                            <%if (!Model.ReadOnly)
                              {%>
                            <%:Html.TextAreaFor(o => o.Operation.Comment, new { @class = "commentBox commentBoxInput" })%>
                            <%}
                              else
                              {%>
                            <div class="commentBox">
                                <%: string.IsNullOrEmpty(Model.Operation.Comment) ? "There is no comments for this operation." : Model.Operation.Comment %>
                            </div>
                            <%}%>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="divRow">
                    <div id="tabs">
                      <ul>
                        <li><a href="#tabs-1">Required Fields</a></li>
                        <li><a href="#tabs-2">Optional Fields</a></li>        
                      </ul>
                      <div id="tabs-1">                        
                            <table border="0" class="styled-table property-bag" >
                                <thead>
                                    <tr><th style="width:10%;">Property</th><th style="width:45%;">Actual</th><th style="width:45%;">Proposed</th></tr>
                                </thead>
                                <tbody>
                                    <%if (Model.Proposed.Count != 0) {%>                                
                                        <%foreach (var kv in Model.Proposed){%>                                                                          
                                            <%if (!Model.Original.ContainsKey(kv.Key) || (Model.Original.ContainsKey(kv.Key) && Model.Original[kv.Key] != kv.Value)){ %>                                            
                                                <%if (kv.GetKeyFormatted().Contains("(*)"))
                                                  {%>
                                                <tr>
                                                    <td>
                                                    <%:kv.GetKeyFormatted()%>                                                
                                                    </td>
                                                    <td>
                                                    <span><%= Model.Original.ContainsKey(kv.Key) ? Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() : "(empty)"%></span>
                                                    </td>
                                                    <%if (Model.Original.ContainsKey(kv.Key) && Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() != kv.GetValueFormatted())
                                                      {%>
                                                    <td class="property-bag-new"><%=kv.GetValueFormatted()%> 
                                                    <%}
                                                      else
                                                      {%>
                                                    <td><%=kv.GetValueFormatted()%>
                                                    <%}%>
                                                    </td>
                                                </tr>
                                                <%} %>
                                            <%}%>
                                        <%}%>
                                    <%} else if (Model.Original.Count != 0) {%>
                                        <%foreach (var kv in Model.Original){%>
                                            <%if (!Model.Proposed.ContainsKey(kv.Key) || (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed[kv.Key] != kv.Value)){ %>
                                                <%if (kv.GetKeyFormatted().Contains("(*)"))
                                                  {%>
                                                <tr>
                                                    <td>
                                                    <%:kv.GetKeyFormatted()%>
                                                
                                                    </td>
                                                    <td>
                                                    <%=Model.Proposed.ContainsKey(kv.Key) ? Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() : "(empty)"%>
                                                    </td>
                                                    <%if (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() == kv.GetValueFormatted())
                                                      {%>
                                                    <td class="property-bag-new"><%=kv.GetValueFormatted()%>
                                                    <% } else {%>
                                                    <td><%=kv.GetValueFormatted()%>
                                                    <%}%>
                                                    </td>
                                                </tr>
                                                <%} %>
                                            <%}%>
                                        <%}%>
                                    <%} else {%>
                                        <tr><td colspan="3"><p>There is no information to display.</p></td></tr>
                                    <% } %>
                                </tbody>
                            </table>                        
                      </div>
                      <div id="tabs-2">
                                 <table border="0" class="styled-table property-bag" >
                                <thead>
                                    <tr><th style="width:10%;">Property</th><th style="width:45%;">Actual</th><th style="width:45%;">Proposed</th></tr>
                                </thead>
                                <tbody>
                                    <%if (Model.Proposed.Count != 0) {%>                                
                                        <%foreach (var kv in Model.Proposed){%>                                                                          
                                            <%if (!Model.Original.ContainsKey(kv.Key) || (Model.Original.ContainsKey(kv.Key) && Model.Original[kv.Key] != kv.Value)){ %>                                            
                                                <%if (!kv.GetKeyFormatted().Contains("(*)"))
                                                  {%>
                                                <tr>
                                                    <td>
                                                    <%:kv.GetKeyFormatted()%>                                                
                                                    </td>
                                                    <td>
                                                    <span><%= Model.Original.ContainsKey(kv.Key) ? Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() : "(empty)"%></span>
                                                    </td>
                                                    <%if (Model.Original.ContainsKey(kv.Key) && Model.Original.First(o => o.Key == kv.Key).GetValueFormatted() != kv.GetValueFormatted())
                                                      {%>
                                                    <td class="property-bag-new"><%=kv.GetValueFormatted()%> 
                                                    <%}
                                                      else
                                                      {%>
                                                    <td><%=kv.GetValueFormatted()%>
                                                    <%}%>
                                                    </td>
                                                </tr>
                                                <%} %>
                                            <%}%>
                                        <%}%>
                                    <%} else if (Model.Original.Count != 0) {%>
                                        <%foreach (var kv in Model.Original){%>
                                            <%if (!Model.Proposed.ContainsKey(kv.Key) || (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed[kv.Key] != kv.Value)){ %>
                                                <%if (!kv.GetKeyFormatted().Contains("(*)"))
                                                  {%>
                                                <tr>
                                                    <td>
                                                    <%:kv.GetKeyFormatted()%>
                                                
                                                    </td>
                                                    <td>
                                                    <%=Model.Proposed.ContainsKey(kv.Key) ? Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() : "(empty)"%>
                                                    </td>
                                                    <%if (Model.Proposed.ContainsKey(kv.Key) && Model.Proposed.First(o => o.Key == kv.Key).GetValueFormatted() == kv.GetValueFormatted())
                                                      {%>
                                                    <td class="property-bag-new"><%=kv.GetValueFormatted()%>
                                                    <% } else {%>
                                                    <td><%=kv.GetValueFormatted()%>
                                                    <%}%>
                                                    </td>
                                                </tr>
                                                <%} %>
                                            <%}%>
                                        <%}%>
                                    <%} else {%>
                                        <tr><td colspan="3"><p>There is no information to display.</p></td></tr>
                                    <% } %>
                                </tbody>
                            </table>
                      </div>
                    </div>
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <th>
                    <div class="pager">
                    <%:Html.ActionLink("First", "Details", "MakerChecker", new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = 1 }, new { @class = "pager-number pager-number-first" })%>
                    <%for (int index = 0; index < Model.TotalPages; index++)
                      {%>
                    <% if ((index + 1) == Model.Page)
                       {%>
                    <span class="pager-number pager-number-selected">
                        <%=Model.Page %></span>
                    <% }
                       else
                       {%>
                    <%:Html.ActionLink((index + 1).ToString(), "Details", "MakerChecker", new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = (index + 1) }, new { @class="pager-number" })%>
                    <%}%>
                    <%}%>
                    <%:Html.ActionLink("Last", "Details", "MakerChecker", new { ChangesetId = Model.Changeset.ChangesetId.ToString(), Page = Model.TotalPages }, new { @class = "pager-number pager-number-last" })%>
                    </div>
                </th>
            </tr>
        </tfoot>
    </table>
</div>
<input type="button" value="Regresar" onclick="javascript: document.location = backUrl;" />

<script type="text/javascript">
        $(document).ready(function () {
            
            $("#tabs").tabs();
        });
    </script>
</asp:Content>
