<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Fussball.Models.Player>>" %>
<%@ Register Assembly="System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" TagPrefix="Test" Namespace="System.Data.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Statistikk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a class="backBtn"></a>
    <h1>Statistikk</h1>
    <ul id="HallOfFameAndShame">
        <li><% var topScorer = ViewData["TopScorer"] as Fussball.Models.Player; %>
            <span class="awardTitle">Flest mål</span> 
            <img src="<%= topScorer.GetSmallImage() %>" alt="bilde av <%= topScorer.Name %>">
            <span class="rewardedPlayer"><%= topScorer.Name %></span> 
            <small>(<%= topScorer.TotalGoals() %>)</small>
        </li>
        <li><% var worstScorer = ViewData["WorstScorer"] as Fussball.Models.Player; %>
            <span class="awardTitle">Færrest mål</span> 
            <img src="<%= worstScorer.GetSmallImage() %>" alt="bilde av <%= worstScorer.Name %>">
            <span class="rewardedPlayer"><%= worstScorer.Name %></span> 
            <small>(<%= worstScorer.TotalGoals() %>)</small>
        </li>
        <li><% var mostSelfScore = ViewData["MostSelfScore"] as Fussball.Models.Player; %>
            <span class="awardTitle">Flest selvmål</span> 
            <img src="<%= mostSelfScore.GetSmallImage() %>" alt="bilde av <%= mostSelfScore.Name %>">
            <span class="rewardedPlayer"><%= mostSelfScore.Name%></span> 
            <small>(<%= mostSelfScore.TotalSelfGoals()%>)</small>
        </li>
        <li><% var leastSelfScore = ViewData["LeastSelfScore"] as Fussball.Models.Player; %>
            <span class="awardTitle">Færrest selvmål</span> 
            <img src="<%= leastSelfScore.GetSmallImage() %>" alt="bilde av <%= leastSelfScore.Name %>">
            <span class="rewardedPlayer"><%= leastSelfScore.Name%></span> 
            <small>(<%= leastSelfScore.TotalSelfGoals()%>)</small>
        </li>
    </ul>
    <%--
        * Spillerstats:
            - kamper
            - gjennomsnittsmål
            - selvmål
            - sluppet inn
            - mål
        
        * Generelle
            - Beste par
            - Beste farge
            - Beste posisjon
            - Værste par
    
     --%>
    <table id="StatisticList" class="dataList">
        <thead>
            <tr>
                <th><span>Spiller</span> <span class="sorter"></span></th>
                <th><span>Kamper</span> <span class="sorter"></span></th>
                <th><span>Mål</span> <span class="sorter"></span></th>
                <th><span>Gj.snittsmål</span> <span class="sorter"></span></th>
                <th><span>Selvmål</span> <span class="sorter"></span></th>
                <th><span>Sluppet inn</span> <span class="sorter"></span></th>
            </tr>
        </thead>
        <tbody>
        <%  foreach (var player in Model)
            { %>
            <tr>
                <td><a href="<%= Url.Content("~/Player/Details/") %>" class="playerName"><%= player.Name %></a></td>
                <td class="numberOfGames"><%= player.TotalGames().ToString() %></td>
                <td><%= player.TotalGoals() %></td>
                <td><%= player.AverageGoals() %></td>
                <td><%= player.TotalSelfGoals() %></td>
                <td><%= player.LetInGoals() %></td>
            </tr>
        <%  } %>
        </tbody>
    </table>

</asp:Content>