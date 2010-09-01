<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Fussball.ViewModels.StatisticsIndexViewModel>>" %>
<%@ Register Assembly="System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" TagPrefix="Test" Namespace="System.Data.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <ul>
    <%  foreach (var stat in Model) 
        { %>
            <li><%= stat.Desc %> - <%= stat.Player.Name %> <small>(<%= stat.Num %>)</small>
    <%  } %>
    </ul>

    <ul>
    <%  foreach (var player in ViewData["AllPlayers"] as List<Fussball.Models.Player>)
        { %>
            <li><%= player.Name %> - <%= player.Games.Count() %></li>
    <%  } %>
    </ul>

</asp:Content>

