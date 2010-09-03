<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.ViewModels.PlayGameViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Play
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="<%= Url.Content("~/Scripts/playgame.js") %>"></script>
    <form id="GameOver" action="<%= Url.Content("GameOver/") %>" type="post">
        <input type="hidden" value="<%= Model.Game.ID %>" id="gameID" name="gameID">
        <input type="hidden" name="winningTeam" id="winningTeam">
    </form>
    <div id="message">Switch!</div>
    <div id="undo">ANGRE!</div>
    <div id="BlueSummary"></div>
    <div id="RedSummary"></div>
    <div id="PlayGame">
        <div id="Position">
            <span>Forsvar</span>
            <span>Angrep</span>
        </div>
        <div id="BlueTeam">
            <div class="summary">
                <span class="closeSummary">Lukk</span>
                <h1>Blå mål</h1>
            </div>
            <div class="score"><strong>0</strong><a>Edit</a></div>
            <button id="<%= Model.Blue1.ID %>" class="playerDefense"><%= Model.Blue1.Name %></button>
            <button id="<%= Model.Blue2.ID %>" class="playerOffense"><%= Model.Blue2.Name %></button>
        </div>

        <div id="RedTeam">
            <div class="summary">
                <span class="closeSummary">Lukk</span>
                <h1>Røde mål</h1>
            </div>
            <div class="score"><strong>0</strong><a>Edit</a></div>
            <button id="<%= Model.Red1.ID %>" class="playerDefense"><%= Model.Red1.Name %></button>
            <button id="<%= Model.Red2.ID %>" class="playerOffense"><%= Model.Red2.Name %></button>
        </div>
        <div class="clear"></div>
    </div>

</asp:Content>
