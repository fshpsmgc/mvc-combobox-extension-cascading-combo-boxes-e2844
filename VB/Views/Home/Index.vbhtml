@ModelType VB.Customer
@Code
    ViewBag.Title = "Home Page"
End Code
<h2>@ViewBag.Message</h2>
@Using (Html.BeginForm())
    @<p>
        To learn more about DevExpress Extensions for ASP.NET MVC visit <a href="http://devexpress.com/Products/NET/Controls/ASP-NET-MVC/"
            title="ASP.NET MVC Website">http://devexpress.com/Products/NET/Controls/ASP-NET-MVC/</a>.</p>

    @Html.DevExpress().TextBox( _
    Sub(settings)
        settings.Name = "ID"
        settings.ReadOnly = True
    End Sub).Bind(Model.ID).GetHtml()
   
    @Html.DevExpress().TextBox( _
    Sub(settings)
        settings.Name = "Name"
    End Sub).Bind(Model.Name).GetHtml()

    @Html.Partial("CountryPartial", Model)
    @Html.Partial("CityPartial", Model)
    @<input type="submit" value="Submit" />
End Using