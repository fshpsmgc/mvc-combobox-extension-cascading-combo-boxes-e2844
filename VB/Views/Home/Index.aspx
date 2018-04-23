<%@ Page Language="VB" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.BeginForm()%>
    <%
        Html.DevExpress().TextBox( _
            Sub(settings)
                settings.Name = "ID"
                settings.ReadOnly = True
            End Sub).Bind(Model.ID).Render()
    %>
    <%
        Html.DevExpress().TextBox( _
            Sub(settings)
                settings.Name = "Name"
            End Sub).Bind(Model.Name).Render()
    %>
    <% Html.RenderPartial("CountryPartial", Model)%>
    <% Html.RenderPartial("CityPartial", Model)%>
    <input type="submit" value="Submit" />
    <% Html.EndForm()%>
</asp:Content>