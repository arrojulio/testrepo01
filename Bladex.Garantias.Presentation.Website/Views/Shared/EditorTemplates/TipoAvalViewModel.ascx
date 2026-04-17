<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.TipoAvalViewModel>" %>
<%= Html.HiddenFor(m=>m.Nombre) %>
<%= Html.Telerik().DropDownListFor(m=>m.Key).Encode(false)
                            .ClientEvents(c => c.OnChange("onDropdownChange").OnLoad("onDropdownChange"))
                        //.BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
                        .BindTo(Model.List)
%>

