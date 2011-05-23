$(document).ready(function () {

    picker.find("ul").width(width - 250 );

    var $picker = $("#playerPicker");
    var $list = $picker.find("ul");
    var playerCount = $list.find("li").length;
    var width = playerCount * ($list.find("li:first").outerWidth() + 13); //13px margin
    $list.width(width);

    /* Playerlist navigation */
    $picker.find(".right").click(function () {
        console.log($list.css("margin-left"));
        if ($list.css("margin-left").replace("px", "") > -($list.width() - $("#PlayerListContainer").width())) {
            $list.animate({ "margin-left": "-=" + 550 });
        }
    });


    $picker.find(".left").click(function () {
        if ($list.css("margin-left").replace("px", "") < 0) {
            $("#playerPicker ul").animate({ "margin-left": "+=" + 550 });
        }
    });

    $elm = null;

    $("#playerPicker ul li").click(function (e) {
        $elm = $(this);
        $("#playerPicker ul li").removeClass("selected");
        $elm.addClass("selected");
        $("#playerForm .freeSpot").addClass("highlight");
    });

    $("#playerPicker ul li").live("dblclick", function () {
        $elm = $(this);
        var $spot = $(".freeSpot:first");
        movePlayerToSpot($spot);
    });

    $(".freeSpot").live("click", function () {
        var $selected = $("ul .selected");
        if ($selected.length > 0) {
            $elm = $selected;
            //TODO: behold i liste, men gjør halvt gjennomsiktig
            movePlayerToSpot($(this));
        }
        $("#playerForm ul:empty").removeClass("filledSpot").addClass("freeSpot");
    });

    $(".filledSpot").live("click", function () {
        $(".selected").removeClass("selected");
        $("li", this).addClass("selected");
        $("#playerForm .freeSpot").addClass("highlight");
    }).live("dblclick", function () {
        $("li", this).appendTo("#playerPicker ul");
        $(this).removeClass("filledSpot").addClass("freeSpot");
        $(".selected").removeClass("selected");
        $("#playerForm .highlight").removeClass("highlight");
    });

    function movePlayerToSpot($spot) {
        $elm.appendTo($spot).removeClass("selected");
        $(".freeSpot").removeClass("highlight");
        $spot.removeClass("freeSpot").addClass("filledSpot");

        if ($(".filledSpot").length == 4) {
            $("#playBtn").removeAttr("disabled");
        } else {
            $("#playBtn").attr("disabled", true);
        }
    }

    $("#playTestBtn").click(function () {
        if ($("li", $("#playerForm")).length < 4)
            return false;

        $("[name=IsTest]").val("true");
    });

    $("#playerForm").submit(function (e) {
        if ($("li", this).length < 4)
            return false;

        $("[name=BlueOff]").val($("#blueOff li").attr("data-playerid"));
        $("[name=BlueDef]").val($("#blueDef li").attr("data-playerid"));
        $("[name=RedOff]").val($("#redOff li").attr("data-playerid"));
        $("[name=RedDef]").val($("#redDef li").attr("data-playerid"));
    });

});