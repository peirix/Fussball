<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Fussball.Models.Player>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bennett Fussball
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/index.js") %>"></script>
    
    <h1>Velg spillere</h1>

    <div id="playerPicker">
        <ul>
            <%  foreach (var player in Model) { %>
            <li data-playerid="<%= player.ID %>"><%=player.Name %></li>
            <%  } %>
        </ul>
    </div>
    
    <form action="<%= Url.Content("~/Home/Play/") %>" method="post" name="StartupForm" id="playerForm">
        <ul id="redDef" class="freeSpot"></ul>
        <ul id="redOff" class="freeSpot"></ul>

        <div id="table"></div>

        <ul id="blueOff" class="freeSpot"></ul>
        <ul id="blueDef" class="freeSpot"></ul>

        <input type="hidden" name="BlueDef">
        <input type="hidden" name="BlueOff">
        <input type="hidden" name="RedDef">
        <input type="hidden" name="RedOff">
        <button type="submit" id="playBtn" disabled>Spill fussball</button>
    </form>

    <form method="post" action="" id="NewPlayer">
        <h4>Opprett ny spiller</h4>
        <input type="text" name="name"><input type="submit" value="Opprett">
    </form>
    <a href="<%= Url.Content("~/Statistics/") %>">Statistikk</a> | <a href="<%= Url.Content("~/Player/") %>">Spillere</a>

</asp:Content>
