<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Fussball.ViewModels.StatisticsIndexViewModel>>" %>
<%@ Register Assembly="System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" TagPrefix="Test" Namespace="System.Data.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Statistikk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a class="backBtn"></a>
    <h1>Statistikk</h1>
    <ul id="HallOfFameAndShame">
    <%  foreach (var stat in Model) 
        { %>
            <li>
                <span class="awardTitle"><%= stat.Desc %></span> 
                <img src="<%= stat.Player.Image_Small %>" alt="bilde av <%= stat.Player.Name %>">
                <span class="rewardedPlayer"><%= stat.Player.Name %></span> 
                <small>(<%= stat.Num %>)</small>
            </li>
    <%  } %>
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
            </tr>
        </thead>
        <tbody>
        <%  foreach (var player in ViewData["AllPlayers"] as Dictionary<Fussball.Models.Player, int>)
            { %>
            <tr>
                <td><a href="<%= Url.Content("~/Player/Details/") %>" class="playerName"><%= player.Key.Name %></a></td>
                <td class="numberOfGames"><%= player.Value %></td>
            </tr>
        <%  } %>
        </tbody>
    </table>

</asp:Content>