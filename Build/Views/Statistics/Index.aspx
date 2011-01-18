<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Fussball.Models.Player>>" %>
<%@ Register Assembly="System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" TagPrefix="Test" Namespace="System.Data.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Statistikk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/jquery.ui.js") %>"></script>
    <script>
        $(function () {
            $("#Tabs").tabs();
        });
    </script>
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
        * Generelle
            - Beste par
            - Beste farge
            - Beste posisjon
            - Værste par
    
    --%>
    <div id="Tabs">
        <ul>
            <li><a href="#Last10">Siste 10 kamper</a></li>
            <li><a href="#Overall">Overall</a></li>
            <li><a href="#General">Generelt</a></li>
        </ul>
        <div id="Last10">
            <table id="StatisticList" class="dataList">
                <thead>
                    <tr>
                        <th><span>Rank</span> <span class="sorter"></span></th>
                        <th><span>Spiller</span> <span class="sorter"></span></th>
                        <th><span>Mål</span> <span class="sorter"></span></th>
                        <th><span>Gj.snittsmål</span> <span class="sorter"></span></th>
                        <th><span>Selvmål</span> <span class="sorter"></span></th>
                        <th><span>Sluppet inn</span> <span class="sorter"></span></th>
                    </tr>
                </thead>
                <tbody>
                <%  int rank = 1;
                    foreach (var player in Model.OrderByDescending(p => p.Ranking()))
                    {
                        var stats = player.GetLast10Stats().Split(new string[] { "," }, StringSplitOptions.None); %>
                    <tr>
                        <td><%= rank++ %></td>
                        <td><a href="<%= Url.Content("~/Player/Details/" + player.ID) %>" class="playerName"><%= player.Name %></a></td>
                        <td><%= stats[0] %></td>
                        <td><%= Math.Round(decimal.Parse(stats[0]) / int.Parse(stats[3]), 2) %></td>
                        <td><%= stats[2] %></td>
                        <td><%= stats[1] %></td>
                    </tr>
                <%  } %>
                </tbody>
            </table>
        </div>
        <div id="Overall">
            <table id="OverallList" class="dataList">
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
                        <td><a href="<%= Url.Content("~/Player/Details/" + player.ID) %>" class="playerName"><%= player.Name %></a></td>
                        <td class="numberOfGames"><%= player.TotalGames().ToString() %></td>
                        <td><%= player.TotalGoals() %></td>
                        <td><%= Math.Round((decimal)player.AverageGoals(), 2) %></td>
                        <td><%= player.TotalSelfGoals() %></td>
                        <td><%= player.LetInGoals() %></td>
                    </tr>
                <%  } %>
                </tbody>
            </table>
        </div>
        <div id="General">
            <span>Blå seiere: <%= ViewData["BlueWins"] %></span><br>
            <span>Rød seiere: <%= ViewData["RedWins"] %></span>
        </div>
    </div>


</asp:Content>