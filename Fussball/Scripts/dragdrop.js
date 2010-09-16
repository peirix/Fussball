$(document).ready(function () {
    var picker = $("#playerPicker");
    var playerCount = picker.find("li").length;
    var width = playerCount * 90; //80px wide + 10px margin

    picker.find("ul").width(width);

    //Drag-drop
    var positions = [];
    positions[0] = [0, 0];
    positions[1] = [$("#redDef").offset().top, $("#redDef").offset().left];
    positions[2] = [$("#redOff").offset().top, $("#redOff").offset().left];
    positions[3] = [$("#blueDef").offset().top, $("#blueDef").offset().left];
    positions[4] = [$("#blueOff").offset().top, $("#blueOff").offset().left];

    for (i = 1; i < 5; i++) {
        console.log(positions[i][0] + ", " + positions[i][1]);
    }


    var left = 0;
    var top = 0;
    var elm = null;
    var down = false;

    function movePlayer(e) {

        var newLeft = e.pageX - left;
        var newTop = e.pageY - top;

        elm.css({
            "margin-top": newTop,
            "margin-left": newLeft
        });
    }

    $("#playerPicker li").mousedown(function (e) {
        down = true;
        var $this = $(this);
        positions[0] = [$this.offset().top, $this.offset().left];
        console.log(positions[0]);

        $(".player").css("background-color", "#5fd26a");

        $this.css({
            "position": "absolute",
            "left": $this.offset().left,
            "top": $this.offset().top
        });

        $this.next().css("margin-left", "95px");

        left = e.pageX;
        top = e.pageY;

        elm = $this;
        $(document).bind("mousemove", movePlayer);

    });

    $(document).mouseup(function (e) {
        if (down) {
            down = false;
            var $this = elm;
            $(".player").css("background-color", "transparent");

            $(document).unbind("mousemove", movePlayer);

            moveToClosest();
        }
    });

    function moveToClosest() {
        var closest = 0;
        var posTop = elm.offset().top;
        var posLeft = elm.offset().left;
        var closeX = Math.abs(posTop - positions[0][0]);
        var closeY = Math.abs(posLeft - positions[0][1]);
        for (i = 1; i < 5; i++) {
            var tempX = Math.abs(posTop - positions[i][0]);
            var tempY = Math.abs(posLeft - positions[i][1]);
            console.log(tempX + ", " + tempY);
            console.log((tempX + tempY) + " < " + (closeX + closeY))
            if ((tempX + tempY) < (closeX + closeY)) {
                closest = i;
                closeX = tempX;
                closeY = tempY;
            }
        }

        switch (closest) {
            case 0:
                movePlayerBack();
                break;
            case 1:
                movePlayerTo("redDef");
                break;
            case 2:
                movePlayerTo("redOff");
                break;
            case 3:
                movePlayerTo("blueDef");
                break;
            case 4:
                movePlayerTo("blueOff");
                break;
        }
    }

    function movePlayerBack() {
        elm.next().css("margin-left", "5px");
        elm.removeAttr("style");
    }

    function movePlayerTo(pos) {
        elm.next().animate({ "margin-left": "-=90px" });
        $("#" + pos).append(elm);
        elm.removeAttr("style");
    }
});