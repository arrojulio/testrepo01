<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<% Html.Telerik().AutoComplete()
          .Value(Model)
          .DataBinding(c => c.Ajax().Enabled(true).Select("GetAutocomplete", "GarantiaBase", new { htmlControlId = ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty) }).Delay(500).Cache(false))
          .Encode(false)
          .ClientEvents(c => c.OnChange("onAutocompleteChange"))
          .Filterable(f => f.MinimumChars(2).FilterMode(AutoCompleteFilterMode.Contains))
          .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
          .HighlightFirstMatch(true)
        .Render(); %>
