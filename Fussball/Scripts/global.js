$(function () {
    $(".backBtn").click(function () {
        history.go(-1);
    });

    $("#StatisticList td a").bigTarget({
        clickZone: 'tr'
    });

    if ($("#StatisticsList tbody tr").length > 0) {
        $("#StatisticList").tablesorter({
            sortList: [[0, 0]]
        });
    }

    $("#OverallList").tablesorter({
        sortList: [[0, 0]]
    });
});