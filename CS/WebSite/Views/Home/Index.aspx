<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<E2844.Models.Customer>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.BeginForm(); %>
    <%
        Html.DevExpress().TextBox(settings => {
            settings.Name = "ID";
            settings.ReadOnly = true;
        })
        .Bind(Model.ID)
        .Render();    
    %>
    <%
        Html.DevExpress().TextBox(settings => {
            settings.Name = "Name";
        })
        .Bind(Model.Name)
        .Render();    
    %>
    <% Html.RenderPartial("CountryPartial", Model); %>
    <% Html.RenderPartial("CityPartial", Model); %>
    <input type="submit" value="Submit" />
    <% Html.EndForm(); %>
</asp:Content>