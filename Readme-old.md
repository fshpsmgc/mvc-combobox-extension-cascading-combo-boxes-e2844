<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128549360/20.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2844)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# MVC ComboBox Extension - Cascading Combo Boxes
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e2844/)**
<!-- run online end -->

This demo illustrates how to use two [ComboBox](https://docs.devexpress.com/AspNetMvc/8984/components/data-editors-extensions/combobox) editors to implement cascading combo boxes within the MVC ComboBox Extension. In the demo, the selection in the first combo box (Country) filters the item list of the second combo box (City).

![example demo](demo.gif)

## Setup Combo Boxes, Tie Them to Models and the Server

Use partial views to setup Country combo box ([CountryPartial.cshtml](./CS/DevExpressMvc3CascadingCombo/Views/Home/CountryPartial.cshtml)) and City combo box ([CityPartial.cshtml](./CS/DevExpressMvc3CascadingCombo/Views/Home/CityPartial.cshtml)). Use [BindList](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.ComboBoxExtension.BindList(System.Object)) method to bind combo box to the Country model. Set the Action property of the [CallbackRouteValues](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.AutoCompleteBoxBaseSettings.CallbackRouteValues) property to tie combo boxes to the server-side method with the specified name.

```c#
// CountryPartial.cshtml
@Html.DevExpress().ComboBox(settings => {
    settings.Name = "Country";
    ...
    settings.CallbackRouteValues = new { Controller = "Home", Action = "CountryPartial" };
    ...
}).BindList(CS.Models.Country.GetCountries()).Bind(Model.Country).GetHtml()  

// CityPartial.cshtml
@Html.DevExpress().ComboBox(settings => {
    settings.Name = "City";
    ...
    settings.CallbackRouteValues = new { Controller = "Home", Action = "CityPartial" };
    ...
}).BindList(CS.Models.City.GetCities(Model.Country)).Bind(Model.City).GetHtml()
```

 Add them to the Index page:

```c# 
@using(Html.BeginForm()) {  
    @Html.Partial("CountryPartial", Model)
    @Html.Partial("CityPartial", Model)
    ...
```

## On Client: Respond to a Selection Change and Initiate a Callback 
Handle the first combo box's client-side [SelectedIndexChanged](https://docs.devexpress.com/AspNet/DevExpress.Web.ComboBoxClientSideEvents.SelectedIndexChanged) event. In the event handler, call the client-side [PerformCallback](https://docs.devexpress.com/AspNetMvc/js-MVCxClientComboBox.BeginCallback) method of the second combo box. This sends a callback to the server for the second editor to filter its item list.

```c# 
// Country combo box.
    ...
    settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { City.PerformCallback(); }";
    ... 
```

Handle the second combo box's client-side [BeginCallback](https://docs.devexpress.com/AspNet/DevExpress.Web.AutoCompleteBoxClientSideEvents.BeginCallback) event. In the event handler, pass the first combo box's selected value to the custom callback parameter to use it as a filter criterion on the server. 

```c#
// City combo box.
    ...
    settings.Properties.ClientSideEvents.BeginCallback = "function(s, e) { e.customArgs['Country'] = Country.GetValue(); }";
    ...
```

## On Server: Filter Data Source Items
In the controller, filter the items and return the updated view. Return a new City combo box partial view and initialize the Country parameter with the country index passed to the server in the custom callback parameter.

```c#
public ActionResult CityPartial() {
    int country = (Request.Params["Country"] != null) ? int.Parse(Request.Params["Country"]) : -1;
    return PartialView(new Customer { Country = country });
}
```
## Files to Look At
* [HomeController.cs](./CS/DevExpressMvc3CascadingCombo/Controllers/HomeController.cs)
* [Customer.cs](./CS/DevExpressMvc3CascadingCombo/Models/Customer.cs)
* [CityPartial.cshtml](./CS/DevExpressMvc3CascadingCombo/Views/Home/CityPartial.cshtml)
* [CountryPartial.cshtml](./CS/DevExpressMvc3CascadingCombo/Views/Home/CountryPartial.cshtml)
* [Index.cshtml](./CS/DevExpressMvc3CascadingCombo/Views/Home/Index.cshtml)
* [Success.cshtml](./CS/DevExpressMvc3CascadingCombo/Views/Home/Success.cshtml)
## Documentation

## More Examples
[Cascading Combo Boxes in ASP.NET Web Forms](https://github.com/DevExpress-Examples/asp-net-web-forms-cascading-comboboxes)