$(function () {
    var counter = 0;
    $("#Last10").load("LastTen table", function () {
        counter++;
        doTheRest();
    });
    $("#Overall").load("AllGames table", function () {
        counter++;
        doTheRest();
    });
    $("#General").load("General table", function () {
        counter++;
        doTheRest();
    });

    function doTheRest() {
        if (counter >= 3) {
            $(".dataList td a").bigTarget({
                clickZone: 'tr',
                hoverClass: 'hover'
            });

            if ($("#StatisticsList tbody tr").length > 0) {
                $("#StatisticList").tablesorter({
                    sortList: [[0, 0]]
                });
            }

            $(".dataList").tablesorter({
                sortList: [[0, 0]]
            });
        }
    }
});