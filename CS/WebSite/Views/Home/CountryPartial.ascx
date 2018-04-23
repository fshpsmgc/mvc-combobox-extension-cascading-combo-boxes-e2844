<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<E2844.Models.Customer>" %>
<%
    Html.DevExpress().ComboBox(settings => {
        settings.Name = "Country";
        settings.CallbackRouteValues = new { Controller = "Home", Action = "CountryPartial" };
        settings.Properties.ValueType = typeof(int);
        settings.Properties.TextField = "Name";
        settings.Properties.ValueField = "ID";
        settings.Properties.EnableCallbackMode = true;
        settings.Properties.CallbackPageSize = 20;
        settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { City.PerformCallback(); }";
    })
    .BindList(E2844.Models.Country.GetCountries())
    .Bind(Model.Country)
    .Render();    
%>