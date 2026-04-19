<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Bladex.Garantias.Presentation.Website.ViewModels.FrecuenciasViewModel>" %>
<%= Html.Telerik().ComboBoxFor(m=>m.Key)
                    .AutoFill(true)
                    .ClientEvents(c => c.OnChange("calcProximaFechaRevisionEvaluacion").OnLoad("calcProximaFechaRevisionEvaluacion"))
                    .BindTo(new SelectList(Model.List, "Value", "Text", Model.Key))
                    .Encode(false)
                    .Filterable(filtering =>
                    {
                        if (true) 
                        {
                            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                        }
                    })
                    .HighlightFirstMatch(true)
    %>

