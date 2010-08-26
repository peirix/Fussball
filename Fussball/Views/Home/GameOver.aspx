<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.ViewModels.GameOverViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GameOver
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        <% if (Model.Game.WinningTeam == 0) { %> Blå Seier!
        <% } else { %>Rød Seier!<% } %>
    </h2>

    <ul>
        <%  foreach (var score in Model.GameScore)
            { %>
                <li><%= score.Player.Name %></li>
        <%  } %>
    </ul>

</asp:Content>

