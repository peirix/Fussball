﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.ViewModels.GameOverViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GameOver
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        Resultat: <span id="blueGoals"><%= Model.BlueGoals %></span> - <span id="redGoals"><%= Model.RedGoals %></span>
    </h1>
    <div id="GoalLists">
        <ul id="blueGoalsList" class="dataList">
            <%  foreach (var score in Model.GameScore.Where(g => g.Team == 0)) {
                    var scoreMin = new TimeSpan();
                    scoreMin = score.GoalDate - Model.Game.DateStart; 
            %>
                    <li <%= score.SelfGoal == 1 ? "class='selfgoal'" : "" %>>
                        <%= score.Player.Name %>
                        <small>(<%= Math.Round(scoreMin.TotalMinutes) %>min.)</small>
                    </li>
            <%  } %>
        </ul>

        <ul id="redGoalsList" class="dataList">
            <% foreach (var score in Model.GameScore.Where(g => g.Team == 1)) {
                var scoreMin = new TimeSpan();
                scoreMin = score.GoalDate - Model.Game.DateStart;
            %>
                <li <%= score.SelfGoal == 1 ? "class='selfgoal'" : "" %>>
                    <%= score.Player.Name %>
                    <small>(<%= Math.Round(scoreMin.TotalMinutes) %>min.)</small>
                </li>
            <% } %>
        </ul>
    </div>
    <a id="done" class="largeBtn" href="<%= Url.Content("~/Home") %>">Ferdig!</a>

</asp:Content>

