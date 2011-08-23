$(document).ready(function () {

    var $picker = $("#playerPicker");
    var $list = $picker.find("ul");
    function resizeList() {
        var playerCount = $list.find("li").length;
        var singleWidth = $list.find("li:first").outerWidth() + 5; //5px margin
        var width = playerCount * singleWidth;
        $list.width(width);
    }
    resizeList();

    /* Playerlist navigation */
    $picker.find(".right").click(function () {
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
        resizeList();
    }).live("dblclick", function () {
        $("li", this).appendTo("#playerPicker ul");
        $(this).removeClass("filledSpot").addClass("freeSpot");
        $(".selected").removeClass("selected");
        $("#playerForm .highlight").removeClass("highlight");
        resizeList();
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

        resizeList();
    }

    $("#playTestBtn").click(function () {
        if ($("li", $("#playerForm")).length < 4)
            return false;

        $("[name=IsTest]").val("true");
    });

    $("#playerForm").submit(function (e) {
        if ($("li", this).length < 4)
            return false;

        $("[name=Blue1]").val($("#blueOff li").attr("data-playerid"));
        $("[name=Blue2]").val($("#blueDef li").attr("data-playerid"));
        $("[name=Red1]").val($("#redOff li").attr("data-playerid"));
        $("[name=Red2]").val($("#redDef li").attr("data-playerid"));
    });

});