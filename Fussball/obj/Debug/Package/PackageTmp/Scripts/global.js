$(function () {
    $(".backBtn").click(function () {
        history.go(-1);
    });

    $("#StatisticList td a").bigTarget({
        clickZone: 'tr'
    });

    $("#StatisticList").tablesorter({
        sortList: [[0,0]]
    });

    $("#OverallList").tablesorter({
        sortList: [[0, 0]]
    });
});