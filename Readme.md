<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128549360/20.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2844)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# MVC ComboBox Extension - Cascading Combo Boxes

This example illustrates how to use the MVC ComboBox extension to implement cascading combo boxes
. In this demo, the selection in the first combo box (Country) filters the item list of the second combo box (City).

![example demo](demo.gif)

Use the following technique to setup a cascade of ComboBox editors:

Handle a selection change of the first combo box (the client-side [SelectedIndexChanged](https://docs.devexpress.com/AspNet/js-ASPxClientComboBox.SelectedIndexChanged) event). In the handler, send a callback for the second ComboBox (with the client-side [PerformCallback](https://docs.devexpress.com/AspNetMvc/js-MVCxClientComboBox.PerformCallback(data)) method) and pass the first combo box's value to the server. On the server, handle the `Action` method of the second ComboBox (specified in the [CallbackRouteValues](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.AutoCompleteBoxBaseSettings.CallbackRouteValues)) and use the passed value to get the items for the second combo box. Pass the items for the second combo box to the View.

## Setup Combo Boxes and Their Items
Setup the Country combo box ([CountryComboView.cshtml](/CS/MvcComboBoxes/Views/Home/CountryComboView.cshtml)) and the City combo box ([CityComboView.cshtml](CS/MvcComboBoxes/Views/Home/CityComboView.cshtml)) as partial views. Use the [BindList](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.ComboBoxExtension.BindList(System.Object)) method to bind the data, obtained from the controller, to the combo boxes. Note, that you should get the data for the first combo box in the Index action and pass it to the view. The second combo box obtains the data during the callback. 

```c#
// Index.cshtml
@Html.Partial("CountryComboView")
@Html.Partial("CityComboView")

// CountryComboView.cshtml
@Html.DevExpress().ComboBox(settings => {
    settings.Properties.ValueType = typeof(string);
    settings.Properties.TextField = "ShipCountry";
    settings.Properties.ValueField = "ShipCountry";
    ...
}).BindList(Model).GetHtml()

// CityComboView.cshtml
@Html.DevExpress().ComboBox(settings => {
    settings.Properties.ValueType = typeof(string);
    settings.Properties.TextField = "ShipCity";
    settings.Properties.ValueField = "ShipCity";
    ...
}).BindList(Model).GetHtml()
```

```c#
public ActionResult Index() {
    var countries = db.Orders.GroupBy(p => p.ShipCountry).Select(g => g.FirstOrDefault()).ToList();
    return View(countries);
}
```

## Respond to a Selection Change and Initiate a Callback
Handle the first combo box's client-side [SelectedIndexChanged](https://docs.devexpress.com/AspNet/js-ASPxClientComboBox.SelectedIndexChanged) event. In the event handler, call the client-side [PerformCallback](https://docs.devexpress.com/AspNetMvc/js-MVCxClientComboBox.PerformCallback(data)) method of the second combo box. This sends a callback to the server for the second editor and calls the handler specified in the [CallbackRouteValues](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.AutoCompleteBoxBaseSettings.CallbackRouteValues). Pass the filter criterion (the first combo box's selected value) to the server in the PerformCallback method's `data` parameter.

```c#
@Html.DevExpress().ComboBox(settings => {
    settings.Name = "Country";
    ...
    settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { City.PerformCallback({countryName: Country.GetValue()}); }";
    ...

```

## Filter Data Source Items

Handle the method specified in the second combo box's [CallbackRouteValues](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.AutoCompleteBoxBaseSettings.CallbackRouteValues) that fires in response to a call to the client-side [PerformCallback](https://docs.devexpress.com/AspNetMvc/js-MVCxClientComboBox.PerformCallback(data)) method. In the handler, use the event argument's `data` property to obtain the first combo box's selected value passed from the client side. Use this value to filter the second combo box's data source.

```c#
@Html.DevExpress().ComboBox(settings => {
    settings.Name = "City";
    ...
    settings.CallbackRouteValues = new { Controller = "Home", Action = "CityComboView" };
    ...
```


```c#
public ActionResult CityComboView(string countryName) {
    var cities = db.Orders.Where(c => c.ShipCountry == countryName).ToList();
    var citiesDistinct = cities.GroupBy(p => p.ShipCity).Select(g => g.First()).ToList();
    return PartialView(citiesDistinct);
}
```

## Files to Look At
- [CountryComboView.cshtml](/CS/MvcComboBoxes/Views/Home/CountryComboView.cshtml)
- [CityComboView.cshtml](/CS/MvcComboBoxes/Views/Home/CityComboView.cshtml)
- [HomeController.cs](CS/MvcComboBoxes/Controllers/HomeController.cs)

## More Examples
- [Combo Box for ASP.NET Web Forms - How to Implement Cascading Combo Boxes](https://github.com/DevExpress-Examples/asp-net-web-forms-cascading-comboboxes)