<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.InternalStatusViewModel>" %>
<%= Html.HiddenFor(m=>m.Key) %>
<%= Html.HiddenFor(m=>m.Nombre) %>
<%: string.Format("{0}", string.IsNullOrEmpty(Model.Nombre) ? "(empty)" : Model.Nombre) %>
        