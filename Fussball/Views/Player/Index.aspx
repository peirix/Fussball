<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Fussball.Models.Player>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Alle spillere
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Alle spillere</h1>

    <ul id="PlayerList" class="dataList">
    <% foreach (var player in Model) { %>
        <li><a href="<%= Url.Content("~/Player/Details/" + player.ID) %>"><%= player.Name %><span>></span></a></li>
    <% } %>
    </ul>

    

</asp:Content>