Fussball = {
    blueTeam: 0,
    redTeam: 0,
    blueGoal: function () {
        this.blueTeam++;
        $("#BlueTeam strong").text(Fussball.blueTeam);
        if (this.blueTeam == 5) {
            $("#BlueTeam > button").toggleClass("playerDefense playerOffense");
            showMessage("Bytte!", "blue");
        }
    },
    redGoal: function () {
        this.redTeam++;
        $("#RedTeam strong").text(Fussball.redTeam);
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
    console.log(height*0.8);
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
    $("button").hide();

    $("#BlueTeam, #RedTeam").click(function (e) {
        if (e.target.tagName.toLowerCase() == "div") {
            $(this).attr("id") == "BlueTeam" ? Goal.team = 0 : Goal.team = 1;
            $("button").show();
        }
    });

    $("button").click(function (e) {
        e.preventDefault();

        Goal.scorer = $(this).attr("id");
        Goal.position = $(this).attr("class") == "playerDefense" ? 0 : 1;
        Goal.gameID = $("#gameID").val();
        if (Goal.team == 0)
            Goal.oppDefId = $("#RedTeam .playerDefense").attr("id");
        else
            Goal.oppDefID = $("#BlueTeam .playerDefense").attr("id");

        if ($(this).parent().attr("id") == "BlueTeam" && Goal.team == 1 ||
            $(this).parent().attr("id") == "RedTeam" && Goal.team == 0)
            Goal.selfGoal = 1;

        //Goal.team == 0 ? Fussball.blueGoal() : Fussball.redGoal();
        //$("button").hide();

        $.ajax({
            url: "/Home/ScoreGoal",
            type: "post",
            data: Goal,
            success: function () {
                Goal.team == 0 ? Fussball.blueGoal() : Fussball.redGoal();
                if (Fussball.blueTeam == 10 || Fussball.redTeam == 10) {
                    Goal.team == 0 ? showMessage("Blå vinner!", "blue") : showMessage("Rød vinner!", "red");
                    $("#winningTeam").val(Goal.team);
                    $("#GameOver").submit();
                }
                $("button").hide();
            }
        });


    });
});