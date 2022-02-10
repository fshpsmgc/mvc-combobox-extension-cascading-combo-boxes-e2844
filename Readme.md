<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128549360/20.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2844)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# MVC ComboBox Extension - Cascading Combo Boxes

This demo illustrates how to use two [ComboBox](https://docs.devexpress.com/AspNetMvc/8984/components/data-editors-extensions/combobox) editors to implement cascading combo boxes within the MVC ComboBox Extension. In the demo, the selection in the first combo box (Country) filters the item list of the second combo box (City).

![example demo](demo.gif)

Use the following technique to setup a cascade of ComboBox editors:
Respond to a selection change of the first combo box (in its client SelectedIndexChanged event) and initiate a callback request for the second combo box to filter data source items on the server (use a combination of the client PerformCallback method and server Callback event).

## Setup Combo Boxes and Their Data Sources
Use partial views to setup Country combo box (CountryPartial.cshtml) and City combo box (CityPartial.cshtml). Use BindList method to bind combo box to the Country model. Set the Action property of the CallbackRouteValues property to tie combo boxes to the server-side method with the specified name. Use BindList method to bind the data, obtained from the controller, to the combo boxes. Note, that you should get the data for the first combo box in the Index action and pass it to the partial view.

```c#
public ActionResult Index() {
    var countries = db.Orders.GroupBy(p => p.ShipCountry).Select(g => g.FirstOrDefault()).ToList();
    return View(countries);
}
```

```c#
@Html.DevExpress().ComboBox(settings => {
    ...
}).BindList(Model).GetHtml()
```

## Respond to a Selection Change and Initiate a Callback
Initiate a callback to the second combo box in the first combo box's client side SelectedIndexChanged event. 

```c#
@Html.DevExpress().ComboBox(settings => {
    settings.Name = "Country";
    ...
    settings.Properties.ClientSideEvents.SelectedIndexChanged = "function(s, e) { City.PerformCallback({countryName: Country.GetValue()}); }";
    ...

```

## Filter Data Source Items
In the page's controller handle the second combo box's event specified in the CallbackRouteValues

Define Action in the CallbackRoutValues settings. In the 

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