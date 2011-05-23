Fussball = {
    blueTeam: 0,
    redTeam: 0,
    updateBlueScore: function () {
        $("#BlueTeam .score a").text(Fussball.blueTeam);
        if (this.blueTeam == 5 && !this.blueSwitched) {
            if (!this.redSwitched)
                $("#RedSwitch").show();
            $("#BlueSwitch").hide();
            $("#BlueTeam > button").toggleClass("playerDefense playerOffense");
            showMessage("Bytte!", "blue");
            this.blueSwitched = true;
        } else if (this.blueTeam < 5 && this.blueSwitched) {
            if (this.redSwitched) {
                $("#BlueSwitch").show();
            }
            $("#BlueTeam > button").toggleClass("playerDefense playerOffense");
            showMessage("Bytte tilbake!", "blue");
            this.blueSwitched = false;
        }
    },
    updateRedScore: function () {
        $("#RedTeam .score a").text(Fussball.redTeam);
        if (this.redTeam == 5 && !this.redSwitched) {
            if (!this.blueSwitched)
                $("#BlueSwitch").show();
            $("#RedSwitch").hide();
            $("#RedTeam > button").toggleClass("playerDefense playerOffense");
            showMessage("Bytte!", "red");
            this.redSwitched = true;
        } else if (this.redTeam < 5 && this.redSwitched) {
            if (this.blueSwitched) {
                $("#RedSwitch").show();
            }
            $("#RedTeam > button").toggleClass("playerDefense playerOffense");
            showMessage("Bytte tilbake!", "red");
            this.redSwitched = false;
        }
    },
    blueSwitched: false,
    redSwitched: false
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

    $("#MastHead a").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var $this = $(this);
        if (confirm("Hvis du forlater spillet, vil alle mål gå tapt.\nSikker?")) {
            $.ajax({
                url: "DeleteGame",
                data: { gameId: $("#gameID").val() },
                type: "post",
                success: function () {
                    location.href = $this.attr("href");
                }
            });
        }
    });

    var min = 0, sec = 0;

    var st = setInterval(function () {
        sec++;
        if (sec == 60) {
            min++;
            $("#PlayTime :first-child").text(min > 9 ? min : "0" + min);
            $("#PlayTime :last-child").text("00");
            sec = 0;
        } else {
            $("#PlayTime :last-child").text(sec > 9 ? sec : "0" + sec);
        }
    }, 1000);

    $("#undo").click(function () {
        $(this).hide();
        $(".overlay").show();
    });

    $("#SelfGoalOverlay").click(function () {
        Goal.selfGoal = 1;
        $(this).addClass("clicked");
    });

    $("#RedSwitch").click(function () {
        $("#RedTeam > button").toggleClass("playerDefense playerOffense");
        Fussball.redSwitched = true;
        $(this).hide();
    });

    $("#BlueSwitch").click(function () {
        $("#BlueTeam > button").toggleClass("playerDefense playerOffense");
        Fussball.blueSwitched = true;
        $(this).hide();
    });

    $("button").click(function (e) {
        e.preventDefault();
        $("#undo").hide();
        var $this = $(this);
        Goal.selfGoal = 0;

        $("#SelfGoalOverlay").show().animate({ opacity: 1 }, 2000, function () {
            $("#SelfGoalOverlay").hide().removeClass("clicked");

            var scorerName = $this.text();

            //Blue team = 0, Red team = 1
            Goal.team = $this.parent().attr("id") == "BlueTeam" ? 0 : 1;

            if (Goal.selfGoal) {
                Goal.team = Goal.team == 0 ? 1 : 0;
            }

            Goal.scorer = $this.attr("id");
            Goal.position = $this.attr("class") == "playerDefense" ? 0 : 1;
            Goal.gameID = $("#gameID").val();
            if (Goal.team == 0)
                Goal.oppDefId = $("#RedTeam .playerDefense").attr("id");
            else
                Goal.oppDefId = $("#BlueTeam .playerDefense").attr("id");

            if (Goal.team == 0) {
                Fussball.blueTeam++;
                Fussball.updateBlueScore();
            } else {
                Fussball.redTeam++;
                Fussball.updateRedScore();
            }

            $.ajax({
                url: "ScoreGoal",
                type: "post",
                data: Goal,
                success: function (goalId) {


                    if (Fussball.blueTeam == 10 || Fussball.redTeam == 10) {
                        Goal.team == 0 ? showMessage("Blå vinner!", "blue") : showMessage("Rød vinner!", "red");
                        $("#winningTeam").val(Goal.team);
                        $("#GameOver").submit();
                    }

                    var summary;

                    if (Goal.selfGoal)
                        summary = $this.parent().siblings().find(".summary");
                    else
                        summary = $this.siblings(".summary");

                    summary.append("<strong>" + scorerName + "</strong> - <a data-goalid='" + goalId + "'>Slett</a><br>");

                    $(".overlay").show();
                }
            });

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