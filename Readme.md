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
- Perform a callback for the second ComboBox via the client-side [PerformCallback](https://docs.devexpress.com/AspNetMvc/js-MVCxClientComboBox.PerformCallback(data)) method.
- Pass the first combo box's value in PerformCallback's parameter to use as a filter on the server. See the [Passing Values to a Controller Action through Callbacks](https://docs.devexpress.com/AspNetMvc/9941/common-features/callback-based-functionality/passing-values-to-a-controller-action-through-callbacks) topic for more details.
- Handle the Action method of the second ComboBox (specified in the ComboBoxSettings.CallbackRouteValues.Action property), get the data based on the passed filters, and pass this Model to the View.

## Setup Combo Boxes and Their Data Sources
Setup the Country combo box ([CountryComboView.cshtml](/CS/MvcComboBoxes/Views/Home/CountryComboView.cshtml)) and the City combo box ([CityComboView.cshtml](CS/MvcComboBoxes/Views/Home/CityComboView.cshtml)) as partial views. Use the [BindList](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.ComboBoxExtension.BindList(System.Object)) method to bind the data, obtained from the controller, to the combo boxes. Note, that you should get the data for the first combo box in the Index action and pass it to the partial view. Items for the second one are obtained during a callback.

```c#
public ActionResult Index() {
    var countries = db.Orders.GroupBy(p => p.ShipCountry).Select(g => g.FirstOrDefault()).ToList();
    return View(countries);
}
```

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

## Respond to a Selection Change and Initiate a Callback
Handle the first combo box's client-side [SelectedIndexChanged](https://docs.devexpress.com/AspNet/js-ASPxClientComboBox.SelectedIndexChanged) event. In the event handler, call the client-side [PerformCallback](https://docs.devexpress.com/AspNetMvc/js-MVCxClientComboBox.PerformCallback(data)) method of the second combo box. This sends a callback to the server for the second editor and calls the handler specified in the [CallbackRouteValues](https://docs.devexpress.com/AspNetMvc/DevExpress.Web.Mvc.AutoCompleteBoxBaseSettings.CallbackRouteValues). Pass the filter criterion to the server (the first combo box's selected value) in the PerformCallback method's parameter.

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