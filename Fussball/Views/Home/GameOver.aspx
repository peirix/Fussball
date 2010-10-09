<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.ViewModels.GameOverViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GameOver
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Resultat: <span id="blueGoals"><%= Model.BlueGoals %></span> - <span id="redGoals"><%= Model.RedGoals %></span>
    </h2>

    <ul id="resultList">
        <%  foreach (var score in Model.GameScore)
            {
                var scoreMin = new TimeSpan();
                scoreMin = score.GoalDate - Model.Game.DateStart;
                
                %>
                <li class="<%= score.Team == 0 ? "blue" : "red" %>">
                    <%= score.Player.Name %>
                    <small>(<%= Math.Round(scoreMin.TotalMinutes) %>min.)</small>
                </li>
        <%  } %>
    </ul>

    <a id="done" href="<%= Url.Content("~/Home") %>">Ferdig!</a>

</asp:Content>

