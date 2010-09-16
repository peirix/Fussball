$(document).ready(function () {
    var picker = $("#playerPicker");
    var playerCount = picker.find("li").length;
    var width = playerCount * 90; //80px wide + 10px margin

    picker.find("ul").width(width);

    //drag and scroll
    var mouseOnPlayer = false;
    var mouseLeft = 0;
    var marginLeft = 0;
    $elm = null;

    $("#playerPicker ul").mousedown(function (e) {
        mouseOnPlayer = e.target.tagName.toLowerCase() == "li" ? true : false;
        $elm = $(e.target);
        mouseLeft = e.pageX;
        marginLeft = $("#playerPicker ul").css("margin-left");
        marginLeft = marginLeft.substr(0, marginLeft.indexOf("px")) - 0; //casting to int

        $(document).bind("mousemove", scrollPlayerList);
    });

    $("#playerPicker ul li").dblclick(function () {
        $elm = $(this);
        var $spot = $(".freeSpot:first");
        movePlayerToSpot($spot);
    });

    $(document).mouseup(function (e) {
        var newLeft = e.pageX - mouseLeft;

        if (Math.abs(newLeft) < 10) {
            $("#playerPicker ul li").removeClass("selected");
            $elm.addClass("selected");
            $("#playerForm .freeSpot").addClass("highlight");
        }

        //unbind
        $(document).unbind("mousemove", scrollPlayerList);
    });

    $(".freeSpot").live("click", function () {
        var $selected = $("#playerPicker ul .selected");
        if ($selected.length > 0) {
            $elm = $selected;
            movePlayerToSpot($(this));
        }
    });

    function scrollPlayerList(e) {
        var newLeft = e.pageX - mouseLeft;
        newLeft += marginLeft;

        if (newLeft > 0) newLeft = 0;

        if (newLeft < $(window).width() - $("#playerPicker ul").width()) newLeft = $(window).width() - $("#playerPicker ul").width();

        $("#playerPicker ul").css("margin-left", newLeft);
    }

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
});