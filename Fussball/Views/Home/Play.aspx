<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.ViewModels.PlayGameViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Play
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="<%= Url.Content("~/Scripts/playgame.js") %>"></script>
    <form id="GameOver" action="<%= Url.Content("GameOver/") %>" method="post">
        <input type="hidden" value="<%= Model.Game.ID %>" id="gameID" name="gameID">
        <input type="hidden" name="winningTeam" id="winningTeam" value="0">
    </form>
    <div id="message">Bytte!</div>
    <div id="undo">ANGRE!</div>
    <div id="BlueSummary"></div>
    <div id="RedSummary"></div>
    <div id="PlayGame">
        <div id="RedTeam">
            <div class="overlay"></div>
            <div class="summary">
                <span class="closeSummary">Lukk</span>
                <h1>Røde mål</h1>
            </div>
            <div class="score"><strong>0</strong><a>Endre</a></div>
            <button id="<%= Model.Red1.ID %>" class="playerDefense">
                <img src="<%= Model.Red1.GetSquareImage() %>" alt="bilde av <%= Model.Red1.Name %>">
                <span><%= Model.Red1.Name %></span>
            </button>
            <button id="<%= Model.Red2.ID %>" class="playerOffense">
                <img src="<%= Model.Red2.GetSquareImage() %>" alt="bilde av <%= Model.Red2.Name %>">
                <span><%= Model.Red2.Name %></span>
            </button>
        </div>
        <div id="BlueTeam">
            <div class="overlay"></div>
            <div class="summary">
                <span class="closeSummary">Lukk</span>
                <h1>Blå mål</h1>
            </div>
            <div class="score"><strong>0</strong><a>Endre</a></div>
            <button id="<%= Model.Blue1.ID %>" class="playerDefense">
                <img src="<%= Model.Blue1.GetSquareImage() %>" alt="bilde av <%= Model.Blue1.Name %>">
                <span><%= Model.Blue1.Name %></span>
            </button>
            <button id="<%= Model.Blue2.ID %>" class="playerOffense">
                <img src="<%= Model.Blue2.GetSquareImage() %>" alt="bilde av <%= Model.Blue2.Name %>">
                <span><%= Model.Blue2.Name %></span>
            </button>
        </div>
        <div class="clear"></div>
    </div>

</asp:Content>
