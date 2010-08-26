<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.ViewModels.PlayGameViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Play
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script>
        Fussball = {
            blueTeam: 0,
            redTeam: 0,
            blueGoal: function () {
                this.blueTeam++;
                $("#BlueTeam strong").text(Fussball.blueTeam);
                if (this.blueTeam == 5) {
                    $("#BlueTeam > button").toggleClass("playerDefense playerOffense");
                }
            },
            redGoal: function () {
                this.redTeam++;
                $("#RedTeam strong").text(Fussball.redTeam);
                if (this.redTeam == 5)
                    $("#RedTeam > button").toggleClass("playerDefense playerOffense");
            }
        };

        $(document).ready(function () {
            $("button").click(function () {
                var scorer = $(this).attr("id"); ;
                var position = $(this).attr("class") == "playerDefense" ? 0 : 1;
                var team = $(this).parent().attr("id") == "BlueTeam" ? 0 : 1;
                var gameID = $("#gameID").val();
                var oppDefID = $(this).parent().siblings().find(".playerDefense").attr("id");
                //team == 0 ? Fussball.blueGoal() : Fussball.redGoal();
                
                $.ajax({
                    url: "/Home/ScoreGoal",
                    type: "post",
                    data: { scorer: scorer, position: position, team: team, gameID: gameID, oppDefID: oppDefID },
                    success: function () {
                        team == 0 ? Fussball.blueGoal() : Fussball.redGoal();
                        if (Fussball.blueTeam == 10 || Fussball.redTeam == 10) {
                            $("#winningTeam").val(team);
                            $("#GameOver").submit();
                        }
                    }
                });
            });
        });
    </script>
    <form id="GameOver" action="/Home/GameOver/" type="post">
        <input type="hidden" value="<%= Model.Game.ID %>" id="gameID" name="gameID">
        <input type="hidden" name="winningTeam" id="winningTeam">
    </form>

    <div id="PlayGame">
        <h2>Play Game!</h2>
        <div id="Position">
            <span>Forsvar</span>
            <span>Angrep</span>
        </div>
        <div id="BlueTeam">
            <strong>0</strong>
            <button id="<%= Model.Blue1.ID %>" class="playerDefense"><%= Model.Blue1.Name %></button>
            <button id="<%= Model.Blue2.ID %>" class="playerOffense"><%= Model.Blue2.Name %></button>
        </div>

        <div id="RedTeam">
            <strong>0</strong>
            <button id="<%= Model.Red1.ID %>" class="playerDefense"><%= Model.Red1.Name %></button>
            <button id="<%= Model.Red2.ID %>" class="playerOffense"><%= Model.Red2.Name %></button>
        </div>
    </div>

</asp:Content>
