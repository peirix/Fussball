Fussball = {
    blueTeam: 0,
    redTeam: 0,
    updateBlueScore: function () {
        $("#BlueTeam .score strong").text(Fussball.blueTeam);
        if (this.blueTeam == 5) {
            $("#BlueTeam > button").toggleClass("playerDefense playerOffense");
            showMessage("Bytte!", "blue");
        }
    },
    updateRedScore: function () {
        $("#RedTeam .score strong").text(Fussball.redTeam);
        if (this.redTeam == 5) {
            $("#RedTeam > button").toggleClass("playerDefense playerOffense");
            showMessage("Bytte!", "red");
        }
    }
};

Goal = {
    scorer: 0,
    position: 0,
    team: 0,
    gameID: 0,
    oppDefId: 0,
    selfGoal: 0
};

function showMessage(msg, color) {
    var height = $("#message").height();
    $("#message")
        .text(msg)
        .css({
            "background-color": color,
            "font-size" : height*0.8 + "px"
        })
        .show()
        .delay(2000)
        .fadeOut(1000);
}

$(document).ready(function () {

    $("#undo").click(function () {
        $(this).hide();
        $(".overlay").show();
    });

    $("#BlueTeam .overlay, #RedTeam .overlay").click(function (e) {
        e.preventDefault();
        $(this).parent().attr("id") == "BlueTeam" ? Goal.team = 0 : Goal.team = 1;
        $("#undo").show();
        $(".overlay").hide();
    });

    $("button").click(function (e) {
        e.preventDefault();
        $("#undo").hide();
        var $this = $(this);

        var scorerName = $(this).text();

        Goal.scorer = $(this).attr("id");
        Goal.position = $(this).attr("class") == "playerDefense" ? 0 : 1;
        Goal.gameID = $("#gameID").val();
        if (Goal.team == 0)
            Goal.oppDefId = $("#RedTeam .playerDefense").attr("id");
        else
            Goal.oppDefId = $("#BlueTeam .playerDefense").attr("id");

        if ($(this).parent().attr("id") == "BlueTeam" && Goal.team == 1 ||
            $(this).parent().attr("id") == "RedTeam" && Goal.team == 0)
            Goal.selfGoal = 1;
        else
            Goal.selfGoal = 0;

        //Goal.team == 0 ? Fussball.blueGoal() : Fussball.redGoal();
        //$("button").hide();

        $.ajax({
            url: "ScoreGoal",
            type: "post",
            data: Goal,
            success: function (goalId) {
                if (Goal.team == 0) {
                    Fussball.blueTeam++;
                    Fussball.updateBlueScore();
                } else {
                    Fussball.redTeam++;
                    Fussball.updateRedScore();
                }

                if (Fussball.blueTeam == 10 || Fussball.redTeam == 10) {
                    Goal.team == 0 ? showMessage("Blå vinner!", "blue") : showMessage("Rød vinner!", "red");
                    $("#winningTeam").val(Goal.team);
                    $("#GameOver").submit();
                }

                $this.siblings(".summary").append("<strong>" + scorerName + "</strong> - <a data-goalid='" + goalId + "'>Delete</a><br>");

                $(".overlay").show();
            }
        });
    });

    $(".score a").click(function () {
        $(this).parent().prev().show();
    });

    $(".summary .closeSummary").click(function () {
        $(this).parent().hide();
    });

    $(".summary a").live("click", function () {
        var $this = $(this);
        var goalId = $this.attr("data-goalid");
        var gameId = $("#gameID").val();
        var team = $this.parent().parent().attr("id");

        $.ajax({
            url: "DeleteGoal",
            type: "post",
            data: { gameID: gameId, goalID: goalId },
            success: function () {
                $this.prev().remove();
                $this.remove();
                if (team == "BlueTeam") {
                    Fussball.blueTeam--;
                    Fussball.updateBlueScore();
                } else {
                    Fussball.redTeam--;
                    Fussball.updateRedScore();
                }
            }
        });
    });
});