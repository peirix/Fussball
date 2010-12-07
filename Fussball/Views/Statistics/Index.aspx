<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Fussball.ViewModels.StatisticsIndexViewModel>>" %>
<%@ Register Assembly="System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" TagPrefix="Test" Namespace="System.Data.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Statistikk
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Statistikk</h1>
    <ul id="HallOfFameAndShame">
    <%  foreach (var stat in Model) 
        { %>
            <li>
                <span class="awardTitle"><%= stat.Desc %></span> 
                <span class="rewardedPlayer"><%= stat.Player.Name %></span> 
                <small>(<%= stat.Num %>)</small>
            </li>
    <%  } %>
    </ul>

    <h2>Antall kamper</h2>
    <ul id="NumberOfGamesList" class="dataList">
    <%  foreach (var player in ViewData["AllPlayers"] as Dictionary<Fussball.Models.Player, int>)
        { %>
            <li><span class="playerName"><%= player.Key.Name %></span> <span class="numberOfGames"><%= player.Value %></span></li>
    <%  } %>
    </ul>

</asp:Content>