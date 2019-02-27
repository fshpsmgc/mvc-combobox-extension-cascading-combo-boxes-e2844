@ModelType VB.Customer

@Html.DevExpress().ComboBox( _
    Sub(settings)
            settings.Name = "City"
            settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "CityPartial"}
            settings.Properties.ValueType = GetType(System.Int32)
            settings.Properties.TextField = "Name"
            settings.Properties.ValueField = "ID"
            settings.Properties.CallbackPageSize = 20
            settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['Country'] = Country.GetValue(); }"
    End Sub).BindList(VB.City.GetCities(Model.Country)).Bind(Model.City).GetHtml()